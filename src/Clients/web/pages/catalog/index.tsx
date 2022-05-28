import { Center, Loader } from "@mantine/core";
import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import CatalogFilter from "modules/catalog/components/Filter/CatalogFilter";
import CatalogTutorProfilesList from "modules/catalog/components/TutorProfilesList/CatalogTutorProfilesList";
import useGetTutorProfilesByFilter from "modules/catalog/hooks/useGetTutorProfilesByFilter";
import { CatalogTutorProfile } from "modules/catalog/types/catalogTutorProfile";
import { NextPage } from "next";
import { useState } from "react";

const CatalogPage: NextPage = () => {
    const [tutorProfiles, setTutorProfiles] = useState<CatalogTutorProfile[]>([]);
    const { isGetTutorProfilesByFilterLoading } = useGetTutorProfilesByFilter({ tutoringSubject: null, tutoringGrades: null, minRateForOneHour: null, maxRateForOneHour: null });

    return (
        <AuthenticationProtectedPage>
            <MainLayout>
                <CatalogFilter setTutorProfiles={setTutorProfiles} />
                {isGetTutorProfilesByFilterLoading ? (
                    <Center style={{ height: "70vh" }}>
                        <Loader size="xl" />
                    </Center>
                ) : (
                    <CatalogTutorProfilesList tutorProfiles={tutorProfiles} />
                )}
            </MainLayout>
        </AuthenticationProtectedPage>
    );
};

export default CatalogPage;
