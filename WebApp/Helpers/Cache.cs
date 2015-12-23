using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.Helpers
{
    public class Sale
    {
        public Int16 DayCount { get; set; }
        public decimal Sales { get; set; }
        public decimal RunningTotal { get; set; }

        public static IEnumerable<Sale> GetSales(int? highestDayCount)
        {
            string cacheKey =
                string.Format("CacheSample.BusinessObjects.GetSalesWithCache({0})",
                highestDayCount);

            

            return CacheProviderFactory.GetCacheProvider<Sale>().Fetch(cacheKey,
                delegate()
                {
                    List<Sale> sales = new List<Sale>();

                    SqlParameter highestDayCountParameter = new
                                       SqlParameter("@HighestDayCount", SqlDbType.SmallInt);
                    if (highestDayCount.HasValue)
                        highestDayCountParameter.Value = highestDayCount;
                    else
                        highestDayCountParameter.Value = DBNull.Value;

                    string connectionStr =
                        System.Configuration.ConfigurationManager
                        .ConnectionStrings["CacheSample"].ConnectionString;

                    using (SqlConnection sqlConn = new SqlConnection(connectionStr))
                    using (SqlCommand sqlCmd = sqlConn.CreateCommand())
                    {
                        sqlCmd.CommandText = "spGetRunningTotals";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(highestDayCountParameter);

                        sqlConn.Open();

                        using (SqlDataReader dr = sqlCmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Sale newSale = new Sale();
                                newSale.DayCount = dr.GetInt16(0);
                                newSale.Sales = dr.GetDecimal(1);
                                newSale.RunningTotal = dr.GetDecimal(2);

                                sales.Add(newSale);
                            }
                        }
                    }

                    return sales;
                },
                null,
                new TimeSpan(0, 10, 0));
            
        }
    }

    public interface ICacheProvider
    {
    }

    public interface ICacheProvider<T> : ICacheProvider
    {
        T Fetch(string key,
            Func<T> retrieveData,
            DateTime? absoluteExpiry,
            TimeSpan? relativeExpiry);

        IEnumerable<T> Fetch(string key,
            Func<IEnumerable<T>> retrieveData,
            DateTime? absoluteExpiry,
            TimeSpan? relativeExpiry);
    }

    public static class CacheProviderFactory
    {
        private static Dictionary<Type, ICacheProvider> cacheProviders
                        = new Dictionary<Type, ICacheProvider>();
        private static object syncRoot = new object();

        public static ICacheProvider<T> GetCacheProvider<T>()
        {
            ICacheProvider<T> cacheProvider = null;
            // Get the Type reference for the type parameter T
            Type typeOfT = typeof(T);

            // Lock the access to the cacheProviders dictionary
            // so mulitiple threads can work with it
            lock (syncRoot)
            {
                // First check if an instance of the ICacheProvider implementation
                // already exists in the cacheProviders dictionary for the type T
                if (cacheProviders.ContainsKey(typeOfT))
                    cacheProvider = (ICacheProvider<T>)cacheProviders[typeOfT];
                else
                {
                    // There is not already an instance of the ICacheProvider in
                    // cacheProviders for the type T
                    // so we need to create one
                    // Get the Type reference for the application's implementation of
                    // ICacheProvider from the configuration
                    Type cacheProviderType =
                        Type.GetType(CacheProviderConfigurationSection.Current.
                                       CacheProviderType);
                    if (cacheProviderType != null)
                    {
                        // Now get a Type reference for the Cache Provider wity the
                        // type T generic parameter
                        Type typeOfCacheProviderTypeForT =
                            cacheProviderType.MakeGenericType(new Type[] { typeOfT });
                        if (typeOfCacheProviderTypeForT != null)
                        {
                            // Create the instance of the Cache Provider and add it to
                            // the cacheProviders dictionary for future use
                            cacheProvider =
                                (ICacheProvider<T>)Activator.CreateInstance(typeOfCacheProviderTypeForT);
                            cacheProviders.Add(typeOfT, cacheProvider);
                        }
                    }
                }
            }

            return cacheProvider;
        }
    }

    internal class CacheProviderConfigurationSection : ConfigurationSection
    {
        public static CacheProviderConfigurationSection Current
        {
            get
            {
                return (CacheProviderConfigurationSection)
                    ConfigurationManager.GetSection("cacheProvider");
            }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string CacheProviderType
        {
            get
            {
                return (string)this["type"];
            }
        }
    }
}