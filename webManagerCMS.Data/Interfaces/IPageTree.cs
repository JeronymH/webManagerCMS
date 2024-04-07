﻿using System;
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

        public Page? GetPageByAlias(string alias);

        public IEnumerable<Page>? GetPagesByLvl(int lvl);
    }
}
