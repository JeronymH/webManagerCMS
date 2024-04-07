using Microsoft.AspNetCore.Components;
using webManagerCMS.Core.PageContentNS;
using webManagerCMS.Core.PageContentNS.Plugins;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;
using PageTree = webManagerCMS.Core.PageContentNS.PageTree;

namespace webManagerCMS.Core.Components
{
    public partial class App
	{
		[Inject]
		IDataStorageAccess? DataStorageAccess { get; set; }

		[Inject]
		ITenantAccess? TenantAccess { get; set; }

		[Inject]
		IHttpContextAccessor? httpContextAccessor { get; set; }

		public PageContent? PageContent { get; set; }

		protected override void OnInitialized()
		{
			var page = GetPage();
			var urlAliases = GetUrlAliases(page);
			var pageTree = new PageTree(DataStorageAccess.WebContentDataStorage.LoadPagesDictionary(true));

			var pluginParameters = new PageContentPluginParameters() {
				dataStorageAccess = DataStorageAccess,
				tenantAccess = TenantAccess,
                contextAccessor = httpContextAccessor,
				currentPage = page,
				pageTree = pageTree,
                urlAliases = urlAliases
			};

            PageContent = new PageContent(page.TemplateNum, urlAliases.ActState, pluginParameters, DataStorageAccess);

			if (urlAliases.CheckAllData())
			{

            }
			else
			{

			}
		}

		private string? GetQueryStringData(string name)
		{
			var data = httpContextAccessor?.HttpContext?.Request.Query[name];
			if (!data.HasValue)
				return null;

			return data;
		}

		private Data.Models.PageContent.Page? GetPage()
		{
			string? alias = GetQueryStringData("rAlias0");
			var page = DataStorageAccess?.WebContentDataStorage.GetPage(alias);

			if (page == null && alias == "")
				page = DataStorageAccess?.WebContentDataStorage.GetHomePage();

			return page;
		}

		private UrlAliases GetUrlAliases(Data.Models.PageContent.Page? page)
		{
			var urlAliases = new UrlAliases(httpContextAccessor);

			if (page == null)
				return urlAliases;

			var idAliasTableName = 0;
			var templateNumber = page.TemplateNum; //templateNum for plugins, default is page template
			var step = 0;

			urlAliases.AddData(page.IsHomePage, page.Name, page.Id, page.IdDB, 0, 0, page.TemplateNum, step, page.PageAlias, 0, 0, true); //add first level alias
			step++;

			for (int i = step; i < urlAliases.QueryAliases.Length; i++)
			{
				if (string.IsNullOrEmpty(urlAliases.QueryAliases[i]))
					break;

                urlAliases.AddData(DataStorageAccess?.WebContentDataStorage.GetAlias(step, page.Id, idAliasTableName, templateNumber, urlAliases.QueryAliases[i]));
				step++;

				if (urlAliases.Aliases[i] == null)
					break;
			}


			return urlAliases;
		}
	}
}
