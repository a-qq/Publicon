using Publicon.Core.Entities.Concrete;

namespace Publicon.Core.Extensions
{
    public static class UserExtensions
    {
        public static string GenerateActivationLink(this User user, string publiconPath)
        {
            return $"{publiconPath}?securityCode={user.SecurityCode}";
        }

        public static string GenerateResetPasswordLink(this User user, string publiconPath)
        {
            return $"{publiconPath}?securityCode={user.PasswordSecurityCode}";
        }

    }
}
