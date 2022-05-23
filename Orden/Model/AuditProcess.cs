using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("AuditProcess")]
    public class AuditProcess
    {
        public DateTime? _PartialDeviance;


        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAuditProcess { get; set; }
        [Key]
        [Column(Order = 1)]
        public string IdTicket { get; set; }
        [Required(ErrorMessage = "Debe seleccionar el estado para la auditoria")]
        public string State { get; set; }
        public string PartialUser { get; set; }
        public string TotalUser { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Range(typeof(DateTime), "01/01/1990", "01/01/9999",
        ErrorMessage = "La DESVARADO PARCIAL esta en un rango no permitido")]
        public DateTime? PartialDeviance { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Range(typeof(DateTime), "01/01/1990", "01/01/9999",
        ErrorMessage = "La DESVARADO TOTAL esta en un rango no permitido")]
        public DateTime? TotalDeviance { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Range(typeof(DateTime), "01/01/1990", "01/01/9999",
        ErrorMessage = "La FECHA LLEGADA esta en un rango no permitido")]
        public DateTime? ArrivalDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Range(typeof(DateTime), "01/01/1990", "01/01/9999",
        ErrorMessage = "La FECHA DE ANALISIS esta en un rango no permitido")]
        public DateTime? AnalysisDate { get; set; }
        public string AnalysisUserRpa { get; set; }
        public string AnalysisUser { get; set; }
        public string OpenUser { get; set; }
        public string PreUser { get; set; }
        public string VerificationUser { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Range(typeof(DateTime), "01/01/1990", "01/01/9999",
        ErrorMessage = "La FECHA VERIFICACIÓN esta en un rango no permitido")]
        public DateTime? VerificationDate { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
