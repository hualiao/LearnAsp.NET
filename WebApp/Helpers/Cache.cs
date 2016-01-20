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

            var sqlCacheDependency = CacheDependencyFactory.
                CreateCacheDependency<ISqlCacheDependency>();
            sqlCacheDependency.DatabaseConnectionName = "CacheSample";
            sqlCacheDependency.TableName = "Sale";

            ICacheDependency[] cacheDependencies =
                new ICacheDependency[] { sqlCacheDependency };

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
                null,
                cacheDependencies);
            
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

        T Fetch(string key,
            Func<T> retrieveData,
            DateTime? absoluteExpiry,
            TimeSpan? relativeExpiry,
            IEnumerable<ICacheDependency> cacheDependencies);

        IEnumerable<T> Fetch(string key,
            Func<IEnumerable<T>> retrieveData,
            DateTime? absoluteExpiry,
            TimeSpan? relativeExpiry);

        IEnumerable<T> Fetch(string key,
            Func<IEnumerable<T>> retrieveData,
            DateTime? absoluteExpiry,
            TimeSpan? relativeExpiry,
            IEnumerable<ICacheDependency> cacheDependencies);
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

        [ConfigurationProperty("cacheDependencies")]
        public CacheDenpedencyConfigurationElementCollection CacheDependencies
        {
            get
            {
                return (CacheDenpedencyConfigurationElementCollection)
                    base["cacheDependencies"];
            }
        }

    }



    internal class CacheDependencyConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("interface",IsRequired=true)]
        public string CacheDependencyInterface
        {
            get
            {
                return (string)this["interface"];
            }
        }

        [ConfigurationProperty("type",IsRequired=true)]
        public string CacheDependencyType
        {
            get
            {
                return (string)this["type"];
            }
        }
    }

    internal class CacheDenpedencyConfigurationElementCollection :
        ConfigurationElementCollection
    {
        protected override string ElementName
        {
            get
            {
                return "cacheDependency";
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheDependencyConfigurationElement();
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CacheDependencyConfigurationElement)element).
                CacheDependencyInterface;
        }

        public CacheDependencyConfigurationElement this[int index]
        {
            get
            {
                return (CacheDependencyConfigurationElement)BaseGet(index);
            }
        }

        public new CacheDependencyConfigurationElement this[string Interface]
        {
            get
            {
                return (CacheDependencyConfigurationElement)BaseGet(Interface);
            }
        }       
    }

    /// <summary>
    /// Provide an efficient mechanism for creating new instances
    /// of ICacheDependency implementation at runtime
    /// </summary>
    internal class CacheDependencyCreator
    {
        /// <summary>
        /// Create a new instance of the relevant ICacheDependency implementation
        /// </summary>
        /// <returns></returns>
        public virtual ICacheDependency Create()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Provide an efficient mechanism for creating new instances
    /// of ICacheDependency implementations at runtime
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class CacheDependencyCreator<T> : CacheDependencyCreator
        where T : ICacheDependency, new()
    {
        /// <summary>
        /// Create a new instance of relevant ICacheDependency implemention
        /// </summary>
        /// <returns></returns>
        public override ICacheDependency Create()
        {
            return new T();
        }
    }

    public static class CacheDependencyFactory
    {
        private static Dictionary<Type, CacheDependencyCreator>
            cacheDependencyCreators = new Dictionary<Type, CacheDependencyCreator>();
        private static object syncRoot = new object();

        public static T CreateCacheDependency<T>()
            where T : ICacheDependency
        {
            ICacheDependency cacheDependency = null;
            Type typeOfT = typeof(T);

            lock (syncRoot)
            {
                if (cacheDependencyCreators.ContainsKey(typeOfT))
                    // The cacheDependencyCreators dictionary already
                    // has a creator for this type of cache dependency,
                    // so create the instance by calling its Create() method
                    cacheDependency = ((CacheDependencyCreator)cacheDependencyCreators[typeOfT])
                        .Create();
                else
                {
                    // Get the cache dependency configuration for the cache dependency
                    // interface type
                    var cacheDependencyConfiguration =
                        CacheProviderConfigurationSection.Current.
                        CacheDependencies[typeOfT.Name];
                    if (cacheDependencyConfiguration != null)
                    {
                        // Get the type for the implementation of the specified cache
                        // dependency interface
                        string strCacheDependencyImplementationType =
                            cacheDependencyConfiguration.CacheDependencyType;
                        Type typeOfCacheDependencyImplementation =
                            Type.GetType(strCacheDependencyImplementationType);
                        // Get the Type reference for the CacheDependencyCreator
                        // generic class
                        Type typeOfCacheDependencyCreator =
                            typeof(CacheDependencyCreator<>);
                        if (typeOfCacheDependencyImplementation != null
                            && typeOfCacheDependencyCreator != null)
                        {
                            // Get the type reference for
                            // CacheDependencyCreator<cache interface implementation>
                            Type typeOfCacheDependencyCreatorForImplementationOfT =
                                typeOfCacheDependencyCreator.MakeGenericType(
                                new Type[] { typeOfCacheDependencyImplementation });
                            if (typeOfCacheDependencyCreatorForImplementationOfT != null)
                            {
                                // Create the CacheDependencyCreator<cache interface implementation>
                                // instance
                                var cacheDependencyCreator = (CacheDependencyCreator)Activator.CreateInstance(
                                    typeOfCacheDependencyCreatorForImplementationOfT);
                                // Add the CacheDependencyCreator<cache interface implementation>
                                // instance to the dictionary
                                cacheDependencyCreators.Add(typeOfT, cacheDependencyCreator);
                                
                                // Create the Cache Dependency instance
                                cacheDependency = cacheDependencyCreator.Create();
                            }
                                
                        }
                    }
                }
            }

            return (T)cacheDependency;

        }
    }
}