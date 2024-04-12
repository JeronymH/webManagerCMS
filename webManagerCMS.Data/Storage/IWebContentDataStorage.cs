﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Models.PageContent;

namespace webManagerCMS.Data.Storage
{
	public interface IWebContentDataStorage
	{
        Page? GetHomePage();
        Page? GetPage(string? pageAlias);
        Alias? GetAlias(int step, int idPage, int idAliasTableName, int templateNumber, string alias);
        Dictionary<int, Page> LoadPagesDictionary(bool fromCache);
		IEnumerable<PageContentPlugin> LoadPageContent(int pageId, int contentColumnId, PageContentPluginType? onlyOnePlugin);
		IEnumerable<PageContentPlugin> LoadPageContent(int pageId, int contentColumnId);

		DocHtmlData? GetDocHtmlData(int idPage, int idPlugin);

		IEnumerable<FileAlias> LoadFileAliases(HashSet<int> fileIds);

		IEnumerable<TreeDisplayDefinedRow> GetTreeDisplayDefinedRows(int idPage, int idPlugin, int pageSize, int templateNumber, IPageTree pageTree);

		IEnumerable<TreeDisplayDefinedRowItem> GetTreeDisplayDefinedRowItems(int idRow, int templateNumber, IPageTree pageTree);
	}
}
