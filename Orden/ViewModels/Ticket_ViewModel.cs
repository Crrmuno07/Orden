using Orden.Model;
using System.Data.Entity;
using System.Linq;

namespace Orden.ViewModels
{
    public class Ticket_ViewModel : GenericRepository<Ticket>
    {
        public Ticket Ticket(string Case, string States)
        {
            using (ModelOrder model = new ModelOrder())
            {
                foreach (var item in States.Split(','))
                {
                    int id = int.Parse(item);
                    if(model.Tickets.Where(P => P.IdTicket == Case && P.IdState == id).Count() > 0)
                    {
                        return model.Tickets.Where(P => P.IdTicket == Case && P.IdState == id).FirstOrDefault();
                    }
                }
            }
            return null;
        }
        public void Delete(string Ticket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                var ToDelete = model.Tickets.Where(X => X.IdTicket == Ticket).FirstOrDefault();
                if (ToDelete != null)
                {
                    model.Entry(ToDelete).State = EntityState.Deleted;
                    model.SaveChanges();
                }
            }
        }
        public Ticket FindTicket(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.Tickets.Where(P => P.IdTicket == Case).FirstOrDefault();
            }
        }
    }
}
