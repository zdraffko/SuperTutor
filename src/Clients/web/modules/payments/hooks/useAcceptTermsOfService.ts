import { AxiosError } from "axios";
import { useMutation } from "react-query";
import acceptTermsOfService, { AcceptTermsOfServiceRequest } from "../api/acceptTermsOfService";

const useAcceptTermsOfService = () => {
    const mutation = useMutation<unknown, AxiosError<string>, AcceptTermsOfServiceRequest, unknown>(acceptTermsOfService);

    return {
        acceptTermsOfService: mutation.mutateAsync,
        isAcceptTermsOfServiceLoading: mutation.isLoading,
        isAcceptTermsOfServiceSuccessful: mutation.isSuccess,
        isAcceptTermsOfServiceFailed: mutation.isError,
        acceptTermsOfServiceErrorMessage: typeof mutation.error?.response?.data === typeof String ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetAcceptTermsOfServiceRequestState: mutation.reset
    };
};

export default useAcceptTermsOfService;
