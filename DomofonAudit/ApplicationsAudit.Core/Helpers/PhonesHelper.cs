using System.Text.RegularExpressions;

namespace ApplicationsAudit.Core.Helpers
{
    public static class PhonesHelper
    {
        private static readonly Regex _emailRegex
         = new Regex(@"[0-9]{10}", RegexOptions.Compiled);

        public static bool IsValid(string phone)
        {
            if(string.IsNullOrWhiteSpace(phone)) return false;
            if(string.IsNullOrEmpty(phone)) return false;

            if (!_emailRegex.IsMatch(phone.Trim()))
            {
                return false;
            }

            return true;
        }
    }
}
