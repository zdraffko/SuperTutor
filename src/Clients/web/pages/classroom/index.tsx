import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { StudentInsideClassroom, StudentOutsideClassroom, TutorInsideClassroom, TutorOutsideClassroom } from "modules/classroom";
import useClassroomHub from "modules/classroom/hooks/useClassroomHub";
import { useRef, useState } from "react";
import Peer from "simple-peer";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { UserType } from "utils/authentication/types";

const ClassroomPage: React.FC = () => {
    const [classroomName, setClassroomName] = useState("");
    const [isInsideClassroom, setIsInsideClassroom] = useState(false);
    const { hubConnection } = useClassroomHub();
    const localPeerRef = useRef<Peer.Instance>();
    const { user } = useAuth();

    return (
        <AuthenticationProtectedPage>
            <MainLayout>
                {isInsideClassroom ? (
                    user?.type === UserType.Tutor ? (
                        <TutorInsideClassroom classroomHub={hubConnection} classroomName={classroomName} setIsInsideClassroom={setIsInsideClassroom} localPeerRef={localPeerRef} />
                    ) : (
                        <StudentInsideClassroom
                            userEmail={user?.email}
                            classroomHub={hubConnection}
                            classroomName={classroomName}
                            setIsInsideClassroom={setIsInsideClassroom}
                            localPeerRef={localPeerRef}
                        />
                    )
                ) : user?.type === UserType.Tutor ? (
                    <TutorOutsideClassroom
                        userEmail={user?.email}
                        classroomHub={hubConnection}
                        classroomName={classroomName}
                        setClassroomName={setClassroomName}
                        setIsInsideClassroom={setIsInsideClassroom}
                        localPeerRef={localPeerRef}
                    />
                ) : (
                    <StudentOutsideClassroom
                        userEmail={user?.email}
                        classroomHub={hubConnection}
                        classroomName={classroomName}
                        setClassroomName={setClassroomName}
                        setIsInsideClassroom={setIsInsideClassroom}
                        localPeerRef={localPeerRef}
                    />
                )}
            </MainLayout>
        </AuthenticationProtectedPage>
    );
};
export default ClassroomPage;
