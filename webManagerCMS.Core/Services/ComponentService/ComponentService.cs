using Microsoft.AspNetCore.Components;
using System.Reflection;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Core.Services.ComponentService
{
	public class ComponentService : IComponentService
	{
		public Dictionary<int, IEnumerable<Type>> DynamicComponents { get; private set; }

		public ComponentService(ApplicationSettings aplicationSettings)
		{
			_applicationSettings = aplicationSettings;
			DynamicComponents = new Dictionary<int, IEnumerable<Type>>();
		}

		private readonly ApplicationSettings _applicationSettings;

		public void LoadDynamicComponents()
		{
			IEnumerable<Type> component;
			string path;
			Assembly assembly;

			foreach (KeyValuePair<string, Tenant> tenant in _applicationSettings.Tenants)
            {
				if (!DynamicComponents.ContainsKey(tenant.Value.IdWWW))
				{
					path = tenant.Value.GetComponentsPath();
					assembly = LoadAssembly(path);

					component = GetTypeWithInterface(assembly);
					DynamicComponents.Add(tenant.Value.IdWWW, component);
				}
			}
		}

		public IDynamicComponents GetTenantDynamicComponent(int IdWWW)
		{
			return DynamicComponents[IdWWW].Select(x => (IDynamicComponents)Activator.CreateInstance(x))
				.SingleOrDefault(x => x.Components.Count > 0);
		}

		private Assembly LoadAssembly(string path)
		{
			var files = Directory.GetFiles(path, "*.dll");
			return files.Select(dll => Assembly.LoadFile(dll)).ToList()[0];
		}

		private IEnumerable<Type> GetTypeWithInterface(Assembly asm)
		{
			var it = typeof(IDynamicComponents);
			return GetLoadableTypes(asm).Where(it.IsAssignableFrom);
		}

		private IEnumerable<Type> GetLoadableTypes(Assembly assembly)
		{
			if (assembly == null) throw new ArgumentNullException("assembly");
			try
			{
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				return e.Types.Where(t => t != null);
			}
		}
	}
}

