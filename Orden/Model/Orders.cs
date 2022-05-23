using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrders { get; set; }
        public string NameOrder { get; set; }
    }
}
