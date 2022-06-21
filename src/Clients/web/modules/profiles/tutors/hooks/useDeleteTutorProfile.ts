import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import deleteTutorProfile, { DeleteTutorProfileRequest } from "../api/deleteTutorProfile";

const useDeleteTutorProfile = () => {
    const mutation = useMutation<void, AxiosError<string>, DeleteTutorProfileRequest, unknown>(deleteTutorProfile, {
        onSuccess: () => {
            queryClient.invalidateQueries("profiles-getAllTutorProfilesForTutor");
            queryClient.refetchQueries("profiles-getAllTutorProfilesForTutor");
        }
    });

    return {
        deleteTutorProfile: mutation.mutateAsync,
        isDeleteTutorProfileLoading: mutation.isLoading,
        isDeleteTutorProfileSuccessful: mutation.isSuccess,
        isDeleteTutorProfileFailed: mutation.isError,
        deleteTutorProfileErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetDeleteTutorProfileRequestState: mutation.reset
    };
};

export default useDeleteTutorProfile;
