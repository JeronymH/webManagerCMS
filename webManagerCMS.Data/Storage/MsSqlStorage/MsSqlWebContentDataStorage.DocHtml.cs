using S9.Core.Extensions;
using S9.Core.Extensions.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models.PageContent;

namespace webManagerCMS.Data.Storage.MsSqlStorage
{
	public partial class MsSqlWebContentDataStorage
	{
		public DocHtmlData? GetDocHtmlData(int idPage, int idPlugin)
		{
			using (var cmd = this.NewCommandProc("dbo.adm_pl_SelectDocHTML1"))
			{
				cmd.AddParam("@IDWWWPage", idPage);
				cmd.AddParam("@IDWWWPageContent", idPlugin);
				if (this.IsAdminView)
					cmd.AddParam("@IsAdmin", 1);
				else
					cmd.AddParam("@IsAdmin", null);

				using (var dataReader = this.ExecReader(cmd))
				{
					if (dataReader.Read())
					{
						return new DocHtmlData()
						{
							Id = (int)dataReader["IDDocHTML1"],
							XmlContent = dataReader["Note"] as string
						};
					}
				}
			}

			return null;
		}
	}
}
