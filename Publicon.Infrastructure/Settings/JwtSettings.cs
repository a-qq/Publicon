namespace Publicon.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int ExpiryTime { get; set; }
    }
}
