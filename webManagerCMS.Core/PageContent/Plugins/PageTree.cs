using webManagerCMS.Data.Models.PageContent;

namespace webManagerCMS.Core.PageContentNS.Plugins
{
    public class PageTree : PageContentPlugin
    {
        public new PageContentPluginType TemplateName { get; } = PageContentPluginType.PAGE_TREE;

        public PageTree(int templateNum, int templateState, PageContentPluginParameters pluginParameters)
        {
            TemplateNum = templateNum;
            TemplateState = templateState;
            PluginParameters = pluginParameters;
        }


    }
}
