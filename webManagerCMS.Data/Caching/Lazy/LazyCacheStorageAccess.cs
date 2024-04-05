using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Caching.Lazy
{
	public class LazyCacheStorageAccess : ICacheStorageAccess
	{
		public LazyCacheStorageAccess(CacheStorageSettings cacheStorageSettings)
		{
			this._cacheStorageSettings = cacheStorageSettings;
		}

		private CacheStorageSettings _cacheStorageSettings;

		public ICacheStorage CacheStorage
		{
			get
			{
				if (this._CacheStorage == null)
					this._CacheStorage = new LazyCacheStorage(this._cacheStorageSettings);
				return this._CacheStorage;
			}
		}
		private LazyCacheStorage _CacheStorage;
	}
}
