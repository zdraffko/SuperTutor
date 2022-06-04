import { Center, Container, Loader } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { Dayjs } from "dayjs";
import { CellColors, RedactionModeColors } from "modules/calendar/constants";
import useAddAvailability from "modules/calendar/hooks/useAddAvailability";
import { CalendarRedactionMode, TimeSlot } from "modules/calendar/types";
import { useEffect } from "react";
import { X } from "tabler-icons-react";
import { DayJs } from "utils/dates";

interface CalendarBodyColumnCellHalfProps {
    date: Dayjs;
    hour: number;
    minute: number;
    selectedRedactionMode: CalendarRedactionMode;
    timeSlot: TimeSlot | undefined;
}

const CalendarBodyColumnCellHalf: React.FC<CalendarBodyColumnCellHalfProps> = ({ date, hour, minute, selectedRedactionMode, timeSlot }) => {
    const formattedDate = date.format("DD/MM/YYYY");
    const formattedTime = DayJs().hour(hour).minute(minute).format("HH:mm:00");
    const { addAvailability, isAddAvailabilityFailed, isAddAvailabilityLoading, addAvailabilityErrorMessage, resetAddAvailabilityRequestState } = useAddAvailability();

    useEffect(() => {
        if (isAddAvailabilityFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при добавянето на наличност",
                message: addAvailabilityErrorMessage,
                color: "red",
                icon: <X />
            });

            resetAddAvailabilityRequestState();

            return;
        }
    }, [addAvailabilityErrorMessage, isAddAvailabilityFailed, resetAddAvailabilityRequestState]);

    const handleOnClick = async () => {
        if (selectedRedactionMode === "AddAvailability") {
            console.log("AddAvailability");
            await addAvailability({ date: formattedDate, startTime: formattedTime });
        }
    };

    if (isAddAvailabilityLoading) {
        return (
            <Container style={{ height: "50px" }} sx={theme => ({ backgroundColor: theme.colors[RedactionModeColors[selectedRedactionMode]][3] })}>
                <Center style={{ height: "100%" }}>
                    <Loader size="sm" />
                </Center>
            </Container>
        );
    }

    return timeSlot ? (
        <Container
            style={{ height: "50px" }}
            sx={theme => ({
                backgroundColor: theme.colors[CellColors[timeSlot.type]][3]
            })}
        ></Container>
    ) : (
        <Container
            onClick={handleOnClick}
            style={{ height: "50px" }}
            sx={theme => ({
                "&:hover": {
                    cursor: "pointer",
                    backgroundColor: theme.colors[RedactionModeColors[selectedRedactionMode]][3]
                }
            })}
        ></Container>
    );
};
export default CalendarBodyColumnCellHalf;
