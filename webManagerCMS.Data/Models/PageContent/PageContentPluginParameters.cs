using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;
using webManagerCMS.Data.Interfaces;

namespace webManagerCMS.Data.Models.PageContent
{
    public class PageContentPluginParameters : IPageContentPluginParameters
    {
        public IDataStorageAccess DataStorageAccess { get; set; }
        public ITenantAccess TenantAccess { get; set; }
        public IHttpContextAccessor ContextAccessor { get; set; }
        public Page CurrentPage { get; set; }
        public IPageTree PageTree { get; set; }
        public IUrlAliases UrlAliases { get; set; }

        public void Init(IDataStorageAccess dataStorageAccess,
                    ITenantAccess tenantAccess,
                    IHttpContextAccessor contextAccessor,
                    Page currentPage,
                    IPageTree pageTree,
                    IUrlAliases urlAliases)
        {
			DataStorageAccess = dataStorageAccess;
            TenantAccess = tenantAccess;
            ContextAccessor = contextAccessor;
            CurrentPage = currentPage;
            PageTree = pageTree;
            UrlAliases = urlAliases;
		}
	}
}
