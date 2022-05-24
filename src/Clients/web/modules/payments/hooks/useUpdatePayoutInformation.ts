import { AxiosError } from "axios";
import { useMutation } from "react-query";
import updatePayoutInformation, { UpdatePayoutInformationRequest } from "../api/updatePayoutInformation";

const useUpdatePayoutInformation = () => {
    const mutation = useMutation<unknown, AxiosError<string>, UpdatePayoutInformationRequest, unknown>(updatePayoutInformation);

    return {
        updatePayoutInformation: mutation.mutateAsync,
        isUpdatePayoutInformationLoading: mutation.isLoading,
        isUpdatePayoutInformationSuccessful: mutation.isSuccess,
        isUpdatePayoutInformationFailed: mutation.isError,
        updatePayoutInformationErrorMessage: typeof mutation.error?.response?.data === typeof String ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetUpdatePayoutInformationRequestState: mutation.reset
    };
};

export default useUpdatePayoutInformation;
