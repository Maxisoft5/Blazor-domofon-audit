using ApplicationsAudit.Core.Interfaces;
using ApplicationsAudit.Core.Services.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomofonAudit.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IManagerService _managerService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SignInEvent _signInEvent;

        [BindProperty]
        public string EmailAddress { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string? ErrorMessage { get; set; }

        public LoginModel(IManagerService managerService, IHttpContextAccessor contextAccessor,
            SignInEvent signInEvent)
        {
            _managerService = managerService;
            _contextAccessor = contextAccessor;
            _signInEvent = signInEvent;
        }

        public async Task OnGet(bool? logout = null)
        {
            if (logout.HasValue && logout.Value)
            {
                var result = await _managerService.SignOut();
            }
        }

        public async Task OnPost()
        {
            ErrorMessage = "";
            if (string.IsNullOrEmpty(EmailAddress))
            {
                ErrorMessage = "¬ведите почту";
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "¬ведите пароль";
                return;
            }

            var signIn = await _managerService.SignIn(new ApplicationsAudit.Core.DTOs.ManagerDTO()
            {
                ManagerInfo = new ApplicationsAudit.Core.DTOs.ManagerInfoDTO()
                {
                    Password = Password,
                    ContactInfo = new ApplicationsAudit.Core.DTOs.ContactInfoDTO()
                    {
                        Email = EmailAddress
                    }
                }
            });

            if (!signIn.Succeeded)
            {
                ErrorMessage = signIn.Message;
                return;
            }
            _signInEvent.NotifySignIn(this, signIn.Manager);
            _contextAccessor?.HttpContext?.Response.Redirect("/application-board");
        }
    }
}
