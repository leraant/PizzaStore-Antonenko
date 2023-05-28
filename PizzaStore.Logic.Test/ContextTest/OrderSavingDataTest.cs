using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;
using Path = System.IO.Path;

namespace PizzaStore.Logic.Test.ContextTest
{
    [TestClass]
    public class OrderSavingDataTest
    {
        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            OrdersSavingData repository = new OrdersSavingData(_testDatabase);
            Orders order = new Orders("Carbonara", ProductType.Pizza, 
                DateTime.Parse("2023-05-19T15:11:50.8881327+03:00"), Location.Kyiv,
                2, "4444 4444 4444 4444", "Lera", "Chretschatyk", 23, "123/234", true,100);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_orders.json");
            _testDatabase.OrdersList.Add(order);

            // Act
            repository.SaveDataToJson(_testDatabase.OrdersList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(Path.GetFullPath(_testFilePath)));
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesCorrectDataToJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            OrdersSavingData repository = new OrdersSavingData(_testDatabase);
            Orders order = new Orders("Carbonara", ProductType.Pizza,
              DateTime.Parse("2023-05-19T15:11:50.8881327+03:00"), Location.Kyiv,
              2, "4444 4444 4444 4444", "Lera", "Chretschatyk", 23, "123/234", true, 100);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_orders.json");
            _testDatabase.OrdersList.Add(order);

            // Act
            repository.SaveDataToJson(_testDatabase.OrdersList, _testFilePath);

            // Assert
            string json = File.ReadAllText(_testFilePath);
            List<Orders> savedOrders = JsonSerializer.Deserialize<List<Orders>>(json);
            Assert.IsNotNull(savedOrders);
            Assert.AreEqual(1, savedOrders.Count);
            Assert.AreEqual(order.Id, savedOrders[0].Id);
            Assert.AreEqual(order.NameOfProduct, savedOrders[0].NameOfProduct);
            Assert.AreEqual(order.TypeOfProduct, savedOrders[0].TypeOfProduct);
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFileWithPath()
        {
            // Arrange
            Database _testDatabase = new Database();
            OrdersSavingData repository = new OrdersSavingData(_testDatabase);
            Orders order = new Orders("Carbonara", ProductType.Pizza,
             DateTime.Parse("2023-05-19T15:11:50.8881327+03:00"), Location.Kyiv,
             2, "4444 4444 4444 4444", "Lera", "Chretschatyk", 23, "123/234", true, 100);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_orders.json");
            _testDatabase.OrdersList.Add(order);

            // Act
            repository.SaveDataToJson(_testDatabase.OrdersList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(_testFilePath));
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsNonNullList()
        {
            // Arrange
            Database _testDatabase = new Database();
            OrdersSavingData repository = new OrdersSavingData(_testDatabase);
            Orders order1 = new Orders("Carbonara", ProductType.Pizza,
             DateTime.Parse("2023-05-19T15:11:50.8881327+03:00"), Location.Kyiv,
             2, "4444 4444 4444 4444", "Lera", "Chretschatyk", 23, "123/234", true, 100);
            Orders order2 = new Orders("Havaii", ProductType.Pizza, DateTime.Parse("2023-05-19T14:43:43.0708336+03:00"), Location.Kyiv, 1, null, null, "dddddddd", 12, null, false,101);
            _testDatabase.OrdersList.Add(order1);
            _testDatabase.OrdersList.Add(order2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_orders.json");
            repository.SaveDataToJson(_testDatabase.OrdersList, _testFilePath);

            // Act
            List<Orders> ordersFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.IsNotNull(ordersFromJson);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectNumberOfItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            OrdersSavingData repository = new OrdersSavingData(_testDatabase);
            Orders order1 = new Orders( "Carbonara", ProductType.Pizza, DateTime.Parse("2023-05-19T15:11:50.8881327+03:00"), Location.Kyiv, 2, "4444 4444 4444 4444", "Lera", "Chretschatyk", 23, "123/234", true, 101);
            Orders order2 = new Orders( "Havaii", ProductType.Pizza, DateTime.Parse("2023-05-19T14:43:43.0708336+03:00"), Location.Kyiv, 1, null, null, "dddddddd", 12, null, false, 101);
            _testDatabase.OrdersList.Add(order1);
            _testDatabase.OrdersList.Add(order2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_orders.json");
            repository.SaveDataToJson(_testDatabase.OrdersList, _testFilePath);

            // Act
            List<Orders> ordersFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(2, ordersFromJson.Count);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectOrderItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            OrdersSavingData repository = new OrdersSavingData(_testDatabase);
            Orders order1 = new Orders( "Carbonara", ProductType.Pizza, DateTime.Parse("2023-05-19T15:11:50.8881327+03:00"), Location.Kyiv, 2, "4444 4444 4444 4444", "Lera", "Chretschatyk", 23, "123/234", true, 101);
            Orders order2 = new Orders( "Havaii", ProductType.Pizza, DateTime.Parse("2023-05-19T14:43:43.0708336+03:00"), Location.Kyiv, 1, null, null, "dddddddd", 12, null, false, 101);
            _testDatabase.OrdersList.Add(order1);
            _testDatabase.OrdersList.Add(order2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_orders.json");
            repository.SaveDataToJson(_testDatabase.OrdersList, _testFilePath);

            // Act
            List<Orders> ordersFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(order1.Id, ordersFromJson[0].Id);
            Assert.AreEqual(order1.NameOfProduct, ordersFromJson[0].NameOfProduct);
            Assert.AreEqual(order1.TypeOfProduct, ordersFromJson[0].TypeOfProduct);
        }
        

    }


}
