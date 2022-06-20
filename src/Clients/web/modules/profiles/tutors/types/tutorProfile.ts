export interface TutorProfile {
    id: string;
    tutoringSubject: number;
    tutoringGrades: number[];
    about: string;
    rateForOneHour: number;
    status: "Inactive" | "Active" | "ForReview" | "ForRedaction";
    redactionComment: string | null;
}
