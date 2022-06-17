import { Avatar, Box, Group, Text, UnstyledButton, useMantineTheme } from "@mantine/core";
import { useAuth } from "utils/authentication/reactQueryAuth";

const NavBarFooter: React.FC = () => {
    const theme = useMantineTheme();
    const { user, logout } = useAuth();

    return (
        <>
            <UnstyledButton
                onClick={() => logout()}
                sx={{
                    display: "block",
                    borderRadius: theme.radius.sm,
                    color: theme.colorScheme === "dark" ? theme.colors.dark[0] : theme.black,

                    "&:hover": {
                        backgroundColor: theme.colorScheme === "dark" ? theme.colors.dark[6] : theme.colors.gray[0]
                    }
                }}
            >
                <Group>
                    <Avatar
                        // TODO Add profile pic src=""
                        radius="xl"
                    />
                    <Box sx={{ flex: 1 }}>
                        <Text size="sm" weight={500}>
                            {`${user?.firstName} ${user?.lastName}`}
                        </Text>
                        <Text color="dimmed" size="xs">
                            {user?.email}
                        </Text>
                    </Box>

                    {/*theme.dir === "ltr" ? <ChevronRight size={18} /> : <ChevronLeft size={18} />*/}
                </Group>
            </UnstyledButton>
        </>
    );
};

export default NavBarFooter;
