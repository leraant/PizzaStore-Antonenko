using PizzaStore.Logic.Models;
using PizzaStore.Logic.Services;
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
    public class FavoriteSavingData : ISavingData<Favorites>
    {
        private Database _database;
        public FavoriteSavingData(Database database)
        {
            _database = database;
        }
        public List<Favorites> ReadFromJson(string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                var options = new JsonSerializerOptions { WriteIndented = true };
                var favoriteList = JsonSerializer.Deserialize<List<Favorites>>(jsonString, options);
                return favoriteList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveDataToJson(List<Favorites> list, string path)
        {
            try
            {
                string jsonstring = JsonSerializer.Serialize<List<Favorites>>(_database.FavoritesList, new JsonSerializerOptions
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
