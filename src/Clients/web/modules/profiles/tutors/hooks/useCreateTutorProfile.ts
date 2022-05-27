import { AxiosError } from "axios";
import { useMutation } from "react-query";
import createTutorProfile, { CreateTutorProfileRequest, CreateTutorProfileResponse } from "../api/createTutorProfile";

const useCreateTutorProfile = () => {
    const mutation = useMutation<CreateTutorProfileResponse, AxiosError<string>, CreateTutorProfileRequest, unknown>(createTutorProfile);

    return {
        createTutorProfile: mutation.mutateAsync,
        isCreateTutorProfileLoading: mutation.isLoading,
        isCreateTutorProfileSuccessful: mutation.isSuccess,
        isCreateTutorProfileFailed: mutation.isError,
        createTutorProfileErrorMessage: typeof mutation.error?.response?.data === typeof String ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetCreateTutorProfileRequestState: mutation.reset
    };
};

export default useCreateTutorProfile;
