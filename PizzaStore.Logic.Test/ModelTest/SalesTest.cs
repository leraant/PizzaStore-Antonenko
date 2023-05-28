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
    public class SalesTest
    {
        [TestMethod]
        public void CalculatePriceWithDiscount_ReturnsCorrectValue()
        {
            // Arrange
            double price = 100;
            int discountPercent = 10;
            double expectedPriceWithDiscount = 90;

            // Act
            double actualPriceWithDiscount = Sales.CalculatePriceWithDiscount(price, discountPercent);

            // Assert
            Assert.AreEqual(expectedPriceWithDiscount, actualPriceWithDiscount);
        }
       
    }
}
