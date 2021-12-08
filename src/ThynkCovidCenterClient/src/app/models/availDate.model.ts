export interface SubmitAvailDateRequestPayload {

    applicationUserId: string;
    locationId: string;
    dateAvailable: string | null;
    availableSlots: Number;
}