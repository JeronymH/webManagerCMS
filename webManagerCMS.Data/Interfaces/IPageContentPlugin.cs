using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Models;

namespace webManagerCMS.Data.Interfaces
{
    public interface IPageContentPlugin
    {
        string Template { get; }
        PageContentPluginType TemplateName { get; }
        int TemplateNum { get; set; }
        int TemplateState { get; set; }

        PageContentPluginParameters PluginParameters { get; }

        string GetLocalizedText(int idLanguage, string textCode);

        string GetLocalizedText(SystemLanguageType languageType, string textCode);
    }
}
