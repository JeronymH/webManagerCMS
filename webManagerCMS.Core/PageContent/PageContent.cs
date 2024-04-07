using Microsoft.AspNetCore.Components;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS
{
    public class PageContent : PageContentPlugin
    {
        private IDataStorageAccess _dataStorageAccess;

        public PageContent(int templateNum, int templateState, PageContentPluginParameters pluginParameters, IDataStorageAccess dataStorageAccess) : base(0) {
            TemplateName = PageContentPluginType.PAGE_CORE;

            TemplateNum = templateNum;
            TemplateState = templateState;
            PluginParameters = pluginParameters;

            _dataStorageAccess = dataStorageAccess;
		}

        public RenderFragment RenderPlugin(PageContentPlugin plugin) => builder =>
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

		public RenderFragment RenderPageContent(int contentColumnId) => builder =>
		{
			var content = _dataStorageAccess.WebContentDataStorage.LoadPageContent(PluginParameters.currentPage.IdDB, 1, null);
            PageContentPlugin? pageContentPlugin;
			Type? component;

			foreach (var plugin in content)
            {
				pageContentPlugin = GetPageContentPlugin(plugin);
                if (pageContentPlugin != null)
                {
				    component = _dataStorageAccess.TenantAccess.Tenant.Components[pageContentPlugin.Template];
				    builder.OpenComponent(0, component);
				    builder.AddAttribute(1, "Objref", pageContentPlugin);
			        builder.CloseComponent();
                }
            }
		};

        public PageContentPlugin GetPageContentPlugin(PageContentPlugin plugin)
        {
            switch (plugin.TemplateName)
            {
                case PageContentPluginType.DOC_H1TEXT:
                    throw new NotImplementedException();
                    break;
                case PageContentPluginType.PAGE_CORE:
                    throw new NotImplementedException();
                    break;
                case PageContentPluginType.TREE_CORE:
                    throw new NotImplementedException();
                    break;
                case PageContentPluginType.GALLERY1:
                    throw new NotImplementedException();
                    break;
                case PageContentPluginType.NOTE1:
                    throw new NotImplementedException();
                    break;
                case PageContentPluginType.PICHEADER:
                    throw new NotImplementedException();
                    break;
                case PageContentPluginType.LINKFOOTER:
                    throw new NotImplementedException();
                    break;
                case PageContentPluginType.TXTHEADER:
                    throw new NotImplementedException();
                    break;
                case PageContentPluginType.TREEDISPLAYDEFINED1:
                    throw new NotImplementedException();
                    break;
                case PageContentPluginType.DOC_HTML:
                    throw new NotImplementedException();
                    break;
            }
            return null;
        }
	}
}
