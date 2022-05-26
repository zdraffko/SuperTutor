import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getAreTutorVerificationDocumentsCollected, { GetAreTutorVerificationDocumentsCollectedResponse } from "../api/getAreTutorVerificationDocumentsCollected";

const useGetAreTutorVerificationDocumentsCollected = () => {
    const query = useQuery<GetAreTutorVerificationDocumentsCollectedResponse, AxiosError<string>>("payments-getAreTutorVerificationDocumentsCollected", getAreTutorVerificationDocumentsCollected, {
        staleTime: 5000
    });

    return {
        areVerificationDocumentsCollected: query.data?.areVerificationDocumentsCollected,
        isGetAreVerificationDocumentsCollectedLoading: query.isLoading,
        isGetAreVerificationDocumentsCollectedSuccessful: query.isSuccess,
        isGetAreVerificationDocumentsCollectedFailed: query.isError,
        getAreVerificationDocumentsCollectedErrorMessage: typeof query.error?.response?.data === typeof String ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetAreTutorVerificationDocumentsCollected;
