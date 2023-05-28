using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string NameOfProduct { get; set; }
        public ProductType TypeOfProduct { get; set; }
        public DateTime Date { get; set; }
        public Location City { get; set; }
        public int NumberOfProducts { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string CVVCVC { get; set; }
        public bool PaymentByCard { get; set; }
        public Orders(){}
        public Orders(string nameOfProduct, ProductType typeOfProduct, DateTime date, Location city,
        int numberOfProducts, string cardNumber, string cardHolderName, string street, 
        int houseNumber, string cvvcvc, bool paymentByCard, int userId)
        {
            Id = userId;
            NameOfProduct = nameOfProduct;
            TypeOfProduct = typeOfProduct;
            Date = date;
            City = city;
            NumberOfProducts = numberOfProducts;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            Street = street;
            HouseNumber = houseNumber;
            CVVCVC = cvvcvc;
            PaymentByCard = paymentByCard;          
        }
        public Orders(string nameOfProduct, ProductType typeOfProduct, DateTime date, Location city,
       int numberOfProducts,  string street, int houseNumber,  bool paymentByCard, int userId)
        {
            Id = userId;
            NameOfProduct = nameOfProduct;
            TypeOfProduct = typeOfProduct;
            Date = date;
            City = city;
            NumberOfProducts = numberOfProducts;
            Street = street;
            HouseNumber = houseNumber;
            PaymentByCard = paymentByCard;
        }
        
    }
}
