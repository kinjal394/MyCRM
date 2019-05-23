using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using System.Web.Caching;

namespace CRM.Models
{
    public class CacheManagement : ICacheService
    {
        private static Cache _cache = new Cache();

        //public static List<CategoryMaster> GetCategoryList
        //{
        //    get
        //    {
        //        if (Cache["Category"] == null)
        //            RefreshCategoryList();
        //        return _cache["Category"] as List<CategoryMaster>;
        //    }
        //}

        public static void RefreshMenuList()
        {
            var listAgency = new CRM_Repository.Service.Permission_Repository().GetPages();
            MemoryCache.Default.Remove("MenuList");
            MemoryCache.Default.Add("MenuList", listAgency, DateTime.Now.AddMinutes(10));
        }
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(10));
            }
            return item;
        }
    }
    interface ICacheService
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
    }
}