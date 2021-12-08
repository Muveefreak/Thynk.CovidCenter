
export interface SubmitEnterResultRequestPayload {

    id: string;
    applicationUserId: string;
    bookingResult : BookingResultType;
}

export interface SubmitGetReportRequestPayload
{
    applicationUserId: string;
    locationId : string;
}

export enum BookingResultType
{
    Negative = 1,
    Positive
}