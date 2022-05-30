export interface CatalogTutorProfile {
    id: string;
    tutorId: string;
    tutorFirstName: string;
    tutorLastName: string;
    tutoringSubject: string;
    tutoringGrades: string[];
    about: string;
    rateForOneHour: number;
}

export interface TutorAvailability {
    date: string;
    hours: string[];
}
