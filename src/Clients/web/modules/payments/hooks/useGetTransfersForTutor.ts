import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getTransfersForTutor, { GetTransfersForTutorResponse } from "../api/getTransfersForTutor";

const useGetTransfersForTutor = () => {
    const query = useQuery<GetTransfersForTutorResponse, AxiosError<string>>("payments-getTransfersForTutor", getTransfersForTutor, { staleTime: 5000 });

    return {
        transfers: query.data?.transfers,
        isGetTransfersForTutorLoading: query.isLoading,
        isGetTransfersForTutorSuccessful: query.isSuccess,
        isGetTransfersForTutorFailed: query.isError,
        getTransfersForTutorErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetTransfersForTutor;
