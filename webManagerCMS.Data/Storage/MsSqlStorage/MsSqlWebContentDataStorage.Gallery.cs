using S9.Core.Extensions.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models.PageContent;
using static System.Net.Mime.MediaTypeNames;

namespace webManagerCMS.Data.Storage.MsSqlStorage
{
	partial class MsSqlWebContentDataStorage
	{

		public int GetGalleryCountRow(int idPage, int idPlugin)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectGallery1"))
			{
				cmd.AddParam("@IDWWWPage", idPage);
				cmd.AddParam("@IDWWWPageContent", idPlugin);
				cmd.AddParam("@PageN", 1);
				cmd.AddParam("@Parent", Convert.ToInt32(0));
				cmd.AddParam("@PageSize", 1);
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				cmd.AddParam("@OnlyCount", 1);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);
				if (this.FilterPluginsByTimeIntervals)
					cmd.AddParam("@FilterByIntervals", 1);

				using (var dataReader = this.ExecReader(cmd))
				{
					if (dataReader.Read())
					{
						return (dataReader["CNT"] as int?) ?? 0;
					}
					return 0;
				}
			}
		}

		public int GetGalleryCountRowPicture(int idDetail)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectGallery1Picture"))
			{
				cmd.AddParam("@IDGallery1", idDetail);
				cmd.AddParam("@OnlyCount", Convert.ToInt32(1));
				cmd.AddParam("@PageN", 1);
				cmd.AddParam("@PageSize", 1);
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);

				using (var dataReader = this.ExecReader(cmd))
				{
					if (dataReader.Read())
					{
						return (dataReader["CNT"] as int?) ?? 0;
					}
					return 0;
				}
			}
		}

		public IEnumerable<GalleryRow> GetGalleryRows(int idPage, int idPlugin, int pageSize, int templateNumber)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectGallery1"))
			{
				cmd.AddParam("@IDWWWPage", idPage);
				cmd.AddParam("@IDWWWPageContent", idPlugin);
				cmd.AddParam("@PageN", 1);
				cmd.AddParam("@Parent", Convert.ToInt32(0));
				cmd.AddParam("@PageSize", pageSize);
				cmd.AddParam("@TemplateNum", templateNumber);
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				cmd.AddParam("@OnlyCount", Convert.ToInt32(0));
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);
				if (this.FilterPluginsByTimeIntervals)
					cmd.AddParam("@FilterByIntervals", 1);

				using (var dataReader = this.ExecReader(cmd))
				{
					int row = 0;

					while (dataReader.Read())
					{
						yield return new GalleryRow()
						{
							Id = (int)dataReader["IDGallery1"],
							AliasValue = dataReader["AliasValue"] as string,
							Title = dataReader["Title"] as string,
							Subtitle = dataReader["SubTitle"] as string,
							Description = dataReader["DESCR"] as string,
							Perex = dataReader["Perex"] as string,
							Note = dataReader["Note"] as string,
							Picture1 = dataReader["PictureSmallFileAlias"] as string,
							Picture2 = dataReader["PictureSmallFileAlias1"] as string,
							Picture3 = dataReader["PictureSmallFileAlias2"] as string,
							PictureCount = this.IsAdminView ? (int)dataReader["CntPicAdmin"] : (int)dataReader["CntPicPublic"],
							ReflectDate = dataReader["ReflectDate"] as DateTime?,
							Row = ++row
						};
					}
				}
			}
		}

		public int GetGalleryDetailId(int idPage, int idPlugin)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectGallery1FirstID"))
			{
				cmd.AddParam("@IDWWWPage", idPage);
				cmd.AddParam("@IDWWWPageContent", idPlugin);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);
				if (this.FilterPluginsByTimeIntervals)
					cmd.AddParam("@FilterByIntervals", 1);

				using (var dataReader = this.ExecReader(cmd))
				{
					if (dataReader.Read())
					{
						return (dataReader["IDGallery1"] as int?) ?? 0;
					}
					return 0;
				}
			}
		}

		public IEnumerable<GalleryRowPicture> GetGalleryRowPictures(int idDetail, int pageSize, int templateNumber, bool randomOrder)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectGallery1Picture"))
			{
				cmd.AddParam("@IDGallery1", idDetail);
				cmd.AddParam("@PageSize", pageSize);
				cmd.AddParam("@IsRandom", randomOrder ? 1 : 0);
				cmd.AddParam("@TemplateNum", templateNumber);
				cmd.AddParam("@PageN", 1);
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				cmd.AddParam("@OnlyCount", Convert.ToInt32(0));
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);

				using (var dataReader = this.ExecReader(cmd))
				{
					int row = 0;

					while (dataReader.Read())
					{
						yield return new GalleryRowPicture()
						{
							Id = (int)dataReader["IDGallery1Picture"],
							IdRow = (int)dataReader["IDGallery1"],
							Title = dataReader["Title"] as string,
							Subtitle = dataReader["SubTitle"] as string,
							Description = dataReader["Descr"] as string,
							Picture1 = dataReader["FileAlias1"] as string,
							Picture2 = dataReader["FileAlias2"] as string,
							Picture3 = dataReader["FileAlias3"] as string,
							Author = dataReader["Author"] as string,
							ReflectDate = dataReader["ReflectDate"] as DateTime?,
							Row = ++row
						};
					}
				}
			}
		}

		public GalleryRow? GetGalleryDetailRow(int idPage, int idPlugin, int idDetail, int templateNumber)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectGallery1Detail"))
			{
				cmd.AddParam("@IDWWWPage", idPage);
				cmd.AddParam("@IDWWWPageContent", idPlugin);
				cmd.AddParam("@IDGallery1", idDetail);
				cmd.AddParam("@TemplateNum", templateNumber);
				cmd.AddParam("@IDWWW", this.IdWWW);
				cmd.AddParam("@IDLanguage", this.IdLanguage);
				cmd.AddParam("@IsAdmin", this.IsAdminView ? 1 : null);
				if (this.FilterPluginsByTimeIntervals)
					cmd.AddParam("@FilterByIntervals", 1);

				using (var dataReader = this.ExecReader(cmd))
				{
					if (dataReader.Read())
					{
						return new GalleryRow()
						{
							Id = (int)dataReader["IDGallery1"],
							Title = dataReader["Title"] as string,
							Subtitle = dataReader["SubTitle"] as string,
							Description = dataReader["DESCR"] as string,
							Perex = dataReader["Perex"] as string,
							Note = dataReader["Note"] as string,
							Picture1 = dataReader["PictureSmallFileAlias"] as string,
							Picture2 = dataReader["PictureSmallFileAlias1"] as string,
							Picture3 = dataReader["PictureSmallFileAlias2"] as string,
							PictureCount = this.IsAdminView ? (int)dataReader["CntPicAdmin"] : (int)dataReader["CntPicPublic"],
							ReflectDate = dataReader["ReflectDate"] as DateTime?,
							Row = 0
						};
					}
					return null;
				}
			}
		}
	}
}
