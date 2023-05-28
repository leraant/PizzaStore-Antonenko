using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Services
{
    public class Administrator:Person
    {
        public Administrator(string email, string password) 
        {
            Email = email;
            Password = password;
        }
        public static Administrator ReadFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Administrator>(json);
        }

    }
}
