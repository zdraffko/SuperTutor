import { Paper, ScrollArea } from "@mantine/core";
import { Canvas } from "./Canvas";

export const Whiteboard = () => (
    <Paper withBorder shadow="xl">
        <ScrollArea style={{ height: "88vh" }}>
            <Canvas />
        </ScrollArea>
    </Paper>
);
