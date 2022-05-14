import { Tabs } from "@mantine/core";
import { Notebook } from "./NoteBook/Notebook";
import { Whiteboard } from "./Whiteboard";

export const WorkSpace: React.FC = () => (
    <Tabs position="center">
        <Tabs.Tab label="Бяла дъска" tabKey="Whiteboard">
            <Whiteboard />
        </Tabs.Tab>
        <Tabs.Tab label="Тетрадка" tabKey="Notebook">
            <Notebook />
        </Tabs.Tab>
    </Tabs>
);
