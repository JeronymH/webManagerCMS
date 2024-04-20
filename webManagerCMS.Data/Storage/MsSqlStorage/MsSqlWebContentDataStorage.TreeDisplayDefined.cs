using S9.Core.Extensions.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models.PageContent;

namespace webManagerCMS.Data.Storage.MsSqlStorage
{
	partial class MsSqlWebContentDataStorage
	{
		public IEnumerable<TreeDisplayDefinedRow> GetTreeDisplayDefinedRows(int idPage, int idPlugin, int pageSize, int templateNumber, IPageTree pageTree)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectTreeDisplayDefined1"))
			{
				cmd.AddParam("@IDWWWPage", idPage);
				cmd.AddParam("@IDWWWPageContent", idPlugin);
				cmd.AddParam("@PageN", 1);
				cmd.AddParam("@PageSize", pageSize);
				cmd.AddParam("@TemplateNum", templateNumber);
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				cmd.AddParam("@OnlyCount", Convert.ToInt32(0));
				cmd.AddParam("@IsNote", -1);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);
				if (this.FilterPluginsByTimeIntervals)
					cmd.AddParam("@FilterByIntervals", 1);

				using (var dataReader = this.ExecReader(cmd))
				{
					int urlIdPage, row = 0;
					string url;

					while (dataReader.Read())
					{
						urlIdPage = dataReader["menu_IDWWWPage"] as int? ?? 0;
						url = pageTree.GetPageUrl(urlIdPage);

						if (string.IsNullOrEmpty(url))
							url = dataReader["menu_URL"] as string;

						yield return new TreeDisplayDefinedRow()
						{
							Id = (int)dataReader["IDTreeDisplayDefined1"],
							Title = dataReader["Title"] as string,
							Subtitle = dataReader["SubTitle"] as string,
							Description = dataReader["DESCR"] as string,
							Perex = dataReader["Perex"] as string,
							Note = dataReader["Note"] as string,
							PictureFileAlias = dataReader["PictureSmallFileAlias"] as string,
							Url = url,
							ReflectDate = dataReader["ReflectDate"] as DateTime?,
							Row = ++row
						};
					}
				}
			}
		}

		public IEnumerable<TreeDisplayDefinedRowItem> GetTreeDisplayDefinedRowItems(int idRow, int templateNumber, IPageTree pageTree)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectTreeDisplayDefined1Item"))
			{
				cmd.AddParam("@IDTreeDisplayDefined1", idRow);
				cmd.AddParam("@TemplateNum", templateNumber);
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);


				using (var dataReader = this.ExecReader(cmd))
				{
					int urlIdPage, row = 0;
					string url;

					while (dataReader.Read())
					{
						urlIdPage = dataReader["menu_IDWWWPage"] as int? ?? 0;
						url = pageTree.GetPageUrl(urlIdPage);

						if (string.IsNullOrEmpty(url))
							url = dataReader["menu_URL"] as string;

						yield return new TreeDisplayDefinedRowItem()
						{
							IdRow = (int)dataReader["IDTreeDisplayDefined1"],
							Id = (int)dataReader["IDTreeDisplayDefined1Item"],
							Title = dataReader["Title"] as string,
							Subtitle = dataReader["SubTitle"] as string,
							Description = dataReader["DESCR"] as string,
							Perex = dataReader["Perex"] as string,
							Note = dataReader["Note"] as string,
							PictureFileAlias = dataReader["PictureSmallFileAlias"] as string,
							Url = url,
							Row = ++row
						};
					}
				}
			}
		}
	}
}
