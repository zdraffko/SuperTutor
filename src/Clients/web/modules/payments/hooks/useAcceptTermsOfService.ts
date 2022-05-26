import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import acceptTermsOfService from "../api/acceptTermsOfService";
import { GetAreTutorTermsOfServiceAcceptedResponse } from "../api/getAreTutorTermsOfServiceAccepted";

const useAcceptTermsOfService = () => {
    const queryKeyThatIsDependantOnThisMutation = "payments-getAreTutorTermsOfServiceAccepted";
    type OptimisticUpdateContext = {
        previousValue: GetAreTutorTermsOfServiceAcceptedResponse | undefined;
        newValue: GetAreTutorTermsOfServiceAcceptedResponse;
    };

    const mutation = useMutation<void, AxiosError<string>, GetAreTutorTermsOfServiceAcceptedResponse, OptimisticUpdateContext>(acceptTermsOfService, {
        onMutate: async () => {
            await queryClient.cancelQueries(queryKeyThatIsDependantOnThisMutation);

            const previousValue = queryClient.getQueryData<GetAreTutorTermsOfServiceAcceptedResponse>(queryKeyThatIsDependantOnThisMutation);
            const newValue: GetAreTutorTermsOfServiceAcceptedResponse = { areTermsOfServiceAccepted: true };

            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, newValue);

            return { previousValue, newValue };
        },
        onError: (error, newValue, context) => {
            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, context?.previousValue);
        }
    });

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
