using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Tenants;

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

        IEnumerable<Page>? GetPagesForMenu(string alias);

        public string GetPageUrl(Page page);

        public string GetPageUrl(string pageAlias);

        public string GetPageUrl(int idPage);
	}
}
