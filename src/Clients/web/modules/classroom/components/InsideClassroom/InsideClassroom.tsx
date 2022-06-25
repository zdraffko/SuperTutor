import { Button, Grid, Group, Stack } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { HubConnection } from "@microsoft/signalr";
import { Dispatch, MutableRefObject, SetStateAction, useEffect, useState } from "react";
import Peer from "simple-peer";
import { Check, ExclamationMark } from "tabler-icons-react";
import RoomIndicator from "./RoomIndicator/RoomIndicator";
import { VideoConference } from "./VideoConference/VideoConference";
import { WorkSpace } from "./WorkSpace/WorkSpace";

interface InsideClassroomProps {
    classroomHub: HubConnection;
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
    setIsInsideClassroom: Dispatch<SetStateAction<boolean>>;
    classroomId: string;
    isInitiatorRef: MutableRefObject<boolean>;
}

export const InsideClassroom: React.FC<InsideClassroomProps> = ({ isInitiatorRef, classroomHub, localPeerRef, classroomId, setIsInsideClassroom }) => {
    const [isLeavingClassroom, setIsLeavingClassroom] = useState(false);
    const [isRemoteUserInsideClassroom, setIsRemoteUserInsideClassroom] = useState(isInitiatorRef.current);
    const [isWorkSpaceSavingChanges, setIsWorkSpaceSavingChanges] = useState(false);

    useEffect(() => {
        classroomHub.off("NewUserJoinedClassroom");
        classroomHub.on("NewUserJoinedClassroom", (username: string) => {
            setIsRemoteUserInsideClassroom(true);

            showNotification({
                autoClose: 5000,
                message: `${username} се присъедини към класната стая`,
                color: "teal",
                icon: <Check />
            });
        });

        classroomHub.off("ClassroomClosed");
        classroomHub.on("ClassroomClosed", () => {
            localPeerRef.current?.destroy();
            localPeerRef.current = undefined;

            setIsInsideClassroom(false);

            showNotification({
                autoClose: 5000,
                message: "Стаята бе затворена",
                color: "orange",
                icon: <ExclamationMark />
            });
        });

        classroomHub.off("ClassroomLeft");
        classroomHub.on("ClassroomLeft", () => {
            localPeerRef.current?.destroy();
            localPeerRef.current = undefined;

            setIsLeavingClassroom(false);
            setIsInsideClassroom(false);

            showNotification({
                autoClose: 5000,
                message: "Стаята бе напусната успешно",
                color: "teal",
                icon: <Check />
            });
        });

        classroomHub.off("UserLeftClassroom");
        classroomHub.on("UserLeftClassroom", (username: string) => {
            console.log("Hub: Received UserLeftClassroom with username " + username);

            localPeerRef.current?.destroy();

            // Start peer recreation
            // Destroying a local peer also destroys the remote peer
            // That is why when the student leaves the room (and his peer is destroyed),
            // We create a new peer for the tutor so he can accept a new connection from the student

            localPeerRef.current = new Peer({ initiator: false });

            localPeerRef.current?.on("signal", async tutorSignalData => {
                console.log("Peer Local: Received signal with tutor signal data " + JSON.stringify(tutorSignalData));

                console.log("Hub: invoking Signal");
                await classroomHub.invoke("Signal", classroomId, JSON.stringify(tutorSignalData));
            });

            //End peer recreation

            setIsRemoteUserInsideClassroom(false);

            showNotification({
                autoClose: 5000,
                message: `${username} напусна стаята`,
                color: "orange",
                icon: <ExclamationMark />
            });
        });
    }, [classroomHub, classroomId, localPeerRef, setIsInsideClassroom]);

    const leaveClassroom = async () => {
        setIsLeavingClassroom(true);

        await classroomHub.invoke("LeaveClassroom", classroomId);
    };

    return (
        <Grid>
            <Grid.Col span={8}>
                <WorkSpace
                    isInitiatorRef={isInitiatorRef}
                    classroomHub={classroomHub}
                    classroomId={classroomId}
                    localPeerRef={localPeerRef}
                    isRemotePeerConnected={isRemoteUserInsideClassroom}
                    setIsWorkSpaceSavingChanges={setIsWorkSpaceSavingChanges}
                />
            </Grid.Col>
            <Grid.Col span={4}>
                <Stack>
                    <Group position="apart">
                        <RoomIndicator isClassroomSavingChanges={isWorkSpaceSavingChanges} />
                        <Button onClick={leaveClassroom} loading={isLeavingClassroom}>
                            Напусни стаята
                        </Button>
                    </Group>
                    <VideoConference localPeerRef={localPeerRef} hasRemotePeerDisconnected={isRemoteUserInsideClassroom} />
                </Stack>
            </Grid.Col>
        </Grid>
    );
};
