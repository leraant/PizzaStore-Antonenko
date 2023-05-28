using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Contexts
{
    public class OrdersSavingData :  ISavingData<Orders>
    {
        private Database _database;
        public OrdersSavingData(Database database)
        {
            _database = database;
        }
      

        public List<Orders> ReadFromJson( string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                var options = new JsonSerializerOptions { WriteIndented = true };
                var ordersList = JsonSerializer.Deserialize<List<Orders>>(jsonString, options);
                return ordersList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void SaveDataToJson(List<Orders> orders, string path)
        {
            try
            {
                string jsonstring = JsonSerializer.Serialize<List<Orders>>(orders, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                });

                File.WriteAllText(path, jsonstring);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving data to JSON file at path {path}: {ex.Message}");
            }
        }
    }
}
