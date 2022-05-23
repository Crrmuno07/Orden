using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("UsersAssistant")]
    public partial class UsersAssistant
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }
        [Key]
        [Column(Order = 1)]
        public int IdAssistant { get; set; }
        public bool Active { get; set; }

        public virtual Assistant Assistant { get; set; }
        public virtual User User { get; set; }
    }
}
