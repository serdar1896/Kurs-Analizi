using System.Collections.Generic;

namespace KursAnaliz.Business.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int id);
        int Count();
        void Save();
    }
}
