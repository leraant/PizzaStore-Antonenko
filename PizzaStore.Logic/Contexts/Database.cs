using PizzaStore.Logic.Models;
using PizzaStore.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.System;

namespace PizzaStore.Logic.Contexts
{
    public class Database
    {
        public List<Cart> CartList { get; set; }
        public List<Menu> MenuList { get ; set ; }
        public List<Orders> OrdersList { get; set; }
        public List<Sales> SalesList { get; set; }
        public List<Favorites> FavoritesList { get; set; }
        public List<Users> UserList { get; set; }
        public List<Feedback> FeedbacksList { get; set; }
        public Database()
        {
            CartList = new List<Cart>();
            MenuList = new List<Menu>();
            OrdersList = new List<Orders>();
            SalesList = new List<Sales>();
            FavoritesList = new List<Favorites>();
            UserList = new List<Users>();
            FeedbacksList = new List<Feedback>();
        }
    }
}
