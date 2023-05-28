using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Models
{
    public class ProductM
    {
       public static string Img { get; set; }
       public static string NameOfproduct { get; set; }
       public static string Ingredient_description { get; set; }
       public static double Price { get; set; }
       public static int Id { get; set; }
       public static ProductType TypeOfproduct { get; set; }
    }
}
