using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Text.Unicode;

namespace PizzaStore.Logic.Contexts
{
    public class MenuRepository : IRepository<Menu>, ISavingData<Menu>
    {
        private Database _database;
        public MenuRepository(Database database)
        {
            _database = database;
        }
        public bool Add(Menu entity)
        {
            _database.MenuList.Add(entity);
            return true;
        }

        public void Delete(int Id)
        {
            Menu productToDelete = _database.MenuList.Find(s => s.Id == Id);
            if (productToDelete != null)
            {
                _database.MenuList.Remove(productToDelete);
            }
        }
        public Menu Find(int Id)
        {
            Menu p = _database.MenuList.FirstOrDefault(f => f.Id == Id);
            if (p != null)
            {
                return p;
            }
            else { throw new Exception("Product not found."); }
        }

        public List<Menu> ReadFromJson( string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                var options = new JsonSerializerOptions { WriteIndented = true };
                var menuList = JsonSerializer.Deserialize<List<Menu>>(jsonString, options);
                return menuList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        public void SaveDataToJson(List<Menu> product, string path)
        {
            try
            {
                string jsonstring = JsonSerializer.Serialize<List<Menu>>(_database.MenuList, new JsonSerializerOptions
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
