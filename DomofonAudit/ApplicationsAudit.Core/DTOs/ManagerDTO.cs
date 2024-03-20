namespace ApplicationsAudit.Core.DTOs
{
    public class ManagerDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid RoleId { get; set; }
        public ManagerRoleDTO ManagerRole { get; set; }
        public int ManagerInfoId { get; set; }
        public ManagerInfoDTO ManagerInfo { get; set; }
        public int WorkingTimeId { get; set; }
        public WorkingTimeDTO WorkingTime { get; set; }
    }
}
