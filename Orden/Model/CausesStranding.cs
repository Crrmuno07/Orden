using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("CausesStranding")]
    public class CausesStranding
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCausal { get; set; }
        [Key]
        [Column(Order = 1)]
        public string IdTicket { get; set; }
        public string OptionName { get; set; }
        public string TypeName { get; set; }
        public string StatusName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreationDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ModifcationDate { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
