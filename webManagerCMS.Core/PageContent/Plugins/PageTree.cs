using webManagerCMS.Data.Models.PageContent;

namespace webManagerCMS.Core.PageContentNS.Plugins
{
    public class PageTree : PageContentPlugin
    {

        public PageTree(int templateNum, int templateState, PageContentPluginParameters pluginParameters)
        {
            TemplateName = PageContentPluginType.PAGE_TREE;

            TemplateNum = templateNum;
            TemplateState = templateState;
            PluginParameters = pluginParameters;
        }

    }
}
