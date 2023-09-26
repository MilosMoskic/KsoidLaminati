using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace KsoidLaminati.Classes
{
    public class SqlDataAccess
    {
        public static List<ItemModel> LoadItems()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ItemModel>("select * from Item", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveItem(ItemModel item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Item (ItemType, ItemBrand, ItemName, Quantity) " +
                    "values (@ItemType, @ItemBrand, @ItemName, @Quantity)", item);
            }
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
