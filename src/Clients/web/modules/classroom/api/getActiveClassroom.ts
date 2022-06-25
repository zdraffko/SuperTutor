import { UserType } from "utils/authentication/types";
import { axios } from "utils/axios";

export interface GetActiveClassroomResponse {
    classroomId: string | null;
}

const getActiveClassroom = async (userType: UserType): Promise<GetActiveClassroomResponse> => {
    const endpoint = userType == UserType.Tutor ? "/classrooms/getActiveClassroomForTutor" : "/classrooms/getActiveClassroomForStudent";

    const axiosResponse = await axios.get<GetActiveClassroomResponse>(endpoint);

    return axiosResponse.data;
};

export default getActiveClassroom;
