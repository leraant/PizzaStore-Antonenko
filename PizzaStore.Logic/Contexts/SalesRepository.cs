using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace PizzaStore.Logic.Contexts
{
    public class SalesRepository : IRepository<Sales>, ISavingData<Sales>
    {
        private Database _database;
        public SalesRepository(Database database)
        {
            _database = database;
        }
        public bool Add(Sales entity)
        {
            _database.SalesList.Add(entity);
            return true;
        }

        public void Delete(int Id)
        {
            Sales saleToDelete = _database.SalesList.Find(s => s.Id == Id );
            if (saleToDelete != null)
            {
                _database.SalesList.Remove(saleToDelete);
            }
        }
        public Sales Find(int Id)
        {
            Sales p = _database.SalesList.FirstOrDefault(f => f.Id == Id);
            if (p != null)
            {
                return p;
            }
            else { throw new Exception("Sale not found."); }
        }

        public List<Sales> ReadFromJson( string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true
                };
                var salesList = JsonSerializer.Deserialize<List<Sales>>(jsonString, options);
                return salesList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       

        public void SaveDataToJson(List<Sales> sales, string path)
        {
            try
            {
                string jsonstring = JsonSerializer.Serialize<List<Sales>>(_database.SalesList, new JsonSerializerOptions
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
