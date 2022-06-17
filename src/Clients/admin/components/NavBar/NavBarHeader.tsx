import { ActionIcon, Center, Group, Text, useMantineColorScheme } from "@mantine/core";
import { MoonStars, Sun } from "tabler-icons-react";

const NavBarHeader: React.FC = () => {
    const { colorScheme, toggleColorScheme } = useMantineColorScheme();

    return (
        <Group>
            <Center>
                <Text size="lg" weight="600">
                    Супер Учител
                </Text>
            </Center>
            <ActionIcon variant="default" onClick={() => toggleColorScheme()} size={30}>
                {colorScheme === "dark" ? <Sun color="yellow" size={16} /> : <MoonStars color="indigo" size={16} />}
            </ActionIcon>
        </Group>
    );
};

export default NavBarHeader;
