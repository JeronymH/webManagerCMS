using System;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Data.Models
{
    public class ApplicationSettings
	{
		public Dictionary<string, Tenant> Tenants { get; set; }
	}
}
