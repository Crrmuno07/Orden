using Orden.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    [Table("PropertyInformation")]
    public partial class PropertyInformation : NotifyPropertyChangedImpl
    {

        public DateTime? _AppraisalDate;
        public string _TypeProperty;
        public string _Deparment;
        public string _City;
        public string _PropertyAddress;
        public string _WarrantyClass;
        public decimal? _AppraisalValue;
        public decimal? _Costs;
        public string _Enrollment;
        public decimal? _ValueTrc;

        [Key]
        public string IdTicket { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Debe Ingresar la fecha de Avaluó")]
        [Range(typeof(DateTime), "01/01/1990", "01/01/9999",
        ErrorMessage = "La FECHA DE AVALÚO esta en un rango no permitido")]
        public DateTime? AppraisalDate
        {
            get => _AppraisalDate;
            set
            {
                if (value != _AppraisalDate)
                {
                    _AppraisalDate = value;
                    RaisePropertyChanged("AppraisalDate");
                }
            }
        }
        [Required(ErrorMessage = "Debe Seleccionar el Topo de Inmueble")]
        public string TypeProperty
        {
            get => _TypeProperty;
            set
            {
                if (value != _TypeProperty)
                {
                    _TypeProperty = value.ToUpper();
                    RaisePropertyChanged("TypeProperty");
                }
            }
        }
        [Required(ErrorMessage = "Debe Seleccionar el Departamento")]
        public string Deparment
        {
            get => _Deparment;
            set
            {
                if (value != _Deparment)
                {
                    _Deparment = value;
                    RaisePropertyChanged("Deparment");
                }
            }
        }
        [Required(ErrorMessage = "Debe Seleccionar la Ciudad")]
        public string City
        {
            get => _City;
            set
            {
                if (value != _City)
                {
                    _City = value;
                    RaisePropertyChanged("City");
                }
            }
        }
        [Required(ErrorMessage = "Debe Ingresar la Dirección del Inmueble")]
        public string PropertyAddress
        {
            get => _PropertyAddress;
            set
            {
                if (value != _PropertyAddress)
                {
                    _PropertyAddress = value;
                    RaisePropertyChanged("PropertyAddress");
                }
            }
        }
        [Required(ErrorMessage = "Debe Ingresar la Clase de Garantia")]
        public string WarrantyClass
        {
            get => _WarrantyClass;
            set
            {
                if (value != _WarrantyClass)
                {
                    _WarrantyClass = value.ToUpper();
                    RaisePropertyChanged("WarrantyClass");
                }
            }
        }
        [Range(25000000, long.MaxValue, ErrorMessage = "El valor Avaluó no puede ser menos de 25.000.000")]
        public decimal? AppraisalValue
        {
            get => _AppraisalValue;
            set
            {
                if (value != _AppraisalValue)
                {
                    _AppraisalValue = value.Value;
                    RaisePropertyChanged("AppraisalValue");
                }
            }
        }
        public decimal? Costs
        {
            get => _Costs;
            set
            {
                if (value != _Costs)
                {
                    _Costs = value.Value;
                    RaisePropertyChanged("Costs");
                }
            }
        }
        public string Enrollment
        {
            get => _Enrollment;
            set
            {
                if (value != _Enrollment)
                {
                    _Enrollment = value;
                    RaisePropertyChanged("Enrollment");
                }
            }
        }
        public decimal? ValueTrc
        {
            get => _ValueTrc;
            set
            {
                if (value != _ValueTrc)
                {
                    _ValueTrc = value;
                    RaisePropertyChanged("ValueTrc");
                }
            }
        }
    }
}
