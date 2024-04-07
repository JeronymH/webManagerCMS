using Microsoft.AspNetCore.Components;
using webManagerCMS.Data.Models.PageContent;

namespace webManagerCMS.Core.PageContent
{
    public class PageContent : PageContentPlugin
    {
        public new PageContentPluginType TemplateName { get; } = PageContentPluginType.PAGE_CORE;

        //public RenderFragment RenderPageTree()
        //{
        //    //RenderFragment dynamicComonent() => builder =>
        //    //{
        //    //    var component = @Storage.TenantAccess.Tenant.Components["Component1"];
        //    //    builder.OpenComponent(0, component);
        //    //    builder.AddAttribute(1, "Title", @Storage.TenantAccess.Tenant.DynamicComponents);
        //    //    builder.CloseComponent();
        //    //};

        //    //return dynamicComonent();
        //}
    }
}
