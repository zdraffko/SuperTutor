import { AppShell } from "@mantine/core";
import SideBar from "components/SideBar/SideBar";
import { ReactNode } from "react";

interface MainLayoutProps {
    children: ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => <AppShell navbar={<SideBar />}>{children}</AppShell>;

export default MainLayout;
