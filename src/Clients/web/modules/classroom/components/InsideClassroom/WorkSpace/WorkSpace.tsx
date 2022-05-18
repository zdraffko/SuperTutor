import { Tabs } from "@mantine/core";
import { Notes, Perspective } from "tabler-icons-react";
import { Notebook } from "./NoteBook/Notebook";
import { Whiteboard } from "./Whiteboard/Whiteboard";

export const WorkSpace: React.FC = () => (
    <Tabs position="center" grow>
        <Tabs.Tab label="Бяла дъска" tabKey="Whiteboard" icon={<Perspective size={20} />}>
            <Whiteboard />
        </Tabs.Tab>
        <Tabs.Tab label="Тетрадка" tabKey="Notebook" icon={<Notes size={20} />}>
            <Notebook />
        </Tabs.Tab>
    </Tabs>
);
