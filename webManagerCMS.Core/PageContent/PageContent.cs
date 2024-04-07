using Microsoft.AspNetCore.Components;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS
{
    public class PageContent : PageContentPlugin
    {
        private IDataStorageAccess _dataStorageAccess;

        public PageContent(int templateNum, int templateState, PageContentPluginParameters pluginParameters, IDataStorageAccess dataStorageAccess) {
            TemplateName = PageContentPluginType.PAGE_CORE;

            TemplateNum = templateNum;
            TemplateState = templateState;
            PluginParameters = pluginParameters;

            _dataStorageAccess = dataStorageAccess;
        }

        public RenderFragment RenderPlugin(IPageContentPlugin plugin) => builder =>
        {
            var component = _dataStorageAccess.TenantAccess.Tenant.Components[plugin.Template];
            builder.OpenComponent(0, component);
            builder.AddAttribute(1, "Objref", plugin);
            builder.CloseComponent();
        };

        public RenderFragment RenderPage()
        {
            return RenderPlugin(this);
		}

        public RenderFragment RenderTree(int templateNum, int templateState)
		{
            var pageTreePlugin = new Plugins.PageTree(templateNum, templateState, PluginParameters);
            return RenderPlugin(pageTreePlugin);
		}

	}
}
