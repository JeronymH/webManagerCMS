using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models;

namespace webManagerCMS.Data.Storage
{
    public interface ISystemDataStorage
    {
        WWWSettings GetWWWSettings(int idWWW, string webBaseUrl, string mutationAlias, bool webDevelopmentBehaviorEnabled);
        IDictionary<string, LocalizedText> GetLocalizedTexts(int idWWW, bool fromCache);
	}
}
