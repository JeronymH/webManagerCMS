using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS.Plugins
{
	public class DocH1Text : PageContentPlugin
	{
		private IDataStorageAccess _dataStorageAccess;

		public DocH1Text(PageContentPlugin plugin) : base(plugin)
		{
			_dataStorageAccess = plugin.PluginParameters.DataStorageAccess;

			TemplateName = PageContentPluginType.DOC_H1TEXT;
			InitData();
		}

		public DocH1TextData PluginContent {  get; set; }

		private void InitData()
		{
			var data = _dataStorageAccess.WebContentDataStorage.GetDocH1TextData(IdPage, Id);
			PluginContent = data ?? new DocH1TextData();
		}
	}
}
