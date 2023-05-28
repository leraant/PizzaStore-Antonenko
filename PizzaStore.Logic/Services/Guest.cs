using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.System;
using Windows.UI.Popups;

namespace PizzaStore.Logic.Services
{
    public class Guest
    {
        private Database _database;

        public Guest(Database database)
        {
            _database = database;
        }

        public bool Register(int id, string firstName, string lastName, string email, string password, string confirmPassword, Location city)
        {
            if (password != confirmPassword) // перевірка паролів на збіг
            {
                return false; 
            }
            else
            {
                // Створення нового об'єкта User на основі гостя
                Users user = new Users(id, email, firstName, lastName, city, password);
                // Додавання користувача до списку UserList
                _database.UserList.Add(user);
                return true;
            }
        }
       
    }
    
}

