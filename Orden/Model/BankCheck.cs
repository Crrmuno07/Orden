using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("BankChecks")]
    public partial class BankCheck
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCheck { get; set; }
        [Key]
        [Column(Order = 1)]
        public string IdTicket { get; set; }
        public string Office { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string Beneficiary { get; set; }
        public decimal? Value { get; set; }
        public string Gmf { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
