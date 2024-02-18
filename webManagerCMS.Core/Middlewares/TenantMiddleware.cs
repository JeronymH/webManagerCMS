using Microsoft.Extensions.Options;
using S9.Core.Exceptions;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;
using webManagerCMS.Data.Models;
using webManagerCMS.Core.Services.ComponentService;

namespace webManagerCMS.Core.Middlewares
{
	public class TenantMiddleware
	{
		internal const string ConstHttpContextItemKeyTenant = "Tenant";
		private const string ConstTemplatesFolderName = "Templates";

		public TenantMiddleware(RequestDelegate next, ILogger<TenantMiddleware> logger, IDataStorageAccess dataStorageAccess, IComponentService componentService)
		{
			this._next = next ?? throw new ArgumentNullException(nameof(next));
			this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this._dataStorageAccess = dataStorageAccess ?? throw new ArgumentNullException(nameof(dataStorageAccess));
			this._componentService = componentService ?? throw new ArgumentNullException(nameof(componentService));
		}

		private readonly RequestDelegate _next;
		private readonly ILogger<TenantMiddleware> _logger;
		private readonly IDataStorageAccess _dataStorageAccess;
		private readonly IComponentService _componentService;

		public async Task Invoke(HttpContext context, IOptionsSnapshot<ApplicationSettings> options)
		{
			context.Items[ConstHttpContextItemKeyTenant] = null;

			var applicationSettings = options?.Value;

			if (applicationSettings == null || applicationSettings.Tenants == null)
				throw new InvalidOperationException("Unable to read tenants from the application settings.");

			var domain = context.Request.Host.Value;
			domain = "localhost";

			ITenant tenant = null;

			if (applicationSettings.Tenants.ContainsKey(domain))
				tenant = applicationSettings.Tenants[domain];

			if (tenant == null)
				throw new AccessDeniedException($"Unable to get a tenant for the [{domain}] domain - bad domain or tenant data.");
			else
			{
				tenant.DomainName = domain;
				tenant.Components = (Dictionary<string, Type>)this._componentService.GetTenantDynamicComponent(tenant.IdWWW).Components;
			}

			context.Items[ConstHttpContextItemKeyTenant] = tenant;

			await this._next.Invoke(context);
		}
	}
}
