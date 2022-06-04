import { Group, Paper, SegmentedControl, Text } from "@mantine/core";
import { RedactionModeColors } from "modules/calendar/constants";
import { CalendarRedactionMode } from "modules/calendar/types";
import { Dispatch, SetStateAction, useEffect, useState } from "react";

const RedactionModes: { label: string; value: CalendarRedactionMode }[] = [
    { label: "Добавяне на наличност", value: "AddAvailability" },
    { label: "Добавяне на почивка", value: "TakeTimeOff" }
];

interface CalendarToolbarProps {
    selectedRedactionMode: CalendarRedactionMode;
    setSelectedRedactionMode: Dispatch<SetStateAction<CalendarRedactionMode>>;
}

export const CalendarToolbar: React.FC<CalendarToolbarProps> = ({ selectedRedactionMode, setSelectedRedactionMode }) => {
    const [redactionModeColor, setRedactionModeColor] = useState(RedactionModeColors[selectedRedactionMode]);

    useEffect(() => {
        setRedactionModeColor(RedactionModeColors[selectedRedactionMode]);
    }, [selectedRedactionMode]);

    return (
        <Paper m="xs" p="xl">
            <Group>
                <Text>Режим:</Text>
                <SegmentedControl value={selectedRedactionMode} onChange={value => setSelectedRedactionMode(value as CalendarRedactionMode)} color={redactionModeColor} data={RedactionModes} />
            </Group>
        </Paper>
    );
};
