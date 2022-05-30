import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getTutorAvailability, { GetTutorAvailabilityRequest, GetTutorAvailabilityResponse } from "../api/getTutorAvailability";

const useGetTutorAvailability = (request: GetTutorAvailabilityRequest) => {
    const query = useQuery<GetTutorAvailabilityResponse, AxiosError<string>>("catalog-getTutorAvailability", () => getTutorAvailability(request), { staleTime: 5000 });

    return {
        tutorAvailabilities: query.data?.availabilities,
        isGetTutorAvailabilityLoading: query.isLoading,
        isGetTutorAvailabilitySuccessful: query.isSuccess,
        isGetTutorAvailabilityFailed: query.isError,
        getTutorAvailabilityErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetTutorAvailability;
