import { ActionIcon, Box, Center, Group, Text, useMantineColorScheme } from "@mantine/core";
import { MoonStars, Sun } from "tabler-icons-react";

const SideBarHeader: React.FC = () => {
    const { colorScheme, toggleColorScheme } = useMantineColorScheme();

    return (
        <Box
            sx={theme => ({
                paddingLeft: theme.spacing.xs,
                paddingRight: theme.spacing.xs,
                paddingBottom: theme.spacing.lg,
                borderBottom: `1px solid ${theme.colorScheme === "dark" ? theme.colors.dark[4] : theme.colors.gray[2]}`
            })}
        >
            <Group position="apart">
                <Center>
                    <Text mt="xs" size="lg" weight="600">
                        Супер Учител
                    </Text>
                </Center>
                <ActionIcon variant="default" onClick={() => toggleColorScheme()} size={30}>
                    {colorScheme === "dark" ? <Sun color="yellow" size={16} /> : <MoonStars color="indigo" size={16} />}
                </ActionIcon>
            </Group>
        </Box>
    );
};

export default SideBarHeader;
