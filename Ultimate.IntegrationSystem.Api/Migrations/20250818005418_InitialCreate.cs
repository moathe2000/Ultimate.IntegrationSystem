using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultimate.IntegrationSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiRequestSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Endpoint = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    HttpMethod = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Headers = table.Column<string>(type: "TEXT", nullable: false),
                    Parametr = table.Column<string>(type: "TEXT", nullable: false),
                    BodyTemplate = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    BodyFormat = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    CreatedAt = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiRequestSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionSetting",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SelectedSystem = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    SchemaName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Year = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Activity = table.Column<int>(type: "INTEGER", nullable: false),
                    Host = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Port = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionSetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "integration_api_settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlatformKey = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: false),
                    ExpiresOn = table.Column<int>(type: "INTEGER", nullable: true),
                    RefreshTokenExpiration = table.Column<int>(type: "INTEGER", nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    AppId = table.Column<string>(type: "TEXT", nullable: false),
                    ApiUrl = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    LoginYear = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginUser = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginActivity = table.Column<int>(type: "INTEGER", nullable: false),
                    PublicIPWithPort = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    DateFormat = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    FracWhenSyncPrice = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    ShowOrderDataWhenSync = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    SyncItemQuantityAndPrice = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    SyncItemQuantityAndPricePeriodInMinuts = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    ShowActiveSyncButton = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    DoLog = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    DName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    AddOnlySubProductsFromOrderWithPriceDistribution = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    RedirectUrl = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integration_api_settings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestSettings_HttpMethod_Endpoint",
                table: "ApiRequestSettings",
                columns: new[] { "HttpMethod", "Endpoint" });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionSetting_Year_Activity",
                table: "ConnectionSetting",
                columns: new[] { "Year", "Activity" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_integration_api_settings_ApiUrl",
                table: "integration_api_settings",
                column: "ApiUrl");

            migrationBuilder.CreateIndex(
                name: "IX_integration_api_settings_Email",
                table: "integration_api_settings",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiRequestSettings");

            migrationBuilder.DropTable(
                name: "ConnectionSetting");

            migrationBuilder.DropTable(
                name: "integration_api_settings");
        }
    }
}
