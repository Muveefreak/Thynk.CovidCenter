namespace Thynk.CovidCenter.Core.Constants
{
    public static class ResponseMessages
    {
        public const string GenericException = "Something went wrong";
        public const string Success = "Request completed successsfully.";
        public const string NoUserRecordFound = "No User Record Found";
        public const string NoLocationRecordFound = "No Location Record Found";
        public const string NoAvailableDatesRecordFound = "No Available Dates Found";
        public const string NoBookingRecordFound = "No Booking Record Found";
        public const string InvalidCredentials = "Invalid Credentials";
        public const string OnlyAdministrator = "Only Administrators can perform this action";
        public const string OnlyIndividual = "Only Individuals can perform this action";
        public const string OnlyLabAdministrator = "Only Lab Administrators can perform this action";
        public const string OnlyAdminLabAdministrator = "Only Administrators or Lab Administrators can perform this action";
        public const string NoDuplicateDatesPerLocation = "No Duplicate Dates Per location Allowed";
        public const string NoDuplicateBokingsLocation = "No Duplicate Bookings Per location Allowed";
        public const string NoAvailableSlots = "No Available Slots";
    }
}
