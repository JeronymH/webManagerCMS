using Microsoft.AspNetCore.Components;
using webManagerCMS.Core.PageContentNS;
using webManagerCMS.Core.PageContentNS.Plugins;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;
using Page = webManagerCMS.Data.Models.PageContent.Page;
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
		IPageContentPluginParameters? PluginParameters { get; set; }

		[Inject]
		IHttpContextAccessor? httpContextAccessor { get; set; }

		public PageContent? PageContent { get; set; }

		protected override void OnInitialized()
		{
			var page = GetPage();
			var urlAliases = GetUrlAliases(page);
			var pageTree = new PageTree(DataStorageAccess.WebContentDataStorage.LoadPagesDictionary(true), TenantAccess);

			PluginParameters.Init(DataStorageAccess, TenantAccess, httpContextAccessor, page, pageTree, urlAliases);

			if (urlAliases.CheckAllData())
			{
				PageContent = new PageContent(page.TemplateNum, urlAliases.ActState, PluginParameters, DataStorageAccess);
            }
			else
			{
				CheckAliasHistory(urlAliases.QueryAliases[0]);

				page = DataStorageAccess?.WebContentDataStorage.GetHomePage();
				PluginParameters.CurrentPage = page;
				PageContent = new PageContent(PageContent.ErrorTemplateNum, PageContent.ErrorTemplateState, PluginParameters, DataStorageAccess);
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

			if (page == null)
				page = new Page();

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

		private void CheckAliasHistory (string alias)
		{
			//load PageTree without cache - after implementing API invalidating cache this wont be necessary
			var pageTree = new PageTree(DataStorageAccess.WebContentDataStorage.LoadPagesDictionary(false), TenantAccess);
			var page = pageTree.GetPageByAlias(alias);

			if (page == null)
				page = new Page();

			if (string.IsNullOrEmpty(page.PageAlias) || page.Id <= 0)
			{
				var historyPage = DataStorageAccess?.WebContentDataStorage.GetPageFromHistory(alias);
				page = pageTree.GetPageByAlias(historyPage.PageAlias);
			}

			if (string.IsNullOrEmpty(page.PageAlias) || page.Id <= 0) return;

			var url = DataStorageAccess?.TenantAccess.Tenant.GetRootAlias() + page.PageAlias;
			url = GetHistoryUrlAlias(page, url);

			if (string.IsNullOrEmpty(url)) return;

			//TODO: end response immediately
			httpContextAccessor.HttpContext.Response.Redirect(url);
		}

		private string GetHistoryUrlAlias(Page page, string url)
		{
			var urlAliases = new UrlAliases(httpContextAccessor);
			Alias? alias = null;
			bool aliasLoaded = false;

			var idAliasTableName = 0;
			var step = 1;

			for (int i = step; i < urlAliases.QueryAliases.Length; i++)
			{
				aliasLoaded = false;

				if (string.IsNullOrEmpty(urlAliases.QueryAliases[i]))
					break;

				alias = DataStorageAccess?.WebContentDataStorage.GetAlias(step, page.Id, idAliasTableName, 0, urlAliases.QueryAliases[i]);
				if (alias != null)
				{
					idAliasTableName = alias.IdTableName?? 0;
					url += "/" + urlAliases.QueryAliases[i];
					aliasLoaded = true;
				}
				if (!aliasLoaded)
				{
					alias = DataStorageAccess?.WebContentDataStorage.GetAliasFromHistory(step, page.Id, idAliasTableName, urlAliases.QueryAliases[i]);
					if (alias != null)
					{
						idAliasTableName = alias.IdTableName ?? 0;
						url += "/" + alias.Name;
						aliasLoaded = true;
					}
				}
				step++;

				if (!aliasLoaded)
					break;
			}

			return url + DataStorageAccess.TenantAccess.Tenant.WWWSettings.PageSuffix;
		}
	}
}
