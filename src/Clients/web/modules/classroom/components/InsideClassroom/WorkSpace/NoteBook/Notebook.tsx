import { Paper, ScrollArea, useMantineColorScheme } from "@mantine/core";
import { useIdle } from "@mantine/hooks";
import { HubConnection } from "@microsoft/signalr";
import { Dispatch, MutableRefObject, SetStateAction, useCallback, useEffect, useRef, useState } from "react";
import Peer from "simple-peer";
import { PeerDataChannelMessage } from "types/peerTypes";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { UserType } from "utils/authentication/types";
import RichTextEditor from "./TextEditor";

const initialValue = "<p>Моята <b>супер</b> тетрадка</p>";

interface NotebookProps {
    classroomHub: HubConnection;
    classroomId: string;
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
    isRemotePeerConnected: boolean;
    setIsNotebookSavingChanges: Dispatch<SetStateAction<boolean>>;
}

export const Notebook: React.FC<NotebookProps> = ({ classroomHub, classroomId, localPeerRef, isRemotePeerConnected, setIsNotebookSavingChanges }) => {
    const [notebookContent, setNotebookContent] = useState(initialValue);
    const { colorScheme } = useMantineColorScheme();
    const { user } = useAuth();
    const isUserIdle = useIdle(2000, { events: ["keydown"] });
    const lastSavedNotebookContent = useRef(initialValue);

    const updateRemoteNotebook = useCallback(
        (newNotebookContent: string) => {
            console.log("Updating remote notebook - " + newNotebookContent);
            const peerMessage: PeerDataChannelMessage<string> = {
                type: "NotebookContentUpdate",
                payload: newNotebookContent
            };

            localPeerRef.current?.send(JSON.stringify(peerMessage).replace(/\s/g, "&nbsp;"));
        },
        [localPeerRef]
    );

    useEffect(() => {
        console.log("isUserIdle " + isUserIdle);
        console.log("diff " + notebookContent !== lastSavedNotebookContent.current);
        if (isUserIdle && notebookContent !== lastSavedNotebookContent.current) {
            setIsNotebookSavingChanges(true);

            classroomHub.invoke("SaveNotebookContent", classroomId, notebookContent).then(() => {
                lastSavedNotebookContent.current = notebookContent;
            });
        }
    }, [isUserIdle, classroomHub, setIsNotebookSavingChanges]);

    // Used to update the student's notebook with the contents from the tutor's notebook when the students joins a room
    // Also used to setup the listener for on data events only once for the lifetime of the component
    useEffect(() => {
        classroomHub.off("NotebookContentSaved");
        classroomHub.on("NotebookContentSaved", () => {
            console.log("Recieved NotebookContentSaved");
            setIsNotebookSavingChanges(false);
        });

        if (isRemotePeerConnected && user?.type === UserType.Tutor) {
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

            updateRemoteNotebook(notebookContent);
        }

        if (user?.type === UserType.Student) {
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
        }
    }, [isRemotePeerConnected]);

    return (
        <Paper withBorder shadow="xl">
            <ScrollArea style={{ height: "88vh" }} offsetScrollbars>
                <RichTextEditor
                    value={notebookContent}
                    onChange={(value, delta, sources) => {
                        setNotebookContent(value);

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
