using Orden.Model;
using System.Data.Entity;
using System.Linq;

namespace Orden.ViewModels
{
    public class FinacleData_ViewModel : GenericRepository<FinacleData>
    {
        public FinacleData FinacleData(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.FinacleDatas.Where(P => P.IdTicket == Case).FirstOrDefault();
            }
        }
        public void Delete(string Ticket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                var ToDelete = model.FinacleDatas.Where(X => X.IdTicket == Ticket).FirstOrDefault();
                if (ToDelete != null)
                {
                    model.Entry(ToDelete).State = EntityState.Deleted;
                    model.SaveChanges();
                }
            }
        }
    }
}
