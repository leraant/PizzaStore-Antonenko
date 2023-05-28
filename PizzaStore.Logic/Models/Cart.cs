using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Models
{
    public class Cart
    {
        public string NameOfProduct { get; set; }
        public ProductType TypeOfProduct { get; set; }
        public int Id { get; set; }
        public double Price { get; set; }
        public int Discount { get; set;}
        public int UserId { get; set; }
        public int OrderNumber { get; set; }
        public string Img { get; set; }

        public Cart(string nameOfProduct, ProductType typeOfProduct, int id, double price, int discount, int userid, int orderNumber, string img)
        {
            NameOfProduct = nameOfProduct;
            TypeOfProduct = typeOfProduct;
            Id = id;
            Price = price;
            Discount = discount;
            UserId = userid;
            OrderNumber = orderNumber;
            Img = img;
        }
    }
}
