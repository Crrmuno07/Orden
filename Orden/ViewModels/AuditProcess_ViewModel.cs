using Orden.Model;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Orden.ViewModels
{
    public class AuditProcess_ViewModel : GenericRepository<AuditProcess>
    {
        public AuditProcess AuditProcess(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.AuditProcesses.Where(P => P.IdTicket == Case).FirstOrDefault();
            }
        }
        public void Delete(string Ticket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                IEnumerable<AuditProcess> ToDelete = model.AuditProcesses.Where(X => X.IdTicket == Ticket);
                if (ToDelete != null)
                {
                    model.AuditProcesses.RemoveRange(ToDelete);
                    model.SaveChanges();
                }
            }
        }
    }
}
