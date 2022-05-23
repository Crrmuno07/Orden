using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("Users")]
    public partial class User
    {
        public User()
        {
            this.UsersAssistants = new HashSet<UsersAssistant>();
        }
        [Key]
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public bool Active { get; set; }
        public string ComputerName { get; set; }
        public int IdTypeUser { get; set; }
        public string FullName { get; set; }

        public virtual TypeUser TypeUser { get; set; }
        public virtual ICollection<UsersAssistant> UsersAssistants { get; set; }
    }
}
