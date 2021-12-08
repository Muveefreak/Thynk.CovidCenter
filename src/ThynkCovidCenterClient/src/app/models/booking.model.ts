export interface SubmitCreateBookingRequestPayload {

    availableDateId: string;
    locationID: string;
    applicationUserId: string;
    testType : TestType;
}

export interface SubmitCancelBookingRequestPayload {

    availableDateId: string;
    locationID: string;
    applicationUserId: string;
}

export enum TestType
{
    PCR = 1,
    RapidTest
}

export enum BookingStatus
{
    Cancelled,
    Pending,
    Completed
}