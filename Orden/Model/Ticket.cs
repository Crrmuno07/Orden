using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("Tickets")]
    public partial class Ticket
    {
        public Ticket()
        {
            BankChecks = new HashSet<BankCheck>();
            BankCreditAccounts = new HashSet<BankCreditAccount>();
            BankDebitAccounts = new HashSet<BankDebitAccount>();
            Disbursements = new HashSet<Disbursement>();
            Insurances = new HashSet<Insurance>();
            CausesStrandings = new HashSet<CausesStranding>();
            Participants = new HashSet<Participant>();
            Sellers = new HashSet<Seller>();
            Subrogations = new HashSet<Subrogation>();
        }
        [Key]
        public string IdTicket { get; set; }
        public int IdState { get; set; }
        public string Region { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public bool? Reversed { get; set; }
        public bool? Priority { get; set; }
        public bool? Locked { get; set; }
        public int? ExecutionOrder { get; set; }
        public string CausesObservations { get; set; }
        public string GeneralObservations { get; set; }
        public string AllObservations { get; set; }

        public virtual ICollection<BankCheck> BankChecks { get; set; }
        public virtual ICollection<BankCreditAccount> BankCreditAccounts { get; set; }
        public virtual ICollection<BankDebitAccount> BankDebitAccounts { get; set; }
        public virtual ICollection<Disbursement> Disbursements { get; set; }
        public virtual ICollection<Insurance> Insurances { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<Seller> Sellers { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Subrogation> Subrogations { get; set; }
        public virtual ICollection<CausesStranding> CausesStrandings { get; set; }
        public virtual ICollection<AuditProcess> AuditProcesses { get; set; }
    }
}
