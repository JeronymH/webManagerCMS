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
using webManagerCMS.Data.Models;
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
                            Parent = (dataReader["Parent"] as int?) ?? 0,
                            MySort = (dataReader["MySort"] as int?) ?? 0,
                            Lvl = (dataReader["lvl"] as int?) ?? 0,
                            TemplateNum = (dataReader["IDTemplateNum"] as int?) ?? 0,
                            Name = dataReader["Name"] as string,
                            Description = dataReader["DESCR"] as string,
                            Url = dataReader["URL"] as string,
                            PageAlias = dataReader["PageAlias"] as string,
                            IsHomePage = (short)dataReader["IsHomePage"] == 1 ? true : false,
                            VisibleInTree = (short)dataReader["VisibleInTree"] == -1 ? true : false,
                        };
                    }
                    return null;
                }
            }
        }
        public Page? GetPage(string? pageAlias)
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
                            Parent = (dataReader["Parent"] as int?) ?? 0,
                            MySort = (dataReader["MySort"] as int?) ?? 0,
                            Lvl = (dataReader["lvl"] as int?) ?? 0,
                            TemplateNum = (dataReader["IDTemplateNum"] as int?) ?? 0,
                            Name = dataReader["Name"] as string,
                            Description = dataReader["DESCR"] as string,
                            Url = dataReader["URL"] as string,
                            PageAlias = dataReader["PageAlias"] as string,
                            IsHomePage = (short)dataReader["IsHomePage"] == 1 ? true : false,
                            VisibleInTree = (int)dataReader["VisibleInTree"] == -1 ? true : false,
                        };
                    }
                    return null;
                }
            }
        }
        public Alias? GetAlias(int step, int idPage, int idAliasTableName, int templateNumber, string alias)
        {
            using (var cmd = this.NewCommandProc("dbo.pubSelectAlias"))
            {
                if (idAliasTableName > 0)
                {
                    cmd.AddParam("@IDWWWPage", 0);
                    cmd.AddParam("@IDAliasTableName", step);
                } 
                else
                {
                    cmd.AddParam("@IDWWWPage", idPage);
                }

                cmd.AddParam("@IDWWWRoot", this.IdWWWRoot);
                cmd.AddParam("@AliasValue", alias);
                cmd.AddParam("@Step", step);

                using (var dataReader = this.ExecReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        int templateNum = (int)dataReader["IDTemplateNum"];
                        if (templateNum < 0)
                            templateNum = templateNumber;

                        return new Alias()
                        {
                            IsHomePage = false,
                            Name = dataReader["Name"] as string,
                            Id = (int)dataReader["IDRecord"],
                            IdDB = (int)dataReader["IDRecord"],
                            IdPageContent = (int)dataReader["IDRecordPageContent"],
                            IdContentCols = (int)dataReader["IDContentCols"],
                            IdTemplateNum = templateNum,
                            IdState = (int)dataReader["IDState"],
                            IdTableName = (int)dataReader["IDAliasTableName"],
                            IdClassCollection = (int)dataReader["IDClassCollection"]
                        };
                    }
                    return null;
                }
            }
        }
    }
}
