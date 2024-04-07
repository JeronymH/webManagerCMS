using webManagerCMS.Data.Interfaces;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;

namespace webManagerCMS.Data.Models.PageContent
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
        public PageContentPluginType TemplateName { get; protected set; }
        public int TemplateNum { get; set; }
        public int TemplateState { get; set; }

        public PageContentPluginParameters? PluginParameters { get; set; }

        public int Id { get; set; }
        public int IdPage { get; set; }

        //Plugin settings additional data
        public string? Title {  get; set; }
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public string? PictureFileAlias { get; set; }

        protected Dictionary<string, string>? CustomProperties { get; set; }

        public PageContentPlugin(PageContentPluginType templateName, int templateNum, int templateState, int id, int idPage, string? title, string? subtitle, string? description, string? note, string? pictureFileAlias)
		{
			TemplateName = templateName;
			TemplateNum = templateNum;
			TemplateState = templateState;
			Id = id;
			IdPage = idPage;
			Title = title;
			Subtitle = subtitle;
			Description = description;
			Note = note;
			PictureFileAlias = pictureFileAlias;
		}

		//This constructor is for content plugins without settings section -> TREE_CORE, PAGE_CORE, PICHEADER, LINKFOOTER...
		public PageContentPlugin(int id)
        {
            Id = id;
		}

        public PageContentPlugin(PageContentPlugin plugin) : this(
            plugin.TemplateName,
            plugin.TemplateNum,
            plugin.TemplateState,
            plugin.Id,
            plugin.IdPage,
            plugin.Title,
            plugin.Subtitle,
            plugin.Description,
            plugin.Note,
            plugin.PictureFileAlias
        ){ }

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

        public string GetCustomProperty(string custPropName)
        {
            if (CustomProperties == null) return "";
            if (!CustomProperties.ContainsKey(custPropName)) return "";

            return CustomProperties[custPropName];
		}

        //TODO: implement this function
        public void InitCustomProperties()
        {

        }
    }
}
