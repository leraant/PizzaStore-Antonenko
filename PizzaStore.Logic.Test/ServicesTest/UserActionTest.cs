using Microsoft.Identity.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using PizzaStore.Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace PizzaStore.Logic.Test.ServicesTest
{

    [TestClass]
    public class UserActionTests
    {
        [TestMethod]
        public void MakeOrderWithCart()
        {
            // Arrange
            Database _database = new Database();
            UserAction _userAction = new UserAction(_database);

            Orders order = new Orders("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2, "5678 2365 1522 5363", "Olena Kvitka", "Almazna", 25, "111/123", true, 101);

            // Act
            bool result = _userAction.MakeOrderWithCard("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2, "5678 2365 1522 5363", "Olena Kvitka", "Almazna", 25, "111/123", true, 101);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _database.OrdersList.Count);
            Assert.AreEqual("Pizza", _database.OrdersList[0].NameOfProduct);
        }
        [TestMethod]
        public void MakeOrderWithCart_ReturnsTrueAndAddsOrderToDatabase()
        {
            // Arrange
            Database _database = new Database();
            UserAction _userAction = new UserAction(_database);

            Orders order = new Orders("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2, "5678 2365 1522 5363", "Olena Kvitka", "Almazna", 25, "111/123", true, 101);

            // Act
            bool result = _userAction.MakeOrderWithCard("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2, "5678 2365 1522 5363", "Olena Kvitka", "Almazna", 25, "111/123", true, 101);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _database.OrdersList.Count);
            Assert.AreEqual("Pizza", _database.OrdersList[0].NameOfProduct);
        }

       
        [TestMethod]
        public void MakeOrderWithCart_AddsOrderWithCorrectProductDetails()
        {
            // Arrange
            Database _database = new Database();
            UserAction _userAction = new UserAction(_database);

            Orders order = new Orders("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2, "5678 2365 1522 5363", "Olena Kvitka", "Almazna", 25, "111/123", true, 101);

            // Act
            bool result = _userAction.MakeOrderWithCard("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2, "5678 2365 1522 5363", "Olena Kvitka", "Almazna", 25, "111/123", true, 101);

            // Assert
            Assert.AreEqual("Pizza", _database.OrdersList[0].NameOfProduct);
            Assert.AreEqual(ProductType.Pizza, _database.OrdersList[0].TypeOfProduct);
        }
        [TestMethod]
        public void MakeOrder()
        {
            // Arrange
            Database _database = new Database();
            UserAction _userAction = new UserAction(_database);

            Orders order = new Orders("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2,  "Almazna", 25,  false, 101);

            // Act
            bool result = _userAction.MakeOrder("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2,  "Almazna", 25,  true, 101);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _database.OrdersList.Count);
            Assert.AreEqual("Pizza", _database.OrdersList[0].NameOfProduct);
        }
        [TestMethod]
        public void MakeOrder_ReturnsTrueAndAddsOrderToDatabase()
        {
            // Arrange
            Database _database = new Database();
            UserAction _userAction = new UserAction(_database);

            Orders order = new Orders("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2, "Almazna", 25,  false, 101);

            // Act
            bool result = _userAction.MakeOrder("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2,  "Almazna", 25,  false, 101);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _database.OrdersList.Count);
            Assert.AreEqual("Pizza", _database.OrdersList[0].NameOfProduct);
        }


        [TestMethod]
        public void MakeOrderAddsOrderWithCorrectProductDetails()
        {
            // Arrange
            Database _database = new Database();
            UserAction _userAction = new UserAction(_database);

            Orders order = new Orders("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2,  "Almazna", 25, false, 101);

            // Act
            bool result = _userAction.MakeOrder("Pizza", ProductType.Pizza, new DateTime(2023, 05, 25), Location.Lviv,
                2,  "Almazna", 25,  false, 101);

            // Assert
            Assert.AreEqual("Pizza", _database.OrdersList[0].NameOfProduct);
            Assert.AreEqual(ProductType.Pizza, _database.OrdersList[0].TypeOfProduct);
        }
        [TestMethod]
        public void AddToCard()
        {
            // Arrange
            Database _database = new Database();
            UserAction _userAction = new UserAction(_database);

            // Act
            bool result = _userAction.AddToCart("Pepperoni", ProductType.Pizza, 130, 12, 10, 101, 101, "Assert/dfdfdf.png");

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _database.CartList.Count);
            Assert.AreEqual("Pepperoni", _database.CartList[0].NameOfProduct);
        }
    
        [TestMethod]
        public void LeaveFeedback_AddsFeedback()
        {
            // Arrange
            Database _database = new Database();
            UserAction _userAction = new UserAction(_database);

            // Act
            bool result = _userAction.LeaveFeedback("Good pizza!", 101, DateTime.Now, "Lera");
             _userAction.LeaveFeedback("Delicious pizza, great service.", 102, DateTime.Now, "Lera");
            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(2, _database.FeedbacksList.Count);
            Assert.AreEqual("Good pizza!", _database.FeedbacksList[0].Feedbacks);
        }

        [TestMethod]
        public void RemoveOrderFromCard()
        {
            // Arrange
            Database database = new Database();
            UserAction userAction = new UserAction(database);


            Cart cart = new Cart("Pepperoni", ProductType.Pizza, 101, 12, 10, 130, 101, "Assert/dfdfdf.png");
            database.CartList.Add(cart);

            // Act
            bool result = userAction.RemoveOrderFromCard(101,101);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, database.CartList.Count);
        }
        
    }
}

    
