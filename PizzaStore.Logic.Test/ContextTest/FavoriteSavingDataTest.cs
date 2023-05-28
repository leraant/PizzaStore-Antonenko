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
    public class FavoriteSavingDataTest
    {
        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            FavoriteSavingData repository = new FavoriteSavingData(_testDatabase);
            Favorites favorite = new Favorites("Havaii", ProductType.Pizza, 101, 102, 11, "sdfgh", "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_favorites.json");
            _testDatabase.FavoritesList.Add(favorite);

            // Act
            repository.SaveDataToJson(_testDatabase.FavoritesList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(Path.GetFullPath(_testFilePath)));
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesCorrectDataToJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            FavoriteSavingData repository = new FavoriteSavingData(_testDatabase);
            Favorites favorite = new Favorites("Havaii", ProductType.Pizza, 101, 102, 11, "sdfgh", "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_favorites.json");
            _testDatabase.FavoritesList.Add(favorite);

            // Act
            repository.SaveDataToJson(_testDatabase.FavoritesList, _testFilePath);

            // Assert
            string json = File.ReadAllText(_testFilePath);
            List<Favorites> savedFavorites = JsonSerializer.Deserialize<List<Favorites>>(json);
            Assert.IsNotNull(savedFavorites);
            Assert.AreEqual(1, savedFavorites.Count);
            Assert.AreEqual(favorite.ProductId, savedFavorites[0].ProductId);
            Assert.AreEqual(favorite.NameOfproduct, savedFavorites[0].NameOfproduct);
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFileWithPath()
        {
            // Arrange
            Database _testDatabase = new Database();
            FavoriteSavingData repository = new FavoriteSavingData(_testDatabase);
            Favorites favorite = new Favorites("Havaii", ProductType.Pizza, 101, 102, 11,  "sdfgh", "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_favorites.json");
            _testDatabase.FavoritesList.Add(favorite);

            // Act
            repository.SaveDataToJson(_testDatabase.FavoritesList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(_testFilePath));
        }
        [TestMethod]
        public void TestReadFromJson_ReturnsNonNullList()
        {
            // Arrange
            Database _testDatabase = new Database();
            FavoriteSavingData repository = new FavoriteSavingData(_testDatabase);
            Favorites favorite1 = new Favorites("Havaii", ProductType.Pizza, 101, 102, 11, "sdfgh", "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            Favorites favorite2 = new Favorites("Drinks", ProductType.Pizza, 103, 104, 12, "qwerty", "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\drinks.png");
            _testDatabase.FavoritesList.Add(favorite1);
            _testDatabase.FavoritesList.Add(favorite2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_favorites.json");
            repository.SaveDataToJson(_testDatabase.FavoritesList, _testFilePath);

            // Act
            List<Favorites> favoritesFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.IsNotNull(favoritesFromJson);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectNumberOfItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            FavoriteSavingData repository = new FavoriteSavingData(_testDatabase);
            Favorites favorite1 = new Favorites("Havaii", ProductType.Pizza, 101, 102,11, "sdfgh", "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            Favorites favorite2 = new Favorites("Drinks", ProductType.Pizza, 103, 104,12, "qwerty", "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\drinks.png");
            _testDatabase.FavoritesList.Add(favorite1);
            _testDatabase.FavoritesList.Add(favorite2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_favorites.json");
            repository.SaveDataToJson(_testDatabase.FavoritesList, _testFilePath);

            // Act
            List<Favorites> favoritesFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(2, favoritesFromJson.Count);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectFavoriteItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            FavoriteSavingData repository = new FavoriteSavingData(_testDatabase);
            Favorites favorite1 = new Favorites("Havaii", ProductType.Pizza, 101, 102,11, "sdfgh", "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\hawaii pizza.png");
            Favorites favorite2 = new Favorites("Drinks", ProductType.Pizza, 103, 104,12, "qwerty", "C:\\Users\\Leraa\\Desktop\\LRera\\2 курс 2 семестр\\ООП (КП)\\PizzaStore\\PizzaStore.Presentation\\bin\\x86\\Debug\\AppX\\Pages\\Assets\\drinks.png");
            _testDatabase.FavoritesList.Add(favorite1);
            _testDatabase.FavoritesList.Add(favorite2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_favorites.json");
            repository.SaveDataToJson(_testDatabase.FavoritesList, _testFilePath);

            // Act
            List<Favorites> favoritesFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(favorite1.ProductId, favoritesFromJson[0].ProductId);
            Assert.AreEqual(favorite1.NameOfproduct, favoritesFromJson[0].NameOfproduct);
        }

    }
}
