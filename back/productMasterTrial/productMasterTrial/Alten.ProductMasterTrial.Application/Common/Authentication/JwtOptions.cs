namespace Alten.ProductMaster.Application.Common.Authentication
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int ExpiryMinutes { get; set; }
    }

    public class AdminOptions
    {
        public List<string> Emails { get; set; } = new();
    }
}