import SideBarLink from "components/SideBar/Links/SideBarLink";
import { CalendarStats, CurrencyDollar, DeviceDesktop, LayoutDashboard, Search, User, Users } from "tabler-icons-react";
import { useAuth } from "utils/authentication/reactQueryAuth";
import { UserType } from "utils/authentication/types";

const tutorSideBarLinks = [
    { icon: <LayoutDashboard size={16} />, color: "blue", label: "Начално табло", href: "/dashboard" },
    { icon: <DeviceDesktop size={16} />, color: "orange", label: "Класна Стая", href: "/classroom" },
    { icon: <CalendarStats size={16} />, color: "violet", label: "Календар", href: "/" },
    { icon: <Users size={16} />, color: "grape", label: "Профили", href: "/profiles" },
    { icon: <CurrencyDollar size={16} />, color: "teal", label: "Плащания", href: "/payments" }
];

const studentSideBarLinks = [
    { icon: <LayoutDashboard size={16} />, color: "blue", label: "Начално табло", href: "/dashboard" },
    { icon: <DeviceDesktop size={16} />, color: "orange", label: "Класна Стая", href: "/classroom" },
    { icon: <Search size={16} />, color: "teal", label: "Каталог", href: "/" },
    { icon: <CalendarStats size={16} />, color: "violet", label: "Календар", href: "/" },
    { icon: <User size={16} />, color: "grape", label: "Профил", href: "/profiles" }
];

const SideBarLinks: React.FC = () => {
    const { user } = useAuth();
    const sideBarLinks = user?.type === UserType.Tutor ? tutorSideBarLinks : studentSideBarLinks;

    return (
        <div>
            {sideBarLinks.map(sideBarLink => (
                <SideBarLink key={sideBarLink.label} {...sideBarLink} />
            ))}
        </div>
    );
};

export default SideBarLinks;
