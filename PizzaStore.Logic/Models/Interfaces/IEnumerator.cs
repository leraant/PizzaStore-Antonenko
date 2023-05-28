using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Models
{
    public interface IEnumerator : IEnumerator<Orders>
    {
        bool MoveNext(); // переміщення на одну позицію вперед в контейнері елементів
        Orders Current { get; } // поточний елемент у контейнері
        void Reset(); // перемещение в начало контейнера
    }
}
