using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Models
{
    public class Favorites
    {
        public string NameOfproduct { get; set; }
        public ProductType TypeOfproduct { get; set; }
        public int UserId { get; set;}
        public int ProductId { get; set;}
        public string Ingredient_description { get; set; }
        public string Img { get; set; }
        public double Price { get; set; }

       
        public Favorites(string nameOfproduct, ProductType typeOfproduct, int userId, int productId, double price, string ingredient_description, string img)
        {
            NameOfproduct = nameOfproduct;
            TypeOfproduct = typeOfproduct;
            UserId = userId;
            ProductId=productId;
            Price = price;
            Ingredient_description=ingredient_description;
            Img=img;
        }

    }
}
