using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Models
{
    public class Alias
    {
        public int? Id { get; set; }
        public int? IdDB { get; set; }
        public int? IdPageContent { get; set; }
        public int? IdContentCols { get; set; }
        public int? IdTemplateNum { get; set; }
        public int? IdState { get; set; }
        public string? AliasName { get; set; }
        public int? IdTableName { get; set; }
        public bool? IsHomePage { get; set; }
        public string? Name { get; set; }
        public int? Level { get; set; }
        public int? IdClassCollection { get; set; }
        public bool? RootLineVisible { get; set; }
    }
}
