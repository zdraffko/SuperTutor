import { Grid } from "@mantine/core";
import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { VideoConference, WorkSpace } from "modules/classroom";

const ClassroomPage: React.FC = () => (
    <AuthenticationProtectedPage>
        <MainLayout>
            <Grid>
                <Grid.Col span={8}>
                    <WorkSpace />
                </Grid.Col>
                <Grid.Col span={4}>
                    <VideoConference />
                </Grid.Col>
            </Grid>
        </MainLayout>
    </AuthenticationProtectedPage>
);
export default ClassroomPage;
