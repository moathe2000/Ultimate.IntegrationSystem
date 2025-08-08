using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using System.Globalization;
using Ultimate.IntegrationSystem.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages + Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// MudBlazor
builder.Services.AddMudServices();

// Œœ„« ﬂ
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Localization
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources/Localization");
builder.Services.Configure<RequestLocalizationOptions>(o =>
{
    var cultures = new[] { new CultureInfo("ar-YE"), new CultureInfo("en-US") };
    o.DefaultRequestCulture = new RequestCulture("ar-YE");
    o.SupportedCultures = cultures;
    o.SupportedUICultures = cultures;
});

var app = builder.Build();

// Middleware
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error");

app.UseStaticFiles();
app.UseRouting();

// Endpoints
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
