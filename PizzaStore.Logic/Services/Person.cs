using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PizzaStore.Logic.Services
{
    public abstract class Person
    {
        private string email;
        private string firstName;
        private string lastName;
        private Location city;
        private string password;
        private int id;
        public string Email
        {
            get { return email; }
            set
            {
                // Перевірка, чи не є значення рядком з пробілами або порожнім рядком
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("The ingredient description cannot be empty or contain only spaces.");
                else if (!value.Contains("@"))
                {
                    throw new ArgumentException("Email must contain '@'");
                }
                email = value;
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                // Перевірка, чи не є значення рядком з пробілами або порожнім рядком
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("The ingredient description cannot be empty or contain only spaces.");

                firstName = value;
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                // Перевірка, чи не є значення рядком з пробілами або порожнім рядком
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("The ingredient description cannot be empty or contain only spaces.");
                lastName = value;
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                // Перевірка, чи не є значення рядком з пробілами або порожнім рядком
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("The ingredient description cannot be empty or contain only spaces.");
                // Перевірка, чи має значення мінімальну довжину 8 символів
                if (value.Length < 8)
                    throw new ArgumentException("The password should be at least 8 characters long.");
                if (value.Length >= 15)
                    throw new ArgumentException("The password should be not longer than 15 characters.");
                password = value;
            }
        }
        public Location City
        {
            get { return city; }
            set { city = value; }
        }
      
        public int Id
        {
            get { return id; }
            set
            {
                // Перевірка, чи значення введено правильно
                if (value < 100 || value > 999)
                    throw new ArgumentException("Id must be a three-digit number.");

                // Перевірка, чи не міститься в рядку інших символів, крім цифр
                if (!int.TryParse(value.ToString(), out int parsedValue))
                    throw new ArgumentException("Id має бути числом.");
                id = value;
            }
        }
       
    }
}
