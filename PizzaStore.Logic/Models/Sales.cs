using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Models
{
    public class Sales
    {
        public string NameOfproduct { get; set; }
        public ProductType TypeOfproduct { get; set; }
        public int PercentDiscount { get; set; }
        public int Id { get; set; }
        public double Price { get; set; }
        public string Img { get; set; }
        public Sales(){}
        public Sales(string nameOfproduct, ProductType typeOfproduct, int percentDiscount, double price, int salesId, string img) 
        {
            NameOfproduct = nameOfproduct;
            TypeOfproduct = typeOfproduct;
            PercentDiscount = percentDiscount;
            Price = price;
            Id = salesId;
            Img = img;
        }
        public static double CalculatePriceWithDiscount(double price, int discountPercent)
        {
            double discountAmount = 0;
            if (discountPercent >= 0)
            {
                discountAmount = (price * (double) discountPercent) / 100;
            }
            double finalPrice = price - discountAmount;
            return Math.Round(finalPrice, 2);
        }
       
    }
}
