using System;
using System.ComponentModel.DataAnnotations;

namespace Orden.Model
{
    public class AuditTicket
    {
        [Key]
        public int IdAuditTicket { get; set; }

        public string IdTicket { get; set; }

        public int IdUser { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int StartState { get; set; }

        public int EndState { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

    }
}
