namespace CarApp.Configs.Common;

public class AuthConfig
{
    public string? SecretKey { get; set; }
    public int? TokenExpiration { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
}