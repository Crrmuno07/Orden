using Orden.Helpers;
using Orden.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Orden.ViewModels
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        StringBuilder CompletResult = new StringBuilder();
        readonly CommonFunctions common = new CommonFunctions();
        public virtual List<T> GetAll()
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.Set<T>().ToList();
            }
        }
        public void Add(T entity)
        {
            using (ModelOrder model = new ModelOrder())
            {
                model.Entry(entity).State = EntityState.Added;
                model.SaveChanges();
            }
        }
        public void Update(T entity)
        {
            using (ModelOrder model = new ModelOrder())
            {
                model.Entry(entity).State = EntityState.Modified;
                model.SaveChanges();
            }
        }
    }
}
