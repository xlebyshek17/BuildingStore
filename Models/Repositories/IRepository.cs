using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.Models.Repositories
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetList(); // get list of items
        T Get(int id); // get one item by id
        void Create(T item);
        void Delete(int id);
        void Update(T item);
    }
}
