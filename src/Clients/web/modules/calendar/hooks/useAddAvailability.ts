import { AxiosError } from "axios";
import { useMutation } from "react-query";
import addAvailability, { AddAvailabilityRequest } from "../api/addAvailability";

const useAddAvailability = () => {
    const mutation = useMutation<void, AxiosError<string>, AddAvailabilityRequest, unknown>(addAvailability);

    return {
        addAvailability: mutation.mutateAsync,
        isAddAvailabilityLoading: mutation.isLoading,
        isAddAvailabilitySuccessful: mutation.isSuccess,
        isAddAvailabilityFailed: mutation.isError,
        addAvailabilityErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetAddAvailabilityRequestState: mutation.reset
    };
};

export default useAddAvailability;
