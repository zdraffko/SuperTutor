import { TutorProfile } from "../../types/tutorProfile";
import TutorProfilesListItem from "./TutorProfilesListItem";

interface TutorProfilesListProps {
    tutorProfiles: TutorProfile[];
}

const TutorProfilesList: React.FC<TutorProfilesListProps> = ({ tutorProfiles }) => (
    <>
        {tutorProfiles.map(tutorProfile => (
            <TutorProfilesListItem key={tutorProfile.id} tutorProfile={tutorProfile} />
        ))}
    </>
);

export default TutorProfilesList;
