using Orden.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{

    [Table("BankDebitAccounts")]
    public partial class BankDebitAccount : NotifyPropertyChangedImpl
    {
        #region fields
        public double? _Porcentage;
        #endregion

        #region properties
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDebitAccount { get; set; }
        [Key]
        [Column(Order = 1)]
        public string IdTicket { get; set; }
        public string AccountType { get; set; }
        public long? AccountNumber { get; set; }
        public double? Porcentage
        {
            get => _Porcentage;
            set
            {
                if (value != null)
                {
                    string f = value.Value.ToString();
                    f.Replace("%", "");
                    _Porcentage = double.Parse(f);
                    RaisePropertyChanged("Porcentage");
                }
            }
        }
        #endregion
        public virtual Ticket Ticket { get; set; }
    }
}
