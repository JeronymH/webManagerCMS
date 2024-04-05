using System;
using WebManager.NET.Core.Caching;

namespace webManagerCMS.Data.Caching
{
	/// <summary>
	/// Storage of cached data.
	/// </summary>
	public interface ICacheStorage
	{
        /// <summary>
        /// Returns site scoped item from cache. If it does not exist, the getDataForCacheDelegate will be called for retrieving and saving it into the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="siteId"></param>
        /// <param name="itemKey"></param>
        /// <param name="getDataForCacheDelegate"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        T GetItem<T>(int siteId, SiteScopedCacheItemKey itemKey, GetDataForCacheDelegate<T> getDataForCacheDelegate, DateTimeOffset? absoluteExpiration) where T : class;

        /// <summary>
        /// Returns site scoped item from cache. If it does not exist, the getDataForCacheDelegate will be called for retrieving and saving it into the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="siteId"></param>
        /// <param name="itemKey"></param>
        /// <param name="getDataForCacheDelegate"></param>
        /// <returns></returns>
        T GetItem<T>(int siteId, SiteScopedCacheItemKey itemKey, GetDataForCacheDelegate<T> getDataForCacheDelegate) where T : class;


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
        T GetItem<T>(int siteId, int sitePartId, SitePartScopedCacheItemKey itemKey, GetDataForCacheDelegate<T> getDataForCacheDelegate, DateTimeOffset? absoluteExpiration) where T : class;

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
		T GetItem<T>(int siteId, int sitePartId, SitePartScopedCacheItemKey itemKey, GetDataForCacheDelegate<T> getDataForCacheDelegate) where T : class;

        /// <summary>
        /// Removes site scoped item from the cache.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="itemKey"></param>
        void Invalidate(int siteId, SiteScopedCacheItemKey itemKey);

        /// <summary>
        /// Removes site part scoped item from the cache.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="sitePartId"></param>
        /// <param name="itemKey"></param>
        void Invalidate(int siteId, int sitePartId, SitePartScopedCacheItemKey itemKey);

		/// <summary>
		/// Removes all site part's data from the cache.
		/// </summary>
		/// <param name="siteId"></param>
		/// <param name="sitePartId"></param>
		void Invalidate(int siteId, int sitePartId);

		/// <summary>
		/// Removes all site's data from the cache.
		/// </summary>
		/// <param name="siteId"></param>
		void Invalidate(int siteId);

		/// <summary>
		/// Removes all data from the cache.
		/// </summary>
		void Invalidate();
	}
}
