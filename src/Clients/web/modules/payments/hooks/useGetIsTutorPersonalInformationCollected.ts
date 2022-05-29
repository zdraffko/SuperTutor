import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getIsTutorPersonalInformationCollected, { GetIsTutorPersonalInformationCollectedResponse } from "../api/getIsTutorPersonalInformationCollected";

const useGetIsTutorPersonalInformationCollected = () => {
    const query = useQuery<GetIsTutorPersonalInformationCollectedResponse, AxiosError<string>>("payments-getIsTutorPersonalInformationCollected", getIsTutorPersonalInformationCollected, {
        staleTime: 5000
    });

    return {
        isTutorPersonalInformationCollected: query.data?.isPersonalInformationCollected,
        isGetIsTutorPersonalInformationCollectedLoading: query.isLoading,
        isGetIsTutorPersonalInformationCollectedSuccessful: query.isSuccess,
        isGetIsTutorPersonalInformationCollectedFailed: query.isError,
        getIsTutorPersonalInformationCollectedErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetIsTutorPersonalInformationCollected;
