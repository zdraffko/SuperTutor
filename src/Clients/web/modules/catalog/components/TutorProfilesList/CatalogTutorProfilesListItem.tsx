import { Avatar, Badge, Button, Grid, Group, Paper, Spoiler, Stack, Text } from "@mantine/core";
import { CatalogTutorProfile } from "modules/catalog/types/catalogTutorProfile";

interface CatalogTutorProfilesListItemProps {
    tutorProfile: CatalogTutorProfile;
}

const CatalogTutorProfilesListItem: React.FC<CatalogTutorProfilesListItemProps> = ({ tutorProfile }) => (
    <Paper withBorder shadow="sm" m="xl" p="xl">
        <Group position="apart">
            <Group>
                <Avatar radius="lg" size="xl" />
                <Stack spacing="xs" style={{ wordBreak: "break-all", width: "700px" }}>
                    <Text weight="bolder">
                        {tutorProfile.tutorFirstName} {tutorProfile.tutorLastName}
                    </Text>
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
            <Stack>
                <Button>Виж още</Button>
            </Stack>
        </Group>
    </Paper>
);

export default CatalogTutorProfilesListItem;
