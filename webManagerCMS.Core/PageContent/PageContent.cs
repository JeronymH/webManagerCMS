using Microsoft.AspNetCore.Components;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS
{
    public class PageContent : PageContentPlugin
    {
        public new PageContentPluginType TemplateName { get; } = PageContentPluginType.PAGE_CORE;

        private IDataStorageAccess _dataStorageAccess;

        public PageContent(IDataStorageAccess dataStorageAccess) {
            _dataStorageAccess = dataStorageAccess;
        }

        public RenderFragment RenderPlugin(IPageContentPlugin plugin) => builder =>
        {
            var component = _dataStorageAccess.TenantAccess.Tenant.Components[plugin.Template];
            builder.OpenComponent(0, component);
            builder.AddAttribute(1, "Object", plugin);
            builder.CloseComponent();
        };

    }
}
