using S9.Core.Data.Storage.MsSqlStorage;
using System.Xml.Linq;
using webManagerCMS.Data.Models;

namespace webManagerCMS.Core
{
    public class UrlAliases
    {
        public int ActLevel { get; private set; }
        public List<Alias> Aliases { get; }
        public string?[] QueryAliases { get; private set; }
        /// <summary>
        /// Number of alias levels recognized within app
        /// </summary>
        private int LevelCount = 3;
        public string? QueryAliasName { get; } = "rAlias";

        private readonly IHttpContextAccessor? _contextAccessor;

        public UrlAliases(IHttpContextAccessor? contextAccessor) {
            this._contextAccessor = contextAccessor;

            ActLevel = -1;
            Aliases = new List<Alias>();
            QueryAliases = new string[LevelCount];

            InitQueryAliases();
        }

        public void AddData(bool? isHomepage, string? name, int? id, int? iddb, int? idPageContent, int? idContentCols, int? idTemplateNum, int? idState, string? aliasName, int? idTableName, int? idClassCollection, bool? rootLineVisible) {
            ActLevel++;

            Aliases.Add(new Alias() {
                IsHomePage = isHomepage,
                Name = name,
                Id = id,
                IdDB = iddb,
                IdPageContent = idPageContent,
                IdContentCols = idContentCols,
                IdTemplateNum = idTemplateNum,
                IdState = idState,
                AliasName = aliasName,
                IdTableName = idTableName,
                Level = ActLevel,
                IdClassCollection = idClassCollection,
                RootLineVisible = rootLineVisible
            });
        }

        public void AddData(Alias? alias) {
            AddData(alias?.IsHomePage, alias?.Name, alias?.Id, alias?.IdDB, alias?.IdPageContent, alias?.IdContentCols, alias?.IdTemplateNum, alias?.IdState, alias?.AliasName, alias?.IdTableName, alias?.IdClassCollection, alias?.RootLineVisible);
        }

        public void InitQueryAliases() {
            string? queryName;
            for (int i = 0; i < LevelCount; i++)
            {
                queryName = QueryAliasName + i.ToString();
                QueryAliases[i] = _contextAccessor?.HttpContext?.Request.Query[queryName];
            }
        }

        public bool CheckAllData() {
            if (Aliases.Count == 0) return false;
            foreach (var alias in Aliases)
            {
                if (!(alias.Id > 0)) return false;
            }
            return true;
        }
    }
}
