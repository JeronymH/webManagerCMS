using Microsoft.AspNetCore.Components;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Core.PageContent
{
    public class PageContentPlugin
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

        [Inject]
        private IDataStorageAccess? DataStorageAccess { get; set; }

        [Inject]
        private ITenantAccess? TenantAccess { get; set; }

        [Inject]
        private IHttpContextAccessor? httpContextAccessor { get; set; }

        public Page ActualPage { get; private set; }
        public PageTree PageTree { get; private set; }
        public UrlAliases UrlAliases { get; private set; }
        public static PageContentPluginParameters PluginParameters { get; private set; }


        //TODO: implement this function
        public static string GetLocalizedText(int idLanguage, string textCode)
        {
            return "";
        }

        public static string GetLocalizedText(SystemLanguageType languageType, string textCode)
        {
            return GetLocalizedText((int)languageType, textCode);
        }

        private static string GetTemplate(PageContentPluginType templateName, int templateState, int templateNum)
        {
            return $"{templateName}-{templateState}-{templateNum}";
        }
    }
}
