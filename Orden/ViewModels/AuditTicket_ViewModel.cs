using Orden.Model;
using System;
using System.Linq;

namespace Orden.ViewModels
{
    public class AuditTicket_ViewModel : GenericRepository<AuditTicket>
    {
        public int IdUser()
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.Users.Where(P => P.NameUser == Environment.UserName).Select(x => x.IdUser).FirstOrDefault();
            }
        }
    }
}
