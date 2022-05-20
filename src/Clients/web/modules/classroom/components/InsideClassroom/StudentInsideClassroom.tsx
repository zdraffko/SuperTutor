import { Button, Grid, Group, Stack } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { HubConnection } from "@microsoft/signalr";
import { Dispatch, MutableRefObject, SetStateAction, useEffect, useState } from "react";
import Peer from "simple-peer";
import { Check, ExclamationMark } from "tabler-icons-react";
import RoomIndicator from "./RoomIndicator/RoomIndicator";
import { VideoConference } from "./VideoConference/VideoConference";
import { WorkSpace } from "./WorkSpace/WorkSpace";

interface StudentInsideClassroomProps {
    userEmail: string | undefined;
    classroomHub: HubConnection;
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
    classroomName: string;
    setIsInsideClassroom: Dispatch<SetStateAction<boolean>>;
}

export const StudentInsideClassroom: React.FC<StudentInsideClassroomProps> = ({ userEmail, classroomHub, localPeerRef, classroomName, setIsInsideClassroom }) => {
    const [isLeavingClassroom, setIsLeavingClassroom] = useState(false);
    const [isWorkSpaceSavingChanges, setIsWorkSpaceSavingChanges] = useState(false);

    useEffect(() => {
        classroomHub.off("RoomClosed");
        classroomHub.on("RoomClosed", (roomName: string) => {
            console.log("Hub: Recieved RoomClosed with roomName" + roomName);

            localPeerRef.current?.destroy();

            setIsInsideClassroom(false);

            showNotification({
                autoClose: 5000,
                message: `Стаята ${roomName} бе затворена`,
                color: "orange",
                icon: <ExclamationMark />
            });
        });

        classroomHub.off("RoomLeft");
        classroomHub.on("RoomLeft", (roomName: string) => {
            console.log("Hub: Recieved RoomLeft with roomName" + roomName);

            localPeerRef.current?.destroy();

            setIsLeavingClassroom(false);
            setIsInsideClassroom(false);

            showNotification({
                autoClose: 5000,
                message: `Стая "${roomName}" бе напусната успешно`,
                color: "teal",
                icon: <Check />
            });
        });
    }, [classroomHub, localPeerRef, setIsInsideClassroom]);

    const leaveClassroom = async () => {
        setIsLeavingClassroom(true);
        console.log("Hub: invoking LeaveRoom");

        await classroomHub.invoke("LeaveRoom", classroomName, userEmail);
    };

    return (
        <Grid>
            <Grid.Col span={8}>
                <WorkSpace localPeerRef={localPeerRef} isRemotePeerConnected={true} setIsWorkSpaceSavingChanges={setIsWorkSpaceSavingChanges} />
            </Grid.Col>
            <Grid.Col span={4}>
                <Stack>
                    <Group position="apart">
                        <RoomIndicator classroomName={classroomName} isClassroomSavingChanges={isWorkSpaceSavingChanges} />
                        <Button onClick={leaveClassroom} loading={isLeavingClassroom}>
                            Напусни стаята
                        </Button>
                    </Group>
                    <VideoConference localPeerRef={localPeerRef} hasRemotePeerDisconnected={false} />
                </Stack>
            </Grid.Col>
        </Grid>
    );
};
