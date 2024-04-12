using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Models.PageContent
{
	public class TreeDisplayDefinedRow
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Subtitle { get; set; }
		public string? Description { get; set; }
		public string? Perex { get; set; }
		public string? Note { get; set; }
		public string? PictureFileAlias { get; set; }
		public string? Url { get; set; }
		public DateTime? ReflectDate { get; set; }
		public int Row {  get; set; }
	}
}
