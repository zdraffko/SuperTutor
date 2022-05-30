import { Center, Group, Loader, Paper, Stack, Text, Title } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import useGetTutorAvailability from "modules/catalog/hooks/useGetTutorAvailability";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useEffect } from "react";
import { X } from "tabler-icons-react";

const TutorCatalogPage: NextPage = () => {
    const router = useRouter();
    const { tutorId } = router.query;
    const { tutorAvailabilities, isGetTutorAvailabilityFailed, isGetTutorAvailabilityLoading, getTutorAvailabilityErrorMessage } = useGetTutorAvailability({ tutorId: `${tutorId}` });

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

    if (isGetTutorAvailabilityLoading || !tutorAvailabilities) {
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
                                    <Text key={`${tutorAvailability.date}-${hour}`}>{hour}</Text>
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
