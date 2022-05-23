using Orden.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.Model
{
    public partial class Participant : NotifyPropertyChangedImpl
    {
        public string _IdentificationType;
        public string _IdentificationNumber;
        public string _NameParticipante;
        public string _TypeParticipant;

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdParticipant { get; set; }
        [Key]
        [Column(Order = 1)]
        public string IdTicket { get; set; }

        [Required(ErrorMessage = "Debe Ingresar el tipo de identificación")]
        public string IdentificationType
        {
            get => _IdentificationType;
            set
            {
                if (value != _IdentificationType)
                {
                    _IdentificationType = value;
                    RaisePropertyChanged("IdentificationType");
                }
            }
        }

        [Required(ErrorMessage = "Debe Ingresar el Numero de identificación")]
        public string IdentificationNumber
        {
            get => _IdentificationNumber;
            set
            {
                if (value != _IdentificationNumber)
                {
                    _IdentificationNumber = value;
                    RaisePropertyChanged("IdentificationNumber");
                }
            }
        }

        [Required(ErrorMessage = "Debe Ingresar el Nombre del participante")]
        public string NameParticipant
        {
            get => _NameParticipante;
            set
            {
                if (value != _NameParticipante)
                {
                    _NameParticipante = value;
                    RaisePropertyChanged("NameParticipant");
                }
            }
        }
        public string TypeParticipant
        {
            get => _TypeParticipant;
            set
            {
                if (value != _TypeParticipant)
                {
                    _TypeParticipant = value;
                    RaisePropertyChanged("TypeParticipant");
                }
            }
        }

        public virtual Ticket Ticket { get; set; }

    }
}
