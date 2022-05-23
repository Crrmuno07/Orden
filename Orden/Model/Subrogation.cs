using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("Subrogations")]
    public partial class Subrogation
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSubrogation { get; set; }
        [Key]
        [Column(Order = 1)]
        public string IdTicket { get; set; }
        public long? SubrogationNumber { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public long? ObligationNumber { get; set; }
        public decimal? Value { get; set; }
        public string Gmf { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
