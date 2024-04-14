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
		public IEnumerable<FooterLinkItem> GetFooterLinkItems(int idPage, int placeNumber, FooterLinkSelectType selectType, bool randomOrder, IPageTree pageTree)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectFooterLink"))
			{
				cmd.AddParam("@IDWWWPage", idPage);
				cmd.AddParam("@PlaceNum", placeNumber);
				cmd.AddParam("@Typ", selectType.ToString());
				cmd.AddParam("@IsRandom", randomOrder);
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);

				using (var dataReader = this.ExecReader(cmd))
				{
					int urlIdPage, row = 0;
					string? url;

					while (dataReader.Read())
					{
						urlIdPage = dataReader["LinkID"] as int? ?? 0;
						url = pageTree.GetPageUrl(urlIdPage);

						if (string.IsNullOrEmpty(url))
							url = dataReader["URL"] as string;

						yield return new FooterLinkItem()
						{
							Id = (int)dataReader["IDFooterLink"],
							Prefix = dataReader["Prefix"] as string,
							Suffix = dataReader["Suffix"] as string,
							Title = dataReader["Title"] as string,
							Description = dataReader["DESCR"] as string,
							Picture = dataReader["PictureSmallFileAlias"] as string,
							LinkTarget = dataReader["TargetHTML"] as string,
							Url = url,
							Row = ++row
						};
					}
				}
			}
		}
	}
}
