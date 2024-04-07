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
    public class PageContentPluginParameters
    {
        public IDataStorageAccess dataStorageAccess { get; set; }
        public ITenantAccess tenantAccess { get; set; }
        public IHttpContextAccessor contextAccessor { get; set; }
        public Page currentPage { get; set; }
        public IPageTree pageTree { get; set; }
        public IUrlAliases urlAliases { get; set; }
    }
}
