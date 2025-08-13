using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using Ultimate.IntegrationSystem.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// ·„·›«  wwwroot Ê _content/*
builder.WebHost.UseStaticWebAssets();

// Œœ„«  √”«”Ì…
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(o => o.DetailedErrors = true);

// MudBlazor +  ÂÌ∆… Snackbar
builder.Services.AddMudServices(cfg =>
{
    cfg.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.TopCenter;
    cfg.SnackbarConfiguration.HideTransitionDuration = 100;
    cfg.SnackbarConfiguration.ShowTransitionDuration = 100;
    cfg.SnackbarConfiguration.VisibleStateDuration = 2500;
    cfg.SnackbarConfiguration.MaxDisplayedSnackbars = 3;
});

// Œœ„« ﬂ


// ÷€ÿ «·«” Ã«»… (SignalR + „Õ ÊÌ«  ⁄«„…)
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" } // ·ﬁ‰Ê«  SignalR
    );
});



var apiBase = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:5001/";

// √›÷· ŒÌ«— ·Ê ‰›” «·„Êﬁ⁄/‰›” «·ÂÊ” :
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>((sp, client) =>
{
    var ctx = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
    client.BaseAddress = new Uri($"{ctx!.Request.Scheme}://{ctx.Request.Host}/");
});
// ”Ã¯· «·Œœ„… «· Ì  ” ﬁ»· HttpClient ›Ì «·‹ctor
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<SelectedEmployeeState>();

//  ⁄—Ì»
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources/Localization");
builder.Services.Configure<RequestLocalizationOptions>(o =>
{
    var cultures = new[]
    {
        new CultureInfo("ar-YE"),
        new CultureInfo("ar"),
        new CultureInfo("en-US")
    };
    o.DefaultRequestCulture = new RequestCulture("ar-YE");
    o.SupportedCultures = cultures;
    o.SupportedUICultures = cultures;
    // «·”„«Õ »«Œ Ì«— «··€… ⁄»— «·ﬂÊﬂÌ“
    o.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});

var app = builder.Build();

// ÷»ÿ «·Àﬁ«›… «·«› —«÷Ì… Œ«—Ã ”Ì«ﬁ HTTP («Œ Ì«—Ì)
var defaultCulture = new CultureInfo("ar-YE");
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

// „Ìœ· ÊÌ—
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseResponseCompression();
app.UseStaticFiles();

app.UseRouting();

// Endpoints
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
