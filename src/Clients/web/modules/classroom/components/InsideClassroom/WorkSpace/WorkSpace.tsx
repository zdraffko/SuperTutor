import { Tabs } from "@mantine/core";
import { Dispatch, MutableRefObject, SetStateAction } from "react";
import Peer from "simple-peer";
import { Notes, Perspective } from "tabler-icons-react";
import { Notebook } from "./NoteBook/Notebook";
import { Whiteboard } from "./Whiteboard/Whiteboard";

interface WorkSpaceProps {
    localPeerRef: MutableRefObject<Peer.Instance | undefined>;
    isRemotePeerConnected: boolean;
    setIsWorkSpaceSavingChanges: Dispatch<SetStateAction<boolean>>;
}

export const WorkSpace: React.FC<WorkSpaceProps> = ({ localPeerRef, isRemotePeerConnected, setIsWorkSpaceSavingChanges }) => (
    <Tabs position="center" grow>
        <Tabs.Tab label="Бяла дъска" tabKey="Whiteboard" icon={<Perspective size={20} />}>
            <Whiteboard localPeerRef={localPeerRef} isRemotePeerConnected={isRemotePeerConnected} setIsWhiteboardSavingChanges={setIsWorkSpaceSavingChanges} />
        </Tabs.Tab>
        <Tabs.Tab label="Тетрадка" tabKey="Notebook" icon={<Notes size={20} />}>
            <Notebook localPeerRef={localPeerRef} isRemotePeerConnected={isRemotePeerConnected} setIsNotebookSavingChanges={setIsWorkSpaceSavingChanges} />
        </Tabs.Tab>
    </Tabs>
);
