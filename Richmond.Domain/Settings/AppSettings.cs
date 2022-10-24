namespace Richmond.Domain.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string issuer { get; set; }
        public string ValidOn { get; set; }
    }
}
