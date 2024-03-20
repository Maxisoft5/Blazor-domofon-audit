using ApplicationsAudit.Core.Services.Delegates;

namespace ApplicationsAudit.Core.Services.Events
{
    public class UploadProfileImageEvent
    {
        public event UploadProfileImageDelegate? OnUploadProfileImage;
        public void NotifyUpload(object sender, byte[] image)
        {
            if (OnUploadProfileImage != null)
            {
                OnUploadProfileImage(sender, image);
            }
        }
    }
}
