using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("Sellers")]
    public partial class Seller
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSeller { get; set; }
        [Key]
        [Column(Order = 1)]
        public string IdTicket { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string NameSeller { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
