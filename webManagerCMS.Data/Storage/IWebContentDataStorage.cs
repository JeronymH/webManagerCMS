using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models.Page;

namespace webManagerCMS.Data.Storage
{
	public interface IWebContentDataStorage
	{
        Page? GetHomePage();
        Page? GetPage(string pageAlias);
        Page? GetAlias(int step, int idPage, int idAliasTableName, string alias);

    }
}
