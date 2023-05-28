using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;

namespace PizzaStore.Logic.Models
{
    public class Menu
    {
        private int id;
        private string nameOfproduct;
        private double price;
        private string ingredient_description;
        private ProductType typeOfproduct;
        private string img;

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

        public string NameOfproduct
        {
            get { return nameOfproduct; }
            set
            {
                // Перевірка, чи не є значення рядком з пробілами або порожнім рядком
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("The product name cannot be empty or contain only spaces.");

                nameOfproduct = value;
            }
        }

        public double Price
        {
            get { return price; }
            set
            {
                // Перевірка, чи значення введено правильно
                if (value < 0)
                    throw new ArgumentException("The value cannot be negative.");

                // Перевірка, чи не міститься в рядку інших символів, крім цифр
                if (!double.TryParse(value.ToString(), out double parsedValue))
                    throw new ArgumentException("Price must be number");
                price = value;
            }
        }

        public string Ingredient_description
        {
            get { return ingredient_description; }
            set
            {
                // Перевірка, чи не є значення рядком з пробілами або порожнім рядком
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("The ingredient description cannot be empty or contain only spaces.");

                ingredient_description = value;
            }
        }

        public ProductType TypeOfproduct
        {
            get { return typeOfproduct; }
            set
            { typeOfproduct = value; }
        }

        public string Img
        {
            get { return img; }
            set
            {
                // Перевірка, чи не є значення рядком з пробілами або порожнім рядком
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("The img path cannot be empty or contain only spaces.");

                img = value;
            }
        }
        public int PercentDiscount { get; set; }
        public Menu() { }
        public Menu(int id, string nameOfProduct, double price, string ingredientDescription, ProductType typeOfProduct, string img, int percentDiscount)
        {
            Id = id;
            NameOfproduct = nameOfProduct;
            Price = price;
            Ingredient_description = ingredientDescription;
            TypeOfproduct = typeOfProduct;
            Img = img;
            PercentDiscount = percentDiscount;
        }
    }
}
