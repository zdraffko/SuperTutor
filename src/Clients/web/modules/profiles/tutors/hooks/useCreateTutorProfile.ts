import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import createTutorProfile, { CreateTutorProfileRequest, CreateTutorProfileResponse } from "../api/createTutorProfile";

const useCreateTutorProfile = () => {
    const mutation = useMutation<CreateTutorProfileResponse, AxiosError<string>, CreateTutorProfileRequest, unknown>(createTutorProfile, {
        onSuccess: () => {
            queryClient.invalidateQueries("profiles-getAllTutorProfilesForTutor");
            queryClient.refetchQueries("profiles-getAllTutorProfilesForTutor");
        }
    });

    return {
        createTutorProfile: mutation.mutateAsync,
        isCreateTutorProfileLoading: mutation.isLoading,
        isCreateTutorProfileSuccessful: mutation.isSuccess,
        isCreateTutorProfileFailed: mutation.isError,
        createTutorProfileErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetCreateTutorProfileRequestState: mutation.reset
    };
};

export default useCreateTutorProfile;
