namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings
{
    public interface ISecretCipher
    {
        string Encrypt(string plain);
        string Decrypt(string cipherBase64);
    }
}
