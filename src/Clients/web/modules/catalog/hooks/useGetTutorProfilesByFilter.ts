import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getTutorProfilesByFilter, { GetTutorProfilesByFilterRequest, GetTutorProfilesByFilterResponse } from "../api/getTutorProfilesByFilter";

const useGetTutorProfilesByFilter = (request: GetTutorProfilesByFilterRequest) => {
    const query = useQuery<GetTutorProfilesByFilterResponse, AxiosError<string>>("catalog-getTutorProfilesByFilter", () => getTutorProfilesByFilter(request), {
        refetchOnWindowFocus: false,
        staleTime: 5000,
        enabled: false
    });

    return {
        tutorProfiles: query.data?.tutorProfiles,
        refetchTutorProfiles: query.refetch,
        isGetTutorProfilesByFilterLoading: query.isFetching,
        isGetTutorProfilesByFilterSuccessful: query.isSuccess,
        isGetTutorProfilesByFilterFailed: query.isError,
        getTutorProfilesByFilterErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetTutorProfilesByFilter;
