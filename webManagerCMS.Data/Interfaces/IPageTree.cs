using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models.PageContent;

namespace webManagerCMS.Data.Interfaces
{
    public interface IPageTree
    {
        Dictionary<int, Page> Pages { get; }
        int MaxLevel { get; }

        void SetPages(Dictionary<int, Page> pages);

        Page? GetPageByAlias(string alias);

        IEnumerable<Page>? GetPagesByLvl(int lvl);

        IEnumerable<Page>? GetPagesForMenu();
	}
}
