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

            IdPage = PluginParameters.currentPage == null ? 0 : PluginParameters.currentPage.Id;

			_dataStorageAccess = dataStorageAccess;
		}

        public static int ErrorTemplateNum = -1;
        public static int ErrorTemplateState = 0;

        public RenderFragment RenderPlugin(PageContentPlugin plugin) => builder =>
        {
            var component = _dataStorageAccess.TenantAccess.Tenant.GetComponent(plugin.Template);
			if (component != null) //if (component != null || _dataStorageAccess.TenantAccess.Tenant.WebDevelopmentBehaviorEnabled)
            {
			    builder.OpenComponent(0, component);
                builder.AddAttribute(1, "Objref", plugin);
                builder.CloseComponent();
            }
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
			var content = _dataStorageAccess.WebContentDataStorage.LoadPageContent(PluginParameters.currentPage.IdDB, contentColumnId, null);
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
            var idPage = IdPage;
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

                case PageContentPluginType.TREEDISPLAYDEFINED1:
                    return new TreeDisplayDefined(plugin);

                case PageContentPluginType.DOC_HTML:
                    return new DocHtml(plugin);
            }
            return null;
        }

		#region HeaderPicture
		public RenderFragment RenderHeaderPicture(int placeNumber, HeaderPictureSelectType selectType, int pictureNumber, bool randomOrder)
		{
			HeaderPicture headerPicture = new HeaderPicture(IdPage, placeNumber, selectType, pictureNumber, randomOrder, PluginParameters);

            return RenderPlugin(headerPicture);
		}

        public RenderFragment? RenderHeaderPictureWithMaxPlaceNumber(int maxPlaceNumber, HeaderPictureSelectType selectType, int pictureNumber, bool randomOrder)
		{
            if (maxPlaceNumber > 9) throw new ArgumentOutOfRangeException("The maximum allowed value of maxPlaceNumber is 9.");

            for (int i = 0; i <= maxPlaceNumber; i++)
            {
			    HeaderPicture headerPicture = new HeaderPicture(IdPage, i, selectType, pictureNumber, randomOrder, PluginParameters);
                if (headerPicture.Id > 0)
					return RenderPlugin(headerPicture);
			}

            return null;
		}

		public RenderFragment RenderHeaderPictureList(int placeNumber)
		{
			HeaderPicture headerPicture = new HeaderPicture(IdPage, placeNumber, PluginParameters);

			return RenderPlugin(headerPicture);
		}
        #endregion

        public RenderFragment RenderFooterLinkList(int templateNumber, int templateState, int placeNumber)
        {
			FooterLink footerLink = new FooterLink(IdPage, templateNumber, templateState, placeNumber, PluginParameters);

			return RenderPlugin(footerLink);
		}
	}
}
