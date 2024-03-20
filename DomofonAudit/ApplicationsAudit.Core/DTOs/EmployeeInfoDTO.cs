namespace ApplicationsAudit.Core.DTOs
{
    public class EmployeeInfoDTO
    {
        public decimal MonthlySalary { get; set; }
        public decimal AdditionalYearlySalary { get; set; }
        public DateTime StartedWorkAt { get; set; }
        public bool IsFired { get; set; }
        public DateTime? FiredDate { get; set; }
    }
}
