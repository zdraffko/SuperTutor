import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getTutorTimeSlotsForWeek, { GetTutorTimeSlotsForWeekRequest, GetTutorTimeSlotsForWeekRequestResponse } from "../api/getTutorTimeSlotsForWeek";

const useGetTutorTimeSlotsForWeek = (request: GetTutorTimeSlotsForWeekRequest) => {
    const query = useQuery<GetTutorTimeSlotsForWeekRequestResponse, AxiosError<string>>("schedule-getTutorTimeSlotsForWeek", () => getTutorTimeSlotsForWeek(request), {
        refetchOnWindowFocus: false,
        staleTime: 5000
    });

    return {
        timeSlotsForWeek: query.data?.timeSlotsForWeek,
        isGetTutorTimeSlotsForWeekLoading: query.isFetching,
        isGetTutorTimeSlotsForWeekSuccessful: query.isSuccess,
        isGetTutorTimeSlotsForWeekFailed: query.isError,
        getTutorTimeSlotsForWeekErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetTutorTimeSlotsForWeek;
