import { AppShell, Header, ScrollArea } from "@mantine/core";
import NavBar from "components/NavBar/NavBar";
import { ReactNode } from "react";

interface MainLayoutProps {
    children: ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => (
    <AppShell
        padding="0"
        header={
            <Header height={60} p="xs">
                <NavBar />
            </Header>
        }
    >
        <ScrollArea type="scroll" style={{ height: "100vh" }} p="xs">
            {children}
        </ScrollArea>
    </AppShell>
);

export default MainLayout;
