using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace webManagerCMS.Data.Tenants
{
    public class Tenant : ITenant
    {
		public int IdWWW { get; set; }

		public string DomainName { get; set; }

		public bool IsHttpSecured { get; set; }

		public string WebBaseUrl { get; set; }

		public string RootFullPath { get; set; }

		public string ComponentsFolderName { get; set; }

		public bool IsSecureZoneEnabled { get; set; }

		public int MaxCountItemInTree { get; set; }

		public int MaxCountItemNodeInTree { get; set; }

		public string PageSuffix { get; set; }

		public Dictionary<string, Type> Components { get; set; }

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
	}
}
