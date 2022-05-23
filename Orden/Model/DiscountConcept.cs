using Orden.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("DiscountConcepts")]
    public partial class DiscountConcept : NotifyPropertyChangedImpl
    {
        #region fields
        private string _AgreenmentDescription;
        private string _EmployeeDescription;
        private string _AttributionsDescription;
        private string _PackageDescription;
        private string _PackageDescription2;

        private double _AgreenmentValue;
        private double _EmployeeValue;
        private double _AttributionsValue;
        private double _PackageValue;
        private double _PackegeValue2;
        private double _TotalRate;
        #endregion
        [Key]
        public string IdTicket { get; set; }
        public string AgreenmentDescription
        {
            get => _AgreenmentDescription;
            set
            {
                if (value != _AgreenmentDescription)
                {
                    _AgreenmentDescription = value.ToString();
                    RaisePropertyChanged("AgreenmentDescription");
                }
            }
        }
        public double AgreenmentValue
        {
            get => _AgreenmentValue;
            set
            {
                if (value != _AgreenmentValue)
                {
                    _AgreenmentValue = value;
                    RaisePropertyChanged("AgreenmentValue");
                }
            }
        }
        public string EmployeeDescription
        {
            get => _EmployeeDescription;
            set
            {
                if (value != _EmployeeDescription)
                {
                    _EmployeeDescription = value.ToString();
                    RaisePropertyChanged("EmployeeDescription");
                }
            }
        }
        public double EmployeeValue
        {
            get => _EmployeeValue;
            set
            {
                if (value != _EmployeeValue)
                {
                    _EmployeeValue = value;
                    RaisePropertyChanged("EmployeeValue");
                }
            }
        }
        public string AttributionsDescription
        {
            get => _AttributionsDescription;
            set
            {
                if (value != _AttributionsDescription)
                {
                    _AttributionsDescription = value.ToString();
                    RaisePropertyChanged("AttributionsDescription");
                }
            }
        }
        public double AttributionsValue
        {
            get => _AttributionsValue;
            set
            {
                if (value != _AttributionsValue)
                {
                    _AttributionsValue = value;
                    RaisePropertyChanged("AttributionsValue");
                }
            }
        }
        public string PackageDescription
        {
            get => _PackageDescription;
            set
            {
                if (value != _PackageDescription)
                {
                    _PackageDescription = value.ToString();
                    RaisePropertyChanged("PackageDescription");
                }
            }
        }
        public double PackageValue
        {
            get => _PackageValue;
            set
            {
                if (value != _PackageValue)
                {
                    _PackageValue = value;
                    RaisePropertyChanged("PackageValue");
                }
            }
        }
        public string PackageDescription2
        {
            get => _PackageDescription2;
            set
            {
                if (value != _PackageDescription2)
                {
                    _PackageDescription2 = value.ToString();
                    RaisePropertyChanged("PackageDescription2");
                }
            }
        }
        public double PackegeValue2
        {
            get => _PackegeValue2;
            set
            {
                if (value != _PackegeValue2)
                {
                    _PackegeValue2 = value;
                    RaisePropertyChanged("PackegeValue2");
                }
            }
        }
        public double TotalRate
        {
            get => _TotalRate;
            set
            {
                if (value != _TotalRate)
                {
                    _TotalRate = value;
                    RaisePropertyChanged("TotalRate");
                }
            }
        }
    }
}
