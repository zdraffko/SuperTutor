import { Paper, ScrollArea } from "@mantine/core";
import { useState } from "react";
import RichTextEditor from "./TextEditor";

const initialValue = "<p>Моята <b>супер</b> тетрадка</p>";

export const Notebook: React.FC = () => {
    const [value, onChange] = useState(initialValue);

    return (
        <Paper withBorder>
            <ScrollArea style={{ height: "88vh", border: "none" }} offsetScrollbars>
                <RichTextEditor
                    value={value}
                    onChange={onChange}
                    style={{ minHeight: "88vh" }}
                    styles={{
                        root: { border: "none" },
                        toolbar: {}
                    }}
                />
            </ScrollArea>
        </Paper>
    );
};
