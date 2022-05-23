using Orden.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Documents;

namespace Orden.Model
{
    [Table("GeneralInformation")]
    public partial class GeneralInformation : NotifyPropertyChangedImpl
    {
        #region fields
        public decimal? _Financing;
        public string _Rating;
        public string _Cession;
        public string _Scheme;
        public string _Vda;
        public string _Office;
        public string _Modality;
        public decimal? _ApprovedValue;
        public string _Term;
        public string _Currency;
        public string _Plan;
        public DateTime? _ApprovedDate;
        public string _ApprovedVto;
        public DateTime? _RevivedDate;
        public double? _Rate;
        public int? _Delivery;
        public decimal? _DeliveryValue;
        public string _Brp;
        public string _Project;
        public string _Builder;
        public string _Ally;
        public string _Segment;
        #endregion

        #region properties
        [Key]
        public string IdTicket { get; set; }
        public decimal? Financing
        {
            get => _Financing;
            set
            {
                if (value != _Financing)
                {
                    string f = value.Value.ToString();
                    f.Replace("%", "");
                    _Financing = decimal.Parse(f);
                    RaisePropertyChanged("Financing");
                }
            }
        }
        [Required(ErrorMessage = "Debe Ingresar la clasificación de Cartera")]
        public string Rating
        {
            get => _Rating;
            set
            {
                if (value != _Rating)
                {
                    _Rating = value;
                    RaisePropertyChanged("Rating");
                }
            }
        }
        [Required(ErrorMessage = "Debe Ingresar Negocio Compra")]
        public string Cession
        {
            get => _Cession;
            set
            {
                if (value != _Cession)
                {
                    _Cession = value;
                    RaisePropertyChanged("Cession");
                }
            }
        }
        [Required(ErrorMessage = "Debe Ingresar el Scheme")]
        public string Scheme
        {
            get => _Scheme;
            set
            {
                _Scheme = value;
                RaisePropertyChanged("Scheme");
            }
        }
        [Required(ErrorMessage = "Debe Ingresar el Campo Vivienda")]
        public string Vda
        {
            get => _Vda;
            set
            {
                if (value != _Vda)
                {
                    _Vda = value;
                    RaisePropertyChanged("Vda");
                }
            }
        }
        [Required(ErrorMessage = "Debe Ingresar el Valor de la Oficina")]
        public string Office
        {
            get => _Office;
            set
            {
                if (value != _Office && value != "")
                {
                    int num = 6 - value.Length;
                    _Office = "BC" + value.Replace("BC", "").PadLeft(num + value.Length, '0').ToString();
                    RaisePropertyChanged("Office");
                }
            }
        }
        [Required(ErrorMessage = "El campo modalidad no puede quedar vacio, verifique para que sea calculado")]
        public string Modality
        {
            get => _Modality;
            set
            {
                if (value != _Modality || value == "")
                {
                    _Modality = value;
                    RaisePropertyChanged("Modality");
                }
            }
        }

        [Range(1, long.MaxValue, ErrorMessage = "El valor aprovado no es válido no puede quedar vacio ni ser cero")]
        [Required(ErrorMessage = "Debe Ingresar el valor aprovado")]
        public decimal? ApprovedValue
        {
            get => _ApprovedValue;
            set
            {
                if (value != _ApprovedValue && value.ToString() != "")
                {
                    _ApprovedValue = value;
                    RaisePropertyChanged("ApprovedValue");
                }
            }
        }
        public string Term
        {
            get => _Term;
            set
            {
                if (value != _Term)
                {
                    _Term = value;
                    RaisePropertyChanged("Term");
                }
            }
        }
        [Required(ErrorMessage = "Debe Seleccionar la Moneda")]
        public string Currency
        {
            get => _Currency;
            set
            {
                if (value != _Currency)
                {
                    _Currency = value;
                    RaisePropertyChanged("Currency");
                }
            }
        }
        [Required(ErrorMessage = "Debe Seleccionar el Plan")]
        public string Plan
        {
            get => _Plan;
            set
            {
                if (value != _Plan)
                {
                    _Plan = value;
                    RaisePropertyChanged("Plan");
                }
            }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Range(typeof(DateTime), "01/01/1990", "01/01/9999",
        ErrorMessage = "La FECHA DE APROVACIÓN esta en un rango no permitido")]
        public DateTime? ApprovedDate
        {
            get => _ApprovedDate;
            set
            {
                if (value.ToString() != "")
                {
                    _ApprovedDate = value.Value;
                    _ApprovedVto = ApprovedDate.Value.AddMonths(9) < DateTime.Now ? "VENCIDO" : "VIGENTE";
                    RaisePropertyChanged("ApprovedDate");
                    RaisePropertyChanged("ApprovedVto");
                }
            }
        }
        public string ApprovedVto
        {
            get => _ApprovedVto;
            set
            {
                if (value != "" && value != null)
                {
                    _ApprovedVto = value;
                    RaisePropertyChanged("ApprovedVto");
                }
            }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Range(typeof(DateTime), "01/01/1990", "01/01/9999",
        ErrorMessage = "La FECHA DE REVIVIDO esta en un rango no permitido")]
        public DateTime? RevivedDate
        {
            get => _RevivedDate;
            set
            {
                if (value != _RevivedDate && value.ToString() != "")
                {
                    _RevivedDate = value.Value;
                    _ApprovedVto = RevivedDate.Value.AddMonths(9) < DateTime.Now ? "VENCIDO" : "VIGENTE";
                    RaisePropertyChanged("RevivedDate");
                    RaisePropertyChanged("ApprovedVto");
                }
            }
        }
        [Range(1, double.MaxValue, ErrorMessage = "La tasa politica no es valida")]
        [Required(ErrorMessage = "El ticket debe tener una tasa permitida")]
        public double? Rate
        {
            get => _Rate;
            set
            {
                if (value.ToString() != "")
                {
                    _Rate = value.Value;
                    RaisePropertyChanged("Rate");
                }
            }
        }
        [Range(1, long.MaxValue, ErrorMessage = "El numero de entrega no es válido")]
        [Required(ErrorMessage = "Debe Ingresar el numero de entrega")]
        public int? Delivery
        {
            get => _Delivery;
            set
            {
                if (value != _Delivery)
                {
                    _Delivery = value.Value;
                    RaisePropertyChanged("Delivery");
                }
            }
        }
        [Range(1, long.MaxValue, ErrorMessage = "El valor entrega no es válido no puede quedar vacio ni ser cero")]
        [Required(ErrorMessage = "Debe Ingresar el valor entrega")]
        public decimal? DeliveryValue
        {
            get => _DeliveryValue;
            set
            {
                if (value != _DeliveryValue)
                {
                    _DeliveryValue = value.Value;
                    RaisePropertyChanged("DeliveryValue");
                }
            }
        }
        [Required(ErrorMessage = "Debe Ingresar el tipo de BRP")]
        public string Brp
        {
            get => _Brp;
            set
            {
                if (value != _Brp)
                {
                    _Brp = value;
                    RaisePropertyChanged("Brp");
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
        public string Builder
        {
            get => _Builder;
            set
            {
                if (value != _Builder)
                {
                    _Builder = value;
                    RaisePropertyChanged("Builder");
                }
            }
        }
        public string Ally
        {
            get => _Ally;
            set
            {
                if (value != _Ally)
                {
                    _Ally = value;
                    RaisePropertyChanged("Ally");
                }
            }
        }
        public string Segment
        {
            get => _Segment;
            set
            {
                if (value != _Segment)
                {
                    _Segment = value;
                    RaisePropertyChanged("Segment");
                }
            }
        }
        #endregion

    }
}
