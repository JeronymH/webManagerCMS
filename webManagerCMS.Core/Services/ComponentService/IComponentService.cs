using Microsoft.AspNetCore.Components;

namespace webManagerCMS.Core.Services.ComponentService
{
	public interface IComponentService
	{
		Dictionary<int, IEnumerable<Type>> DynamicComponents {  get; }
		void LoadDynamicComponents();

        IDynamicComponents GetTenantDynamicComponent(int IdWWW);
	}
}
