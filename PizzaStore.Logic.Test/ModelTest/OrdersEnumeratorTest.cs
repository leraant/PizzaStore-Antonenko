using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Test.ModelTest
{
    [TestClass]
    public class OrdersEnumeratorTest
    {
        [TestMethod]
        public void TestOrdersEnumerator()
        {
            // Arrange 
            List<Orders> ordersList = new List<Orders>()
            {
                new Orders("Pizza Margherita", ProductType.Pizza, DateTime.Now, Location.Kyiv, 2, "Nezalezhnosti street", 23, true, 1),
                new Orders("Juice", ProductType.Drinks, DateTime.Now, Location.Lviv, 1, "Almazna", 4, false, 2),
                new Orders("Tiramisu", ProductType.Desserts, DateTime.Now, Location.Lviv, 3, "Almazna", 7, true, 3)
            };
            // Act
            OrdersEnumerator enumerator = new OrdersEnumerator(ordersList);
            // Assert
            foreach (Orders order in enumerator)
            {
                Assert.IsNotNull(order);
            }
        }
    }
}
