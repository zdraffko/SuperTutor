import { Group, Text, ThemeIcon, UnstyledButton, useMantineTheme } from "@mantine/core";
import Link from "next/link";
import { useRouter } from "next/router";

interface NavBarLinkProps {
    icon: React.ReactNode;
    color: string;
    label: string;
    href: string;
}

const NavBarLink: React.FC<NavBarLinkProps> = ({ icon, color, label, href }) => {
    const router = useRouter();
    const isLinkActive = router.pathname === href;

    const theme = useMantineTheme();
    const linkHighlightColor = theme.colorScheme === "dark" ? theme.colors.dark[6] : theme.colors.gray[0];

    return (
        <Link href={href}>
            <UnstyledButton
                sx={theme => ({
                    display: "block",
                    padding: theme.spacing.xs,
                    borderRadius: theme.radius.sm,
                    color: theme.colorScheme === "dark" ? theme.colors.dark[0] : theme.black,
                    backgroundColor: isLinkActive ? linkHighlightColor : "",

                    "&:hover": {
                        backgroundColor: linkHighlightColor
                    }
                })}
            >
                <Group>
                    <ThemeIcon color={color} variant="light">
                        {icon}
                    </ThemeIcon>

                    <Text size="sm">{label}</Text>
                </Group>
            </UnstyledButton>
        </Link>
    );
};
export default NavBarLink;
