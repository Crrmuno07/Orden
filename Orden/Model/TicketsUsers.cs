using System.ComponentModel.DataAnnotations;

namespace Orden.Model
{
    public class TicketsUsers
    {
        [Key]
        public string IdTicket { get; set; }
        public int IdUser { get; set; }
    }
}
