using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaStore.Logic.Contexts;
using PizzaStore.Logic.Models;
using PizzaStore.Logic.Services;
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
    public class UserSavingDataTest
    {
        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            UsersSavingData repository = new UsersSavingData(_testDatabase);
            Users user = new Users(100, "lera@gmail.com", "Lera", "Antonenko", Location.Lviv, "12345678");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_users.json");
            _testDatabase.UserList.Add(user);

            // Act
            repository.SaveDataToJson(_testDatabase.UserList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(Path.GetFullPath(_testFilePath)));
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesCorrectDataToJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            UsersSavingData repository = new UsersSavingData(_testDatabase);
            Users user = new Users(100, "lera@gmail.com", "Lera", "Antonenko", Location.Lviv, "12345678");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_users.json");
            _testDatabase.UserList.Add(user);

            // Act
            repository.SaveDataToJson(_testDatabase.UserList, _testFilePath);

            // Assert
            string json = File.ReadAllText(_testFilePath);
            List<Users> savedUsers = JsonSerializer.Deserialize<List<Users>>(json);
            Assert.IsNotNull(savedUsers);
            Assert.AreEqual(1, savedUsers.Count);
            Assert.AreEqual(user.Email, savedUsers[0].Email);
            Assert.AreEqual(user.FirstName, savedUsers[0].FirstName);
            Assert.AreEqual(user.LastName, savedUsers[0].LastName);
            Assert.AreEqual(user.City, savedUsers[0].City);
            Assert.AreEqual(user.Id, savedUsers[0].Id);
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFileWithPath()
        {
            // Arrange
            Database _testDatabase = new Database();
            UsersSavingData repository = new UsersSavingData(_testDatabase);
            Users user = new Users(100, "lera@gmail.com", "Lera", "Antonenko", Location.Lviv, "12345678");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_users.json");
            _testDatabase.UserList.Add(user);

            // Act
            repository.SaveDataToJson(_testDatabase.UserList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(_testFilePath));
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsNonNullList()
        {
            // Arrange
            Database _testDatabase = new Database();
            UsersSavingData repository = new UsersSavingData(_testDatabase);
            Users user1 = new Users(100, "lera@gmail.com", "Lera", "Antonenko", Location.Lviv, "12345678");
            Users user2 = new Users(101, "tata@gmail.com", "Tata", "Tit", Location.Kyiv, "12345678");
            _testDatabase.UserList.Add(user1);
            _testDatabase.UserList.Add(user2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_users.json");
            repository.SaveDataToJson(_testDatabase.UserList, _testFilePath);

            // Act
            List<Users> usersFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.IsNotNull(usersFromJson);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectNumberOfItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            UsersSavingData repository = new UsersSavingData(_testDatabase);
            Users user1 = new Users(100, "lera@gmail.com", "Lera", "Antonenko", Location.Lviv, "12345678");
            Users user2 = new Users(101, "tata@gmail.com", "Tata", "Tit", Location.Kyiv, "12345678");
            _testDatabase.UserList.Add(user1);
            _testDatabase.UserList.Add(user2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_users.json");
            repository.SaveDataToJson(_testDatabase.UserList, _testFilePath);

            // Act
            List<Users> usersFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(2, usersFromJson.Count);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectUserItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            UsersSavingData repository = new UsersSavingData(_testDatabase);
            Users user1 = new Users(100, "lera@gmail.com", "Lera", "Antonenko", Location.Lviv, "12345678");
            Users user2 = new Users(101, "tata@gmail.com", "Tata", "Tit", Location.Kyiv, "12345678");
            _testDatabase.UserList.Add(user1);
            _testDatabase.UserList.Add(user2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_users.json");
            repository.SaveDataToJson(_testDatabase.UserList, _testFilePath);

            // Act
            List<Users> usersFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(user1.Email, usersFromJson[0].Email);
            Assert.AreEqual(user1.FirstName, usersFromJson[0].FirstName);
            Assert.AreEqual(user1.LastName, usersFromJson[0].LastName);
            Assert.AreEqual(user1.City, usersFromJson[0].City);
            Assert.AreEqual(user1.Id, usersFromJson[0].Id);
        }
    }

}
