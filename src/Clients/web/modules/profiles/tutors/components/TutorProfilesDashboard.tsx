import { Button, Group, Stack, Title } from "@mantine/core";
import { useState } from "react";
import AddTutorProfileSvg from "./AddTutorProfileSvg";
import CreateTutorProfileModal from "./CreateTutorProfileModal";
import TutorProfilesList from "./TutorProfilesList";

export const TutorProfilesDashboard: React.FC = () => {
    const hasProfiles = false;
    const [isCreateTutorProfileModalOpened, setIsCreateTutorProfileModalOpened] = useState(false);

    return (
        <>
            <CreateTutorProfileModal isOpened={isCreateTutorProfileModalOpened} onClose={() => setIsCreateTutorProfileModalOpened(false)} />
            <Group position="right">
                <Button m="xs" onClick={() => setIsCreateTutorProfileModalOpened(true)}>
                    Създай нов профил
                </Button>
            </Group>
            {hasProfiles ? (
                <TutorProfilesList />
            ) : (
                <Stack align="center" justify="space-between" style={{ height: "85vh" }}>
                    <Title mb="lg">Изглежда все още нямаш създаден профил</Title>
                    <AddTutorProfileSvg />
                </Stack>
            )}
        </>
    );
};
