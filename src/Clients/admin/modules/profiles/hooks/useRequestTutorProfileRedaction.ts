import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import requestTutorProfileRedaction, { RequestTutorProfileRedactionRequest } from "../api/requestTutorProfileRedaction";

const useRequestTutorProfileRedaction = () => {
    const mutation = useMutation<void, AxiosError<string>, RequestTutorProfileRedactionRequest, unknown>(requestTutorProfileRedaction, {
        onSuccess: () => {
            queryClient.invalidateQueries("profiles-getAllTutorProfilesForReview");
            queryClient.refetchQueries("profiles-getAllTutorProfilesForReview");
        }
    });

    return {
        requestTutorProfileRedaction: mutation.mutateAsync,
        isRequestTutorProfileRedactionLoading: mutation.isLoading,
        isRequestTutorProfileRedactionSuccessful: mutation.isSuccess,
        isRequestTutorProfileRedactionFailed: mutation.isError,
        requestTutorProfileRedactionErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetRequestTutorProfileRedactionRequestState: mutation.reset
    };
};

export default useRequestTutorProfileRedaction;
