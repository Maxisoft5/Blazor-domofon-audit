using System.Globalization;

namespace Common.DTOs
{
    public class SmtpSettings
    {
        public static string? Server
        {
            get
               => "smtp.gmail.com";
        }
        public static int Port
        {
            get
                => 465;
        }
        public static string? Username
        {
            get
                => "domofon.noreply@gmail.com";
        }
        public static string? From
        {
            get => "domofon.noreply@gmail.com";
        }
        public static string? Password
        {
            get
                => "hkjr oxdr gdme zixu";
        }
        public static bool EnableSsl
        {
            get => true;
        }
        public static int Timeout
        {
            get
                => 0;
        }
    }
}
