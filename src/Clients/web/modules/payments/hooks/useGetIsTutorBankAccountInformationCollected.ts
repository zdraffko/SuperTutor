import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getIsTutorBankAccountInformationCollected, { GetIsTutorBankAccountInformationCollectedResponse } from "../api/getIsTutorBankAccountInformationCollected";

const useGetIsTutorBankAccountInformationCollected = () => {
    const query = useQuery<GetIsTutorBankAccountInformationCollectedResponse, AxiosError<string>>("payments-getIsTutorBankAccountInformationCollected", getIsTutorBankAccountInformationCollected, {
        staleTime: 5000
    });

    return {
        isTutorBankAccountInformationCollected: query.data?.isBankAccountInformationCollected,
        isGetIsTutorBankAccountInformationCollectedLoading: query.isLoading,
        isGetIsTutorBankAccountInformationCollectedSuccessful: query.isSuccess,
        isGetIsTutorBankAccountInformationCollectedFailed: query.isError,
        getIsTutorBankAccountInformationCollectedErrorMessage: typeof query.error?.response?.data === typeof String ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetIsTutorBankAccountInformationCollected;
