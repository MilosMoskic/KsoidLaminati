using Dapper;
using System.Collections.Generic;
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
        //public static ItemModel FindItem(int id)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        string selectQuery = $@"select * from Item where id = {id}";
        //        var entity = cnn.Query<ItemModel>(selectQuery, id);
        //        return entity as ItemModel;
        //    }
        //}
        public static void SaveItem(ItemModel item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Item (ItemType, ItemBrand, ItemName, Quantity) " +
                    "values (@ItemType, @ItemBrand, @ItemName, @Quantity)", item);
            }
        }
        public static void UpdateItem(ItemModel itemModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string selectQuery = $@"select * from Item where id = {itemModel.Id}";
                var entity = cnn.Query<ItemModel>(selectQuery, itemModel.Id);
                if(entity != null)
                {
                    string updateQuery = @"update Item Set ItemType = @ItemType, ItemBrand = @ItemBrand, ItemName = @ItemName, Quantity = @Quantity Where id = @Id";

                    cnn.Execute(updateQuery, itemModel);
                }
            }
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
