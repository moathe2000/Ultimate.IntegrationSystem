using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;

namespace Ultimate.IntegrationSystem.Web.Models
{
    public class NavItem
    {
        public string TitleKey { get; set; } = "";
        public string Icon { get; set; } = Icons.Material.Filled.Home;
        public string? Href { get; set; }
        public NavLinkMatch Match { get; set; } = NavLinkMatch.All;
        public string[]? Roles { get; set; }
        public List<NavItem>? Children { get; set; }
    }
}
