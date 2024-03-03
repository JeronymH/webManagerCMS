using S9.Core.Data.Storage;
using S9.Core.Data.Storage.MsSqlStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Data.Storage.MsSqlStorage.Access
{
	public partial class MsSqlDataStorageAccess : IDataStorageAccess
	{
		private readonly MsSqlDataStorageSettings _settings;
		private readonly MsSqlDataStorageSettings _settingsLogDb;
		private readonly ITenantAccess _tenantAccess;

		public MsSqlDataStorageAccess(MsSqlDataStorageSettings settings, MsSqlDataStorageSettings settingsLogDb, ITenantAccess tenantAccess)
		{
			this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
			this._settingsLogDb = settingsLogDb ?? throw new ArgumentNullException(nameof(settingsLogDb));
			this._tenantAccess = tenantAccess ?? throw new ArgumentNullException(nameof(tenantAccess));
		}

		public ISystemDataStorage SystemDataStorage
		{
			get
			{
				this._SystemDataStorage ??= new MsSqlSystemDataStorage(this, this._settings);
				return this._SystemDataStorage;
			}
		}
		private ISystemDataStorage _SystemDataStorage;

		public IWebContentDataStorage WebContentDataStorage
		{
			get
			{
				this._WebContentDataStorage ??= new MsSqlWebContentDataStorage(this, this._settings);
				return this._WebContentDataStorage;
			}
		}
		private IWebContentDataStorage _WebContentDataStorage;

		public ITenantAccess TenantAccess
		{
			get { return _tenantAccess; }
		}

		public ILogDataStorage LogDataStorage
		{
			get
			{
				this._LogDataStorage ??= new MsSqlLogDataStorage(this, this._settingsLogDb);
				return this._LogDataStorage;
			}
		}
		private ILogDataStorage _LogDataStorage;
	}
}
