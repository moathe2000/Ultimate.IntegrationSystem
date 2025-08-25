namespace Ultimate.IntegrationSystem.Api.Integrations
{
    public interface IBasePlatformService
    {
        string PlatformKey { get; }
        // Task<HttpClient> GetAuthorizedHttpClientAsync();
      //  Task<(HttpClient client, string token, string baseUrl)> GetAuthorizedHttpClientAsync(string platformKey);
        Task<(HttpClient client, string token, string baseUrl, string AppId, string AppKey)> GetAuthorizedHttpClientAsync(string platformKey);
    }
}
