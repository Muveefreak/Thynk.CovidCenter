export interface SubmitCreateRequestPayload {

    userName: string;
    email: string;
    password: string;
    userRole: UserRole;
}

export enum UserRole{
    Administrator, Individual, LabAdministrator
}