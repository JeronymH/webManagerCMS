using Microsoft.AspNetCore.Components;
using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Core.PageContentNS
{
    public class PageContentPlugin : IPageContentPlugin
    {
        public string Template
        {
            get
            {
                return GetTemplate(TemplateName, TemplateState, TemplateNum);
            }
        }
        public PageContentPluginType TemplateName { get; }
        public int TemplateNum { get; set; }
        public int TemplateState { get; set; }

        public PageContentPluginParameters PluginParameters { get; set; }

        //TODO: implement this function
        public string GetLocalizedText(int idLanguage, string textCode)
        {
            return "";
        }

        public string GetLocalizedText(SystemLanguageType languageType, string textCode)
        {
            return GetLocalizedText((int)languageType, textCode);
        }

        private static string GetTemplate(PageContentPluginType templateName, int templateState, int templateNum)
        {
            return $"{templateName}_{templateState}_{templateNum}";
        }
    }
}
