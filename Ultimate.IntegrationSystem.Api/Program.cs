using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

using System.Globalization;
using System.Text;
using Ultimate.IntegrationSystem.Api.AutoMapper;
using Ultimate.IntegrationSystem.Api.DBMangers;
using Ultimate.IntegrationSystem.Api.Integrations.Muqeem;
using Ultimate.IntegrationSystem.Api.Interface;
using Ultimate.IntegrationSystem.Api.Services;


var builder = WebApplication.CreateBuilder(args);

// Àﬁ«›… «› —«÷Ì…
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("ar-YE");
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("ar-YE");

// Serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .Enrich.FromLogContext());

// --- MVC/Controllers („⁄ œ⁄„ XML) ---
builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
})
.AddXmlSerializerFormatters(); // »œ·« „‰ AddMvc + ≈÷«›… «·›Ê—„« — ÌœÊÌ«

// Swagger + JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiSync", Version = "v2" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT in Authorization header. Example: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme
            { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
          Array.Empty<string>() }
    });
});

// --- EF Core: SQLite ---
var dbPath = Path.Combine(AppContext.BaseDirectory, "dbSqlit.db");
builder.Services.AddDbContext<IntegrationApiDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// --- DI ·Œœ„«  «·œÊ„Ì‰ ---
// „·«ÕŸ…:  √ﬂœ √‰ «·Ê«ÃÂ… „’Õ¯Õ… ≈·Ï IDataAccessService (Ê·Ì” IDataAccessService)
builder.Services.AddScoped<IDataAccessService, OracleDataAccessService>();
builder.Services.AddSingleton<IDbModelMappingService, DbModelMappingService>();
//builder.Services.AddScoped<Platfo>();

builder.Services.AddScoped<MuqeemService>();
builder.Services.AddScoped<ISyncToMuqeemService, SyncToMuqeemService>();
//builder.Services.AddScoped<ISyncToBMTBOXServiceTest, SyncToBMTBOXServiceTest>();
// ≈‰ √—œ   ﬂ«„· Muqeem ·«Õﬁ« (Ìﬁ—√ «·≈⁄œ«œ«  „‰ «·ÃœÊ· ›ﬁÿ) ”Ã¯· «·Œœ„«  Â‰«:
// builder.Services.AddMemoryCache();
// builder.Services.AddSingleton<IMuqeemRuntimeConfigAccessor, MuqeemRuntimeConfigAccessor>();
// builder.Services.AddSingleton<IMuqeemTokenProvider, MuqeemTokenProvider>();
// builder.Services.AddTransient<MuqeemHeadersHandler>();
// builder.Services.AddHttpClient<MuqeemClient>().AddHttpMessageHandler<MuqeemHeadersHandler>();
builder.Services.AddAutoMapper(typeof(EmployeeProfile));
// --- JWT Auth ---
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents { OnTokenValidated = OnTokenValidated };
        var issuer = builder.Configuration["Jwt:Issuer"];
        var audience = builder.Configuration["Jwt:Audience"];
        var key = builder.Configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience) || string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("JWT settings (Issuer/Audience/Key) are missing.");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Swagger œ«∆„« («Œ Ì«—Ì: ﬁ’—Â ⁄·Ï «· ÿÊÌ—)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Validating the token
static Task OnTokenValidated(TokenValidatedContext context)
{
    context.Success();
    return Task.CompletedTask;
}
