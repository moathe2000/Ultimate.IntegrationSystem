using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using System.Globalization;
using System.Net;
using System.Net.Http;
using Ultimate.IntegrationSystem.Web.Service;
using Ultimate.IntegrationSystem.Web.Service.Muqeem;

var builder = WebApplication.CreateBuilder(args);

// ·„·›«  wwwroot Ê _content/*
builder.WebHost.UseStaticWebAssets();

// Razor/Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(o => o.DetailedErrors = true);

// MudBlazor + Snackbar
builder.Services.AddMudServices(cfg =>
{
    cfg.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.TopCenter;
    cfg.SnackbarConfiguration.HideTransitionDuration = 100;
    cfg.SnackbarConfiguration.ShowTransitionDuration = 100;
    cfg.SnackbarConfiguration.VisibleStateDuration = 2500;
    cfg.SnackbarConfiguration.MaxDisplayedSnackbars = 3;
});

// ÷€ÿ «·«” Ã«»… + SignalR
builder.Services.AddResponseCompression(opts =>
{
    opts.EnableForHttps = true;
    opts.Providers.Add<BrotliCompressionProvider>();
    opts.Providers.Add<GzipCompressionProvider>();
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});
builder.Services.Configure<BrotliCompressionProviderOptions>(o => o.Level = System.IO.Compression.CompressionLevel.Fastest);
builder.Services.Configure<GzipCompressionProviderOptions>(o => o.Level = System.IO.Compression.CompressionLevel.Fastest);

// „“Ê¯œ ”Ì«ﬁ HTTP
builder.Services.AddHttpContextAccessor();

// œ«·… ·Õ”„ ﬁ«⁄œ… ⁄‰Ê«‰ «·‹ API
static Uri ResolveApiBase(IServiceProvider sp, IConfiguration cfg)
{
    var accessor = sp.GetRequiredService<IHttpContextAccessor>();
    var ctx = accessor.HttpContext;
    var apiBase = cfg["ApiBaseUrl"];

    if (!string.IsNullOrWhiteSpace(apiBase) && Uri.TryCreate(apiBase, UriKind.Absolute, out var abs))
        return abs;

    if (ctx?.Request?.Host.HasValue == true)
        return new Uri($"{ctx.Request.Scheme}://{ctx.Request.Host}/");

    return new Uri("https://localhost:5001/");
}

//  ”ÃÌ· HttpClient ··Œœ„« 
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>((sp, client) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = ResolveApiBase(sp, cfg);
})
.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
{
    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
});

builder.Services.AddHttpClient<IIqamaService, IqamaService>((sp, client) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = ResolveApiBase(sp, cfg);
})
.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
{
    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
});

// Õ«·… „Œ «—… (state)
builder.Services.AddScoped<SelectedEmployeeState>();

// «· ⁄—Ì»
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

    o.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider
    {
        CookieName = CookieRequestCultureProvider.DefaultCookieName
    });
});

// œ⁄„ Reverse Proxy
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
    // options.KnownProxies.Add(IPAddress.Parse("x.x.x.x"));
});

var app = builder.Build();

var defaultCulture = new CultureInfo("ar-YE");
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// ÷⁄ ForwardedHeaders „»ﬂ—« Œ·› »—Êﬂ”Ì
app.UseForwardedHeaders();

app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
