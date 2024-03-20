using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ApplicationsAudit.Core.Helpers
{
    public static class EmailHelper
    {
        private static readonly Regex _emailRegex
           = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);

        public static bool IsEmailValid(string email)
        {
            if (email == null)
            {
                return false;
            }

            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith(".", StringComparison.InvariantCulture))
            {
                return false;
            }
            if (!_emailRegex.IsMatch(trimmedEmail))
            {
                return false;
            }
            // to do: static class to log error
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                var address = new MailAddress(email);
                return address.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}
