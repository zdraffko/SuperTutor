import { AxiosError } from "axios";
import { useMutation } from "react-query";
import createCharge, { CreateChargeRequest, CreateChargeResponse } from "../api/createCharge";

const useCreateCharge = () => {
    const mutation = useMutation<CreateChargeResponse, AxiosError<string>, CreateChargeRequest, unknown>(createCharge);

    return {
        createCharge: mutation.mutateAsync,
        isCreateChargeLoading: mutation.isLoading,
        isCreateChargeSuccessful: mutation.isSuccess,
        isCreateChargeFailed: mutation.isError,
        createChargeErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetCreateChargeRequestState: mutation.reset
    };
};

export default useCreateCharge;
