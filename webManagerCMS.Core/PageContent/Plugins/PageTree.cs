using webManagerCMS.Data.Models.PageContent;

namespace webManagerCMS.Core.PageContentNS.Plugins
{
    public class PageTree : PageContentPlugin
    {

        public PageTree(int templateNum, int templateState, PageContentPluginParameters pluginParameters) : base (0)
        {
            TemplateName = PageContentPluginType.TREE_CORE;

            TemplateNum = templateNum;
            TemplateState = templateState;
            PluginParameters = pluginParameters;
        }

    }
}
