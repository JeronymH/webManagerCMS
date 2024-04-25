using S9.Core.Extensions.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models.PageContent;

namespace webManagerCMS.Data.Storage.MsSqlStorage
{
	partial class MsSqlWebContentDataStorage
	{
		public DocH1TextData? GetDocH1TextData(int idPage, int idPlugin)
		{
			using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectDocH1Text"))
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
						return new DocH1TextData()
						{
							Title = dataReader["Note_Title"] as string,
							Subtitle = dataReader["Note_SubTitle"] as string,
							Perex = dataReader["Note_Perex"] as string,
							Note = dataReader["Note"] as string
						};
					}
				}
			}

			return null;
		}
	}
}
