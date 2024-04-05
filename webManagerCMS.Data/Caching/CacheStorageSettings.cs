using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Caching
{
	public class CacheStorageSettings
	{
		public int? DefaultCacheDurationMinutes { get; set; }

		public TimeSpan? SlidingExpiration { get; set; }
	}
}
