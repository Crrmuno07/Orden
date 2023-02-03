namespace Orden.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DistributeTicketsUsers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DistributeTicketsUsers()
        {

        }
        [Key]
        public int? IdDistribute { get; set; }
        public int? IdUser { get; set; }
        public string IdTicket { get; set; }
        public DateTime? CreationDate { get; set; }

    }
}
