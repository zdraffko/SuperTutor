export interface User {
    id: string;
    email: string;
    type: UserType;
    firstName: string;
    lastName: string;
}

export enum UserType {
    Tutor = 1,
    Student = 2
}
