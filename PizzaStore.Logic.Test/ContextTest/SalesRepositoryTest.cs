using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Test.ContextTest
{
    [TestClass]
    public class SalesRepositoryTest
    {

        [TestMethod]
        public void TestAddNewSale_Success()
        {
            // Arrange
            Database _testDatabase = new Database();
            SalesRepository repository = new SalesRepository(_testDatabase);
            Sales newSale = new Sales("Desserts cake", ProductType.Desserts, 30, 15,
                101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");

            // Act
            bool result = repository.Add(newSale);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _testDatabase.SalesList.Count);
            Assert.IsTrue(_testDatabase.SalesList.Contains(newSale));
        }
        [TestMethod]
        public void TestAddNewSale_Failure()
        {
            // Arrange
            SalesRepository repository = null;
            Sales newSale = new Sales("Desserts cake", ProductType.Desserts, 30, 15,
                101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");

            // Act
            bool result = repository?.Add(newSale) ?? false;

            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestAddNewSale_DatabaseCount()
        {
            // Arrange
            Database _testDatabase = new Database();
            SalesRepository repository = new SalesRepository(_testDatabase);
            Sales newSale = new Sales("Desserts cake", ProductType.Desserts, 30, 15,
                101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");

            // Act
            bool result = repository.Add(newSale);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _testDatabase.SalesList.Count);
        }


        [TestMethod]
        public void Delete_FirstItem_RemovesFromSalesList()
        {
            // Arrange
            Database _testDatabase = new Database();
            _testDatabase.SalesList.Add(new Sales("Pizza", ProductType.Pizza, 30, 15,
                100, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png"));
            _testDatabase.SalesList.Add(new Sales("Juice", ProductType.Drinks, 30, 15,
                101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png"));
            SalesRepository repository = new SalesRepository(_testDatabase);

            // Act
            repository.Delete(100);

            // Assert
            Assert.AreEqual(1, _testDatabase.SalesList.Count);
            Assert.IsFalse(_testDatabase.SalesList.Any(s => s.Id == 100));
            Assert.IsTrue(_testDatabase.SalesList.Any(s => s.Id == 101));
        }
        [TestMethod]
        public void Delete_NonExistingItem_DoesNotModifySalesList()
        {
            // Arrange
            Database _testDatabase = new Database();
            _testDatabase.SalesList.Add(new Sales("Pizza", ProductType.Pizza, 30, 15,
                100, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png"));
            SalesRepository repository = new SalesRepository(_testDatabase);

            // Act
            repository.Delete(101);

            // Assert
            Assert.AreEqual(1, _testDatabase.SalesList.Count);
            Assert.IsTrue(_testDatabase.SalesList.Any(s => s.Id == 100));
            Assert.IsFalse(_testDatabase.SalesList.Any(s => s.Id == 101));
        }
        [TestMethod]
        public void Delete_LastItem_RemovesFromSalesList()
        {
            // Arrange
            Database _testDatabase = new Database();
            _testDatabase.SalesList.Add(new Sales("Pizza", ProductType.Pizza, 30, 15,
                100, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png"));
            _testDatabase.SalesList.Add(new Sales("Juice", ProductType.Drinks, 30, 15,
                101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png"));
            SalesRepository repository = new SalesRepository(_testDatabase);

            // Act
            repository.Delete(101);

            // Assert
            Assert.AreEqual(1, _testDatabase.SalesList.Count);
            Assert.IsTrue(_testDatabase.SalesList.Any(s => s.Id == 100));
            Assert.IsFalse(_testDatabase.SalesList.Any(s => s.Id == 101));
        }

        [TestMethod]
        public void Find_SaleExists_ReturnsSale()
        {
            // Arrange
            Database _database = new Database();
            SalesRepository repository = new SalesRepository(_database);
            Sales sale = new Sales("Desserts cake", ProductType.Desserts, 3, 15,
            101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            _database.SalesList.Add(sale);
            // Act
            Sales testDessert = repository.Find(sale.Id);

            // Assert
            Assert.IsNotNull(testDessert);
            Assert.AreEqual(sale, testDessert);
        }
        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            SalesRepository repository = new SalesRepository(_testDatabase);
            Sales sale1 = new Sales("Pizza", ProductType.Pizza, 3, 15, 101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            Sales sale2 = new Sales("Juice", ProductType.Drinks, 3, 15, 102, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_sales.json");
            _testDatabase.SalesList.Add(sale1);
            _testDatabase.SalesList.Add(sale2);

            // Act
            repository.SaveDataToJson(_testDatabase.SalesList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(_testFilePath));
        }
        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesCorrectDataToJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            SalesRepository repository = new SalesRepository(_testDatabase);
            Sales sale1 = new Sales("Pizza", ProductType.Pizza, 3, 15, 101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            Sales sale2 = new Sales("Juice", ProductType.Drinks, 3, 15, 102, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_sales.json");
            _testDatabase.SalesList.Add(sale1);
            _testDatabase.SalesList.Add(sale2);

            // Act
            repository.SaveDataToJson(_testDatabase.SalesList, _testFilePath);

            // Assert
            string json = File.ReadAllText(_testFilePath);
            List<Sales> savedSales = JsonSerializer.Deserialize<List<Sales>>(json);
            Assert.IsNotNull(savedSales);
            Assert.AreEqual(2, savedSales.Count);
            Assert.AreEqual(sale1.Id, savedSales[0].Id);
        }
        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFileWithPath()
        {
            // Arrange
            Database _testDatabase = new Database();
            SalesRepository repository = new SalesRepository(_testDatabase);
            Sales sale1 = new Sales("Pizza", ProductType.Pizza, 3, 15, 101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            Sales sale2 = new Sales("Juice", ProductType.Drinks, 3, 15, 102, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_sales.json");
            _testDatabase.SalesList.Add(sale1);
            _testDatabase.SalesList.Add(sale2);

            // Act
            repository.SaveDataToJson(_testDatabase.SalesList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(_testFilePath));
        }
        [TestMethod]
        public void TestReadFromJson_ReturnsNonNullList()
        {
            // Arrange
            Database _testDatabase = new Database();
            SalesRepository repository = new SalesRepository(_testDatabase);
            Sales sale1 = new Sales("Pizza", ProductType.Pizza, 3, 15,
             101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            Sales sale2 = new Sales("Juice", ProductType.Drinks, 3, 15,
             101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            _testDatabase.SalesList.Add(sale1);
            _testDatabase.SalesList.Add(sale2);
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_sales.json");
            repository.SaveDataToJson(_testDatabase.SalesList, _testFilePath);

            // Act
            List<Sales> salesFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.IsNotNull(salesFromJson);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectNumberOfItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            SalesRepository repository = new SalesRepository(_testDatabase);
            Sales sale1 = new Sales("Pizza", ProductType.Pizza, 3, 15,
             101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            Sales sale2 = new Sales("Juice", ProductType.Drinks, 3, 15,
             101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            _testDatabase.SalesList.Add(sale1);
            _testDatabase.SalesList.Add(sale2);
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_sales.json");
            repository.SaveDataToJson(_testDatabase.SalesList, _testFilePath);

            // Act
            List<Sales> salesFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(2, salesFromJson.Count);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectSalesItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            SalesRepository repository = new SalesRepository(_testDatabase);
            Sales sale1 = new Sales("Pizza", ProductType.Pizza, 3, 15,
             101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            Sales sale2 = new Sales("Juice", ProductType.Drinks, 3, 15,
             101, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\margherita pizza.png");
            _testDatabase.SalesList.Add(sale1);
            _testDatabase.SalesList.Add(sale2);
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_sales.json");
            repository.SaveDataToJson(_testDatabase.SalesList, _testFilePath);

            // Act
            List<Sales> salesFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(sale1.NameOfproduct, salesFromJson[0].NameOfproduct);
            Assert.AreEqual(sale1.TypeOfproduct, salesFromJson[0].TypeOfproduct);
        }
    }
}
