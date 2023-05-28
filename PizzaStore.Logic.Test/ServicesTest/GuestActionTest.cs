using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using PizzaStore.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Test.ServicesTest
{
    [TestClass]
    public class GuestActionTest
    {
        [TestMethod]
        public void Register_ReturnsTrueWhenPasswordsMatch()
        {
            // Arrange
            Database database = new Database();
            Guest guestAction = new Guest(database);

            // Act
            bool result = guestAction.Register(101, "OLena", "Kvitka", "olenakvitka@gmail.com", "password", "password", Location.Kyiv);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, database.UserList.Count);
        }

        [TestMethod]
        public void Register_ReturnsFalseWhenPasswordsDoNotMatch()
        {
            // Arrange
            Database database = new Database();
            Guest guestAction = new Guest(database);
            
            // Act
            bool result = guestAction.Register(101, "OLena", "Kvitka", "olenakvitka@gmail.com", "password", "wrongpassword", Location.Kyiv);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, database.UserList.Count);
        }

      
    }
}
