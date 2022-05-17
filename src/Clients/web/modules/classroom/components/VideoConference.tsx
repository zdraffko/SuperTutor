import { Button, Center, Group, Paper, Stack, TextInput } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import useVideoConferenceHub from "modules/classroom/hubs/useVideoConferenceHub";
import { useEffect, useRef, useState } from "react";
import Peer from "simple-peer";
import { Check, ExclamationMark, X } from "tabler-icons-react";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { UserType } from "utils/authentication/types";

export const VideoConference: React.FC = () => {
    const { hubConnection, isHubConnected } = useVideoConferenceHub();
    const { user } = useAuth();
    const [videoStream, setVideoStream] = useState<MediaStream>();
    const peerRef = useRef<Peer.Instance>();
    const localVideoRef = useRef<HTMLVideoElement>(null);
    const remoteVideoRef = useRef<HTMLVideoElement>(null);
    const [roomName, setRoomName] = useState("");
    const [isRoomCreated, setIsRoomCreated] = useState(false);
    const [isCreatingRoom, setIsCreatingRoom] = useState(false);
    const [isClosingRoom, setIsClosingRoom] = useState(false);
    const [isRoomJoined, setIsRoomJoined] = useState(false);
    const [isJoiningRoom, setIsJoiningRoom] = useState(false);
    const [isLeavingRoom, setIsLeavingRoom] = useState(false);
    const [hasARoomBeenCreated, setHasARoomBeenCreated] = useState(false);
    const [hasARoomBeenJoined, setHasARoomBeenJoined] = useState(false);

    useEffect(() => {
        navigator.mediaDevices
            .getUserMedia({ video: true, audio: true })
            .then(currentStream => {
                setVideoStream(currentStream);

                if (localVideoRef.current) {
                    localVideoRef.current.srcObject = currentStream;
                }
            })
            .catch((error: DOMException) => {
                // TODO: handle all of the other cases https://blog.addpipe.com/common-getusermedia-errors/
                if (error.name === "NotAllowedError" || error.name === "PermissionDeniedError") {
                    showNotification({
                        autoClose: false,
                        title: "Достъпът до камера и микрофон бе отказан",
                        message: "Моля разрешете достъпа до камера и микрофон за нормална работа в класната стая",
                        color: "red",
                        icon: <X />
                    });

                    return;
                }

                showNotification({
                    autoClose: false,
                    title: "Възникна неочакван проблем при опит за достъп до камера и микрофон",
                    message: "Моля проверете дали устройствата са изрядни и обновете страницата",
                    color: "red",
                    icon: <X />
                });
            });
    }, []);

    const createRoom = async () => {
        setIsCreatingRoom(true);

        // TODO: Refactor this. It is a hack to not register duplicate handlers for the same hub method
        if (!hasARoomBeenCreated) {
            hubConnection.on("RoomCreated", () => {
                console.log("Hub: Recieved RoomCreated");

                setIsCreatingRoom(false);
                setIsRoomCreated(true);
                setHasARoomBeenCreated(true);

                showNotification({
                    autoClose: 5000,
                    message: `Стая ${roomName} бе създадена успешно`,
                    color: "teal",
                    icon: <Check />
                });
            });

            hubConnection.on("RoomCreationFailed", (data: { message: string }) => {
                console.log("Hub: Recieved RoomCreationFailed");

                setIsCreatingRoom(false);
                setIsRoomCreated(false);

                showNotification({
                    autoClose: 5000,
                    title: "Възникна проблем при създаването на стая",
                    message: data.message,
                    color: "red",
                    icon: <X />
                });
            });

            hubConnection.on("StudentJoinedRoom", (data: { studentName: string; studentSignalData: string }) => {
                console.log("Hub: Recieved StudentJoined with student signal data " + data.studentSignalData);

                peerRef.current?.signal(data.studentSignalData);

                showNotification({
                    autoClose: 5000,
                    message: `${data.studentName} се присаидини към стаята`,
                    color: "teal",
                    icon: <Check />
                });
            });

            hubConnection.on("RoomClosed", (roomName: string) => {
                console.log("Hub: Recieved RoomClosed with roomName" + roomName);

                peerRef.current?.end();
                peerRef.current?.destroy();
                if (remoteVideoRef.current) {
                    remoteVideoRef.current.srcObject = null;
                }

                setIsRoomCreated(false);
                setIsClosingRoom(false);

                showNotification({
                    autoClose: 5000,
                    message: `Стаята ${roomName} бе успешно затворена`,
                    color: "teal",
                    icon: <Check />
                });
            });

            hubConnection.on("StudentLeftRoom", (studentName: string) => {
                console.log("Hub: Recieved StudentLeftRoom with student name" + studentName);

                // Destroying a local peer also destryies the remote peer
                // That is why when the student leaves the room (and his peer is destroied),
                // We create a new peer for the tutor so he can accept a new connection from the student
                const peer = new Peer({ initiator: false, trickle: false, stream: videoStream });
                peerRef.current = peer;
                if (remoteVideoRef.current) {
                    remoteVideoRef.current.srcObject = null;
                }

                peerRef.current?.on("signal", async tutorSignalData => {
                    console.log("Peer Local: Recieved signal with tutor signal data " + JSON.stringify(tutorSignalData));

                    console.log("Hub: invoking AcknowledgeNewlyJoinedStudent");
                    await hubConnection.invoke("AcknowledgeNewlyJoinedStudent", roomName, JSON.stringify(tutorSignalData));
                });

                peerRef.current?.on("stream", remoteStream => {
                    console.log("Peer Local: Recieved stream");

                    if (remoteVideoRef.current) {
                        remoteVideoRef.current.srcObject = remoteStream;
                    }
                });

                showNotification({
                    autoClose: 5000,
                    message: `${studentName} напусна стаята`,
                    color: "orange",
                    icon: <ExclamationMark />
                });
            });
        }

        const peer = new Peer({ initiator: false, trickle: false, stream: videoStream });
        peerRef.current = peer;

        peerRef.current?.on("signal", async tutorSignalData => {
            console.log("Peer Local: Recieved signal with tutor signal data " + JSON.stringify(tutorSignalData));

            console.log("Hub: invoking AcknowledgeNewlyJoinedStudent");
            await hubConnection.invoke("AcknowledgeNewlyJoinedStudent", roomName, JSON.stringify(tutorSignalData));
        });

        peerRef.current?.on("stream", remoteStream => {
            console.log("Peer Local: Recieved stream");

            if (remoteVideoRef.current) {
                remoteVideoRef.current.srcObject = remoteStream;
            }
        });

        if (!isHubConnected()) {
            console.log("Cannot create a room. Hub not connected");
        }

        console.log("Hub: invoking CreateRoom");
        await hubConnection.invoke("CreateRoom", roomName, user?.email);
    };

    const joinRoom = () => {
        setIsJoiningRoom(true);

        // TODO: Refactor this. It is a hack to not register duplicate handlers for the same hub method
        if (!hasARoomBeenJoined) {
            hubConnection.on("StudentAcknowledged", (tutorSignalData: string) => {
                console.log("Hub: Recieved JoinRoomRequestAnswered with tutor signal data " + tutorSignalData);

                peerRef.current?.signal(tutorSignalData);

                setIsJoiningRoom(false);
                setIsRoomJoined(true);
                setHasARoomBeenJoined(true);

                showNotification({
                    autoClose: 5000,
                    message: `Успешно присаидиняване към стаята`,
                    color: "teal",
                    icon: <Check />
                });
            });

            hubConnection.on("JoinRoomFailed", (data: { message: string }) => {
                console.log("Hub: Recieved JoinRoomFailed");

                setIsJoiningRoom(false);
                setIsRoomCreated(false);

                showNotification({
                    autoClose: 5000,
                    title: "Възникна проблем при присаидиняването към стая",
                    message: data.message,
                    color: "red",
                    icon: <X />
                });
            });

            hubConnection.on("RoomClosed", (roomName: string) => {
                console.log("Hub: Recieved RoomClosed with roomName" + roomName);

                peerRef.current?.end();
                peerRef.current?.destroy();
                if (remoteVideoRef.current) {
                    remoteVideoRef.current.srcObject = null;
                }

                setIsRoomJoined(false);

                showNotification({
                    autoClose: 5000,
                    message: `Стаята ${roomName} бе затворена`,
                    color: "orange",
                    icon: <ExclamationMark />
                });
            });

            hubConnection.on("RoomLeft", (roomName: string) => {
                console.log("Hub: Recieved RoomLeft with roomName" + roomName);

                peerRef.current?.end();
                peerRef.current?.destroy();
                peerRef.current = undefined;
                if (remoteVideoRef.current) {
                    remoteVideoRef.current.srcObject = null;
                }

                setIsRoomJoined(false);
                setIsLeavingRoom(false);

                showNotification({
                    autoClose: 5000,
                    message: `Стаята ${roomName} бе напусната успешно`,
                    color: "teal",
                    icon: <Check />
                });
            });
        }

        const peer = new Peer({
            initiator: true,
            trickle: false,
            stream: videoStream
        });
        peerRef.current = peer;

        peerRef.current?.on("signal", async studentSignalData => {
            console.log("Peer remote: Recieved student signal data " + JSON.stringify(studentSignalData));

            console.log("Hub: invoking JoinRoom");

            await hubConnection.invoke("JoinRoom", roomName, user?.email, JSON.stringify(studentSignalData));
        });

        peerRef.current?.on("stream", stream => {
            console.log("Peer remote: Recieved stream");

            if (remoteVideoRef.current) {
                remoteVideoRef.current.srcObject = stream;
            }
        });
    };

    const closeRoom = async () => {
        setIsClosingRoom(true);
        console.log("Hub: invoking CloseRoom");

        await hubConnection.invoke("CloseRoom", roomName);
    };

    const leaveRoom = async () => {
        setIsLeavingRoom(true);
        console.log("Hub: invoking LeaveRoom");

        await hubConnection.invoke("LeaveRoom", roomName, user?.email);
    };

    const tutorControls = isRoomCreated ? (
        <Button onClick={closeRoom} loading={isClosingRoom}>
            Затвори стаята
        </Button>
    ) : (
        <Button onClick={createRoom} loading={isCreatingRoom}>
            Създай стая
        </Button>
    );
    const studentControls = isRoomJoined ? (
        <Button onClick={leaveRoom} loading={isLeavingRoom}>
            Напусни стаята
        </Button>
    ) : (
        <Button onClick={joinRoom} loading={isJoiningRoom}>
            Влез в стая
        </Button>
    );

    return (
        <Stack align="stretch">
            <Group position="apart">
                <TextInput
                    disabled={isRoomCreated || isCreatingRoom || isRoomJoined || isJoiningRoom}
                    value={roomName}
                    onChange={event => setRoomName(event.currentTarget.value)}
                    placeholder="Име на стаята"
                />
                {user?.type === UserType.Tutor ? tutorControls : studentControls}
            </Group>
            <Paper withBorder>
                <Center>
                    <video id="local" playsInline ref={localVideoRef} autoPlay muted style={{ height: "300px" }} poster="/anonymous-user.png" />
                </Center>
            </Paper>
            <Paper withBorder>
                <Center>
                    <video id="remote" playsInline ref={remoteVideoRef} autoPlay muted style={{ height: "300px" }} poster="/anonymous-user.png" />
                </Center>
            </Paper>
        </Stack>
    );
};
