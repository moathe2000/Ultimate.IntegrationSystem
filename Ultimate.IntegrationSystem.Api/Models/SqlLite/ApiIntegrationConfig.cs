using System.ComponentModel.DataAnnotations;

namespace Ultimate.IntegrationSystem.Api.Models.SqlLite
{
    public class ApiIntegrationConfig
    {
        [Key]
        public int Id { get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int? ExpiresOn { get; set; }
        public int? RefreshTokenExpiration { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
        public int LoginYear { get; set; }
        public int LoginUser { get; set; }
        public int LoginActivity { get; set; }
        public string PublicIPWithPort { get; set; }
        public string DateFormat { get; set; }
        public int FracWhenSyncPrice { get; set; }
        public int ShowOrderDataWhenSync { get; set; }
        public int SyncItemQuantityAndPrice { get; set; }
        public int SyncItemQuantityAndPricePeriodInMinuts { get; set; }
        public int ShowActiveSyncButton { get; set; }
        public int DoLog { get; set; }
        public string DName { get; set; }
        public int AddOnlySubProductsFromOrderWithPriceDistribution { get; set; }
        public string RedirectUrl { get; set; }
    }
}
