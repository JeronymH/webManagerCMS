using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webManagerCMS.Core.Configuration
{
	public class CacheStorageConfigSettings
	{
		public CacheStorageSettings? StandardCacheStorageSettings { get; set; }
	}

	public class CacheStorageSettings
	{
		public int DefaultCacheDurationMinutes { get; set; }

		public int SlidingExpirationHours { get; set; }

		public int SlidingExpirationMinutes { get; set; }

		public int SlidingExpirationSeconds { get; set; }
	}
}
