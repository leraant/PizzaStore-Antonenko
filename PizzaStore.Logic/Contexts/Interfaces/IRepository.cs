using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Contexts
{
    public interface IRepository<T>
    {
        bool Add(T entity);
        T Find(int Id);
        void Delete(int Id);
    }
}
