using S9.Core.Data.Storage.MsSqlStorage;
using S9.Core.Extensions.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Storage.MsSqlStorage.Access;
using webManagerCMS.Data.Storage.MsSqlStorage.Base;

namespace webManagerCMS.Data.Storage.MsSqlStorage
{
    public class MsSqlSystemDataStorage : MsSqlDataStorageBase, ISystemDataStorage
    {
        public MsSqlSystemDataStorage(MsSqlDataStorageAccess dataAccess, MsSqlDataStorageSettings settings) : base(dataAccess, settings)
        { }

        public WWWSettings GetWWWSettings(int idWWW, string webBaseUrl, string mutationAlias, bool webDevelopmentBehaviorEnabled)
        {
            using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectWWWSettings"))
            {
                cmd.AddParam("@IDWWW", idWWW);
                cmd.AddParam("@URL", webBaseUrl);
                cmd.AddParam("@RootAlias", mutationAlias);
                cmd.AddParam("@IsDevelopment", webDevelopmentBehaviorEnabled);

                using (var dataReader = this.ExecReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        return new WWWSettings()
                        {
                            IdWWWRoot = (int)dataReader["IDWWWRoot"],
                            IdLanguage = (int)dataReader["IDLanguage"],
                            MutationAlias = (string)dataReader["AliasInURL"],
                            PageSuffix = (string)dataReader["PageSuffix"]
                        };
                    }
                    return null;
                }
            }
        }
    }
}
