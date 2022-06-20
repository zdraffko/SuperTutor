import { AxiosError } from "axios";
import { useMutation } from "react-query";
import updateTutorProfileAbout, { UpdateTutorProfileAboutRequest } from "../api/updateTutorProfileAbout";

const useUpdateTutorProfileAbout = () => {
    const mutation = useMutation<void, AxiosError<string>, UpdateTutorProfileAboutRequest, unknown>(updateTutorProfileAbout);

    return {
        updateTutorProfileAbout: mutation.mutateAsync,
        isUpdateTutorProfileAboutLoading: mutation.isLoading,
        isUpdateTutorProfileAboutSuccessful: mutation.isSuccess,
        isUpdateTutorProfileAboutFailed: mutation.isError,
        updateTutorProfileAboutErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetUpdateTutorProfileAboutRequestState: mutation.reset
    };
};

export default useUpdateTutorProfileAbout;
