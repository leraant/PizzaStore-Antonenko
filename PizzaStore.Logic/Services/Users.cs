using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Services
{
   public class Users: Person
    {
        public Users(int id, string email, string firstName, string lastName, Location city, string password)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Password = password;
        }

    }
}
