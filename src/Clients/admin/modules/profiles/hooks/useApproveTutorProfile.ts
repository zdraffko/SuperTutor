import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import approveTutorProfile, { ApproveTutorProfileRequest } from "../api/approveTutorProfile";

const useApproveTutorProfile = () => {
    const mutation = useMutation<void, AxiosError<string>, ApproveTutorProfileRequest, unknown>(approveTutorProfile, {
        onSuccess: () => {
            queryClient.invalidateQueries("profiles-getAllTutorProfilesForReview");
            queryClient.refetchQueries("profiles-getAllTutorProfilesForReview");
        }
    });

    return {
        approveTutorProfile: mutation.mutateAsync,
        isApproveTutorProfileLoading: mutation.isLoading,
        isApproveTutorProfileSuccessful: mutation.isSuccess,
        isApproveTutorProfileFailed: mutation.isError,
        approveTutorProfileErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetApproveTutorProfileRequestState: mutation.reset
    };
};

export default useApproveTutorProfile;
