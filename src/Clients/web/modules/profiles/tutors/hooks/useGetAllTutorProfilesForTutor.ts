import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getAllTutorProfilesForTutor, { GetAllTutorProfilesForTutorResponse } from "../api/getAllTutorProfilesForTutor";

const useGetAllTutorProfilesForTutor = () => {
    const query = useQuery<GetAllTutorProfilesForTutorResponse, AxiosError<string>>("profiles-getAllTutorProfilesForTutor", getAllTutorProfilesForTutor, { staleTime: 5000 });

    return {
        tutorProfiles: query.data?.tutorProfiles,
        isGetAllTutorProfilesForTutorLoading: query.isLoading,
        isGetAllTutorProfilesForTutorSuccessful: query.isSuccess,
        isGetAllTutorProfilesForTutorFailed: query.isError,
        getAllTutorProfilesForTutorErrorMessage: typeof query.error?.response?.data === typeof String ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetAllTutorProfilesForTutor;
