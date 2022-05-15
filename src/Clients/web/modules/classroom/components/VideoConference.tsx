import { Button } from "@mantine/core";
import useVideoConferenceHub from "modules/classroom/hubs/useVideoConferenceHub";
import { useEffect, useRef, useState } from "react";
import Peer from "simple-peer";
import { useAuth } from "utils/authentication/reactQueryAuth";

export const VideoConference: React.FC = () => {
    const { hubConnection, createRoom } = useVideoConferenceHub();
    const { user } = useAuth();
    const [stream, setStream] = useState<MediaStream>();
    const localVideoRef = useRef<HTMLVideoElement>(null);
    const remoteVideoRef = useRef<HTMLVideoElement>(null);
    const peerRef = useRef<Peer.Instance>();
    const [caller, setCaller] = useState("");
    const [callerSignal, setCallerSignal] = useState("");

    useEffect(() => {
        navigator.mediaDevices.getUserMedia({ video: true, audio: true }).then(currentStream => {
            setStream(currentStream);

            if (localVideoRef.current) {
                localVideoRef.current.srcObject = currentStream;
            }
        });

        hubConnection.on("RoomCreated", data => {
            console.log("Hub: Recieved RoomCreated with data " + data);

            setCaller(data.from);
            setCallerSignal(data.signal);
        });

    }, [hubConnection]);

    const openRoom = () => {
        const peer = new Peer({ initiator: true, trickle: false, stream });

        peer.on("signal", data => {
            console.log("Peer Local: Recieved signal");

            createRoom(data);
        });

        peer.on("stream", remoteStream => {
            console.log("Peer Local: Recieved stream");

            if (remoteVideoRef.current) {
                remoteVideoRef.current.srcObject = remoteStream;
            }
        });

        hubConnection.on("StudentJoined", (signal: string) => {
            console.log("Hub: Recieved StudentJoined");

            peer.signal(signal);
        });

        peerRef.current = peer;
    };

    const joinRoom = () => {
        const peer = new Peer({
            initiator: false,
            trickle: false,
            stream: stream
        });

        peer.on("signal", data => {
            console.log("Peer remote: Recieved signal");
            hubConnection.invoke("JoinRoom", "test-room", JSON.stringify(data));
        });

        peer.on("stream", stream => {
            console.log("Peer remote: Recieved stream");

            if (remoteVideoRef.current) {
                remoteVideoRef.current.srcObject = stream;
            }
        });

        hubConnection.on("JoinedRoom", (tutorSignal: string) => {
            console.log("Hub: Recieved JoinedRoom");

            peer.signal(tutorSignal);
        });

        console.log("Invoking JoinRoomInitial");
        hubConnection.invoke("JoinRoomInitial", "test-room");
    };

    return (
        <>
            <h1>Video Conference</h1>
            <Button onClick={openRoom}>Create room</Button>
            <Button onClick={joinRoom}>Join room</Button>
            <video id="local" playsInline ref={localVideoRef} autoPlay muted />
            <video id="remote" playsInline ref={remoteVideoRef} autoPlay muted />
        </>
    );
};
