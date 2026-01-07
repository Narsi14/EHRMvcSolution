namespace EHRMvcDemo.Models
{
    internal class DoctorSummaryViewModel
    {
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public int TotalAppointments { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
    }
}