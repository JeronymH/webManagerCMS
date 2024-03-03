using S9.Core.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Data.Storage
{
	public interface IDataStorageAccess
	{
		ITenantAccess TenantAccess { get; }

        ISystemDataStorage SystemDataStorage { get; }

        ILogDataStorage LogDataStorage { get; }

		IWebContentDataStorage WebContentDataStorage { get; }
	}
}
