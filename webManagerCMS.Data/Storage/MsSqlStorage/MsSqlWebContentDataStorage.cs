using S9.Core.Data.Storage.MsSqlStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Storage.MsSqlStorage.Access;
using webManagerCMS.Data.Storage.MsSqlStorage.Base;

namespace webManagerCMS.Data.Storage.MsSqlStorage
{
	public partial class MsSqlWebContentDataStorage : MsSqlDataStorageBase, IWebContentDataStorage
	{

		public MsSqlWebContentDataStorage(MsSqlDataStorageAccess dataAccess, MsSqlDataStorageSettings settings) : base(dataAccess, settings)
		{ }

	}
}
