using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using Ultimate.IntegrationSystem.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// áãáİÇÊ wwwroot æ _content/*
builder.WebHost.UseStaticWebAssets();

// Razor/Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(o => o.DetailedErrors = true);

// MudBlazor + Snackbar
builder.Services.AddMudServices(cfg =>
{
    cfg.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.TopCenter;
    cfg.SnackbarConfiguration.HideTransitionDuration = 100;
    cfg.SnackbarConfiguration.ShowTransitionDuration = 100;
    cfg.SnackbarConfiguration.VisibleStateDuration = 2500;
    cfg.SnackbarConfiguration.MaxDisplayedSnackbars = 3;
});

// ÖÛØ ÇáÇÓÊÌÇÈÉ (Brotli + Gzip) + ÏÚã SignalR
builder.Services.AddResponseCompression(opts =>
{
    opts.EnableForHttps = true;
    opts.Providers.Add<BrotliCompressionProvider>();
    opts.Providers.Add<GzipCompressionProvider>();
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" } // áŞäæÇÊ SignalR
    );
});

// ãÒæøÏ ÓíÇŞ HTTP (ŞÏ íßæä null ÃËäÇÁ ÇáÜ prerender)
builder.Services.AddHttpContextAccessor();

// ÖÈØ HttpClient ÇáãØÈæÚ ááÎÏãÉ ãÚ ŞæÇÚÏ ÚäæÇä API
var apiBase = builder.Configuration["ApiBaseUrl"]; // íãßä Ãä Êßæä İÇÑÛÉ Ãæ äÓÈíÉ
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>((sp, client) =>
{
    var accessor = sp.GetRequiredService<IHttpContextAccessor>();
    var ctx = accessor.HttpContext;

    // 1) Åä ßÇäÊ ApiBaseUrl ãõÚÑøİÉ æãØáŞÉ¡ ÇÓÊÎÏãåÇ
    if (!string.IsNullOrWhiteSpace(apiBase) && Uri.TryCreate(apiBase, UriKind.Absolute, out var absolute))
    {
        client.BaseAddress = absolute;
        return;
    }

    // 2) æÅáÇ ÍÇæá ÇÓÊÎÏÇã äİÓ ÇáãÖíİ ÃËäÇÁ ÇáØáÈ
    if (ctx?.Request?.Host.HasValue == true)
    {
        var scheme = ctx.Request.Scheme;
        var host = ctx.Request.Host.Value;
        client.BaseAddress = new Uri($"{scheme}://{host}/");
        return;
    }

    // 3) Fallback ÃÎíÑ: Åä ßÇäÊ ApiBaseUrl äÓÈíøÉ Ãæ ÛíÑ ÕÇáÍÉ¡ ÍæøáåÇ áÚäæÇä ãÍáí Âãä
    var fallback = string.IsNullOrWhiteSpace(apiBase) ? "https://localhost:5001/" : apiBase;
    client.BaseAddress = new Uri(fallback, UriKind.RelativeOrAbsolute);
});

// ÍÇáÉ ãÎÊÇÑÉ (state)
builder.Services.AddScoped<SelectedEmployeeState>();

// ÇáÊÚÑíÈ
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

    // ÇáÓãÇÍ ÈÇÎÊíÇÑ ÇááÛÉ ÚÈÑ ÇáßæßíÒ
    o.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider
    {
        CookieName = CookieRequestCultureProvider.DefaultCookieName
    });
});

// ÏÚã Reverse Proxy (X-Forwarded-For/Proto/Host)
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
    // ÅĞÇ áÏíß ÈÑæßÓíÇÊ ãæËæŞÉ ãÍÏÏÉ¡ ÃÖİåÇ åäÇ ÚÈÑ KnownProxies/KnownNetworks
});

var app = builder.Build();

// ËŞÇİÉ ÇİÊÑÇÖíÉ Úáì ãÓÊæì ÇáÎíæØ (ÇÎÊíÇÑí)
var defaultCulture = new CultureInfo("ar-YE");
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

// ÊÑÊíÈ ÇáãíÏá æíÑ
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseResponseCompression();

// ÚäÏ ÇáÚãá Îáİ Proxy (İÚøá ÈÚÏ UseHttpsRedirection)
app.UseForwardedHeaders();

app.UseStaticFiles();

app.UseRouting();

// Endpoints
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
