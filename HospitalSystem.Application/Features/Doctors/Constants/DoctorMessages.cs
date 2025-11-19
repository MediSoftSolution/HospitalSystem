namespace HospitalSystem.Application.Features.Doctors.Constants
{
    public static class DoctorMessages
    {
        public const string UserAlreadyDoctor = "This user is already registered as a doctor.";
        public const string WorkingTimesEmpty = "A doctor must have at least one working time.";
        public const string InvalidConsultingFee = "Consultation fee cannot be zero or negative.";
        public const string SpecialtyNotFound = "The specified specialty does not exist.";
        public const string OfficeNotFound = "The specified office does not exist.";
        public const string InvalidWorkingHours = "Invalid working hours specified for the selected day.";
    }
}