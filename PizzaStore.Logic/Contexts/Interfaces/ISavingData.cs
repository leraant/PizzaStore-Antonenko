using PizzaStore.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Logic.Contexts
{
    public interface ISavingData<T>
    {
        void SaveDataToJson(List<T> list, string path);
        List<T> ReadFromJson(string path);
    }
}
