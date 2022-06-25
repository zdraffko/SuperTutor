import { Button, Center, Group, Loader, Stack, Title } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { HubConnection } from "@microsoft/signalr";
import useGetActiveClassroom from "modules/classroom/hooks/useGetActiveClassroom";
import { Dispatch, MutableRefObject, SetStateAction, useEffect, useState } from "react";
import Peer from "simple-peer";
import { Check, X } from "tabler-icons-react";
import { useAuth } from "utils/authentication/reactQueryAuth";
import WhiteboardTeachingSvg from "./WhiteboardTeachingSvg";

interface OutsideClassroomProps {
    classroomHub: HubConnection;
    setIsInsideClassroom: Dispatch<SetStateAction<boolean>>;
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
    classroomIdRef: MutableRefObject<string | null>;
    isInitiatorRef: MutableRefObject<boolean>;
}

export const OutsideClassroom: React.FC<OutsideClassroomProps> = ({ isInitiatorRef, classroomHub, classroomIdRef, setIsInsideClassroom, localPeerRef }) => {
    const { classroomId, isGetActiveClassroomFailed, isGetActiveClassroomLoading, isGetActiveClassroomSuccessful, getActiveClassroomErrorMessage } = useGetActiveClassroom();
    const { user } = useAuth();
    const [isJoiningClassroom, setIsJoiningClassroom] = useState(false);

    useEffect(() => {
        if (isGetActiveClassroomSuccessful && classroomId) {
            classroomIdRef.current = classroomId;
        }

        if (isGetActiveClassroomFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при зареждането на класната стая",
                message: getActiveClassroomErrorMessage,
                color: "red",
                icon: <X />
            });
        }
    }, [classroomId, classroomIdRef, getActiveClassroomErrorMessage, isGetActiveClassroomFailed, isGetActiveClassroomSuccessful]);

    useEffect(() => {
        classroomHub.off("ClassroomJoined");
        classroomHub.on("ClassroomJoined", (isInitiator: boolean) => {
            console.log("isInitiator@@@@@@ " + isInitiator);

            localPeerRef.current = new Peer({ initiator: isInitiator });

            localPeerRef.current?.on("signal", async tutorSignalData => {
                await classroomHub.invoke("Signal", classroomId, JSON.stringify(tutorSignalData));
            });

            isInitiatorRef.current = isInitiator;
            if (!isInitiator) {
                setIsJoiningClassroom(false);
                setIsInsideClassroom(true);

                showNotification({
                    autoClose: 5000,
                    message: " Успешно присъединяване към класната стая",
                    color: "teal",
                    icon: <Check />
                });
            }
        });

        classroomHub.off("SignalReceived");
        classroomHub.on("SignalReceived", (studentSignalData: string) => {
            localPeerRef.current?.signal(studentSignalData);
        });
    }, [classroomHub, classroomId, isInitiatorRef, localPeerRef, setIsInsideClassroom]);

    const joinClassroom = async () => {
        setIsJoiningClassroom(true);
        await classroomHub.invoke("JoinClassroom", classroomId, `${user?.firstName} ${user?.lastName}`);

        localPeerRef.current?.on("connect", async () => {
            console.log("Peer remote: Recieved connect event");

            if (isInitiatorRef.current) {
                setIsJoiningClassroom(false);
                setIsInsideClassroom(true);
                await classroomHub.invoke("ConfirmClassroomJoin", classroomId, `${user?.firstName} ${user?.lastName}`);

                showNotification({
                    autoClose: 5000,
                    message: " Успешно присъединяване към класната стая",
                    color: "teal",
                    icon: <Check />
                });
            }
        });
    };

    if (isGetActiveClassroomLoading) {
        return (
            <Center style={{ height: "50vh" }}>
                <Loader size="xl" />
            </Center>
        );
    }

    if (isGetActiveClassroomSuccessful && classroomId) {
        return (
            <Stack align="center" justify="space-around" style={{ height: "100%" }}>
                <Title order={3}>Имаш активен урок в момента</Title>
                <Group>
                    <Button loading={isJoiningClassroom} onClick={joinClassroom}>
                        Влез в класната стая
                    </Button>
                </Group>
                <WhiteboardTeachingSvg />
            </Stack>
        );
    }

    return (
        <Stack align="center" justify="space-around" style={{ height: "100%" }}>
            <Title order={3}>Изглежда, че нямаш насрочен урок в момента</Title>
            <WhiteboardTeachingSvg />
        </Stack>
    );
};
