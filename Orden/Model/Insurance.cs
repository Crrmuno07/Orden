using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("Insurances")]
    public partial class Insurance
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInsurance { get; set; }
        [Key]
        [Column(Order = 1)]
        public string IdTicket { get; set; }
        public string IdentificationNumber { get; set; }
        public string PolicyType { get; set; }
        public string InsuranceType { get; set; }
        public string InsuranceCarrier { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Range(typeof(DateTime), "01/01/1990", "01/01/9999",
        ErrorMessage = "La FECHA DE INICIO ENDOSO esta en un rango no permitido")]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Range(typeof(DateTime), "01/01/1990", "01/01/9999",
        ErrorMessage = "La FECHA DE FIN ENDOSO esta en un rango no permitido")]
        public DateTime? EndDate { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
