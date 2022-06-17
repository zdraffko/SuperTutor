import { Group } from "@mantine/core";
import NavBarLink from "components/NavBar/Links/NavBarLink";
import { LayoutDashboard, Users } from "tabler-icons-react";

const adminSideBarLinks = [
    { icon: <LayoutDashboard size={16} />, color: "blue", label: "Начално табло", href: "/dashboard" },
    { icon: <Users size={16} />, color: "grape", label: "Профили", href: "/profiles" }
];

const NavBarLinks: React.FC = () => (
    <Group>
        {adminSideBarLinks.map(sideBarLink => (
            <NavBarLink key={sideBarLink.label} {...sideBarLink} />
        ))}
    </Group>
);

export default NavBarLinks;
