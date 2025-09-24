namespace HDA.Business.Identity
{
    public class IdentitySettings
    {
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public double ExpirationInMinutes { get; set; }
    }
}
