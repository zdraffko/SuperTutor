import { Box, Navbar, ScrollArea } from "@mantine/core";
import SideBarLinks from "components/SideBar/Links/SideBarLinks";
import SideBarFooter from "components/SideBar/SideBarFooter";
import SideBarHeader from "components/SideBar/SideBarHeader";

const SideBar: React.FC = () => (
    <Navbar height={"100vh"} width={{ base: 300 }}>
        <Navbar.Section mt="xs">
            <SideBarHeader />
        </Navbar.Section>
        <Navbar.Section grow component={ScrollArea} mx="-xs" px="xs">
            <Box py="md">
                <SideBarLinks />
            </Box>
        </Navbar.Section>
        <Navbar.Section>
            <SideBarFooter />
        </Navbar.Section>
    </Navbar>
);

export default SideBar;
