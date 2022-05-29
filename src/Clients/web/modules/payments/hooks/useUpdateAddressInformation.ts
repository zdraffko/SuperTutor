import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import { GetIsTutorAddressInformationCollectedResponse } from "../api/getIsTutorAddressInformationCollected";
import updateAddressInformation, { UpdateAddressInformationRequest } from "../api/updateAddressInformation";

const useUpdateAddressInformation = () => {
    const queryKeyThatIsDependantOnThisMutation = "payments-getIsTutorAddressInformationCollected";
    type OptimisticUpdateContext = {
        previousValue: GetIsTutorAddressInformationCollectedResponse | undefined;
        newValue: GetIsTutorAddressInformationCollectedResponse;
    };

    const mutation = useMutation<void, AxiosError<string>, UpdateAddressInformationRequest, OptimisticUpdateContext>(updateAddressInformation, {
        onMutate: async () => {
            await queryClient.cancelQueries(queryKeyThatIsDependantOnThisMutation);

            const previousValue = queryClient.getQueryData<GetIsTutorAddressInformationCollectedResponse>(queryKeyThatIsDependantOnThisMutation);
            const newValue: GetIsTutorAddressInformationCollectedResponse = { isAddressInformationCollected: true };

            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, newValue);

            return { previousValue, newValue };
        },
        onError: (error, newValue, context) => {
            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, context?.previousValue);
        }
    });

    return {
        updateAddressInformation: mutation.mutateAsync,
        isUpdateAddressInformationLoading: mutation.isLoading,
        isUpdateAddressInformationSuccessful: mutation.isSuccess,
        isUpdateAddressInformationFailed: mutation.isError,
        updateAddressInformationErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetUpdateAddressInformationRequestState: mutation.reset
    };
};

export default useUpdateAddressInformation;
