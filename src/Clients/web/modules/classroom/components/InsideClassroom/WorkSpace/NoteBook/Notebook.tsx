import { Paper, ScrollArea, useMantineColorScheme } from "@mantine/core";
import { MutableRefObject, useEffect, useState } from "react";
import Peer from "simple-peer";
import RichTextEditor from "./TextEditor";

const initialValue = "<p>Моята <b>супер</b> тетрадка</p>";

interface PeerDataChannelMessage<TPayload> {
    type: string;
    payload: TPayload;
}

interface NotebookProps {
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
    isRemotePeerConnected: boolean;
}

export const Notebook: React.FC<NotebookProps> = ({ localPeerRef, isRemotePeerConnected }) => {
    const [notebookContent, setNotebookContent] = useState(initialValue);
    const { colorScheme } = useMantineColorScheme();

    useEffect(() => {
        console.log("Notebook init");
        localPeerRef.current?.on("data", (remoteData: string) => {
            console.log("Peer Local: Recieved ntoebook data " + remoteData);
            try {
                const peerMessage: PeerDataChannelMessage<string> = JSON.parse(remoteData);
                if (peerMessage.type !== "NotebookContentUpdate") {
                    return;
                }

                setNotebookContent(peerMessage.payload);
            } catch (error) {} // Message not for us
        });
    }, [localPeerRef]);

    const updateRemoteNotebook = (newNotebookContent: string) => {
        console.log("Updating remote notebook");
        const peerMessage: PeerDataChannelMessage<string> = {
            type: "NotebookContentUpdate",
            payload: newNotebookContent
        };

        localPeerRef.current?.send(JSON.stringify(peerMessage).replace(/\s/g, "&nbsp;"));
    };

    return (
        <Paper withBorder shadow="xl">
            <ScrollArea style={{ height: "88vh" }} offsetScrollbars>
                <RichTextEditor
                    value={notebookContent}
                    onChange={(value, delta, sources) => {
                        if (isRemotePeerConnected && sources === "user") {
                            updateRemoteNotebook(value);
                        }
                    }}
                    style={{ minHeight: "88vh" }}
                    styles={{
                        root: { border: "none", backgroundColor: colorScheme === "dark" ? "" : "#F8F9FA" },
                        toolbar: {}
                    }}
                />
            </ScrollArea>
        </Paper>
    );
};
