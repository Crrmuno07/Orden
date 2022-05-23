using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("TypeUser")]
    public partial class TypeUser
    {
        public TypeUser()
        {
            this.Users = new HashSet<User>();
        }
        [Key]
        public int IdTypeUser { get; set; }
        public string NameTypeUser { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
