using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    public class OrderStates
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrder { get; set; }
        [Key]
        [Column(Order = 1)]
        public int IdState { get; set; }
        public bool Active { get; set; }
    }
}
