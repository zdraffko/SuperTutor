import { Paper, ScrollArea, useMantineColorScheme } from "@mantine/core";
import { useState } from "react";
import RichTextEditor from "./TextEditor";

const initialValue = "<p>Моята <b>супер</b> тетрадка</p>";

export const Notebook: React.FC = () => {
    const [value, onChange] = useState(initialValue);
    const { colorScheme } = useMantineColorScheme();

    return (
        <Paper withBorder shadow="xl">
            <ScrollArea style={{ height: "88vh" }} offsetScrollbars>
                <RichTextEditor
                    value={value}
                    onChange={onChange}
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
