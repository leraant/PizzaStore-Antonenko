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
    public class FeedbackSavingDataTest
    {
        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            FeedbacksSavingData repository = new FeedbacksSavingData(_testDatabase);
            Feedback feedback = new Feedback("sssssssssfffffffhjkl;\\u0027;lkjhbgfcvbnm,x.c,mnbv", 100, DateTime.Parse("2023-05-20T19:21:50.7923992+03:00"), "Lera Antonenko");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_feedback.json");
            _testDatabase.FeedbacksList.Add(feedback);

            // Act
            repository.SaveDataToJson(_testDatabase.FeedbacksList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(Path.GetFullPath(_testFilePath)));
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesCorrectDataToJsonFile()
        {
            // Arrange
            Database _testDatabase = new Database();
            FeedbacksSavingData repository = new FeedbacksSavingData(_testDatabase);
            Feedback feedback = new Feedback("sssssssssfffffffhjkl;\\u0027;lkjhbgfcvbnm,x.c,mnbv", 100, DateTime.Parse("2023-05-20T19:21:50.7923992+03:00"), "Lera Antonenko");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_feedback.json");
            _testDatabase.FeedbacksList.Add(feedback);

            // Act
            repository.SaveDataToJson(_testDatabase.FeedbacksList, _testFilePath);

            // Assert
            string json = File.ReadAllText(_testFilePath);
            List<Feedback> savedFeedbacks = JsonSerializer.Deserialize<List<Feedback>>(json);
            Assert.IsNotNull(savedFeedbacks);
            Assert.AreEqual(1, savedFeedbacks.Count);
            Assert.AreEqual(feedback.Feedbacks, savedFeedbacks[0].Feedbacks);
            Assert.AreEqual(feedback.UserId, savedFeedbacks[0].UserId);
        }

        [TestMethod]
        public void SaveDataToJson_ValidInput_SavesJsonFileWithPath()
        {
            // Arrange
            Database _testDatabase = new Database();
            FeedbacksSavingData repository = new FeedbacksSavingData(_testDatabase);
            Feedback feedback = new Feedback("sssssssssfffffffhjkl;\\u0027;lkjhbgfcvbnm,x.c,mnbv", 100, DateTime.Parse("2023-05-20T19:21:50.7923992+03:00"), "Lera Antonenko");
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_feedback.json");
            _testDatabase.FeedbacksList.Add(feedback);

            // Act
            repository.SaveDataToJson(_testDatabase.FeedbacksList, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(_testFilePath));
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsNonNullList()
        {
            // Arrange
            Database _testDatabase = new Database();
            FeedbacksSavingData repository = new FeedbacksSavingData(_testDatabase);
            Feedback feedback1 = new Feedback("sssssssssfffffffhjkl;\\u0027;lkjhbgfcvbnm,x.c,mnbv", 100, DateTime.Parse("2023-05-20T19:21:50.7923992+03:00"), "Lera Antonenko");
            Feedback feedback2 = new Feedback("Perfect pizza!", 100, DateTime.Parse("2023-05-20T19:32:59.9979228+03:00"), "Lera Antonenko");
            _testDatabase.FeedbacksList.Add(feedback1);
            _testDatabase.FeedbacksList.Add(feedback2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_feedback.json");
            repository.SaveDataToJson(_testDatabase.FeedbacksList, _testFilePath);

            // Act
            List<Feedback> feedbacksFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.IsNotNull(feedbacksFromJson);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectNumberOfItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            FeedbacksSavingData repository = new FeedbacksSavingData(_testDatabase);
            Feedback feedback1 = new Feedback("sssssssssfffffffhjkl;\\u0027;lkjhbgfcvbnm,x.c,mnbv", 100, DateTime.Parse("2023-05-20T19:21:50.7923992+03:00"), "Lera Antonenko");
            Feedback feedback2 = new Feedback("Perfect pizza!", 100, DateTime.Parse("2023-05-20T19:32:59.9979228+03:00"), "Lera Antonenko");
            _testDatabase.FeedbacksList.Add(feedback1);
            _testDatabase.FeedbacksList.Add(feedback2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_feedback.json");
            repository.SaveDataToJson(_testDatabase.FeedbacksList, _testFilePath);

            // Act
            List<Feedback> feedbacksFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(2, feedbacksFromJson.Count);
        }

        [TestMethod]
        public void TestReadFromJson_ReturnsCorrectFeedbackItems()
        {
            // Arrange
            Database _testDatabase = new Database();
            FeedbacksSavingData repository = new FeedbacksSavingData(_testDatabase);
            Feedback feedback1 = new Feedback("sssssssssfffffffhjkl;\\u0027;lkjhbgfcvbnm,x.c,mnbv", 100, DateTime.Parse("2023-05-20T19:21:50.7923992+03:00"), "Lera Antonenko");
            Feedback feedback2 = new Feedback("Perfect pizza!", 100, DateTime.Parse("2023-05-20T19:32:59.9979228+03:00"), "Lera Antonenko");
            _testDatabase.FeedbacksList.Add(feedback1);
            _testDatabase.FeedbacksList.Add(feedback2);
            string _testFilePath = Path.Combine(Path.GetTempPath(), "test_feedback.json");
            repository.SaveDataToJson(_testDatabase.FeedbacksList, _testFilePath);

            // Act
            List<Feedback> feedbacksFromJson = repository.ReadFromJson(_testFilePath);

            // Assert
            Assert.AreEqual(feedback1.Feedbacks, feedbacksFromJson[0].Feedbacks);
            Assert.AreEqual(feedback1.UserId, feedbacksFromJson[0].UserId);
        }
    }

}
