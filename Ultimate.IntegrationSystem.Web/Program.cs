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

// ������ wwwroot � _content/*
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

// ��� ��������� (Brotli + Gzip) + ��� SignalR
builder.Services.AddResponseCompression(opts =>
{
    opts.EnableForHttps = true;
    opts.Providers.Add<BrotliCompressionProvider>();
    opts.Providers.Add<GzipCompressionProvider>();
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" } // ������ SignalR
    );
});

// ����� ���� HTTP (�� ���� null ����� ��� prerender)
builder.Services.AddHttpContextAccessor();

// ��� HttpClient ������� ������ �� ����� ����� API
var apiBase = builder.Configuration["ApiBaseUrl"]; // ���� �� ���� ����� �� �����
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>((sp, client) =>
{
    var accessor = sp.GetRequiredService<IHttpContextAccessor>();
    var ctx = accessor.HttpContext;

    // 1) �� ���� ApiBaseUrl ������� �����ɡ ��������
    if (!string.IsNullOrWhiteSpace(apiBase) && Uri.TryCreate(apiBase, UriKind.Absolute, out var absolute))
    {
        client.BaseAddress = absolute;
        return;
    }

    // 2) ���� ���� ������� ��� ������ ����� �����
    if (ctx?.Request?.Host.HasValue == true)
    {
        var scheme = ctx.Request.Scheme;
        var host = ctx.Request.Host.Value;
        client.BaseAddress = new Uri($"{scheme}://{host}/");
        return;
    }

    // 3) Fallback ����: �� ���� ApiBaseUrl ������ �� ��� ����ɡ ������ ������ ���� ���
    var fallback = string.IsNullOrWhiteSpace(apiBase) ? "https://localhost:5001/" : apiBase;
    client.BaseAddress = new Uri(fallback, UriKind.RelativeOrAbsolute);
});

// ���� ������ (state)
builder.Services.AddScoped<SelectedEmployeeState>();

// �������
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

    // ������ ������� ����� ��� �������
    o.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider
    {
        CookieName = CookieRequestCultureProvider.DefaultCookieName
    });
});

// ��� Reverse Proxy (X-Forwarded-For/Proto/Host)
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
    // ��� ���� �������� ������ ����ɡ ����� ��� ��� KnownProxies/KnownNetworks
});

var app = builder.Build();

// ����� �������� ��� ����� ������ (�������)
var defaultCulture = new CultureInfo("ar-YE");
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

// ����� ������ ���
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseResponseCompression();

// ��� ����� ��� Proxy (���� ��� UseHttpsRedirection)
app.UseForwardedHeaders();

app.UseStaticFiles();

app.UseRouting();

// Endpoints
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
