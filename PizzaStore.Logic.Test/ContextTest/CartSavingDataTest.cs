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
    public class CartSavingDataTest
    {
        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            CartSavingData repository = new CartSavingData(_testDatabase);
            Cart cart = new Cart( "Havaii", ProductType.Pizza, 101, 9.9, 10, 100, 100, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_cart.json");
            _testDatabase.CartList.Add(cart);

            // Act
            repository.SaveDataToJson(_testDatabase.CartList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(Path.GetFullPath(_testFilePath)));
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesCorrectDataToJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            CartSavingData repository = new CartSavingData(_testDatabase);
            Cart cart = new Cart( "Havaii", ProductType.Pizza, 101, 9.9, 10, 100, 100, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_cart.json");
            _testDatabase.CartList.Add(cart);

            // Act
            repository.SaveDataToJson(_testDatabase.CartList, _testFilePath);

            // Assert
            string json = File.ReadAllText(_testFilePath);
            List<Cart> savedCart = JsonSerializer.Deserialize<List<Cart>>(json);
            Assert.IsNotNull(savedCart);
            Assert.AreEqual(1, savedCart.Count);
            Assert.AreEqual(cart.Id, savedCart[0].Id);
            Assert.AreEqual(cart.NameOfProduct, savedCart[0].NameOfProduct);
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFileWithPath()
        {
            // Arrange
            Database _testDatabase = new Database();
            CartSavingData repository = new CartSavingData(_testDatabase);
            Cart cart = new Cart("Havaii", ProductType.Pizza, 101, 9.9, 10, 100, 100, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_cart.json");
            _testDatabase.CartList.Add(cart);

            // Act
            repository.SaveDataToJson(_testDatabase.CartList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(_testFilePath));
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsNonNullList()
        {
            // Arrange
            Database _testDatabase = new Database();
            CartSavingData repository = new CartSavingData(_testDatabase);
            Cart cart1 = new Cart( "Havaii", ProductType.Pizza, 101, 9.9, 10, 100, 100, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            Cart cart2 = new Cart( "Drinks", ProductType.Drinks, 102, 12.3, 0, 100, 101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\location.png");
            _testDatabase.CartList.Add(cart1);
            _testDatabase.CartList.Add(cart2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_cart.json");
            repository.SaveDataToJson(_testDatabase.CartList, _testFilePath);

            // Act
            List<Cart> cartFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.IsNotNull(cartFromJson);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectNumberOfItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            CartSavingData repository = new CartSavingData(_testDatabase);
            Cart cart1 = new Cart( "Havaii", ProductType.Pizza, 101, 9.9, 10, 100, 100, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            Cart cart2 = new Cart("Drinks", ProductType.Drinks, 102, 12.3, 0, 100, 101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\location.png");
            _testDatabase.CartList.Add(cart1);
            _testDatabase.CartList.Add(cart2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_cart.json");
            repository.SaveDataToJson(_testDatabase.CartList, _testFilePath);

            // Act
            List<Cart> cartFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(2, cartFromJson.Count);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectCartItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            CartSavingData repository = new CartSavingData(_testDatabase);
            Cart cart1 = new Cart( "Havaii", ProductType.Pizza, 101, 9.9, 10, 100, 100, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            Cart cart2 = new Cart( "Drinks", ProductType.Drinks, 102, 12.3, 0, 100, 101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\location.png");
            _testDatabase.CartList.Add(cart1);
            _testDatabase.CartList.Add(cart2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_cart.json");
            repository.SaveDataToJson(_testDatabase.CartList, _testFilePath);

            // Act
            List<Cart> cartFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(cart1.Id, cartFromJson[0].Id);
            Assert.AreEqual(cart1.NameOfProduct, cartFromJson[0].NameOfProduct);
        }

    }
}
