using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS.Plugins
{
	public class TreeDisplayDefined : PageContentPlugin
	{
		private IDataStorageAccess _dataStorageAccess;

		public TreeDisplayDefined(PageContentPlugin plugin) : base(plugin)
		{
			_dataStorageAccess = plugin.PluginParameters.DataStorageAccess;

			TemplateName = PageContentPluginType.TREEDISPLAYDEFINED1;
		}

		private IEnumerable<TreeDisplayDefinedRow> Rows { get; set; }

		public IEnumerable<TreeDisplayDefinedRow> GetTreeDisplayDefinedRows(int pageSize)
		{
			if (Rows == null)
				Rows = _dataStorageAccess.WebContentDataStorage.GetTreeDisplayDefinedRows(IdPage, Id, pageSize, TemplateNum, PluginParameters.PageTree);

			return Rows;
		}

		public IEnumerable<TreeDisplayDefinedRowItem> GetTreeDisplayDefinedRowItems(int idRow)
		{
			return _dataStorageAccess.WebContentDataStorage.GetTreeDisplayDefinedRowItems(idRow, TemplateNum, PluginParameters.PageTree);
		}
	}
}
