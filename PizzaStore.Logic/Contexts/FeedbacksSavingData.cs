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
    public class FeedbacksSavingData : ISavingData<Feedback>
    {
        private Database _database;
        public FeedbacksSavingData(Database database)
        {
            _database = database;
        }

        public List<Feedback> ReadFromJson(string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                var feedbackList = JsonSerializer.Deserialize<List<Feedback>>(jsonString);
                return feedbackList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading data from JSON file at path {path}: {ex.Message}");
            }
        }

        public void SaveDataToJson(List<Feedback> list, string path)
        {
            try
            {
                string jsonstring = JsonSerializer.Serialize<List<Feedback>>(_database.FeedbacksList, new JsonSerializerOptions
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
