namespace EPMS_API.Models
{
    public class AuthorizationSettings
    {
        public string AppId { get; set; } = null!;

        public string AppSecret { get; set; } = null!;

        public string TokenExpiredSeconds { get; set; } = null!;

        public string Issuer { get; set; } = null!;

    }
}
