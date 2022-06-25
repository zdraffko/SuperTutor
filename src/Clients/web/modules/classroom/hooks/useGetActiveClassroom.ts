import { AxiosError } from "axios";
import { useQuery } from "react-query";
import { useAuth } from "utils/authentication/reactQueryAuth";
import getActiveClassroomForTutor, { GetActiveClassroomResponse } from "../api/getActiveClassroom";

const useGetActiveClassroom = () => {
    const { user } = useAuth();
    const query = useQuery<GetActiveClassroomResponse, AxiosError<string>>("classrooms-getActiveClassroom", () => getActiveClassroomForTutor(user!.type));

    return {
        classroomId: query.data?.classroomId,
        isGetActiveClassroomLoading: query.isLoading,
        isGetActiveClassroomSuccessful: query.isSuccess,
        isGetActiveClassroomFailed: query.isError,
        getActiveClassroomErrorMessage: typeof query.error?.response?.data === "string" ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetActiveClassroom;
