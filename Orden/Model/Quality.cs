using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("Quality")]
    public partial class Quality
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string IdTicket { get; set; }
        public DateTime CreationQuality { get; set; }
        public string NameMonth { get; set; }
        public string UserProcess { get; set; }
        public bool SpecialAttributes { get; set; }
        public bool CausalStranded { get; set; }
        public bool Scheme { get; set; }
        public bool Department { get; set; }
        public bool City { get; set; }
        public bool DisbursementForm { get; set; }
        public bool Insurance { get; set; }
        public bool Rate { get; set; }
        public bool FrechValue { get; set; }
        public bool ApprovedValue { get; set; }
        public bool AppraisedValue { get; set; }
        public bool Equality { get; set; }
        public bool Modality { get; set; }
        public bool ValidateForm { get; set; }
        public bool Gmf { get; set; }
        public bool Participants { get; set; }
        public bool Term { get; set; }
        public bool Currency { get; set; }
        public bool Result { get; set; }
        public bool Error { get; set; }
        public string Causal { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
