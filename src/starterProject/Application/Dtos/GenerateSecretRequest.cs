namespace Application.Dtos;
public class GenerateSecretRequest
{
    /// <summary>
    /// Apple Developer --> Team ID --> ex. => 9JA89QQLNQ
    /// </summary>
    public string TeamId { get; set; }
    /// <summary>
    /// Apple Developer --> Service ID or App ID. --> ex => com.example.myapp
    /// </summary>
    public string ClientId { get; set; }
    /// <summary>
    /// Apple Developer --> Key ID --> ex => 4XQ1234567
    /// </summary>
    public string KeyId { get; set; }
    /// <summary>
    /// Apple Developer --> PrivateKey --> ex. => MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQD...
    /// </summary>
    public string PrivateKey { get; set; }
}
