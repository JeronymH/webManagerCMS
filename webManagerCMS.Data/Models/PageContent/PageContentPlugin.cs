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
        public string? Picture { get; set; }

        public string? Note_Title { get; set; }
        public string? Note_Subtitle { get; set; }
        public string? Note_Perex { get; set; }

        protected Dictionary<string, string>? CustomProperties { get; set; }

        public PageContentPlugin(PageContentPluginType templateName, int templateNum, int templateState, int id, int idPage, string? title, string? subtitle, string? description, string? note, string? note_Title, string? note_Subtitle, string? note_Perex, string? pictureFileAlias, PageContentPluginParameters pluginParameters)
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
			Note_Title = note_Title;
			Note_Subtitle = note_Subtitle;
			Note_Perex = note_Perex;
			Picture = pictureFileAlias;
            PluginParameters = pluginParameters;
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
            plugin.Note_Title,
            plugin.Note_Subtitle,
            plugin.Note_Perex,
            plugin.Picture,
            plugin.PluginParameters
        ){ }

		public PageContentPlugin(PageContentPluginType templateName, int templateNum, int templateState, int id, int idPage, PageContentPluginParameters pluginParameters)
		{
			TemplateName = templateName;
			TemplateNum = templateNum;
			TemplateState = templateState;
			Id = id;
			IdPage = idPage;
			PluginParameters = pluginParameters;
		}


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
            return $"{templateName}_{templateNum}_{templateState}";
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
