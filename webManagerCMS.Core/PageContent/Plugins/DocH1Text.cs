using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;

namespace webManagerCMS.Core.PageContentNS.Plugins
{
	public class DocH1Text : PageContentPlugin
	{

		public DocH1Text(PageContentPlugin plugin) : base(plugin)
		{
			TemplateName = PageContentPluginType.DOC_H1TEXT;
		}
	}
}
