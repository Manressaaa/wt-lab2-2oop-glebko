using System.Collections.Generic;

namespace wt_lab2_2oop_glebko.Interfaces
{
    public interface IItemService<T>
    {
        void Add(T item);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}