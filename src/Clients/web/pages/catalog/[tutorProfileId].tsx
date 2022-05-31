import { Center, Group, Loader, Paper, Stack, Text, Title, useMantineTheme } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import useGetTutorAvailability from "modules/catalog/hooks/useGetTutorAvailability";
import useGetTutorProfileById from "modules/catalog/hooks/useGetTutorProfileById";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useEffect } from "react";
import { X } from "tabler-icons-react";

const TutorCatalogPage: NextPage = () => {
    const theme = useMantineTheme();
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

    if (isGetTutorProfileByIdLoading || isGetTutorAvailabilityLoading || !tutorAvailabilities) {
        return (
            <Center style={{ height: "50vh" }}>
                <Loader size="xl" />
            </Center>
        );
    }

    return (
        <AuthenticationProtectedPage>
            <MainLayout>
                <Paper m="sm" style={{ height: "100vh" }}>
                    <Title align="center">Свободни часове</Title>
                    <Group mt="xl" position="center" align="flex-start">
                        {tutorAvailabilities.map(tutorAvailability => (
                            <Stack key={tutorAvailability.date}>
                                <Title order={4}>{tutorAvailability.date}</Title>
                                {tutorAvailability.hours.map(hour => (
                                    <Text onClick={() => console.log("Clicked")} color={theme.primaryColor} key={`${tutorAvailability.date}-${hour}`}>
                                        {hour}
                                    </Text>
                                ))}
                            </Stack>
                        ))}
                    </Group>
                </Paper>
            </MainLayout>
        </AuthenticationProtectedPage>
    );
};

export default TutorCatalogPage;
