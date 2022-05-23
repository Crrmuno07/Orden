using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("ValidationFrech")]
    public partial class ValidationFrech
    {
        [Key]
        public string IdTicket { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "El valor ingresos no es válido no puede quedar vacio ni ser cero")]
        public decimal? Income { get; set; }
        [Required(ErrorMessage = "Debe de Indicar si el Cliente Posee FRECH")]
        public string HasFrech { get; set; }
        [Required(ErrorMessage = "Debe de Indicar si el Cliente Posee FONVIVIENDA")]
        public string HasFonvivienda { get; set; }
        [Required(ErrorMessage = "Debe de Indicar si el Cliente FORMATO DE COBERTURA")]
        public string Format { get; set; }
        public string Subsidy { get; set; }
        public string ResultFrech { get; set; }
        public int? ReservationNumber { get; set; }
        public string TypeFrech { get; set; }
        public string LivingPlace { get; set; }
        public int? YearSubsidy { get; set; }
        public decimal? points { get; set; }
    }
}
