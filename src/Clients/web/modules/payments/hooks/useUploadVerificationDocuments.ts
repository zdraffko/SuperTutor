import { AxiosError } from "axios";
import { useMutation } from "react-query";
import uploadVerificationDocuments, { UploadVerificationDocumentsRequest } from "../api/uploadVerificationDocuments";

const useUploadVerificationDocuments = () => {
    const mutation = useMutation<unknown, AxiosError<string>, UploadVerificationDocumentsRequest, unknown>(uploadVerificationDocuments);

    return {
        uploadVerificationDocuments: mutation.mutateAsync,
        isUploadVerificationDocumentsLoading: mutation.isLoading,
        isUploadVerificationDocumentsSuccessful: mutation.isSuccess,
        isUploadVerificationDocumentsFailed: mutation.isError,
        uploadVerificationDocumentsErrorMessage: typeof mutation.error?.response?.data === typeof String ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetUploadVerificationDocumentsRequestState: mutation.reset
    };
};

export default useUploadVerificationDocuments;
