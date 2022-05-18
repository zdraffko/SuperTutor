import { Center, Paper, Stack } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { MutableRefObject, useEffect, useRef } from "react";
import Peer from "simple-peer";
import { X } from "tabler-icons-react";

interface VideoConferenceProps {
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
}

export const VideoConference: React.FC<VideoConferenceProps> = ({ localPeerRef }) => {
    const localVideoRef = useRef<HTMLVideoElement>(null);
    const remoteVideoRef = useRef<HTMLVideoElement>(null);

    useEffect(() => {
        if (remoteVideoRef.current) {
            remoteVideoRef.current.srcObject = null;
        }

        localPeerRef.current?.on("stream", remoteMediaStream => {
            console.log("Peer Local: Recieved stream");

            if (remoteVideoRef.current) {
                remoteVideoRef.current.srcObject = remoteMediaStream;
            }
        });

        navigator.mediaDevices
            .getUserMedia({ video: true, audio: true })
            .then(localMediaStream => {
                if (localVideoRef.current && !localVideoRef.current.srcObject) {
                    localVideoRef.current.srcObject = localMediaStream;
                }

                console.log("add stream");
                localPeerRef.current?.addStream(localMediaStream);
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
    }, [localPeerRef]);

    return (
        <Stack align="stretch">
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
