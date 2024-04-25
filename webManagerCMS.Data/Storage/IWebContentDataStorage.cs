using System;
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
        Page GetPageFromHistory(string? pageAlias);
        Alias? GetAlias(int step, int idPage, int idAliasTableName, int templateNumber, string alias);
        Alias? GetAliasFromHistory(int step, int idPage, int idAliasTableName, string alias);
        Dictionary<int, Page> LoadPagesDictionary(bool fromCache);
		IEnumerable<PageContentPlugin> LoadPageContent(int pageId, int contentColumnId, PageContentPluginType? onlyOnePlugin);
		IEnumerable<PageContentPlugin> LoadPageContent(int pageId, int contentColumnId);

		DocH1TextData? GetDocH1TextData(int idPage, int idPlugin);

		DocHtmlData? GetDocHtmlData(int idPage, int idPlugin);
		IEnumerable<FileAlias> LoadFileAliases(HashSet<int> fileIds);

		IEnumerable<TreeDisplayDefinedRow> GetTreeDisplayDefinedRows(int idPage, int idPlugin, int pageSize, int templateNumber, IPageTree pageTree);
		IEnumerable<TreeDisplayDefinedRowItem> GetTreeDisplayDefinedRowItems(int idRow, int templateNumber, IPageTree pageTree);

		int GetGalleryCountRow(int idPage, int idPlugin);
		int GetGalleryCountRowPicture(int idDetail);
		int GetGalleryDetailId(int idPage, int idPlugin);
		IEnumerable<GalleryRow> GetGalleryRows(int idPage, int idPlugin, int pageSize, int templateNumber);
		IEnumerable<GalleryRowPicture> GetGalleryRowPictures(int idDetail, int pageSize, int templateNumber, bool randomOrder);
		GalleryRow? GetGalleryDetailRow(int idPage, int idPlugin, int idDetail, int templateNumber);

		HeaderPictureData? GetHeaderPicture(int idPage, int placeNumber, HeaderPictureSelectType selectType, int pictureNumber, bool randomOrder);
		IEnumerable<HeaderPictureData> GetHeaderPictures(int idPage, int placeNumber, HeaderPictureSelectType selectType, bool randomOrder, int maxNumberOfItems);

		IEnumerable<FooterLinkItem> GetFooterLinkItems(int idPage, int placeNumber, FooterLinkSelectType selectType, bool randomOrder, IPageTree pageTree);
	}
}
