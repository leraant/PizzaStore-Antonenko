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
    public class MenuRepositoryTest
    {
        [TestMethod]
        public void Add_NewDessert_ReturnsTrue()
        {
            // Arrange
            Database _testDatabase = new Database();
            MenuRepository repository = new MenuRepository(_testDatabase);
            Menu newDessert = new Menu(103, "Tiramisu", 50, "cake", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);

            // Act
            bool result = repository.Add(newDessert);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add_NewDessert_IncreasesMenuListCount()
        {
            // Arrange
            Database _testDatabase = new Database();
            MenuRepository repository = new MenuRepository(_testDatabase);
            Menu newDessert = new Menu(103, "Tiramisu", 50, "cake", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);

            // Act
            repository.Add(newDessert);

            // Assert
            Assert.AreEqual(1, _testDatabase.MenuList.Count);
        }

        [TestMethod]
        public void Add_NewDessert_AddsToMenuList()
        {
            // Arrange
            Database _testDatabase = new Database();
            MenuRepository repository = new MenuRepository(_testDatabase);
            Menu newDessert = new Menu(103, "Tiramisu", 50, "cake", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);

            // Act
            repository.Add(newDessert);

            // Assert
            Assert.IsTrue(_testDatabase.MenuList.Contains(newDessert));
        }


        [TestMethod]
        public void Delete_ExistingDessert_RemovesFromMenuList()
        {
            // Arrange
            Database _testDatabase = new Database();
            _testDatabase.MenuList.Add(new Menu(101, "Chocolate Cake", 30, "Chocolate", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0));
            _testDatabase.MenuList.Add(new Menu(102, "Cheesecake", 40, "Cheese", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0));
            MenuRepository repository = new MenuRepository(_testDatabase);

            // Act
            repository.Delete(101);

            // Assert
            Assert.AreEqual(1, _testDatabase.MenuList.Count);
            Assert.IsFalse(_testDatabase.MenuList.Any(d => d.Id == 101));
        }

        [TestMethod]
        public void Delete_ExistingDessert_DecreasesMenuListCount()
        {
            // Arrange
            Database _testDatabase = new Database();
            _testDatabase.MenuList.Add(new Menu(101, "Chocolate Cake", 30, "Chocolate", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0));
            _testDatabase.MenuList.Add(new Menu(102, "Cheesecake", 40, "Cheese", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0));
            MenuRepository repository = new MenuRepository(_testDatabase);

            // Act
            repository.Delete(101);

            // Assert
            Assert.AreEqual(1, _testDatabase.MenuList.Count);
        }

        [TestMethod]
        public void Delete_NonExistingDessert_DoesNotAffectMenuList()
        {
            // Arrange
            Database _testDatabase = new Database();
            _testDatabase.MenuList.Add(new Menu(101, "Chocolate Cake", 30, "Chocolate", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0));
            _testDatabase.MenuList.Add(new Menu(102, "Cheesecake", 40, "Cheese", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0));
            MenuRepository repository = new MenuRepository(_testDatabase);

            // Act
            repository.Delete(999); // Assuming 999 is not a valid dessert ID

            // Assert
            Assert.AreEqual(2, _testDatabase.MenuList.Count);
        }

        [TestMethod]
        public void Find_DessertExists_ReturnsDessert()
        {
            // Arrange
            Database _database = new Database();
            MenuRepository _repository = new MenuRepository(_database);
            Menu dessert = new Menu(101, "Ice cream", 50, "Vanilla flavor", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            _database.MenuList.Add(dessert);

            // Act
            Menu testDessert = _repository.Find(dessert.Id);

            // Assert
            Assert.IsNotNull(testDessert);
        }

        [TestMethod]
        public void Find_DessertDoesNotExist_ThrowsException()
        {
            // Arrange
            Database _database = new Database();
            MenuRepository _repository = new MenuRepository(_database);
            Menu dessert = new Menu(101, "Ice cream", 50, "Vanilla flavor", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            _database.MenuList.Add(dessert);

            // Act and Assert
            Assert.ThrowsException<Exception>(() => _repository.Find(999));
        }

        [TestMethod]
        public void Find_DessertExists_ReturnsCorrectDessert()
        {
            // Arrange
            Database _database = new Database();
            MenuRepository _repository = new MenuRepository(_database);
            Menu dessert1 = new Menu(101, "Ice cream", 50, "Vanilla flavor", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            Menu dessert2 = new Menu(102, "Chocolate Cake", 30, "Chocolate", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            _database.MenuList.Add(dessert1);
            _database.MenuList.Add(dessert2);

            // Act
            Menu testDessert = _repository.Find(dessert2.Id);

            // Assert
            Assert.IsNotNull(testDessert);
            Assert.AreEqual(dessert2, testDessert);
        }
        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            MenuRepository repository = new MenuRepository(_testDatabase);
            Menu desserts = new Menu(101, "Chocolate Cake", 30, "Chocolate", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_desserts.json");
            _testDatabase.MenuList.Add(desserts);

            // Act
            repository.Add(desserts);
            repository.SaveDataToJson(_testDatabase.MenuList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(Path.GetFullPath(_testFilePath)));
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesCorrectDataToJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            MenuRepository repository = new MenuRepository(_testDatabase);
            Menu desserts = new Menu(101, "Chocolate Cake", 30, "Chocolate", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_desserts.json");
            _testDatabase.MenuList.Add(desserts);

            // Act
            repository.Add(desserts);
            repository.SaveDataToJson(_testDatabase.MenuList, _testFilePath);

            // Assert
            string json = File.ReadAllText(_testFilePath);
            List<Menu> savedDesserts = JsonSerializer.Deserialize<List<Menu>>(json);
            Assert.IsNotNull(savedDesserts);
            Assert.AreEqual(2, savedDesserts.Count);
            Assert.AreEqual(desserts.Id, savedDesserts[0].Id);
            Assert.AreEqual(desserts.NameOfproduct, savedDesserts[0].NameOfproduct);
        }

        
        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFileWithPath()
        {
                // Arrange
                Database _testDatabase = new Database();
                MenuRepository repository = new MenuRepository(_testDatabase);
                Menu desserts = new Menu(101, "Chocolate Cake", 30, "Chocolate", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
                string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_desserts.json");
                _testDatabase.MenuList.Add(desserts);

                // Act
                repository.Add(desserts);
                repository.SaveDataToJson(_testDatabase.MenuList, _testFilePath);

                // Assert
                Assert.IsTrue(File.Exists(_testFilePath));
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsNonNullList()
        {
            // Arrange
            Database _testDatabase = new Database();
            MenuRepository repository = new MenuRepository(_testDatabase);
            Menu dessert1 = new Menu(101, "Chocolate Cake", 30, "Chocolate", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            Menu dessert2 = new Menu(102, "Cheesecake", 40, "Cheese", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            _testDatabase.MenuList.Add(dessert1);
            _testDatabase.MenuList.Add(dessert2);
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_desserts.json");
            repository.SaveDataToJson(_testDatabase.MenuList, _testFilePath);

            // Act
            List<Menu> dessertsFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.IsNotNull(dessertsFromJson);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectNumberOfItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            MenuRepository repository = new MenuRepository(_testDatabase);
            Menu dessert1 = new Menu(101, "Chocolate Cake", 30, "Chocolate", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            Menu dessert2 = new Menu(102, "Cheesecake", 40, "Cheese", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            _testDatabase.MenuList.Add(dessert1);
            _testDatabase.MenuList.Add(dessert2);
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_desserts.json");
            repository.SaveDataToJson(_testDatabase.MenuList, _testFilePath);

            // Act
            List<Menu> dessertsFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(2, dessertsFromJson.Count);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectMenuItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            MenuRepository repository = new MenuRepository(_testDatabase);
            Menu dessert1 = new Menu(101, "Chocolate Cake", 30, "Chocolate", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            Menu dessert2 = new Menu(102, "Cheesecake", 40, "Cheese", ProductType.Desserts, "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png", 0);
            _testDatabase.MenuList.Add(dessert1);
            _testDatabase.MenuList.Add(dessert2);
            string _testFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "test_desserts.json");
            repository.SaveDataToJson(_testDatabase.MenuList, _testFilePath);

            // Act
            List<Menu> dessertsFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(dessert1.Id, dessertsFromJson[0].Id);
            Assert.AreEqual(dessert1.NameOfproduct, dessertsFromJson[0].NameOfproduct);
        }

    }
}
