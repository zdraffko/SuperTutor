import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getTutorAvailability, { GetTutorAvailabilityResponse } from "../api/getTutorAvailability";

const useGetTutorAvailability = (tutorId: string | undefined) => {
    const query = useQuery<GetTutorAvailabilityResponse, AxiosError<string>>(["catalog", "getTutorAvailability", tutorId], () => getTutorAvailability({ tutorId: tutorId! }), {
        staleTime: 5000,
        enabled: !!tutorId
    });

    return {
        tutorAvailabilities: query.data?.availabilities,
        isGetTutorAvailabilityLoading: query.isLoading,
        isGetTutorAvailabilitySuccessful: query.isSuccess,
        isGetTutorAvailabilityFailed: query.isError,
        getTutorAvailabilityErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetTutorAvailability;
