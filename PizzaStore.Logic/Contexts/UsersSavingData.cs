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
    public class UsersSavingData : ISavingData<Users>
    {
        private Database _database;
        public UsersSavingData(Database database)
        {
            _database = database;
        }
        public List<Users> ReadFromJson(string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                var usersList = JsonSerializer.Deserialize<List<Users>>(jsonString);
                return usersList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading data from JSON file at path {path}: {ex.Message}");
            }
        }

        public void SaveDataToJson(List<Users> list, string path)
        {
            try
            {
                string jsonstring = JsonSerializer.Serialize<List<Users>>(_database.UserList, new JsonSerializerOptions
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
