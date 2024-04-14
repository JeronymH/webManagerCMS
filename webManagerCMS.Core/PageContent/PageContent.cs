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
                    if (component != null) //if (component != null || _dataStorageAccess.TenantAccess.Tenant.WebDevelopmentBehaviorEnabled)
                    {
                        builder.OpenComponent(0, component);
                        builder.AddAttribute(1, "Objref", pageContentPlugin);
                        builder.CloseComponent();
                    }
                }
            }
		};

        public RenderFragment RenderPageDetailContent() => builder =>
        {
            PageContentPlugin? pageContentPlugin = null;
            Type? component;

            var idTableName = PluginParameters.urlAliases.Aliases[1].IdTableName ?? 0;
            var idPage = PluginParameters.currentPage.Id;
            var idPageContent = PluginParameters.urlAliases.Aliases[1].IdPageContent ?? 0;
            var idDetail = PluginParameters.urlAliases.Aliases[1].Id ?? 0;
            var templateNum = PluginParameters.urlAliases.Aliases[1].IdTemplateNum ?? 0;


			switch (PluginParameters.urlAliases.Aliases[1].IdTableName)
            {
                case 8:
                    pageContentPlugin = new Gallery(templateNum, TemplateState, PluginParameters, idPageContent, idPage, idDetail);
					break;
            }

			if (pageContentPlugin != null)
			{
				component = _dataStorageAccess.TenantAccess.Tenant.GetComponent(pageContentPlugin.Template);
				if (component != null) //if (component != null || _dataStorageAccess.TenantAccess.Tenant.WebDevelopmentBehaviorEnabled)
				{
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
                    return new DocH1Text(plugin);

                case PageContentPluginType.GALLERY1:
                    return new Gallery(plugin);

                case PageContentPluginType.NOTE1:
                    
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
