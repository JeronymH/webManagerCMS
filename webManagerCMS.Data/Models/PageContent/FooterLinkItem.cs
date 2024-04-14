using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Models.PageContent
{
	public class FooterLinkItem
	{
		public int Id { get; set; }
		public string? Prefix { get; set; }
		public string? Suffix { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? LinkTarget { get; set; }
		public string? Picture { get; set; }
		public string? Url { get; set; }
		public int Row { get; set; }
	}
}
