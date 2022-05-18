import { Button, Group, Stack, TextInput, Title } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { HubConnection } from "@microsoft/signalr";
import { Dispatch, MutableRefObject, SetStateAction, useEffect, useState } from "react";
import Peer from "simple-peer";
import { Check, X } from "tabler-icons-react";
import WhiteboardTeachingSvg from "./WhiteboardTeachingSvg";

interface StudentOutsideClassroomProps {
    userEmail: string | undefined;
    classroomHub: HubConnection;
    classroomName: string;
    setClassroomName: Dispatch<SetStateAction<string>>;
    setIsInsideClassroom: Dispatch<SetStateAction<boolean>>;
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
}

export const StudentOutsideClassroom: React.FC<StudentOutsideClassroomProps> = ({ userEmail, classroomHub, classroomName, setClassroomName, setIsInsideClassroom, localPeerRef }) => {
    const [isJoiningClassroom, setIsJoiningClassroom] = useState(false);

    useEffect(() => {
        console.log("StudentOutsideRoomInit");
        // classroomHub.on("StudentAcknowledged", (tutorSignalData: string) => {
        //     console.log("Hub: Recieved JoinRoomRequestAnswered with tutor signal data " + tutorSignalData);

        //     localPeerRef.current?.signal(tutorSignalData);

        //     setIsJoiningClassroom(false);
        //     setIsInsideClassroom(true);

        //     showNotification({
        //         autoClose: 5000,
        //         message: `Успешно присаидиняване към стаята`,
        //         color: "teal",
        //         icon: <Check />
        //     });
        // });

        classroomHub.on("JoinRoomFailed", (data: { message: string }) => {
            console.log("Hub: Recieved JoinRoomFailed");

            setIsJoiningClassroom(false);

            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при присаидиняването към стая",
                message: data.message,
                color: "red",
                icon: <X />
            });
        });

        classroomHub.on("SignalReceived", (studentSignalData: string) => {
            console.log("Hub: Recieved SignalReceived with student signal data " + studentSignalData);

            localPeerRef.current?.signal(studentSignalData);
        });
    }, [classroomHub, localPeerRef, setIsInsideClassroom]);

    const joinClassroom = () => {
        console.log("joinClassroom method");

        setIsJoiningClassroom(true);

        localPeerRef.current = new Peer({
            initiator: true
        });

        localPeerRef.current?.on("signal", async studentSignalData => {
            console.log("Peer remote: Recieved student signal data " + JSON.stringify(studentSignalData));
            console.log("isJoiningClassroom " + isJoiningClassroom);
            if (studentSignalData.type === "offer") {
                console.log("Hub: invoking JoinRoom");

                await classroomHub.invoke("JoinRoom", classroomName, userEmail, JSON.stringify(studentSignalData));

                return;
            }

            console.log("Hub: invoking Signal");
            await classroomHub.invoke("Signal", classroomName, JSON.stringify(studentSignalData));
        });

        localPeerRef.current?.on("connect", async () => {
            console.log("Peer remote: Recieved student connect event");

            setIsJoiningClassroom(false);
            setIsInsideClassroom(true);

            await classroomHub.invoke("ConfirmJoinRoom", classroomName, userEmail);

            showNotification({
                autoClose: 5000,
                message: `Успешно присаидиняване към стаята "${classroomName}"`,
                color: "teal",
                icon: <Check />
            });
        });
    };

    return (
        <Stack align="center" justify="space-around" style={{ height: "100%" }}>
            <Title order={3}>Изглежда, че не си се присаидинил към стая</Title>
            <Group>
                <TextInput disabled={isJoiningClassroom} value={classroomName} onChange={event => setClassroomName(event.currentTarget.value)} placeholder="Име на стаята" />
                <Button loading={isJoiningClassroom} onClick={joinClassroom}>
                    Присаидини се към стая
                </Button>
            </Group>
            <WhiteboardTeachingSvg />
        </Stack>
    );
};
