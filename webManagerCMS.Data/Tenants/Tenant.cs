﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Storage;


namespace webManagerCMS.Data.Tenants
{
    public class Tenant : ITenant
    {
		public int IdWWW { get; set; }

		public WWWSettings WWWSettings { get; set; }

        public string DomainName { get; set; }

		public bool IsHttpSecured { get; set; }

		//prepared for AdminView logic - Admin view emables non public data
		public bool IsAdminView { get; set; } = false;

		public bool FilterPluginsByTimeIntervals { get; set; }

		public bool WebDevelopmentBehaviorEnabled { get; set; }

        public string WebBaseUrl { get; set; }

		public string RootFullPath { get; set; }

		public string ComponentsFolderName { get; set; }

		public Dictionary<string, Type> Components { get; set; }

		public IDynamicComponents DynamicComponents { get; set; }

		public string GetWebBaseUrl()
		{
			if (this._WebBaseUrl == null)
				this._WebBaseUrl = string.IsNullOrWhiteSpace(this.WebBaseUrl) ?
					$"http{(this.IsHttpSecured ? "s" : null)}://{this.DomainName}" :
					this.WebBaseUrl;
			return this._WebBaseUrl;
		}
		private string _WebBaseUrl;

		public string GetComponentsPath()
		{
			if (this._ComponentsPath == null)
				this._ComponentsPath = string.IsNullOrWhiteSpace(this._ComponentsPath) ?
					this.RootFullPath + this.ComponentsFolderName : this._ComponentsPath;
			return this._ComponentsPath;

		}
		private string _ComponentsPath;

		public string GetRootAlias()
		{
			string url = "/";
			if (WWWSettings.MutationAlias == "")
				return url;

			url += WWWSettings.MutationAlias + "/";
			return url;
		}

		public Type? GetComponent(string componentName)
		{
			if (string.IsNullOrEmpty(componentName))
				return null;

			if (!Components.ContainsKey(componentName))
				return null;

			return Components[componentName];
		}
	}
}
