using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Models.PageContent
{
	public class HeaderPictureData
	{
		public int Id { get; set; }
		public int IdPage { get; set; }
		public string? Title { get; set; }
		public string? Subtitle { get; set; }
		public string? Description { get; set; }
		public string? Picture { get; set; }
		public string? PictureFull { get; set; }
		public int ObjectCount { get; set; }
		public int Row { get; set; }
	}
}
