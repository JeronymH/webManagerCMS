using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Models.Page
{
    public class Page
    {
        public int Id { get; set; }
        public int IdDB { get; set; }
        public int IdPageType { get; set; }
        public int Parent { get; set; }
        public int MySort { get; set; }
        public int Lvl { get; set; }
        public int TemplateNum { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? PageAlias { get; set; }
        public bool IsHomePage { get; set; }
        public bool VisibleInTree { get; set; }
    }
}
