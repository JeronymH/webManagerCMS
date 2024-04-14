using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Models.PageContent
{
	public class GalleryRowPicture
	{
		public int Id { get; set; }
		public int IdRow { get; set; }
		public string? Title { get; set; }
		public string? Subtitle { get; set; }
		public string? Description { get; set; }
		public string? Picture1 { get; set; }
		public string? Picture2 { get; set; }
		public string? Picture3 { get; set; }
		public string? Author { get; set; }
		public DateTime? ReflectDate { get; set; }
		public int Row {  get; set; }
	}
}
