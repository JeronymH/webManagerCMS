using S9.Core.Data.Storage.MsSqlStorage;
using S9.Core.Extensions.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using WebManager.NET.Core.Caching;
using webManagerCMS.Data.Caching;
using webManagerCMS.Data.Models;
using webManagerCMS.Data.Storage.MsSqlStorage.Access;
using webManagerCMS.Data.Storage.MsSqlStorage.Base;

namespace webManagerCMS.Data.Storage.MsSqlStorage
{
    public class MsSqlSystemDataStorage : MsSqlDataStorageBase, ISystemDataStorage
    {
        public MsSqlSystemDataStorage(MsSqlDataStorageAccess dataAccess, MsSqlDataStorageSettings settings) : base(dataAccess, settings)
        { }

        public WWWSettings GetWWWSettings(int idWWW, string webBaseUrl, string mutationAlias, bool webDevelopmentBehaviorEnabled)
        {
            using (var cmd = this.NewCommandProc("dbo.pub_pl_SelectWWWSettings"))
            {
                cmd.AddParam("@IDWWW", idWWW);
                cmd.AddParam("@URL", webBaseUrl);
                cmd.AddParam("@RootAlias", mutationAlias);
                cmd.AddParam("@IsDevelopment", webDevelopmentBehaviorEnabled);

                using (var dataReader = this.ExecReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        return new WWWSettings()
                        {
                            IdWWWRoot = (int)dataReader["IDWWWRoot"],
                            IdLanguage = (int)dataReader["IDLanguage"],
                            MutationAlias = (string)dataReader["AliasInURL"],
                            PageSuffix = (string)dataReader["PageSuffix"]
                        };
                    }
                    return null;
                }
            }
        }

        public IDictionary<string, LocalizedText> GetLocalizedTexts(int idWWW, bool fromCache)
        {
            Func<IDictionary<string, LocalizedText>> getLocalizedTextsFunc = () => this.GetDictionaryWithLocalizedTextsFromDb(idWWW);

            return fromCache ?
                this.CacheStorageAccess.CacheStorage.GetItem<IDictionary<string, LocalizedText>>(idWWW, SiteScopedCacheItemKey.LocalizedTexts, new GetDataForCacheDelegate<IDictionary<string, LocalizedText>>(getLocalizedTextsFunc))
                : getLocalizedTextsFunc();
        }

        private IDictionary<string, LocalizedText> GetDictionaryWithLocalizedTextsFromDb(int idWWW)
        {
            return this.GetLocalizedTextsFromDb(idWWW).ToDictionary(t => t.Identifier, t => t);
        }

        private IEnumerable<LocalizedText> GetLocalizedTextsFromDb(int idWWW)
        {
            using (var cmd = this.NewCommandProc("dbo.pubSelectTexts"))
            {
                cmd.AddParam("@IDWWW", idWWW);

                using (var dataReader = this.ExecReader(cmd))
                {
                    while (dataReader.Read())
                    {
                        yield return new LocalizedText()
                        {
                            IdLanguage = (int)dataReader["IDLanguage"],
                            IdCategory = (int)dataReader["IDCategory"],
                            Code = (string)dataReader["Code"],
                            Text = (string)dataReader["Text"]
                        };
                    }
                }
            }
        }
    }
}
