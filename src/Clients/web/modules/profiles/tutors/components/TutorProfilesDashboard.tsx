import { Button, Center, Group, Loader, Stack, Text, Title } from "@mantine/core";
import { useState } from "react";
import useGetAllTutorProfilesForTutor from "../hooks/useGetAllTutorProfilesForTutor";
import AddTutorProfileSvg from "./AddTutorProfileSvg";
import CreateTutorProfileModal from "./CreateTutorProfileModal";
import TutorProfilesList from "./TutorProfilesList/TutorProfilesList";

export const TutorProfilesDashboard: React.FC = () => {
    const { tutorProfiles, isGetAllTutorProfilesForTutorLoading } = useGetAllTutorProfilesForTutor();
    const [isCreateTutorProfileModalOpened, setIsCreateTutorProfileModalOpened] = useState(false);

    if (isGetAllTutorProfilesForTutorLoading) {
        return (
            <Center style={{ height: "100vh" }}>
                <Loader size="xl" />
            </Center>
        );
    }

    return (
        <>
            <CreateTutorProfileModal isOpened={isCreateTutorProfileModalOpened} onClose={() => setIsCreateTutorProfileModalOpened(false)} />
            <Group position="apart">
                <Text>Брой Профили: {tutorProfiles ? tutorProfiles.length : 0}</Text>
                <Button m="xs" onClick={() => setIsCreateTutorProfileModalOpened(true)}>
                    Създай нов профил
                </Button>
            </Group>
            {tutorProfiles && tutorProfiles.length > 0 ? (
                <TutorProfilesList tutorProfiles={tutorProfiles} />
            ) : (
                <Stack align="center" justify="space-between" style={{ height: "85vh" }}>
                    <Title mb="lg">Изглежда все още нямаш създаден профил</Title>
                    <AddTutorProfileSvg />
                </Stack>
            )}
        </>
    );
};
