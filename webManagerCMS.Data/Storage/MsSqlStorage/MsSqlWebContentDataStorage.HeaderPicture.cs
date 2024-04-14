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
		public HeaderPictureData? GetHeaderPicture(int idPage, int placeNumber, HeaderPictureSelectType selectType, int pictureNumber, bool randomOrder)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectHeaderPicture"))
			{
				cmd.AddParam("@IDWWWPage", idPage);
				cmd.AddParam("@PlaceNum", placeNumber);
				cmd.AddParam("@Typ", selectType.ToString());
				cmd.AddParam("@PicNum", pictureNumber);
				cmd.AddParam("@IsRandom", randomOrder);
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);

				using (var dataReader = this.ExecReader(cmd))
				{
					if (dataReader.Read())
					{
						return new HeaderPictureData()
						{
							Id = (int)dataReader["IDHeaderPicture"],
							IdPage = (int)dataReader["IDWWWPage"],
							Title = dataReader["Title"] as string,
							Subtitle = dataReader["SubTitle"] as string,
							Description = dataReader["DESCR"] as string,
							Picture = dataReader["FileAlias1"] as string,
							PictureFull = dataReader["FullURLFileAlias"] as string,
							ObjectCount = (int)dataReader["CNT"],
							Row = 1
						};
					}
				}
			}

			return null;
		}

		public IEnumerable<HeaderPictureData> GetHeaderPictures(int idPage, int placeNumber, HeaderPictureSelectType selectType, bool randomOrder, int maxNumberOfItems)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectHeaderPicture"))
			{
				cmd.AddParam("@IDWWWPage", idPage);
				cmd.AddParam("@PlaceNum", placeNumber);
				cmd.AddParam("@Typ", selectType.ToString());
				cmd.AddParam("@PicNum", null);
				cmd.AddParam("@IsRandom", randomOrder);
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);

				using (var dataReader = this.ExecReader(cmd))
				{
					int row = 0;
					string? Picture;

					while (dataReader.Read() && row < maxNumberOfItems)
					{
						Picture = dataReader["FileAlias1"] as string;
						if (!string.IsNullOrEmpty(Picture))
						{
							yield return new HeaderPictureData()
							{
								Id = (int)dataReader["IDHeaderPicture"],
								IdPage = (int)dataReader["IDWWWPage"],
								Title = dataReader["Title"] as string,
								Subtitle = dataReader["SubTitle"] as string,
								Description = dataReader["DESCR"] as string,
								Picture = dataReader["FileAlias1"] as string,
								PictureFull = dataReader["FullURLFileAlias"] as string,
								ObjectCount = (int)dataReader["CNT"],
								Row = ++row
							};
						}
					}
				}
			}
		}
	}
}
