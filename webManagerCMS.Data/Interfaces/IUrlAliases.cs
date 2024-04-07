using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Models;

namespace webManagerCMS.Data.Interfaces
{
    public interface IUrlAliases
    {
        int ActLevel { get; }
        List<Alias> Aliases { get; }
        string?[] QueryAliases { get; }
        string? QueryAliasName { get; }

        public void AddData(bool? isHomepage, string? name, int? id, int? iddb, int? idPageContent, int? idContentCols, int? idTemplateNum, int? idState, string? aliasName, int? idTableName, int? idClassCollection, bool? rootLineVisible);

        public void AddData(Alias? alias);

        public void InitQueryAliases();

        public bool CheckAllData();

    }
}
