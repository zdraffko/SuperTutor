import { AppShell, ScrollArea } from "@mantine/core";
import SideBar from "components/SideBar/SideBar";
import { ReactNode } from "react";

interface MainLayoutProps {
    children: ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => (
    <AppShell padding="0" navbar={<SideBar />}>
        <ScrollArea type="scroll" style={{ height: "100vh" }}>
            {children}
        </ScrollArea>
    </AppShell>
);

export default MainLayout;
