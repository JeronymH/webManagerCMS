using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Core.Services.ComponentService
{
	public interface IComponentService
	{
		Dictionary<int, IEnumerable<Type>> DynamicComponents {  get; }
		void LoadDynamicComponents();

        IDynamicComponents GetTenantDynamicComponent(int IdWWW);
	}
}
