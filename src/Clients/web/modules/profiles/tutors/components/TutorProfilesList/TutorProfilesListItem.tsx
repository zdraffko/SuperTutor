import { Avatar, Badge, Button, Group, Paper, Spoiler, Stack, Text } from "@mantine/core";
import { tutoringGrades } from "types/tutoringGrades";
import { tutoringSubjects } from "types/tutoringSubjects";
import { TutorProfile } from "../../types/tutorProfile";

interface TutorProfilesListItemProps {
    tutorProfile: TutorProfile;
}

const TutorProfilesListItem: React.FC<TutorProfilesListItemProps> = ({ tutorProfile }) => {
    const tutoringSubjectName = tutoringSubjects.find(tutoringSubject => tutoringSubject.value === tutorProfile.tutoringSubject)?.name;
    const tutoringGradeNames = tutorProfile.tutoringGrades.map(tutoringGradeValue => tutoringGrades.find(tutoringGrade => tutoringGrade.value === tutoringGradeValue)?.name);

    return (
        <Paper m="xl" p="xl">
            <Group position="apart">
                <Group>
                    <Avatar radius="lg" size="xl" />
                    <Stack spacing="xs" style={{ wordBreak: "break-all", width: "700px" }}>
                        <Group>
                            <Text>Предмет:</Text>
                            <Text>{tutoringSubjectName}</Text>
                        </Group>
                        <Group>
                            <Text>Класове:</Text>
                            {tutoringGradeNames.map(tutoringGradeName => (
                                <Badge key={`${tutorProfile.id}-${tutoringGradeName}`}>{tutoringGradeName}</Badge>
                            ))}
                        </Group>
                        <Group>
                            <Text>Цена за час:</Text>
                            <Text>{tutorProfile.rateForOneHour} лв.</Text>
                        </Group>
                        <Group>
                            <Text>Описание:</Text>
                            <Spoiler maxHeight={50} showLabel="Show more" hideLabel="Hide">
                                <Text>{tutorProfile.about}</Text>
                            </Spoiler>
                        </Group>
                    </Stack>
                </Group>
                <Stack>
                    <Button color="blue">Редактирай</Button>
                    <Button color="red">Изтрий</Button>
                </Stack>
            </Group>
        </Paper>
    );
};

export default TutorProfilesListItem;
