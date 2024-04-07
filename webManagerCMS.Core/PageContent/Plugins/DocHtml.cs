using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContent.Plugins
{
	public class DocHtml : PageContentPlugin
	{
		private IDataStorageAccess _dataStorageAccess;

		public DocHtml(PageContentPlugin plugin) : base(plugin)
		{
			_dataStorageAccess = plugin.PluginParameters.dataStorageAccess;

			TemplateName = PageContentPluginType.DOC_HTML;
			InitData();
		}

		//TODO: implement function
		private void InitData()
		{
			_dataStorageAccess.WebContentDataStorage.GetDocHtmlData(IdPage, Id);
		}
	}
}
