using webManagerCMS.Data.Models.PageContent;

namespace webManagerCMS.Core
{
    public class PageContent : PageContentPlugin
    {
        public new PageContentPluginType TemplateName { get; } = PageContentPluginType.PAGE_CORE;


    }
}
