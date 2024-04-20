using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Models
{
    public class WWWSettings
    {
        public int IdWWWRoot { get; set; }
        public int IdLanguage { get; set; }
        public string MutationAlias { get; set; }
        public string PageSuffix { get; set; }
		/// <summary>
		/// Represents allowed history for page alias in months
		/// </summary>
		public int PageAliasMaxHistoryPeriod { get; set; }
	}
}
