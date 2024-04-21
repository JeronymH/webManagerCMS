using System.Collections.Generic;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Core.PageContentNS
{
    public class PageTree : IPageTree
    {
        public Dictionary<int, Page> Pages { get; private set; }
        private Dictionary<string, int>? AliasMapper { get; set; } = null;
        private Dictionary<int, List<int>>? LvlMapper { get; set; } = null;

        public int MaxLevel { get; private set; }

		private readonly ITenantAccess? _tenantAccess;

		public PageTree(Dictionary<int, Page> pages, ITenantAccess tenantAccess)
        {
            _tenantAccess = tenantAccess;

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
            if (!AliasMapper.ContainsKey(alias))
                return null;

			return Pages[AliasMapper[alias]];
        }

        public IEnumerable<Page> GetPagesByLvl(int lvl)
        {
            if (!LvlMapper.ContainsKey(lvl))
                return Enumerable.Empty<Page>();

            List<int> keys = LvlMapper[lvl];

            if (!(keys.Count > 0))
                return Enumerable.Empty<Page>();

            return Pages.Where(x => keys.Contains(x.Key)).Select(x => x.Value);
        }

        public IEnumerable<Page> GetPagesForMenu()
        {
            return GetPagesByLvl(1).Where(x => x.VisibleInTree);
		}

        public IEnumerable<Page> GetPagesForMenu(string alias)
        {
            Page? parentPage = GetPageByAlias(alias);

            if (parentPage == null) return Enumerable.Empty<Page>();

            int parent = parentPage.Id;
			int level = parentPage.Lvl + 1;

			return GetPagesByLvl(level).Where(x => x.VisibleInTree && x.Parent == parent);
		}

		public string GetPageUrl(Page page)
		{
            if (!string.IsNullOrEmpty(page.Url))
				return GetPageUrl(page.Url, true);

			return GetPageUrl(page.PageAlias, false);
		}

		public string GetPageUrl(string pageUrl, bool absolutUrl)
		{
            if (absolutUrl) return pageUrl;

            string url = _tenantAccess.Tenant.GetRootAlias();

			if (string.IsNullOrEmpty(pageUrl))
				return url;

			url += pageUrl + _tenantAccess.Tenant.WWWSettings.PageSuffix;
			return url;
		}

		public string GetPageUrl(int idPage)
		{
            if (idPage == 0)
                return "";

            if (!Pages.TryGetValue(idPage, out var page))
                return "";

			return GetPageUrl(page);
		}
	}
}
