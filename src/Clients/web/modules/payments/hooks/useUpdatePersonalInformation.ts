import { AxiosError } from "axios";
import { useMutation } from "react-query";
import updatePersonalInformation, { UpdatePersonalInformationRequest } from "../api/updatePersonalInformation";

const useUpdatePersonalInformation = () => {
    const mutation = useMutation<unknown, AxiosError<string>, UpdatePersonalInformationRequest, unknown>(updatePersonalInformation);

    return {
        updatePersonalInformation: mutation.mutateAsync,
        isUpdatePersonalInformationLoading: mutation.isLoading,
        isUpdatePersonalInformationSuccessful: mutation.isSuccess,
        isUpdatePersonalInformationFailed: mutation.isError,
        updatePersonalInformationErrorMessage: typeof mutation.error?.response?.data === typeof String ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetUpdatePersonalInformationRequestState: mutation.reset
    };
};

export default useUpdatePersonalInformation;
