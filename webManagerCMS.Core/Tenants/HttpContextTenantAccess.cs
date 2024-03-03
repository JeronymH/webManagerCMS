using webManagerCMS.Core.Middlewares;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Core.Tenants
{
    public class HttpContextTenantAccess : ITenantAccess
    {
		public HttpContextTenantAccess(IHttpContextAccessor httpContextAccessor)
		{
			this._httpContextAccessor = httpContextAccessor;
		}

		private readonly IHttpContextAccessor _httpContextAccessor;

		public ITenant Tenant => this._httpContextAccessor.HttpContext?.Items[TenantMiddleware.ConstHttpContextItemKeyTenant] as ITenant;

		public int IdWWW => this.Tenant?.IdWWW ?? throw new NullReferenceException("Unable to retrieve IdWWW from current HttpContext.");

		public int IdWWWRoot => this.Tenant?.WWWSettings.IdWWWRoot ?? throw new NullReferenceException("Unable to retrieve IdWWWRoot from current HttpContext.");

		public int IdLanguage => this.Tenant?.WWWSettings.IdLanguage ?? throw new NullReferenceException("Unable to retrieve IdLanguage from current HttpContext.");
	}
}
