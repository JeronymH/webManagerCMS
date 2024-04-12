using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Tenants
{
	public interface ITenantAccess
	{
		ITenant Tenant { get; }

		int IdWWW { get; }

		int IdWWWRoot { get; }

		int IdLanguage { get; }

		bool IsAdminView { get; }

		bool FilterPluginsByTimeIntervals { get; }
    }
}
