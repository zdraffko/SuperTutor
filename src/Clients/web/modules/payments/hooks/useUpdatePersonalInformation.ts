import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import { GetIsTutorPersonalInformationCollectedResponse } from "../api/getIsTutorPersonalInformationCollected";
import updatePersonalInformation, { UpdatePersonalInformationRequest } from "../api/updatePersonalInformation";

const useUpdatePersonalInformation = () => {
    const queryKeyThatIsDependantOnThisMutation = "payments-getIsTutorPersonalInformationCollected";
    type OptimisticUpdateContext = {
        previousValue: GetIsTutorPersonalInformationCollectedResponse | undefined;
        newValue: GetIsTutorPersonalInformationCollectedResponse;
    };

    const mutation = useMutation<void, AxiosError<string>, UpdatePersonalInformationRequest, OptimisticUpdateContext>(updatePersonalInformation, {
        onMutate: async () => {
            await queryClient.cancelQueries(queryKeyThatIsDependantOnThisMutation);

            const previousValue = queryClient.getQueryData<GetIsTutorPersonalInformationCollectedResponse>(queryKeyThatIsDependantOnThisMutation);
            const newValue: GetIsTutorPersonalInformationCollectedResponse = { isPersonalInformationCollected: true };

            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, newValue);

            return { previousValue, newValue };
        },
        onError: (error, newValue, context) => {
            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, context?.previousValue);
        }
    });

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
