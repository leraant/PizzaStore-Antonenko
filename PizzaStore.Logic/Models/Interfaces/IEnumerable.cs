using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Models
{
    public interface IEnumerable<T>
    {
        IEnumerator<T> GetEnumerator();
    }
}
