using LazyCache;
using S9.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebManager.NET.Core.Caching;

namespace webManagerCMS.Data.Caching.Lazy
{
	public class LazyCacheStorage : ICacheStorage
	{
        private const string ConstSiteScopedItemKeyTemplate = "S{0}-{1}";
        private const string ConstItemKeySitePrefixTemplate = "S{0}-";
		private const string ConstItemKeySitePartPrefixTemplate = "S{0}-P{1}-";
		private const string ConstSitePartScopedItemKeyTemplate = "S{0}-P{1}-{2}";

		public LazyCacheStorage(CacheStorageSettings settings)
		{
			this._settings = settings ?? throw new ArgumentNullException(nameof(settings));

			this._cache = new CachingService();
			if (this._settings.DefaultCacheDurationMinutes.HasValue)
				this._cache.DefaultCachePolicy.DefaultCacheDurationSeconds = this._settings.DefaultCacheDurationMinutes.Value * 60;

			this._cacheKeys = new ConcurrentUniqueValuesList<string>();
		}

		private IAppCache _cache;
		private CacheStorageSettings _settings;
		private ConcurrentUniqueValuesList<string> _cacheKeys;

        #region ICacheStorage implementation




        /// <summary>
        /// Returns site scoped item from cache. If it does not exist, the getDataForCacheDelegate will be called for retrieving and saving it into the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="siteId"></param>
        /// <param name="itemKey"></param>
        /// <param name="getDataForCacheDelegate"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        public T GetItem<T>(int siteId, SiteScopedCacheItemKey itemKey, GetDataForCacheDelegate<T> getDataForCacheDelegate, DateTimeOffset? absoluteExpiration)
            where T : class
        {
            var itemKeyStr = GenerateItemKey(siteId, itemKey);

            T item = this.GetOrAdd(itemKeyStr, () => getDataForCacheDelegate(), absoluteExpiration);

            if (item == null)
                this.InvalidateKey(itemKeyStr);

            return item;
        }

        /// <summary>
        /// Returns site scoped item from cache. If it does not exist, the getDataForCacheDelegate will be called for retrieving and saving it into the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="siteId"></param>
        /// <param name="itemKey"></param>
        /// <param name="getDataForCacheDelegate"></param>
        /// <returns></returns>
        public T GetItem<T>(int siteId, SiteScopedCacheItemKey itemKey, GetDataForCacheDelegate<T> getDataForCacheDelegate)
            where T : class
        {
            return this.GetItem(siteId, itemKey, getDataForCacheDelegate, null);
        }

        /// <summary>
        /// Returns site part scoped item from cache. If it does not exist, the getDataForCacheDelegate will be called for retrieving and saving it into the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="siteId"></param>
        /// <param name="sitePartId"></param>
        /// <param name="itemKey"></param>
        /// <param name="getDataForCacheDelegate"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        public T GetItem<T>(int siteId, int sitePartId, SitePartScopedCacheItemKey itemKey, GetDataForCacheDelegate<T> getDataForCacheDelegate, DateTimeOffset? absoluteExpiration)
			where T : class
		{
			var itemKeyStr = GenerateItemKey(siteId, sitePartId, itemKey);

			T item = this.GetOrAdd(itemKeyStr, () => getDataForCacheDelegate(), absoluteExpiration);

			if (item == null)
				this.InvalidateKey(itemKeyStr);

			return item;
		}

		/// <summary>
		/// Returns site part scoped item from cache. If it does not exist, the getDataForCacheDelegate will be called for retrieving and saving it into the cache.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="siteId"></param>
		/// <param name="sitePartId"></param>
		/// <param name="itemKey"></param>
		/// <param name="getDataForCacheDelegate"></param>
		/// <param name="absoluteExpiration"></param>
		/// <returns></returns>
		public T GetItem<T>(int siteId, int sitePartId, SitePartScopedCacheItemKey itemKey, GetDataForCacheDelegate<T> getDataForCacheDelegate)
			where T : class
		{
			return this.GetItem(siteId, sitePartId, itemKey, getDataForCacheDelegate, null);
		}

        /// <summary>
        /// Removes site scoped item from the cache.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="itemKey"></param>
        public void Invalidate(int siteId, SiteScopedCacheItemKey itemKey)
        {
            var itemKeyStr = GenerateItemKey(siteId, itemKey);

            this.InvalidateKey(itemKeyStr);
        }

        /// <summary>
        /// Removes site part scoped item from the cache.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="sitePartId"></param>
        /// <param name="itemKey"></param>
        public void Invalidate(int siteId, int sitePartId, SitePartScopedCacheItemKey itemKey)
		{
			var itemKeyStr = GenerateItemKey(siteId, sitePartId, itemKey);

			this.InvalidateKey(itemKeyStr);
		}

		/// <summary>
		/// Removes all site part's data from the cache.
		/// </summary>
		/// <param name="siteId"></param>
		/// <param name="sitePartId"></param>
		public void Invalidate(int siteId, int sitePartId)
		{
			var sitePartKey = string.Format(ConstItemKeySitePartPrefixTemplate, siteId, sitePartId);
			var keys = this._cacheKeys.Where(k => k.StartsWith(sitePartKey));

			this.InvalidateKeys(keys);
		}

		/// <summary>
		/// Removes all site's data from the cache.
		/// </summary>
		/// <param name="siteId"></param>
		public void Invalidate(int siteId)
		{
			var siteKey = string.Format(ConstItemKeySitePrefixTemplate, siteId);
			var keys = this._cacheKeys.Where(k => k.StartsWith(siteKey));

			this.InvalidateKeys(keys);
		}

		/// <summary>
		/// Removes all data from the cache.
		/// </summary>
		public void Invalidate()
		{
			this.InvalidateKeys(this._cacheKeys);
		}

		#endregion

		#region private implementation

		private T GetOrAdd<T>(string itemKeyStr, Func<T> getData, DateTimeOffset? absoluteExpiration)
		{

			Func<T> getDataWithIndication = () =>
			{
				this._cacheKeys.TryAdd(itemKeyStr);
				return getData();
			};

			if (absoluteExpiration.HasValue)
				return this._cache.GetOrAdd(itemKeyStr, getDataWithIndication, absoluteExpiration.Value);
			if (this._settings.SlidingExpiration.HasValue)
				return this._cache.GetOrAdd(itemKeyStr, getDataWithIndication, this._settings.SlidingExpiration.Value);
			return this._cache.GetOrAdd(itemKeyStr, getDataWithIndication);
		}

		private T GetOrAdd<T>(string itemKeyStr, Func<T> getData)
		{
			return this.GetOrAdd(itemKeyStr, getData, null);
		}

		private void InvalidateKeys(IEnumerable<string> keys)
		{
			foreach (string key in keys)
				this.InvalidateKey(key);
		}

		private void InvalidateKey(string key)
		{
			this._cache.Remove(key);
			this._cacheKeys.TryRemove(key);
		}

        private static string GenerateItemKey(int siteId, SiteScopedCacheItemKey itemKey)
        {
            return string.Format(ConstSiteScopedItemKeyTemplate, siteId, (int)itemKey);
        }

        private static string GenerateItemKey(int siteId, int sitePartId, SitePartScopedCacheItemKey itemKey)
		{
			return string.Format(ConstSitePartScopedItemKeyTemplate, siteId, sitePartId, (int)itemKey);
		}

		#endregion
	}
}
