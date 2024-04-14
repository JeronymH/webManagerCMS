using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS.Plugins
{
	public class FooterLink : PageContentPlugin
	{
		private IDataStorageAccess _dataStorageAccess;

		public FooterLink(int idPage, int templateNumber, int templateState, int placeNumber, PageContentPluginParameters pluginParameters) : base(0)
		{
			PluginParameters = pluginParameters;
			_dataStorageAccess = PluginParameters.dataStorageAccess;

			TemplateName = PageContentPluginType.LINKFOOTER;
			TemplateNum = templateNumber;
			TemplateState = templateState;

			IdPage = idPage;
			PlaceNumber = placeNumber;
		}

		public int PlaceNumber { get; private set; }

		public IEnumerable<FooterLinkItem> GetFooterLinkItems(FooterLinkSelectType selectType, bool randomOrder)
		{
			return _dataStorageAccess.WebContentDataStorage.GetFooterLinkItems(IdPage, PlaceNumber, selectType, randomOrder, PluginParameters.pageTree);
		}
	}
}
