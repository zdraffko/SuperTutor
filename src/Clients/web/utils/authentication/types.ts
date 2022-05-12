export interface User {
    email: string;
    type: UserType;
}

export enum UserType {
    Tutor = 1,
    Student = 2
}
