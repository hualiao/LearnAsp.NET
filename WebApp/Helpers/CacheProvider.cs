
namespace WebApp.Helpers
{
    using System.Web.Caching;

    public interface ICacheDependency
    {
    }

    public interface ISqlCacheDependency : ICacheDependency
    {
        string DatabaseConnectionName { get; set; }
        string TableName { get; set; }
        System.Data.SqlClient.SqlCommand SqlCommand { get; set; }
    }

    public interface IKeyCacheDependency : ICacheDependency
    {
        string[] Keys { get; set; }
    }

    public interface IAspNetCacheDependency
    {
        CacheDependency CreateAspNetCacheDependency();
    }

    public class AspNetCacheDependency : ISqlCacheDependency, IAspNetCacheDependency
    {

        #region ISqlCacheDependency Members

        public string DatabaseConnectionName
        {
            get;
            set;
        }

        public string TableName
        {
            get;
            set;
        }

        public System.Data.SqlClient.SqlCommand SqlCommand
        {
            get;
            set;
        }

        #endregion

        #region IAspNetCacheDependency Members
        public System.Web.Caching.CacheDependency CreateAspNetCacheDependency()
        {
            if (SqlCommand != null)
                return new SqlCacheDependency(SqlCommand);
            else
                return new SqlCacheDependency(DatabaseConnectionName, TableName);
        }

        #endregion 
    }

    public class AspNetKeyCacheDependency : IKeyCacheDependency, IAspNetCacheDependency
    {
        #region IKeyCacheDependency Members

        public string[] Keys
        {
            get;
            set;
        }

        #endregion

        #region IAspNetCacheDependency Members

        public CacheDependency CreateAspNetCacheDependency()
        {
            return new CacheDependency(null, Keys);
        }

        #endregion
    }
}