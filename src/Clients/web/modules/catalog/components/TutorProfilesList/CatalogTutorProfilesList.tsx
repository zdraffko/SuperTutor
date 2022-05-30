import { Stack, Title } from "@mantine/core";
import { CatalogTutorProfile } from "modules/catalog/types";
import CatalogNoResultsForFilterSvg from "./CatalogNoResultsForFilterSvg";
import TutorProfilesListItem from "./CatalogTutorProfilesListItem";

interface CatalogTutorProfilesListProps {
    tutorProfiles: CatalogTutorProfile[];
}

const CatalogTutorProfilesList: React.FC<CatalogTutorProfilesListProps> = ({ tutorProfiles }) => (
    <>
        {tutorProfiles.length === 0 ? (
            <Stack align="center" style={{ height: "70vh" }}>
                <Title order={3}>Изглежда, че няма профили, които да отговарят на зададеният филтър</Title>
                <CatalogNoResultsForFilterSvg />
            </Stack>
        ) : (
            tutorProfiles.map(tutorProfile => <TutorProfilesListItem key={tutorProfile.id} tutorProfile={tutorProfile} />)
        )}
    </>
);

export default CatalogTutorProfilesList;
