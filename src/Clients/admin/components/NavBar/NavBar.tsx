import { Group } from "@mantine/core";
import NavBarLinks from "components/NavBar/Links/NavBarLinks";
import NavBarFooter from "components/NavBar/NavBarFooter";
import NavBarHeader from "components/NavBar/NavBarHeader";

const NavBar: React.FC = () => (
    <Group sx={{ height: "100%" }} position="apart">
        <NavBarHeader />
        <NavBarLinks />
        <NavBarFooter />
    </Group>
);

export default NavBar;
