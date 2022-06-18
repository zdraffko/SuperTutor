import { Center, Loader, Title } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import useGetAllTutorProfilesForReview from "modules/profiles/hooks/useGetAllTutorProfilesForReview";
import { useEffect } from "react";
import { X } from "tabler-icons-react";
import TutorProfileForReview from "./TutorProfileForReview";

export const TutorProfilesForReview: React.FC = () => {
    const { tutorProfiles, isGetAllTutorProfilesForReviewFailed, isGetAllTutorProfilesForReviewLoading, getAllTutorProfilesForReviewErrorMessage } = useGetAllTutorProfilesForReview();

    useEffect(() => {
        if (isGetAllTutorProfilesForReviewFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при зареждането на учителските профили за ревю",
                message: getAllTutorProfilesForReviewErrorMessage,
                color: "red",
                icon: <X />
            });
        }
    }, [getAllTutorProfilesForReviewErrorMessage, isGetAllTutorProfilesForReviewFailed]);

    if (isGetAllTutorProfilesForReviewLoading || !tutorProfiles) {
        return (
            <Center style={{ height: "100vh" }}>
                <Loader size="xl" />
            </Center>
        );
    }

    return (
        <>
            <Title align="center">Учителски профили за одобрение</Title>
            {tutorProfiles.length === 0 ? (
                <Title align="center" order={3}>
                    Няма профили за одобрение в момента
                </Title>
            ) : (
                tutorProfiles.map(tutorProfile => <TutorProfileForReview key={tutorProfile.id} tutorProfile={tutorProfile} />)
            )}
        </>
    );
};
