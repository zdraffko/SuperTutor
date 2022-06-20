import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import submitTutorProfileForReview, { SubmitTutorProfileForReviewRequest } from "../api/submitTutorProfileForReview";

const useSubmitTutorProfileForReview = () => {
    const mutation = useMutation<void, AxiosError<string>, SubmitTutorProfileForReviewRequest, unknown>(submitTutorProfileForReview, {
        onSuccess: () => {
            queryClient.invalidateQueries("profiles-getAllTutorProfilesForTutor");
            queryClient.refetchQueries("profiles-getAllTutorProfilesForTutor");
        }
    });

    return {
        submitTutorProfileForReview: mutation.mutateAsync,
        isSubmitTutorProfileForReviewLoading: mutation.isLoading,
        isSubmitTutorProfileForReviewSuccessful: mutation.isSuccess,
        isSubmitTutorProfileForReviewFailed: mutation.isError,
        submitTutorProfileForReviewErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetSubmitTutorProfileForReviewRequestState: mutation.reset
    };
};

export default useSubmitTutorProfileForReview;
