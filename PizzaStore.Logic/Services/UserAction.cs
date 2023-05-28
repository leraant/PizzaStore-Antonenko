using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace PizzaStore.Logic.Services
{
    
    public class UserAction
    {
        private Database _database;
        public UserAction(Database database)
        {
            _database = database;
        }
        public delegate void OrderRemovedEventHandler(object sender, string message);
        public delegate void MakeOrderEventHandler(object sender, string message);
        public event OrderRemovedEventHandler OrderRemovedNotify;
        public event MakeOrderEventHandler MakeOrderNotify;
       
        public bool MakeOrderWithCard(string nameOfproduct, ProductType typeOfproduct, DateTime date, Location city, int numberOfproducts, string cardNumber, string cardHolderName, string Street, int houseNumber, string CVVCVC, bool payment_by_card, int userId)
        {
            Orders order = new Orders(nameOfproduct, typeOfproduct, date, city, numberOfproducts, cardNumber, cardHolderName, Street, houseNumber, CVVCVC, payment_by_card, userId);
            _database.OrdersList.Add(order);
            return true;
        }
        public bool MakeOrder(string nameOfproduct, ProductType typeOfproduct, DateTime date, Location city, int numberOfproducts,  string Street, int houseNumber,  bool payment_by_card, int userId)
        {
            Orders order = new Orders(nameOfproduct, typeOfproduct, date, city, numberOfproducts, Street, houseNumber, payment_by_card, userId);
            _database.OrdersList.Add(order);
            return true;
        }

        public bool AddToCart(string nameOfproduct, ProductType typeOfproduct,int id, double price, int discount, int userId, int orderNumber, string img)
        {
            Cart card = new Cart(nameOfproduct, typeOfproduct, id, price, discount, userId, orderNumber, img);
            _database.CartList.Add(card);
            MakeOrderNotify?.Invoke(this, "Added to cart.");
            return true;
        }

        public bool LeaveFeedback(string feedback, int UserId, DateTime date, string name)
        {
            Feedback fb = new Feedback(feedback, UserId, date, name);
            _database.FeedbacksList.Add(fb);
            return true;
        }
        public bool RemoveOrderFromCard( int id, int orderNumber)
        {
            Cart product = _database.CartList.FirstOrDefault(c => c.Id == id && c.OrderNumber == orderNumber);
            if (product == null)
            {
                return false;
            }
            _database.CartList.Remove(product);
            OrderRemovedNotify?.Invoke(this, "Product removed.");
            return true;
        }
        public async void DisplayMessage(object sender, string message)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}
