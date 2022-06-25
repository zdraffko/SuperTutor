import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { InsideClassroom, OutsideClassroom } from "modules/classroom";
import useClassroomHub from "modules/classroom/hooks/useClassroomHub";
import { NextPage } from "next";
import { useRef, useState } from "react";
import Peer from "simple-peer";

const ClassroomPage: NextPage = () => {
    const [isInsideClassroom, setIsInsideClassroom] = useState(false);
    const { classroomHub } = useClassroomHub();
    const localPeerRef = useRef<Peer.Instance>();
    const classroomIdRef = useRef<string | null>(null);
    const isInitiatorRef = useRef<boolean>(false);

    return (
        <AuthenticationProtectedPage>
            <MainLayout>
                {isInsideClassroom && classroomIdRef.current ? (
                    <InsideClassroom
                        isInitiatorRef={isInitiatorRef}
                        classroomHub={classroomHub}
                        classroomId={classroomIdRef.current}
                        setIsInsideClassroom={setIsInsideClassroom}
                        localPeerRef={localPeerRef}
                    />
                ) : (
                    <OutsideClassroom
                        isInitiatorRef={isInitiatorRef}
                        classroomHub={classroomHub}
                        classroomIdRef={classroomIdRef}
                        setIsInsideClassroom={setIsInsideClassroom}
                        localPeerRef={localPeerRef}
                    />
                )}
            </MainLayout>
        </AuthenticationProtectedPage>
    );
};
export default ClassroomPage;
