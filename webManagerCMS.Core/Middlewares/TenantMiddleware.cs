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
        internal const string ConstTemplatesFolderName = "Templates";
		internal const string ConstApplicationSettingsKeyIdWWWRoots = "IdWWWRoots";
		internal const string ConstRequestQueryStringKeyMutationAlias = "rMutationAlias";

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
			var tenantKey = domain;
            string? mutationAlias = context.Request.Query[ConstRequestQueryStringKeyMutationAlias];
			if (mutationAlias == null)
                mutationAlias = "";

            //when developing, domain is always with different port, so this removes the port for tenantKey
            if (applicationSettings.WebDevelopmentBehaviorEnabled)
                tenantKey = "localhost";

			ITenant tenant = null;

			if (applicationSettings.Tenants.ContainsKey(tenantKey))
				tenant = applicationSettings.Tenants[tenantKey];

			if (tenant == null)
				throw new AccessDeniedException($"Unable to get a tenant for the [{tenantKey}] domain - bad domain or tenant data.");
			else
			{
				tenant.DomainName = domain;
				tenant.DynamicComponents = this._componentService.GetTenantDynamicComponent(tenant.IdWWW);
				tenant.Components = (Dictionary<string, Type>)tenant.DynamicComponents.Components;
                tenant.WWWSettings = this._dataStorageAccess.SystemDataStorage.GetWWWSettings(tenant.IdWWW, tenant.GetWebBaseUrl(), mutationAlias, applicationSettings.WebDevelopmentBehaviorEnabled);
            }

			context.Items[ConstHttpContextItemKeyTenant] = tenant;
			await this._next.Invoke(context);
		}
	}
}
