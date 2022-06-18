import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getAllTutorProfilesForReview, { GetAllTutorProfilesForReviewResponse } from "../api/getAllTutorProfilesForReview";

const useGetAllTutorProfilesForReview = () => {
    const query = useQuery<GetAllTutorProfilesForReviewResponse, AxiosError<string>>("profiles-getAllTutorProfilesForReview", getAllTutorProfilesForReview, { staleTime: 5000 });

    return {
        tutorProfiles: query.data?.tutorProfiles,
        isGetAllTutorProfilesForReviewLoading: query.isLoading,
        isGetAllTutorProfilesForReviewSuccessful: query.isSuccess,
        isGetAllTutorProfilesForReviewFailed: query.isError,
        getAllTutorProfilesForReviewErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetAllTutorProfilesForReview;
