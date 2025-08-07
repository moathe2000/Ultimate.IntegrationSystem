using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using System.Globalization;
using Ultimate.IntegrationSystem.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// 1. Œœ„… Razor Pages Ê Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// 2. MudBlazor
builder.Services.AddMudServices();

// 3. Œœ„« ﬂ «·Œ«’…
builder.Services.AddScoped<EmployeeService>();
//builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
//bubuilder.Services.AddScoped<IAuthService, AuthService>();        // „À«·
//ilder.Services.AddScoped<ISyncLocalStorageService, SyncLocalStorageService>();

// 4. Localization
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources/Localization");
builder.Services.Configure<RequestLocalizationOptions>(o =>
{
    var cultures = new[] { new CultureInfo("ar-YE"), new CultureInfo("en-US") };
    o.DefaultRequestCulture = new RequestCulture("ar-YE");
    o.SupportedCultures = cultures;
    o.SupportedUICultures = cultures;
});

var app = builder.Build();

// 5. Middleware
app.UseRequestLocalization(app.Services
    .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error");

app.UseStaticFiles();
app.UseRouting();

// 6. «·Œ—«∆ÿ
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
