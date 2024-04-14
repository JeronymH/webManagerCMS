using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models;

namespace webManagerCMS.Data.Tenants
{
	public interface ITenant
	{
		int IdWWW { get; set; }

		WWWSettings WWWSettings { get; set; }

		string DomainName { get; set; }

		bool IsHttpSecured { get; set; }

		bool IsAdminView { get; set; }

		bool FilterPluginsByTimeIntervals { get; set; }

		bool WebDevelopmentBehaviorEnabled { get; set; }

		string WebBaseUrl { get; set; }

		string RootFullPath { get; set; }

		string ComponentsFolderName { set; }

		Dictionary<string, Type> Components { get; set; }

        IDynamicComponents DynamicComponents { get; set; }

		string GetWebBaseUrl();
		string GetComponentsPath();
		string GetRootAlias();
		Type? GetComponent(string componentName);
	}
}
