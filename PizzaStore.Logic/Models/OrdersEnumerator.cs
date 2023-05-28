using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Models
{
    public class OrdersEnumerator : IEnumerable<Orders>
    {
        List<Orders> orders;
        Orders CurrentOrder;
        int currentIndex;
        public OrdersEnumerator(List<Orders> ordersList)
        {
            orders = ordersList;
            currentIndex = -1;
        }

        public object Current => CurrentOrder;

        public IEnumerator<Orders> GetEnumerator()
        {
            foreach (Orders orders in orders)
            {
                yield return orders;
            }
        }

        IEnumerator<Orders> IEnumerable<Orders>.GetEnumerator()
        {
            return (IEnumerator<Orders>)this.GetEnumerator();
        }

        public bool MoveNext()
        {
            if ((currentIndex++) >= orders.Count() - 1)
                return false;
            else
                CurrentOrder = orders[currentIndex];
            return true;
        }

        public void Reset()
        {
            // we dont have to implement this method.
        }
    }
}
