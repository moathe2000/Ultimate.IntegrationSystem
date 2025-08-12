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
                    Endpoint = table.Column<string>(type: "TEXT", nullable: false),
                    HttpMethod = table.Column<string>(type: "TEXT", nullable: false),
                    Headers = table.Column<string>(type: "TEXT", nullable: false),
                    Parametr = table.Column<string>(type: "TEXT", nullable: false),
                    BodyTemplate = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: false),
                    BodyFormat = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiRequestSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DBSetting",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SelectedSystem = table.Column<string>(type: "TEXT", nullable: false),
                    SchemaName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<string>(type: "TEXT", nullable: false),
                    Activity = table.Column<int>(type: "INTEGER", nullable: false),
                    Host = table.Column<string>(type: "TEXT", nullable: false),
                    Port = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBSetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "integration_api_settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: false),
                    ExpiresOn = table.Column<int>(type: "INTEGER", nullable: true),
                    RefreshTokenExpiration = table.Column<int>(type: "INTEGER", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: false),
                    ApiUrl = table.Column<string>(type: "TEXT", nullable: false),
                    LoginYear = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginUser = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginActivity = table.Column<int>(type: "INTEGER", nullable: false),
                    PublicIPWithPort = table.Column<string>(type: "TEXT", nullable: false),
                    DateFormat = table.Column<string>(type: "TEXT", nullable: false),
                    FracWhenSyncPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    ShowOrderDataWhenSync = table.Column<int>(type: "INTEGER", nullable: false),
                    SyncItemQuantityAndPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    SyncItemQuantityAndPricePeriodInMinuts = table.Column<int>(type: "INTEGER", nullable: false),
                    ShowActiveSyncButton = table.Column<int>(type: "INTEGER", nullable: false),
                    DoLog = table.Column<int>(type: "INTEGER", nullable: false),
                    DName = table.Column<string>(type: "TEXT", nullable: false),
                    AddOnlySubProductsFromOrderWithPriceDistribution = table.Column<int>(type: "INTEGER", nullable: false),
                    RedirectUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integration_api_settings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DBSetting_Year_Activity",
                table: "DBSetting",
                columns: new[] { "Year", "Activity" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiRequestSettings");

            migrationBuilder.DropTable(
                name: "DBSetting");

            migrationBuilder.DropTable(
                name: "integration_api_settings");
        }
    }
}
