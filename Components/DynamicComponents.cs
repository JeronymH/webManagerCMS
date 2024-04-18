using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Models.PageContent;
using webManagerCMS.Data.Tenants;

namespace Components
{
	public class DynamicComponents : IDynamicComponents
	{
		public IDictionary<string, Type> Components => new Dictionary<string, Type>
		{
			{ "TREE_CORE_0_0", typeof(PAGE_TREE_0_0) },
			{ "PAGE_CORE_0_0", typeof(PAGE_CORE_0_0) },
			{ "PAGE_CORE_0_1", typeof(PAGE_CORE_0_1) },
			{ "DOC_HTML_5_0", typeof(DOC_HTML_5_0) },
			{ "TREEDISPLAYDEFINED1_0_0", typeof(TREEDISPLAYDEFINED1_0_0) },
			{ "DOC_H1TEXT_0_0", typeof(DOC_H1TEXT_0_0) },
			{ "GALLERY1_0_0", typeof(GALLERY1_0_0) },
			{ "GALLERY1_0_1", typeof(GALLERY1_0_1) },
			{ "PICHEADER_0_0", typeof(PICHEADER_0_0) },
			{ "PICHEADER_2_0", typeof(PICHEADER_2_0) },
			{ "LINKFOOTER_0_0", typeof(LINKFOOTER_0_0) }
		};

		public static string GetTermsConditionsURL(SystemLanguageType language, ITenant tenant) {
			switch (language)
			{
				case SystemLanguageType.Czech:
					return tenant.GetRootAlias() + "terms-&-conditions/";
				case SystemLanguageType.English:
					return tenant.GetRootAlias() + "terms-&-conditions/";
				default:
					return tenant.GetRootAlias() + "terms-&-conditions/";
			}
		}

		public static string GetCookiePolicyURL(SystemLanguageType language, ITenant tenant) {
			switch (language)
			{
				case SystemLanguageType.Czech:
					return tenant.GetRootAlias() + "gdpr/";
				case SystemLanguageType.English:
					return tenant.GetRootAlias() + "gdpr/";
				default:
					return tenant.GetRootAlias() + "gdpr/";
			}
		}

		public static string GetSocialMediaIcon(string name) {
			switch (name.ToLower())
			{
				case "instagram":
					return "/public/images/sprite.svg#instagram";
				case "facebook":
					return "/public/images/sprite.svg#facebook";
				case "youtube":
					return "/public/images/sprite.svg#youtube";
			}
			return "";
		}

        public string test2()
        {
            return "yyy";
        }
    }
}
