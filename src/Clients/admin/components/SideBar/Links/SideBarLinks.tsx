import SideBarLink from "components/SideBar/Links/SideBarLink";
import { LayoutDashboard } from "tabler-icons-react";

const adminSideBarLinks = [{ icon: <LayoutDashboard size={16} />, color: "blue", label: "Начално табло", href: "/dashboard" }];

const SideBarLinks: React.FC = () => (
    <div>
        {adminSideBarLinks.map(sideBarLink => (
            <SideBarLink key={sideBarLink.label} {...sideBarLink} />
        ))}
    </div>
);

export default SideBarLinks;
