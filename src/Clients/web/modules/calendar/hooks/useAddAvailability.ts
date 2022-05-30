import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import addAvailability, { AddAvailabilityRequest } from "../api/addAvailability";
import { GetTutorTimeSlotsForWeekRequestResponse } from "../api/getTutorTimeSlotsForWeek";
import { TimeSlot } from "../types";

const useAddAvailability = () => {
    const queryKeyThatIsDependantOnThisMutation = "schedule-getTutorTimeSlotsForWeek";
    type OptimisticUpdateContext = {
        previousValue: GetTutorTimeSlotsForWeekRequestResponse | undefined;
        newValue: GetTutorTimeSlotsForWeekRequestResponse;
    };

    const mutation = useMutation<void, AxiosError<string>, AddAvailabilityRequest, OptimisticUpdateContext>(addAvailability, {
        onMutate: async addAvailabilityRequest => {
            await queryClient.cancelQueries(queryKeyThatIsDependantOnThisMutation);

            const previousValue = queryClient.getQueryData<GetTutorTimeSlotsForWeekRequestResponse>(queryKeyThatIsDependantOnThisMutation);
            const previousTimeSlotsForWeek = previousValue ? previousValue.timeSlotsForWeek : [];

            // TODO - This needs to be refactored
            const newTimeSlot: TimeSlot = {
                id: "",
                tutorId: "",
                date: addAvailabilityRequest.date,
                startTime: addAvailabilityRequest.startTime,
                type: "Availability",
                status: "Unassigned"
            };
            const newValue: GetTutorTimeSlotsForWeekRequestResponse = { timeSlotsForWeek: [...previousTimeSlotsForWeek, newTimeSlot] };

            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, newValue);

            return { previousValue, newValue };
        },
        onError: (error, newValue, context) => {
            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, context?.previousValue);
        }
    });

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
