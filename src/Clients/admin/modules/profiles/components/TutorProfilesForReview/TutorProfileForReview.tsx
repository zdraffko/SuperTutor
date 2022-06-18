import { Avatar, Badge, Button, Group, Paper, Spoiler, Stack, Text } from "@mantine/core";
import { TutorProfile } from "modules/profiles/types";

interface TutorProfileForReviewProps {
    tutorProfile: TutorProfile;
}

const TutorProfileForReview: React.FC<TutorProfileForReviewProps> = ({ tutorProfile }) => (
    <Paper m="xl" p="xl">
        <Group position="apart">
            <Group>
                <Avatar radius="lg" size="xl" />
                <Stack spacing="xs" style={{ wordBreak: "break-all", width: "700px" }}>
                    <Group>
                        <Text>Предмет:</Text>
                        <Text>{tutorProfile.tutoringSubject}</Text>
                    </Group>
                    <Group>
                        <Text>Класове:</Text>
                        {tutorProfile.tutoringGrades.map(tutoringGradeName => (
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
                <Button>Одобри</Button>
            </Stack>
        </Group>
    </Paper>
);

export default TutorProfileForReview;
