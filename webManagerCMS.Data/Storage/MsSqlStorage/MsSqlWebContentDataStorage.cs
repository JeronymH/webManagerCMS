using S9.Core.Data.Storage.MsSqlStorage;
using S9.Core.Extensions.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Storage.MsSqlStorage.Access;
using webManagerCMS.Data.Storage.MsSqlStorage.Base;
using webManagerCMS.Data.Models.Page;

namespace webManagerCMS.Data.Storage.MsSqlStorage
{
	public partial class MsSqlWebContentDataStorage : MsSqlDataStorageBase, IWebContentDataStorage
	{

		public MsSqlWebContentDataStorage(MsSqlDataStorageAccess dataAccess, MsSqlDataStorageSettings settings) : base(dataAccess, settings)
		{ }

        //TODO: dodělat
        public Page GetHomePage(int idPage)
        {
            using (var cmd = this.NewCommandProc("dbo.pubSelectHomePageID"))
            {
                cmd.AddParam("@IDWWWRoot", idPage);
                cmd.AddParam("@IDWWW", this.IdWWW);

                using (var dataReader = this.ExecReader(cmd))
                {
                    return null;
                }
            }
        }
    }
}
