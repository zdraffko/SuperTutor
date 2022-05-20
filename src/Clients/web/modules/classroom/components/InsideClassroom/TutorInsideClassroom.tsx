import { Button, Grid, Group, Stack } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { HubConnection } from "@microsoft/signalr";
import { Dispatch, MutableRefObject, SetStateAction, useEffect, useState } from "react";
import Peer from "simple-peer";
import { Check, ExclamationMark } from "tabler-icons-react";
import RoomIndicator from "./RoomIndicator/RoomIndicator";
import { VideoConference } from "./VideoConference/VideoConference";
import { WorkSpace } from "./WorkSpace/WorkSpace";

interface TutorInsideClassroomProps {
    classroomHub: HubConnection;
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
    classroomName: string;
    setIsInsideClassroom: Dispatch<SetStateAction<boolean>>;
}

export const TutorInsideClassroom: React.FC<TutorInsideClassroomProps> = ({ classroomHub, localPeerRef, classroomName, setIsInsideClassroom }) => {
    const [isClosingClassroom, setIsClosingClassroom] = useState(false);
    const [isStudentInsideClassroom, setIsStudentInsideClassroom] = useState(false);
    const [isWorkSpaceSavingChanges, setIsWorkSpaceSavingChanges] = useState(false);

    useEffect(() => {
        classroomHub.off("StudentJoinedRoom");
        classroomHub.on("StudentJoinedRoom", (studentName: string) => {
            console.log("Hub: Recieved StudentJoined with student data " + studentName);

            //localPeerRef.current?.signal(data.studentSignalData);

            setIsStudentInsideClassroom(true);

            showNotification({
                autoClose: 5000,
                message: `${studentName} се присаидини към стаята`,
                color: "teal",
                icon: <Check />
            });
        });

        classroomHub.off("RoomClosed");
        classroomHub.on("RoomClosed", (roomName: string) => {
            console.log("Hub: Recieved RoomClosed with roomName" + roomName);

            localPeerRef.current?.destroy();

            setIsInsideClassroom(false);
            setIsClosingClassroom(false);

            showNotification({
                autoClose: 5000,
                message: `Стаята ${roomName} бе успешно затворена`,
                color: "teal",
                icon: <Check />
            });
        });

        classroomHub.off("StudentLeftRoom");
        classroomHub.on("StudentLeftRoom", (studentName: string) => {
            console.log("Hub: Received StudentLeftRoom with student name" + studentName);

            localPeerRef.current?.destroy();

            // Start peer recreation
            // Destroying a local peer also destroys the remote peer
            // That is why when the student leaves the room (and his peer is destroyed),
            // We create a new peer for the tutor so he can accept a new connection from the student

            localPeerRef.current = new Peer({ initiator: false });

            localPeerRef.current?.on("signal", async tutorSignalData => {
                console.log("Peer Local: Received signal with tutor signal data " + JSON.stringify(tutorSignalData));

                console.log("Hub: invoking Signal");
                await classroomHub.invoke("Signal", classroomName, JSON.stringify(tutorSignalData));
            });

            //End peer recreation

            setIsStudentInsideClassroom(false);

            showNotification({
                autoClose: 5000,
                message: `${studentName} напусна стаята`,
                color: "orange",
                icon: <ExclamationMark />
            });
        });
    }, [classroomHub, classroomName, localPeerRef, setIsInsideClassroom]);

    const closeClassroom = async () => {
        setIsClosingClassroom(true);
        console.log("Hub: invoking CloseRoom");

        await classroomHub.invoke("CloseRoom", classroomName);
    };

    return (
        <Grid>
            <Grid.Col span={8}>
                <WorkSpace localPeerRef={localPeerRef} isRemotePeerConnected={isStudentInsideClassroom} setIsWorkSpaceSavingChanges={setIsWorkSpaceSavingChanges} />
            </Grid.Col>
            <Grid.Col span={4}>
                <Stack>
                    <Group position="apart">
                        <RoomIndicator classroomName={classroomName} isClassroomSavingChanges={isWorkSpaceSavingChanges} />
                        <Button onClick={closeClassroom} loading={isClosingClassroom}>
                            Затвори стаята
                        </Button>
                    </Group>
                    <VideoConference localPeerRef={localPeerRef} hasRemotePeerDisconnected={isStudentInsideClassroom} />
                </Stack>
            </Grid.Col>
        </Grid>
    );
};
