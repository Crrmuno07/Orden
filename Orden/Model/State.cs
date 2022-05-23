using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("States")]
    public partial class State
    {
        public State()
        {
            Tickets = new HashSet<Ticket>();
        }
        [Key]
        public int IdState { get; set; }
        public string NameState { get; set; }
        public int IdAssistant { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
