import { Badge, Grid, Group, Spoiler, Stack, Text, Title } from "@mantine/core";
import { CatalogTutorProfile } from "modules/catalog/types";

interface TutorProfileInformationProps {
    tutorProfile: CatalogTutorProfile;
}

export const TutorProfileInformation: React.FC<TutorProfileInformationProps> = ({ tutorProfile }) => (
    <>
        <Title order={3} align="center">
            За Учителят
        </Title>
        <Group>
            <Stack spacing="xs" style={{ wordBreak: "break-all", width: "700px" }}>
                <Grid>
                    <Grid.Col span={6}>
                        <Group>
                            <Text>Предмет:</Text>
                            <Text>{tutorProfile.tutoringSubject}</Text>
                        </Group>
                    </Grid.Col>
                    <Grid.Col span={6}>
                        <Group>
                            <Text>Класове:</Text>
                            {tutorProfile.tutoringGrades.map(tutoringGradeName => (
                                <Badge key={`${tutorProfile.id}-${tutoringGradeName}`}>{tutoringGradeName}</Badge>
                            ))}
                        </Group>
                    </Grid.Col>
                    <Grid.Col span={6}>
                        <Group>
                            <Text>Цена за час:</Text>
                            <Text>{tutorProfile.rateForOneHour} лв.</Text>
                        </Group>
                    </Grid.Col>
                    <Grid.Col span={6}>
                        <Group>
                            <Text>Описание:</Text>
                            <Spoiler maxHeight={50} showLabel="Show more" hideLabel="Hide">
                                <Text>{tutorProfile.about}</Text>
                            </Spoiler>
                        </Group>
                    </Grid.Col>
                </Grid>
            </Stack>
        </Group>
    </>
);
