using System.Collections.Generic;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS
{
    public class PageTree : IPageTree
    {
        public Dictionary<int, Page> Pages { get; private set; }
        private Dictionary<string, int>? AliasMapper { get; set; } = null;
        private Dictionary<int, List<int>>? LvlMapper { get; set; } = null;

        public int MaxLevel { get; private set; }

        public PageTree(Dictionary<int, Page> pages)
        {
            SetPages(pages);
        }

        public void SetPages(Dictionary<int, Page> pages)
        {
            Pages = pages;
            InitMappers();
        }

        private void InitMappers()
        {
            AliasMapper = new Dictionary<string, int>();
            LvlMapper = new Dictionary<int, List<int>>();

            int PageIdDB, PageLvl;

            foreach (var page in Pages)
            {
                PageIdDB = page.Key;
                PageLvl = page.Value.Lvl;

                if (!AliasMapper.ContainsKey(page.Value.PageAlias))
                    AliasMapper.Add(page.Value.PageAlias, PageIdDB);

                if (LvlMapper.ContainsKey(PageLvl))
                    LvlMapper[PageLvl].Add(PageIdDB);
                else
                    LvlMapper.Add(PageLvl, new List<int> { PageIdDB });
            }
        }

        public Page? GetPageByAlias(string alias)
        {
            return Pages[AliasMapper[alias]];
        }

        public IEnumerable<Page>? GetPagesByLvl(int lvl)
        {
            if (!LvlMapper.ContainsKey(lvl))
                return null;

            List<int> keys = LvlMapper[lvl];

            if (!(keys.Count > 0))
                return null;

            return Pages.Where(x => keys.Contains(x.Key)).Select(x => x.Value);
        }

        public IEnumerable<Page>? GetPagesForMenu()
        {
            return GetPagesByLvl(1).Where(x => x.VisibleInTree);
		}
	}
}
