import { Container } from "@mantine/core";
import { Dayjs } from "dayjs";
import { DayJs } from "utils/dates";

interface CalendarBodyColumnCellHalfProps {
    date: Dayjs;
    hour: number;
    minute: number;
}

const CalendarBodyColumnCellHalf: React.FC<CalendarBodyColumnCellHalfProps> = ({ date, hour, minute }) => {
    const formattedDate = date.format("DD/MM/YYYY");
    const formattedTime = DayJs().hour(hour).minute(minute).format("HH:mm:00");

    return (
        <Container
            onClick={() => console.log(formattedDate + " - " + formattedTime)}
            style={{ height: "50px" }}
            sx={theme => ({
                "&:hover": {
                    backgroundColor: theme.colors.gray[1]
                }
            })}
        ></Container>
    );
};
export default CalendarBodyColumnCellHalf;
