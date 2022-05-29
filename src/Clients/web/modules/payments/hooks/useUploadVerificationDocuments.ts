import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import { GetAreTutorVerificationDocumentsCollectedResponse } from "../api/getAreTutorVerificationDocumentsCollected";
import uploadVerificationDocuments, { UploadVerificationDocumentsRequest } from "../api/uploadVerificationDocuments";

const useUploadVerificationDocuments = () => {
    const queryKeyThatIsDependantOnThisMutation = "payments-getAreTutorVerificationDocumentsCollected";
    type OptimisticUpdateContext = {
        previousValue: GetAreTutorVerificationDocumentsCollectedResponse | undefined;
        newValue: GetAreTutorVerificationDocumentsCollectedResponse;
    };

    const mutation = useMutation<void, AxiosError<string>, UploadVerificationDocumentsRequest, OptimisticUpdateContext>(uploadVerificationDocuments, {
        onMutate: async () => {
            await queryClient.cancelQueries(queryKeyThatIsDependantOnThisMutation);

            const previousValue = queryClient.getQueryData<GetAreTutorVerificationDocumentsCollectedResponse>(queryKeyThatIsDependantOnThisMutation);
            const newValue: GetAreTutorVerificationDocumentsCollectedResponse = { areVerificationDocumentsCollected: true };

            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, newValue);

            return { previousValue, newValue };
        },
        onError: (error, newValue, context) => {
            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, context?.previousValue);
        }
    });

    return {
        uploadVerificationDocuments: mutation.mutateAsync,
        isUploadVerificationDocumentsLoading: mutation.isLoading,
        isUploadVerificationDocumentsSuccessful: mutation.isSuccess,
        isUploadVerificationDocumentsFailed: mutation.isError,
        uploadVerificationDocumentsErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetUploadVerificationDocumentsRequestState: mutation.reset
    };
};

export default useUploadVerificationDocuments;
