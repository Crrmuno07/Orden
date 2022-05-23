using Orden.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("FinacleData")]
    public partial class FinacleData : NotifyPropertyChangedImpl
    {
        #region fields
        private string _CIF;
        private string _Limit;
        private string _ObligationNumber;
        private string _Collateral;
        private string _Project;
        private string _Transaction;
        #endregion
        [Key]
        public string IdTicket { get; set; }
        [Required(ErrorMessage = "Debe Ingresar el CIF")]
        public string CIF
        {
            get => _CIF;
            set
            {
                if (value != _CIF)
                {
                    _CIF = value;
                    RaisePropertyChanged("CIF");
                }
            }
        }
        [Required(ErrorMessage = "Debe Ingresar el Limit")]
        public string Limit
        {
            get => _Limit;
            set
            {
                if (value != _Limit)
                {
                    _Limit = value;
                    RaisePropertyChanged("Limit");
                }
            }
        }
        public string ObligationNumber
        {
            get => _ObligationNumber;
            set
            {
                if (value != _ObligationNumber)
                {
                    _ObligationNumber = value;
                    RaisePropertyChanged("ObligationNumber");
                }
            }
        }
        public string Collateral
        {
            get => _Collateral;
            set
            {
                if (value != _Collateral)
                {
                    _Collateral = value;
                    RaisePropertyChanged("Collateral");
                }
            }
        }
        public string Project
        {
            get => _Project;
            set
            {
                if (value != _Project)
                {
                    _Project = value;
                    RaisePropertyChanged("Project");
                }
            }
        }
        public string Transaction
        {
            get => _Transaction;
            set
            {
                if (value != _Transaction)
                {
                    _Transaction = value;
                    RaisePropertyChanged("Transaction");
                }
            }
        }
    }
}
