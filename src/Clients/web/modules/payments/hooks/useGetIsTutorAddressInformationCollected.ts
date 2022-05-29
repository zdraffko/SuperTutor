import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getIsTutorAddressInformationCollected, { GetIsTutorAddressInformationCollectedResponse } from "../api/getIsTutorAddressInformationCollected";

const useGetIsTutorAddressInformationCollected = () => {
    const query = useQuery<GetIsTutorAddressInformationCollectedResponse, AxiosError<string>>("payments-getIsTutorAddressInformationCollected", getIsTutorAddressInformationCollected, {
        staleTime: 5000
    });

    return {
        isTutorAddressInformationCollected: query.data?.isAddressInformationCollected,
        isGetIsTutorAddressInformationCollectedLoading: query.isLoading,
        isGetIsTutorAddressInformationCollectedSuccessful: query.isSuccess,
        isGetIsTutorAddressInformationCollectedFailed: query.isError,
        getIsTutorAddressInformationCollectedErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetIsTutorAddressInformationCollected;
