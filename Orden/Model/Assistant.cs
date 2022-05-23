using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("Assistant")]
    public partial class Assistant
    {
        public Assistant()
        {
            UsersAssistants = new HashSet<UsersAssistant>();
        }
        [Key]
        public int IdAssistant { get; set; }
        public string AssistantName { get; set; }

        public virtual ICollection<UsersAssistant> UsersAssistants { get; set; }
    }
}
