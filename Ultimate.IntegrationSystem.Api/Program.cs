


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Text;
using System.Globalization;
using System.Text;
using Ultimate.IntegrationSystem.Api.DBMangers;
using Ultimate.IntegrationSystem.Api.Interface;
using Ultimate.IntegrationSystem.Api.Services;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("ar-YE");

//Configure Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
//builder.Services.AddScoped(sp => new HttpClient() { BaseAddress = new Uri("https://aka.ms/aspnetcore/swashbuckle", UriKind.Absolute), Timeout = TimeSpan.FromDays(1) });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiSync", Version = "v2" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"


    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }
    });

});

//Add Automapper profiles
builder.Services.AddDbContext<IntegrationApiDbContext>(options =>
{
    var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "dbSqlit.db");
    options.UseSqlite($"Data Source={dbPath}");
});

//////var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "dbSqlit.db");

//////var columnsToEnsure = new Dictionary<string, string>
//////{
//////    { "DName", "TEXT" }
//////};

//////DbSchemaUpdater.EnsureColumnsExist(dbPath, "integration_api_settings", columnsToEnsure);


// ≈÷«›… «·Œœ„«  «·Œ«’…
//builder.Services.AddScoped<ApiIntegrationConfig>();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
////Add Domain Services
builder.Services.AddScoped<IDataAccessSerivce, OracleDataAccessService>();
builder.Services.AddSingleton<IDbModelMappingService, DbModelMappingService>();
//builder.Services.AddScoped<IItemServies, ItemServies>();
//builder.Services.AddScoped<BTMBOXServes>();
//builder.Services.AddScoped<ISyncToBMTBOXServivce, SyncToBMTBOXServivce>();
//builder.Services.AddScoped<ISyncToBMTBOXServiceTest, SyncToBMTBOXServiceTest>();
//builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
//{
//    options.JsonSerializerOptions.MaxDepth = 256;
//});
//builder.Services.AddControllers(
//options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


//builder.Services.AddApplicationInsightsTelemetry(Configuration);
builder.Services.AddMvc(config =>
{
    // Add XML Content Negotiation
    config.RespectBrowserAcceptHeader = true;
    config.InputFormatters.Add(new XmlSerializerInputFormatter(config));
    config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});
//builder.Services.AddControllers().Addn(options =>
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);
//add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents()
        {
            OnTokenValidated = OnTokenValidated,
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Validating the token
static async Task OnTokenValidated(TokenValidatedContext context)
{
    context.Success();
    var x = context.Result.Succeeded;
    await Task.CompletedTask;

}