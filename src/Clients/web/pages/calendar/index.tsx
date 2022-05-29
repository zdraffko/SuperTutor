import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { Calendar, CalendarToolbar } from "modules/calendar";
import { CalendarRedactionMode } from "modules/calendar/types";
import { NextPage } from "next";
import { useState } from "react";
import { DayJs, dayJsRange } from "utils/dates";

const CalendarPage: NextPage = () => {
    const selectedDateRange = dayJsRange(DayJs(), DayJs().add(5, "days"));
    const [selectedRedactionMode, setSelectedRedactionMode] = useState<CalendarRedactionMode>("AddAvailability");

    return (
        <AuthenticationProtectedPage>
            <MainLayout>
                <CalendarToolbar selectedRedactionMode={selectedRedactionMode} setSelectedRedactionMode={setSelectedRedactionMode} />
                <Calendar selectedDateRange={selectedDateRange} selectedRedactionMode={selectedRedactionMode} />
            </MainLayout>
        </AuthenticationProtectedPage>
    );
};

export default CalendarPage;
