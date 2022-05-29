import { Container, Divider, Grid, Paper } from "@mantine/core";

export const CalendarBody: React.FC = () => (
    <>
        <Divider size="sm" />
        <Grid columns={8} gutter={0} grow>
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
            <Grid.Col span={1}>
                <Grid columns={1} gutter={0}>
                    {Array.from(Array(24).keys()).map(index => (
                        <Grid.Col key={index} span={1}>
                            <Paper key={index} style={{ height: "101px" }}>
                                <Container style={{ height: "50px" }}></Container>
                                <Divider variant="dashed" size="sm" />
                                <Container style={{ height: "50px" }}></Container>
                            </Paper>
                            <Divider size="sm" />
                        </Grid.Col>
                    ))}
                </Grid>
            </Grid.Col>
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
            <Grid.Col span={1}>
                <Grid columns={1} gutter={0}>
                    {Array.from(Array(24).keys()).map(index => (
                        <Grid.Col key={index} span={1}>
                            <Paper key={index} style={{ height: "101px" }}>
                                <Container style={{ height: "50px" }}></Container>
                                <Divider variant="dashed" size="sm" />
                                <Container style={{ height: "50px" }}></Container>
                            </Paper>
                            <Divider size="sm" />
                        </Grid.Col>
                    ))}
                </Grid>
            </Grid.Col>
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
            <Grid.Col span={1}>
                <Grid columns={1} gutter={0}>
                    {Array.from(Array(24).keys()).map(index => (
                        <Grid.Col key={index} span={1}>
                            <Paper key={index} style={{ height: "101px" }}>
                                <Container style={{ height: "50px" }}></Container>
                                <Divider variant="dashed" size="sm" />
                                <Container style={{ height: "50px" }}></Container>
                            </Paper>
                            <Divider size="sm" />
                        </Grid.Col>
                    ))}
                </Grid>
            </Grid.Col>
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
            <Grid.Col span={1}>
                <Grid columns={1} gutter={0}>
                    {Array.from(Array(24).keys()).map(index => (
                        <Grid.Col key={index} span={1}>
                            <Paper key={index} style={{ height: "101px" }}>
                                <Container style={{ height: "50px" }}></Container>
                                <Divider variant="dashed" size="sm" />
                                <Container style={{ height: "50px" }}></Container>
                            </Paper>
                            <Divider size="sm" />
                        </Grid.Col>
                    ))}
                </Grid>
            </Grid.Col>
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
            <Grid.Col span={1}>
                <Grid columns={1} gutter={0}>
                    {Array.from(Array(24).keys()).map(index => (
                        <Grid.Col key={index} span={1}>
                            <Paper key={index} style={{ height: "101px" }}>
                                <Container style={{ height: "50px" }}></Container>
                                <Divider variant="dashed" size="sm" />
                                <Container style={{ height: "50px" }}></Container>
                            </Paper>
                            <Divider size="sm" />
                        </Grid.Col>
                    ))}
                </Grid>
            </Grid.Col>
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
            <Grid.Col span={1}>
                <Grid columns={1} gutter={0}>
                    {Array.from(Array(24).keys()).map(index => (
                        <Grid.Col key={index} span={1}>
                            <Paper key={index} style={{ height: "101px" }}>
                                <Container style={{ height: "50px" }}></Container>
                                <Divider variant="dashed" size="sm" />
                                <Container style={{ height: "50px" }}></Container>
                            </Paper>
                            <Divider size="sm" />
                        </Grid.Col>
                    ))}
                </Grid>
            </Grid.Col>
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
            <Grid.Col span={1}>
                <Grid columns={1} gutter={0}>
                    {Array.from(Array(24).keys()).map(index => (
                        <Grid.Col key={index} span={1}>
                            <Paper key={index} style={{ height: "101px" }}>
                                <Container style={{ height: "50px" }}></Container>
                                <Divider variant="dashed" size="sm" />
                                <Container style={{ height: "50px" }}></Container>
                            </Paper>
                            <Divider size="sm" />
                        </Grid.Col>
                    ))}
                </Grid>
            </Grid.Col>
            <Divider orientation="vertical" style={{ height: "346vh" }} size="sm" />
        </Grid>
    </>
);
