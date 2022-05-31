import { Avatar, Center, Divider, Loader, Paper, Stack, Title } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { TutorProfileAvailability, TutorProfileInformation } from "modules/catalog";
import useGetTutorAvailability from "modules/catalog/hooks/useGetTutorAvailability";
import useGetTutorProfileById from "modules/catalog/hooks/useGetTutorProfileById";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useEffect } from "react";
import { X } from "tabler-icons-react";

const TutorCatalogPage: NextPage = () => {
    const router = useRouter();
    const { tutorProfileId } = router.query;
    const { tutorProfile, isGetTutorProfileByIdFailed, isGetTutorProfileByIdLoading, getTutorProfileByIdErrorMessage } = useGetTutorProfileById({ tutorProfileId: `${tutorProfileId}` });
    const { tutorAvailabilities, isGetTutorAvailabilityFailed, isGetTutorAvailabilityLoading, getTutorAvailabilityErrorMessage } = useGetTutorAvailability(tutorProfile?.tutorId);

    useEffect(() => {
        if (isGetTutorProfileByIdFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при зареждането на профила на учителят",
                message: getTutorProfileByIdErrorMessage,
                color: "red",
                icon: <X />
            });
        }
    }, [getTutorAvailabilityErrorMessage, getTutorProfileByIdErrorMessage, isGetTutorAvailabilityFailed, isGetTutorProfileByIdFailed]);

    useEffect(() => {
        if (isGetTutorAvailabilityFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при зареждането на свободните часове на учителят",
                message: getTutorAvailabilityErrorMessage,
                color: "red",
                icon: <X />
            });
        }
    }, [getTutorAvailabilityErrorMessage, isGetTutorAvailabilityFailed]);

    if (isGetTutorProfileByIdLoading || isGetTutorAvailabilityLoading || !tutorProfile || !tutorAvailabilities) {
        return (
            <Center style={{ height: "50vh" }}>
                <Loader size="xl" />
            </Center>
        );
    }

    return (
        <AuthenticationProtectedPage>
            <MainLayout>
                <Paper m="sm" p="xl" style={{ height: "100vh" }}>
                    <Stack align="center">
                        <Stack align="center">
                            <Avatar radius="lg" size="xl" />
                            <Title order={3}>
                                {tutorProfile.tutorFirstName} {tutorProfile.tutorLastName}
                            </Title>
                        </Stack>
                        <Divider style={{ width: "100%" }} />
                        <TutorProfileInformation tutorProfile={tutorProfile} />
                        <Divider style={{ width: "100%" }} />
                        <TutorProfileAvailability tutorAvailabilities={tutorAvailabilities} tutorProfile={tutorProfile} />
                    </Stack>
                </Paper>
            </MainLayout>
        </AuthenticationProtectedPage>
    );
};

export default TutorCatalogPage;
