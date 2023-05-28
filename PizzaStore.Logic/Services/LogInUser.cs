using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Services
{
    public class LogInUser
    {
        public static int Id { get; set; }
        public static bool InAccount { get; set; } = false;
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static Location location { get; set; } = Location.Kyiv;   
    }
}
