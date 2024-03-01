using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Tenants
{
	public interface ITenant
	{
		int IdWWW { get; set; }

		int IdWWWRoot { get; set; }

        Dictionary<string, int> IdWWWRoots { get; set; }

		string DomainName { get; set; }

		bool IsHttpSecured { get; set; }

		string WebBaseUrl { get; set; }

		string RootFullPath { get; set; }

		string ComponentsFolderName { set; }

		/// <summary>
		/// Indication whether the tenant has a secured zone (aka business zone)
		/// </summary>
		bool IsSecureZoneEnabled { get; set; }

		/// <summary>
		/// Max number of items allowed in tree
		/// </summary>
		int MaxCountItemInTree { get; set; }

		/// <summary>
		/// Max number of nodes allowed in tree
		/// </summary>
		int MaxCountItemNodeInTree { get; set; }

		string PageSuffix { get; set; }

		Dictionary<string, Type> Components { get; set; }

		//TODO: add DynamicComponents to project
		//IDynamicComponents Components { get; set; }

		string GetWebBaseUrl();
		string GetComponentsPath();
	}
}
