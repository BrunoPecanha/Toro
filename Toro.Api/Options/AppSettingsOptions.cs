namespace Toro.Api.Options {
    public class AppSettingsOptions {
        public string Secret { get; set; }
        public string ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
    }
}
