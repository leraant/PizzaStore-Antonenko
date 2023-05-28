using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace PizzaStore.Logic.Models
{
    public class Feedback
    {
        public string Feedbacks { get; set; }
        public int UserId { get; set; }
        public DateTime Date{ get; set;}
        public string Name { get; set; }
        public Feedback() { }
        public Feedback(string feedback, int userId, DateTime date, string name)
        {
            Feedbacks = feedback;
            UserId = userId;
            Date = date;
            Name = name;
        }

    }
}
