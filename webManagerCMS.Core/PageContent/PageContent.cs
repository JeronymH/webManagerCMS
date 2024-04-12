using Microsoft.AspNetCore.Components;
using webManagerCMS.Core.PageContentNS.Plugins;
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
                plugin.PluginParameters = PluginParameters; 
				pageContentPlugin = GetPageContentPlugin(plugin);
                if (pageContentPlugin != null)
                {
				    component = _dataStorageAccess.TenantAccess.Tenant.GetComponent(pageContentPlugin.Template);
                    if (component != null)
                    {
                        builder.OpenComponent(0, component);
                        builder.AddAttribute(1, "Objref", pageContentPlugin);
                        builder.CloseComponent();
                    }
                }
            }
		};

        public PageContentPlugin GetPageContentPlugin(PageContentPlugin plugin)
        {
            switch (plugin.TemplateName)
            {
                case PageContentPluginType.DOC_H1TEXT:
                    return new DocH1Text(plugin);

                case PageContentPluginType.PAGE_CORE:
                    
                    break;
                case PageContentPluginType.TREE_CORE:
                    
                    break;
                case PageContentPluginType.GALLERY1:
                    
                    break;
                case PageContentPluginType.NOTE1:
                    
                    break;
                case PageContentPluginType.PICHEADER:
                    
                    break;
                case PageContentPluginType.LINKFOOTER:
                    
                    break;
                case PageContentPluginType.TXTHEADER:
                    
                    break;
                case PageContentPluginType.TREEDISPLAYDEFINED1:
                    return new TreeDisplayDefined(plugin);

                case PageContentPluginType.DOC_HTML:
                    return new DocHtml(plugin);
            }
            return null;
        }
	}
}
