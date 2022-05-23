using Orden.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orden.ViewModels
{
    [NotMapped]
    public class Check_ViewModel : CausesStranding
    {
        public bool IsSelect { get; set; }
    }
}
