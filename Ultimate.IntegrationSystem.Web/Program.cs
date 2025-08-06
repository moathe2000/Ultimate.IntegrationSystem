using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using System.Globalization;
using Ultimate.IntegrationSystem.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// 1) «·Œœ„« 
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources/Localization");
builder.Services.Configure<RequestLocalizationOptions>(o =>
{
    var supported = new[] { new CultureInfo("ar-YE"), new CultureInfo("en-US") };
    o.DefaultRequestCulture = new RequestCulture("ar-YE");
    o.SupportedCultures = supported;
    o.SupportedUICultures = supported;
});

var app = builder.Build();

// 2) Middleware
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");
app.UseStaticFiles();
app.UseRouting();

// 3) Œ—Ìÿ… «·’›Õ«  Ê Blazor
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

