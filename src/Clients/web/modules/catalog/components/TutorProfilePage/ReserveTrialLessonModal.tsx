import { Button, Center, Group, Modal, Select, Text } from "@mantine/core";
import { CatalogTutorProfile } from "modules/catalog/types";
import { useState } from "react";

interface ReserveTrialLessonModalProps {
    isOpened: boolean;
    onClose: () => void;
    tutorProfile: CatalogTutorProfile;
    trialLessonDate: string;
    trialLessonHour: string;
}

export const ReserveTrialLessonModal: React.FC<ReserveTrialLessonModalProps> = ({ isOpened, onClose, tutorProfile, trialLessonDate, trialLessonHour }) => {
    const [trialLessonGrade, setTrialLessonGrade] = useState<string | null>("");

    return (
        <Modal size="xl" opened={isOpened} onClose={onClose} title="Запази пробен урок">
            <Group>
                <Text>Учител:</Text>
                <Text>
                    {tutorProfile.tutorFirstName} {tutorProfile.tutorLastName}
                </Text>
            </Group>
            <Group>
                <Text>Предмет:</Text>
                <Text>{tutorProfile.tutoringSubject}</Text>
            </Group>
            <Group>
                <Text>Дата:</Text>
                <Text>{trialLessonDate}</Text>
            </Group>
            <Group>
                <Text>Час:</Text>
                <Text>{trialLessonHour}</Text>
            </Group>
            <Select
                //disabled={isCreateTutorProfileLoading}
                value={trialLessonGrade}
                onChange={setTrialLessonGrade}
                required
                data={tutorProfile.tutoringGrades}
                label="Избери за кой клас ще бъде урокът"
                mb="lg"
                onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля избери клас")}
                onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
            />
            <Center>
                <Button type="submit" fullWidth size="sm" style={{ width: "50%" }}>
                    Запази час
                </Button>
            </Center>
        </Modal>
    );
};
