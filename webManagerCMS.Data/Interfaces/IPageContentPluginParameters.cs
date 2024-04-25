using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Data.Interfaces
{
	public interface IPageContentPluginParameters
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
							IUrlAliases urlAliases);
	}
}
