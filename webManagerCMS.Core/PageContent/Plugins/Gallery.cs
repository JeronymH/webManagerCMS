using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS.Plugins
{
	public class Gallery : PageContentPlugin
	{
		private IDataStorageAccess _dataStorageAccess;

		public Gallery(PageContentPlugin plugin) : base(plugin)
		{
			_dataStorageAccess = plugin.PluginParameters.dataStorageAccess;

			TemplateName = PageContentPluginType.GALLERY1;
			InitCountRow();
		}

		public int CountRow { get; private set; }

		private int IdDetail {  get; set; }
		private int CountRowPicture {  get; set; }
		private IEnumerable<GalleryRow>? Rows { get; set; }
		private IEnumerable<GalleryRowPicture>? RowPictures { get; set; }
		private GalleryRow? DetailRow { get; set; }

		public void InitCountRow()
		{
			CountRow = _dataStorageAccess.WebContentDataStorage.GetGalleryCountRow(IdPage, Id);
		}

		public IEnumerable<GalleryRow> GetGalleryRows(int pageSize)
		{
			if (Rows == null)
				Rows = _dataStorageAccess.WebContentDataStorage.GetGalleryRows(IdPage, Id, pageSize, TemplateNum);

			return Rows;
		}

		public IEnumerable<GalleryRowPicture> GetGalleryRowPictures(int pageSize, bool randomOrder)
		{
			if (RowPictures == null)
				RowPictures = _dataStorageAccess.WebContentDataStorage.GetGalleryRowPictures(GetIdDetail(), pageSize, TemplateNum, randomOrder);

			return RowPictures;
		}

		public int GetIdDetail()
		{
			if (IdDetail <= 0)
				IdDetail = _dataStorageAccess.WebContentDataStorage.GetGalleryDetailId(IdPage, Id);

			return IdDetail;
		}

		public int GetCountRowPicture()
		{
			if (CountRowPicture <= 0)
				CountRowPicture = _dataStorageAccess.WebContentDataStorage.GetGalleryCountRowPicture(GetIdDetail());

			return CountRowPicture;
		}

		public GalleryRow? GetDetailRow()
		{
			if (DetailRow == null)
				DetailRow =  _dataStorageAccess.WebContentDataStorage.GetGalleryDetailRow(IdPage, Id, GetIdDetail(), TemplateNum);

			return DetailRow;
		}

		public string GetRowURL(GalleryRow row)
		{
			return PluginParameters.pageTree.GetPageUrl(PluginParameters.currentPage)
					+ row.AliasValue + _dataStorageAccess.TenantAccess.Tenant.WWWSettings.PageSuffix;
		}
	}
}
