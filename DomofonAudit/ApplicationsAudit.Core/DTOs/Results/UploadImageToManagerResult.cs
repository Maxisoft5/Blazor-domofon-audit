namespace ApplicationsAudit.Core.DTOs.Results
{
    public class UploadImageToManagerResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public ContactInfoDTO? ContactInfo { get; set; }
    }
}
