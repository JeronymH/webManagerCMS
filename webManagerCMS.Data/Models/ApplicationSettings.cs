using System;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Data.Models
{
    public class ApplicationSettings
	{
        /// <summary>
        /// Indication whether the web development behavior is enabled.
        /// Set it always to false in production!
        /// It makes some changes suitable when the web is being developed.
        /// </summary>
        public bool WebDevelopmentBehaviorEnabled { get; set; }

        public Dictionary<string, Tenant> Tenants { get; set; }
	}
}
