using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models;

namespace webManagerCMS.Data.Tenants
{
	public interface ITenantAccess
	{
		ITenant Tenant { get; }

		int IdWWW { get; }

		int IdWWWRoot { get; }

		int IdLanguage { get; }

		SystemLanguageType Language { get; }

		bool IsAdminView { get; }

		bool FilterPluginsByTimeIntervals { get; }
    }
}
