import { Button, Group, Stack, TextInput, Title } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { HubConnection } from "@microsoft/signalr";
import { Dispatch, MutableRefObject, SetStateAction, useEffect, useState } from "react";
import Peer from "simple-peer";
import { Check, X } from "tabler-icons-react";
import WhiteboardTeachingSvg from "./WhiteboardTeachingSvg";

interface TutorOutsideClassroomProps {
    userEmail: string | undefined;
    classroomHub: HubConnection;
    classroomName: string;
    setClassroomName: Dispatch<SetStateAction<string>>;
    setIsInsideClassroom: Dispatch<SetStateAction<boolean>>;
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
}

export const TutorOutsideClassroom: React.FC<TutorOutsideClassroomProps> = ({ userEmail, classroomHub, classroomName, setClassroomName, setIsInsideClassroom, localPeerRef }) => {
    const [isCreatingClassroom, setIsCreatingClassroom] = useState(false);

    useEffect(() => {
        classroomHub.on("RoomCreated", (createdClassroomName: string) => {
            console.log("Hub: Recieved RoomCreated");

            setIsCreatingClassroom(false);
            setIsInsideClassroom(true);

            showNotification({
                autoClose: 5000,
                message: `Стаята "${createdClassroomName}" бе създадена успешно`,
                color: "teal",
                icon: <Check />
            });
        });

        classroomHub.on("RoomCreationFailed", (data: { message: string }) => {
            console.log("Hub: Recieved RoomCreationFailed");

            setIsCreatingClassroom(false);

            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при създаването на стая",
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

    const createClassroom = async () => {
        setIsCreatingClassroom(true);

        localPeerRef.current = new Peer({ initiator: false });

        localPeerRef.current?.on("signal", async tutorSignalData => {
            console.log("Peer Local: Received signal with tutor signal data " + JSON.stringify(tutorSignalData));

            console.log("Hub: invoking Signal");
            await classroomHub.invoke("Signal", classroomName, JSON.stringify(tutorSignalData));
        });

        console.log("Hub: invoking CreateRoom");
        await classroomHub.invoke("CreateRoom", classroomName, userEmail);
    };

    return (
        <Stack align="center" justify="space-around" style={{ height: "100%" }}>
            <Title order={3}>Изглежда, че нямаш активна стая</Title>
            <Group>
                <TextInput disabled={isCreatingClassroom} value={classroomName} onChange={event => setClassroomName(event.currentTarget.value)} placeholder="Име на стаята" />
                <Button loading={isCreatingClassroom} onClick={createClassroom}>
                    Създай нова стая
                </Button>
            </Group>
            <WhiteboardTeachingSvg />
        </Stack>
    );
};
