using Orden.Model;
using System.Data.Entity;
using System.Linq;

namespace Orden.ViewModels
{
    public class PropertyInformation_ViewModel : GenericRepository<PropertyInformation>
    {
        public PropertyInformation PropertyInformation(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.PropertyInformations.Where(P => P.IdTicket == Case).FirstOrDefault();
            }
        }
        public void Delete(string Ticket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                var ToDelete = model.PropertyInformations.Where(X => X.IdTicket == Ticket).FirstOrDefault();
                if (ToDelete != null)
                {
                    model.Entry(ToDelete).State = EntityState.Deleted;
                    model.SaveChanges();
                }
            }
        }
    }
}
