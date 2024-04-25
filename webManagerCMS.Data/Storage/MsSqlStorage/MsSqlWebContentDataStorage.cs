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
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Caching;
using System.Collections;
using S9.Core.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                        // Empty string for roots and null aliases
                        var alias = ((dataReader["IsHomePage"] as short?) ?? 0) == 1 ? System.String.Empty : (dataReader["PageAlias"] as string) ?? System.String.Empty;

                        return new Page()
                        {
                            Id = (int)dataReader["IDWWWPage"],
                            IdDB = (int)dataReader["IDWWWPage"],
                            IdPageType = (int)dataReader["IDPageType"],
                            Parent = dataReader["Parent"] as int?,
                            MySort = (dataReader["MySort"] as int?) ?? 0,
                            Lvl = (dataReader["lvl"] as int?) ?? 0,
                            TemplateNum = (dataReader["IDTemplateNum"] as int?) ?? 0,
                            Name = dataReader["Name"] as string,
                            Description = dataReader["DESCR"] as string,
                            Url = dataReader["URL"] as string,
                            PageAlias = alias,
                            IsHomePage = (short)dataReader["IsHomePage"] == 1 ? true : false,
                            VisibleInTree = (short)dataReader["VisibleInTree"] == -1 ? true : false,

                            SEOTitle = dataReader["Title"] as string,
                            SEOSubtitle = dataReader["SubTitle"] as string,
                            SEOKeywords = dataReader["Keywords"] as string,
                            SEODescription = dataReader["Description"] as string,
                            SEOSocialMetaTags = dataReader["SocialMetaTags"] as string,
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
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);

				using (var dataReader = this.ExecReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        // Empty string for roots and null aliases
                        var alias = ((dataReader["IsHomePage"] as short?) ?? 0) == 1 ? System.String.Empty : (dataReader["PageAlias"] as string) ?? System.String.Empty;

                        return new Page()
                        {
                            Id = (int)dataReader["IDWWWPage"],
                            IdDB = (int)dataReader["IDWWWPage"],
                            IdPageType = (int)dataReader["IDPageType"],
                            Parent = dataReader["Parent"] as int?,
                            MySort = (dataReader["MySort"] as int?) ?? 0,
                            Lvl = (dataReader["lvl"] as int?) ?? 0,
                            TemplateNum = (dataReader["IDTemplateNum"] as int?) ?? 0,
                            Name = dataReader["Name"] as string,
                            Description = dataReader["DESCR"] as string,
                            Url = dataReader["URL"] as string,
                            PageAlias = alias,
                            IsHomePage = (short)dataReader["IsHomePage"] == 1 ? true : false,
                            VisibleInTree = (int)dataReader["VisibleInTree"] == -1 ? true : false,

                            SEOTitle = dataReader["Title"] as string,
                            SEOSubtitle = dataReader["SubTitle"] as string,
                            SEOKeywords = dataReader["Keywords"] as string,
                            SEODescription = dataReader["Description"] as string,
                            SEOSocialMetaTags = dataReader["SocialMetaTags"] as string,
                        };
                    }
                    return null;
                }
            }
        }

        public Page GetPageFromHistory(string? pageAlias)
        {
            using (var cmd = this.NewCommandProc("dbo.pubSelectPageAliasHistory"))
            {
                cmd.AddParam("@IDWWW", this.IdWWW);
                cmd.AddParam("@IDWWWRoot", this.IdWWWRoot);
                cmd.AddParam("@PageAlias", pageAlias);
				cmd.AddParam("@maxHistoryPeriod", this.Tenant.WWWSettings.PageAliasMaxHistoryPeriod);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);

				using (var dataReader = this.ExecReader(cmd))
                {
                    int historyIdPage = 0;
                    string historyPageAlias = "";

					while (dataReader.Read())
					{
                        historyIdPage = (int)dataReader["IDWWWPage"];
                        historyPageAlias = dataReader["PageAlias"] as string;

                        if (!string.IsNullOrEmpty(historyPageAlias) && historyIdPage > 0)
                        {
                            break;
                        }
					}
					return new Page()
					{
						Id = historyIdPage,
						PageAlias = historyPageAlias,
					}; ;
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

        public Alias? GetAliasFromHistory(int step, int idPage, int idAliasTableName, string alias)
        {
            using (var cmd = this.NewCommandProc("dbo.pubSelectAliasHistory"))
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
				cmd.AddParam("@maxHistoryPeriod", this.Tenant.WWWSettings.PageAliasMaxHistoryPeriod);

				using (var dataReader = this.ExecReader(cmd))
				{
					int historyIdAlias = 0, idTableName = 0;
					string historyAlias = "";

					while (dataReader.Read())
					{
						historyIdAlias = (int)dataReader["IDWWWRootAlias"];
						historyAlias = dataReader["AliasValue"] as string;
						idTableName = (dataReader["NextGlobal_IDAliasTableName"] as int?) ?? 0;

						if (!string.IsNullOrEmpty(historyAlias) && historyIdAlias > 0)
						{
							break;
						}
					}
					return new Alias()
					{
						Id = historyIdAlias,
						Name = historyAlias,
						IdTableName = idTableName
					}; ;
				}
			}
        }

        public Dictionary<int, Page> LoadPagesDictionary(bool fromCache)
        {
            Func<Dictionary<int, Page>> getPagesDictionaryFunc = () => this.LoadPagesDictionary();

            return fromCache ?
                this.CacheStorageAccess.CacheStorage.GetItem<Dictionary<int, Page>>(this.IdWWW, this.IdWWWRoot, SitePartScopedCacheItemKey.WebContentPages, new GetDataForCacheDelegate<Dictionary<int, Page>>(getPagesDictionaryFunc))
                : getPagesDictionaryFunc();
        }

        private Dictionary<int, Page> LoadPagesDictionary()
        {
            return this.LoadPagesFromDb().ToDictionary(p => p.IdDB, p => p);
        }

        private IEnumerable<Page> LoadPagesFromDb()
        {
            using (var cmd = this.NewCommandProc("dbo.pubSelectPageTree"))
            {
                cmd.AddParam("@IDWWW", this.IdWWW);
                cmd.AddParam("@IDWWWRoot", this.IdWWWRoot);
                cmd.AddParam("@IDRow", Convert.ToInt32(0));
                cmd.AddParam("@Typ", "TREE");
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);

				using (var dataReader = this.ExecReader(cmd))
                {
                    while (dataReader.Read())
                    {
                        // Empty string for roots and null aliases
                        var alias = ((dataReader["IsHomePage"] as short?) ?? 0) == 1 ? System.String.Empty : (dataReader["PageAlias"] as string) ?? System.String.Empty;

                        yield return new Page()
                        {
                            Id = (int)dataReader["IDWWWPage"],
                            IdDB = (int)dataReader["IDWWWPageDB"],
                            IdPageType = (int)dataReader["IDPageType"],
                            Parent = dataReader["Parent"] as int?,
                            MySort = (dataReader["MySort"] as int?) ?? 0,
                            Lvl = (dataReader["lvl"] as int?) ?? 0,
                            TemplateNum = (dataReader["IDTemplateNum"] as int?) ?? 0,
                            Name = dataReader["Name"] as string,
                            Description = dataReader["DESCR"] as string,
                            Url = dataReader["URL"] as string,
                            PageAlias = alias,
                            IsHomePage = (short)dataReader["IsHomePage"] == 1 ? true : false,
                            VisibleInTree = (int)dataReader["VisibleInTree"] == -1 ? true : false,
                        };
                    }
                }
            }
        }

		public IEnumerable<PageContentPlugin> LoadPageContent(int pageId, int contentColumnId, PageContentPluginType? onlyOnePlugin)
		{
			using (var cmd = this.NewCommandProc("dbo.pubSelectWWWPageContent"))
			{
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDWWWPage", pageId);
				cmd.AddParam("@IDWWWContentCols", contentColumnId);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				if (onlyOnePlugin != null) cmd.AddParam("@OnlyOneClass", onlyOnePlugin.Value);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);

				using (var dataReader = this.ExecReader(cmd))
				{
                    int templateNumber;
					while (dataReader.Read())
					{
                        templateNumber = (int)dataReader["IDTemplateNum"];
                        if (templateNumber < 0) templateNumber = 0;

						yield return new PageContentPlugin(
								(PageContentPluginType)Enum.ToObject(typeof(PageContentPluginType), (int)dataReader["IDClassCollection"]),
								templateNumber,
                                0,
                                (int)dataReader["IDWWWPageContent"],
                                (int)dataReader["IDWWWPage"],
                                dataReader["Title"] as string,
                                dataReader["Subtitle"] as string,
                                dataReader["DESCR"] as string,
                                dataReader["PictureFileAlias"] as string,
                                null
                            );
                    }
				}
			}
		}

		public IEnumerable<PageContentPlugin> LoadPageContent(int pageId, int contentColumnId)
		{
            return LoadPageContent(pageId, contentColumnId, null);
		}
	}
}
