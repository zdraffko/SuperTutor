import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import { GetIsTutorBankAccountInformationCollectedResponse } from "../api/getIsTutorBankAccountInformationCollected";
import updatePayoutInformation, { UpdatePayoutInformationRequest } from "../api/updatePayoutInformation";

const useUpdatePayoutInformation = () => {
    const queryKeyThatIsDependantOnThisMutation = "payments-getIsTutorBankAccountInformationCollected";
    type OptimisticUpdateContext = {
        previousValue: GetIsTutorBankAccountInformationCollectedResponse | undefined;
        newValue: GetIsTutorBankAccountInformationCollectedResponse;
    };

    const mutation = useMutation<void, AxiosError<string>, UpdatePayoutInformationRequest, OptimisticUpdateContext>(updatePayoutInformation, {
        onMutate: async () => {
            await queryClient.cancelQueries(queryKeyThatIsDependantOnThisMutation);

            const previousValue = queryClient.getQueryData<GetIsTutorBankAccountInformationCollectedResponse>(queryKeyThatIsDependantOnThisMutation);
            const newValue: GetIsTutorBankAccountInformationCollectedResponse = { isBankAccountInformationCollected: true };

            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, newValue);

            return { previousValue, newValue };
        },
        onError: (error, newValue, context) => {
            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, context?.previousValue);
        }
    });

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
