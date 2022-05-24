import { AxiosError } from "axios";
import { useMutation } from "react-query";
import updateAddressInformation, { UpdateAddressInformationRequest } from "../api/updateAddressInformation";

const useUpdateAddressInformation = () => {
    const mutation = useMutation<unknown, AxiosError<string>, UpdateAddressInformationRequest, unknown>(updateAddressInformation);

    return {
        updateAddressInformation: mutation.mutateAsync,
        isUpdateAddressInformationLoading: mutation.isLoading,
        isUpdateAddressInformationSuccessful: mutation.isSuccess,
        isUpdateAddressInformationFailed: mutation.isError,
        updateAddressInformationErrorMessage: typeof mutation.error?.response?.data === typeof String ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetUpdateAddressInformationRequestState: mutation.reset
    };
};

export default useUpdateAddressInformation;
