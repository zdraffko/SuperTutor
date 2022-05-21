export interface User {
    id: string;
    email: string;
    type: UserType;
}

export enum UserType {
    Tutor = 1,
    Student = 2
}
