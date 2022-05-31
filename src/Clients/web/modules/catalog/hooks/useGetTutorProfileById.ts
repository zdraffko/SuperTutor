import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getTutorProfileById, { GetTutorProfileByIdRequest, GetTutorProfileByIdResponse } from "../api/getTutorProfileById";

const useGetTutorProfileById = (request: GetTutorProfileByIdRequest) => {
    const query = useQuery<GetTutorProfileByIdResponse, AxiosError<string>>("catalog-getTutorProfileById", () => getTutorProfileById(request), {
        staleTime: 5000
    });

    return {
        tutorProfile: query.data?.profile,
        isGetTutorProfileByIdLoading: query.isFetching,
        isGetTutorProfileByIdSuccessful: query.isSuccess,
        isGetTutorProfileByIdFailed: query.isError,
        getTutorProfileByIdErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetTutorProfileById;
