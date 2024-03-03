using S9.Core.Data.Storage.MsSqlStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Storage.MsSqlStorage.Access;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Data.Storage.MsSqlStorage.Base
{
	public abstract class MsSqlDataStorageBase : MsSqlDatabaseAccessPoint
	{
		public MsSqlDataStorageBase(MsSqlDataStorageAccess dataAccess, MsSqlDataStorageSettings settings) : base(settings)
		{
			this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
		}

		private readonly MsSqlDataStorageAccess _dataAccess;

		protected MsSqlDataStorageAccess DataAccess
		{
			get { return this._dataAccess; }
		}

		#region ITenantAccess implementation

		public ITenant Tenant => this.DataAccess.TenantAccess.Tenant;

		public int IdWWW => this.DataAccess.TenantAccess.IdWWW;

		public int IdWWWRoot => this.DataAccess.TenantAccess.IdWWWRoot;

		public int IdLanguage => this.DataAccess.TenantAccess.IdLanguage;

		#endregion
	}
}
