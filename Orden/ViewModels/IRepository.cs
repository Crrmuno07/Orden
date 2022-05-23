using System.Collections.Generic;

namespace Orden.ViewModels
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
    }
}
