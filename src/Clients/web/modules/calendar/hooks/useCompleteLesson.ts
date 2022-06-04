import { AxiosError } from "axios";
import { useMutation } from "react-query";
import { queryClient } from "utils/reactQuery";
import completeLesson, { CompleteLessonRequest } from "../api/completeLesson";
import { GetScheduledLessonsForStudentResponse } from "../api/getScheduledLessonsForStudent";

const useCompleteLesson = () => {
    const queryKeyThatIsDependantOnThisMutation = "schedule-getScheduledLessonsForStudent";
    type OptimisticUpdateContext = {
        previousValue: GetScheduledLessonsForStudentResponse | undefined;
        newValue: GetScheduledLessonsForStudentResponse;
    };

    const mutation = useMutation<void, AxiosError<string>, CompleteLessonRequest, OptimisticUpdateContext>(completeLesson, {
        onMutate: async completeLessonRequest => {
            await queryClient.cancelQueries(queryKeyThatIsDependantOnThisMutation);

            const previousValue = queryClient.getQueryData<GetScheduledLessonsForStudentResponse>(queryKeyThatIsDependantOnThisMutation);
            const scheduledLessons = previousValue ? previousValue.scheduledLessons : [];

            const lessonForCompletion = scheduledLessons.find(scheduledLesson => scheduledLesson.id == completeLessonRequest.lessonId);
            if (lessonForCompletion) {
                lessonForCompletion.status = "Завършен";
            }

            const newValue: GetScheduledLessonsForStudentResponse = { scheduledLessons };

            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, newValue);

            return { previousValue, newValue };
        },
        onError: (error, newValue, context) => {
            queryClient.setQueryData(queryKeyThatIsDependantOnThisMutation, context?.previousValue);
        }
    });

    return {
        completeLesson: mutation.mutateAsync,
        isCompleteLessonLoading: mutation.isLoading,
        isCompleteLessonSuccessful: mutation.isSuccess,
        isCompleteLessonFailed: mutation.isError,
        completeLessonErrorMessage: typeof mutation.error?.response?.data === "string" ? mutation.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно.",
        resetCompleteLessonRequestState: mutation.reset
    };
};

export default useCompleteLesson;
