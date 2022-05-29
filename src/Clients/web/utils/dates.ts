import dayjs, { Dayjs } from "dayjs";
import "dayjs/locale/bg";

dayjs.locale("bg");
export const DayJs = dayjs;

export const dayJsRange = (startDate: Dayjs, endDate: Dayjs): Dayjs[] => {
    const dateRange: Dayjs[] = [startDate];
    let currentDate = startDate;

    while (currentDate.isBefore(endDate) || currentDate.isSame(endDate)) {
        currentDate = currentDate.add(1, "day");
        dateRange.push(currentDate);
    }

    return dateRange;
};

export const convertDateToDateOnlyString = (date: Date) => `${("0" + date.getDate()).slice(-2)}/${("0" + (date.getMonth() + 1)).slice(-2)}/${date.getFullYear()}`;
