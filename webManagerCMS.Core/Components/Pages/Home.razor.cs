using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;
using webManagerCMS.Data.Models.Page;
using Microsoft.VisualBasic;
using webManagerCMS.Data.Models;

namespace webManagerCMS.Core.Components.Pages
{
    public partial class Home
    {
        [Inject]
        IDataStorageAccess? DataStorageAccess { get; set; }

        [Inject]
        ITenantAccess? TenantAccess { get; set; }

        [Inject]
        IHttpContextAccessor? httpContextAccessor {  get; set; }


        protected override void OnInitialized() {
            var page = GetPage();
            var urlAliases = GetUrlAliases(page);


        }

        private string? GetQueryStringData(string name) {
            var data = httpContextAccessor?.HttpContext?.Request.Query[name];
            if (!data.HasValue)
                return null;

            return data;
        }

        private Data.Models.Page.Page? GetPage() {
            string? alias = GetQueryStringData("rAlias0");
            var page = DataStorageAccess?.WebContentDataStorage.GetPage(alias);

            if (page == null && alias == "")
                page = DataStorageAccess?.WebContentDataStorage.GetHomePage();

            return page;
        }

        private UrlAliases GetUrlAliases(Data.Models.Page.Page? page) {
            var urlAliases = new UrlAliases(httpContextAccessor);

            if (page == null)
                return urlAliases;

            var idAliasTableName = 0;
            var templateNumber = page.TemplateNum; //templateNum for plugins, default is page template
            var step = 0;

            urlAliases.AddData(page.IsHomePage, page.Name, page.Id, page.IdDB, 0, 0, page.TemplateNum, step, page.PageAlias, 0, 0, true); //add first level alias

            for (int i = step; i < urlAliases.QueryAliases.Length; i++)
            {
                urlAliases.AddData(DataStorageAccess?.WebContentDataStorage.GetAlias(step, page.Id, idAliasTableName, templateNumber, urlAliases.QueryAliases[i]));
                step++;

                if (urlAliases.Aliases[i] == null)
                    i = urlAliases.QueryAliases.Length;
            }


            return urlAliases;
        }
    }
}
