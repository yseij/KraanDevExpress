using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System.Configuration;

namespace KraanDevExpress.Module.BusinessObjects
{
    class DbConnectie
    {
        public IDataLayer GetDataLayer()
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conn = XpoDefault.GetConnectionPoolString(conn);
            XPDictionary dict = new ReflectionDictionary();
            IDataStore store = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.SchemaAlreadyExists);
            dict.GetDataStoreSchema(System.Reflection.Assembly.GetExecutingAssembly());
            IDataLayer dl = new ThreadSafeDataLayer(dict, store);
            return dl;
        }
    }
}
