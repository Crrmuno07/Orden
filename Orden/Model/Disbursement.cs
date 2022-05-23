using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("Disbursement")]
    public partial class Disbursement
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDisbursement { get; set; }
        [Key]
        [Column(Order = 1)]
        public string IdTicket { get; set; }
        public string Contingency { get; set; }
        public decimal? Value { get; set; }
        public string Gmf { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
