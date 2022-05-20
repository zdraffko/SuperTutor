import { Paper, ScrollArea, useMantineColorScheme } from "@mantine/core";
import { useIdle } from "@mantine/hooks";
import { TDDocument, Tldraw, TldrawApp } from "@tldraw/tldraw";
import useClassroomHub from "modules/classroom/hooks/useClassroomHub";
import { Dispatch, MutableRefObject, SetStateAction, useCallback, useEffect, useRef, useState } from "react";
import Peer from "simple-peer";
import { PeerDataChannelMessage } from "types/peerTypes";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { UserType } from "utils/authentication/types";

interface WhiteboardUpdatePayload {
    document: TDDocument;
}

interface WhiteboardProps {
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
    isRemotePeerConnected: boolean;
    setIsWhiteboardSavingChanges: Dispatch<SetStateAction<boolean>>;
}

export const Whiteboard: React.FC<WhiteboardProps> = ({ localPeerRef, isRemotePeerConnected, setIsWhiteboardSavingChanges }) => {
    const { colorScheme } = useMantineColorScheme();
    const drawApp = useRef<TldrawApp>();
    const [drawDocument, setDrawDocument] = useState<TDDocument>();
    const { user } = useAuth();
    const { classroomHub, isHubConnected } = useClassroomHub();
    const isUserIdle = useIdle(2000, { events: ["mousedown", "mouseup"] });
    const lastSavedWhiteboardContent = useRef("");

    const updateRemoteWhiteboard = useCallback(
        (document: TDDocument, reason: string | undefined) => {
            if (!isRemotePeerConnected) {
                return;
            }

            if (!reason || reason === "replace" || reason === "panned") {
                return;
            }

            console.log("$$$$$$$$$$$$$$$$$$$ " + reason);

            console.log("Updating remote whiteboard - " + document);

            const peerMessage: PeerDataChannelMessage<WhiteboardUpdatePayload> = {
                type: "WhiteboardUpdate",
                payload: {
                    document: document
                }
            };

            localPeerRef.current?.send(JSON.stringify(peerMessage));
        },
        [isRemotePeerConnected, localPeerRef]
    );

    useEffect(() => {
        console.log("isUserIdle " + isUserIdle);

        const whiteboardContent = JSON.stringify(drawDocument);

        console.log("diff " + whiteboardContent !== lastSavedWhiteboardContent.current);
        console.log("conn" + isHubConnected());
        if (isHubConnected() && isUserIdle && whiteboardContent !== lastSavedWhiteboardContent.current) {
            console.log("saving");
            setIsWhiteboardSavingChanges(true);

            classroomHub.invoke("SaveWhiteboardContent", whiteboardContent).then(() => {
                lastSavedWhiteboardContent.current = whiteboardContent;
            });
        }
    }, [isUserIdle, classroomHub, isHubConnected, , setIsWhiteboardSavingChanges]);

    useEffect(() => {
        classroomHub.off("WhiteboardContentSaved");
        classroomHub.on("WhiteboardContentSaved", () => {
            console.log("Recieved WhiteboardContentSaved");
            setIsWhiteboardSavingChanges(false);
        });

        if (isRemotePeerConnected && user?.type === UserType.Tutor) {
            localPeerRef.current?.on("data", (remoteData: string) => {
                try {
                    const peerMessage: PeerDataChannelMessage<WhiteboardUpdatePayload> = JSON.parse(remoteData);
                    if (peerMessage.type !== "WhiteboardUpdate") {
                        return;
                    }

                    console.log("Peer Local: Recieved whiteboard data " + JSON.stringify(peerMessage.payload.document));

                    setDrawDocument(peerMessage.payload.document);
                } catch (error) {} // Message not for us
            });

            if (drawApp.current?.document) {
                updateRemoteWhiteboard(drawApp.current.document, "init");
            }
        }

        if (user?.type === UserType.Student) {
            localPeerRef.current?.on("data", (remoteData: string) => {
                console.log("Peer Local: Recieved whiteboard data " + remoteData);
                try {
                    const peerMessage: PeerDataChannelMessage<WhiteboardUpdatePayload> = JSON.parse(remoteData);
                    if (peerMessage.type !== "WhiteboardUpdate") {
                        return;
                    }

                    console.log("Peer Local: Recieved whiteboard data " + peerMessage.payload.document);

                    setDrawDocument(peerMessage.payload.document);
                } catch (error) {} // Message not for us
            });
        }
    }, [isRemotePeerConnected]);

    return (
        <Paper withBorder shadow="xl">
            <ScrollArea style={{ height: "88vh" }}>
                <Tldraw
                    document={drawDocument}
                    onChange={(app, reason) => {
                        setDrawDocument(app.document);
                        updateRemoteWhiteboard(app.document, reason);
                    }}
                    onMount={app => (drawApp.current = app)}
                    darkMode={colorScheme === "dark"}
                />
            </ScrollArea>
        </Paper>
    );
};
