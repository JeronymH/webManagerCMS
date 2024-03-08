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
        public Page? GetHomePage()
        {
            using (var cmd = this.NewCommandProc("dbo.pubSelectHomePageID"))
            {
                cmd.AddParam("@IDWWW", this.IdWWW);
                cmd.AddParam("@IDWWWRoot", this.IdWWWRoot);

                using (var dataReader = this.ExecReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        return new Page()
                        {
                            Id = (int)dataReader["IDWWWPage"],
                            IdDB = (int)dataReader["IDWWWPage"],
                            IdPageType = (int)dataReader["IDPageType"],
                            Parent = (int)dataReader["Parent"],
                            MySort = (int)dataReader["MySort"],
                            Lvl = (int)dataReader["lvl"],
                            TemplateNum = (int)dataReader["IDTemplateNum"],
                            Name = dataReader["Name"] as string,
                            Description = dataReader["DESCR"] as string,
                            Url = dataReader["URL"] as string,
                            PageAlias = dataReader["PageAlias"] as string,
                            IsHomePage = (bool)dataReader["IsHomePage"],
                            VisibleInTree = (bool)dataReader["VisibleInTree"],
                        };
                    }
                    return null;
                }
            }
        }
        public Page? GetPage(string pageAlias)
        {
            using (var cmd = this.NewCommandProc("dbo.pubSelectPageAlias"))
            {
                cmd.AddParam("@IDWWW", this.IdWWW);
                cmd.AddParam("@IDWWWRoot", this.IdWWWRoot);
                cmd.AddParam("@PageAlias", pageAlias);
                if (this.IsAdminView)
                    cmd.AddParam("@IsAdmin", 1);
                else
                    cmd.AddParam("@IsAdmin", null);

                using (var dataReader = this.ExecReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        return new Page()
                        {
                            Id = (int)dataReader["IDWWWPage"],
                            IdDB = (int)dataReader["IDWWWPage"],
                            IdPageType = (int)dataReader["IDPageType"],
                            Parent = (int)dataReader["Parent"],
                            MySort = (int)dataReader["MySort"],
                            Lvl = (int)dataReader["lvl"],
                            TemplateNum = (int)dataReader["IDTemplateNum"],
                            Name = dataReader["Name"] as string,
                            Description = dataReader["DESCR"] as string,
                            Url = dataReader["URL"] as string,
                            PageAlias = dataReader["PageAlias"] as string,
                            IsHomePage = (bool)dataReader["IsHomePage"],
                            VisibleInTree = (bool)dataReader["VisibleInTree"],
                        };
                    }
                    return null;
                }
            }
        }
        public Page? GetAlias(int step, int idPage, int idAliasTableName, string alias)
        {
            using (var cmd = this.NewCommandProc("dbo.pubSelectAlias"))
            {
                cmd.AddParam("@IDWWWPage", idPage);
                cmd.AddParam("@IDWWWRoot", this.IdWWWRoot);
                cmd.AddParam("@AliasValue", alias);
                cmd.AddParam("@Step", step);
                cmd.AddParam("@IDAliasTableName", step);

                using (var dataReader = this.ExecReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        return new Page()
                        {
                            Id = (int)dataReader["IDWWWPage"],
                            IdDB = (int)dataReader["IDWWWPage"],
                            IdPageType = (int)dataReader["IDPageType"],
                            Parent = (int)dataReader["Parent"],
                            MySort = (int)dataReader["MySort"],
                            Lvl = (int)dataReader["lvl"],
                            TemplateNum = (int)dataReader["IDTemplateNum"],
                            Name = dataReader["Name"] as string,
                            Description = dataReader["DESCR"] as string,
                            Url = dataReader["URL"] as string,
                            PageAlias = dataReader["PageAlias"] as string,
                            IsHomePage = (bool)dataReader["IsHomePage"],
                            VisibleInTree = (bool)dataReader["VisibleInTree"],
                        };
                    }
                    return null;
                }
            }
        }
    }
}
