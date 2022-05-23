using Orden.Helpers;
using Orden.Model;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Orden.ViewModels
{
    public class GeneralInformation_ViewModel : GenericRepository<GeneralInformation>
    {
        private GeneralInformation general;
        private ValidationFrech validationFrech;
        readonly CommonFunctions common = new CommonFunctions();
        private string Query;

        public GeneralInformation GeneralInformation(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.GeneralInformations.Where(P => P.IdTicket == Case).FirstOrDefault();
            }
        }
        public string Modality(string City, string Dept, decimal? Val)
        {
            if (City != "" && Dept != "" && Val != 0)
            {
                Query = "SELECT [Valor] FROM [CiudadesModalidad$] WHERE [Departamento] = '" + Dept + "' AND [Municipio] = '" + City + "'";
                string ValCity = common.ExecuteQuery(Query);
                if (ValCity == "") ValCity = "0";
                if (Val.ToString() != "")
                {
                    Query = "SELECT[Moda] FROM[Modalidad$] WHERE[Ciudad] = " + ValCity + " AND " + Val + " BETWEEN[Rango_Inicial] AND[Rango_Final]";
                    return common.ExecuteQuery(Query);
                }
            }
            return "";
        }
        public void RuleFinancing(string ApprovedValue, string AppraisalValue, TextBox textBox, Grid grid)
        {
            general = (GeneralInformation)grid.DataContext;
            if (ApprovedValue != "" && AppraisalValue != "" && decimal.Parse(AppraisalValue, System.Globalization.NumberStyles.Currency) != 0)
            {
                textBox.Text = decimal.Round(decimal.Parse(ApprovedValue.Replace("$", "")) / decimal.Parse(AppraisalValue.Replace("$", "")) * 100, 2).ToString();
                if (decimal.Parse(textBox.Text) > 80 && (general.Modality.ToString() == "VIS" || general.Modality.ToString() == "VIP" || general.Modality.ToString() == "DIFVI"))
                {
                    textBox.Foreground = Brushes.Red;
                    textBox.ToolTip = "Porcentaje de Financiación supera el establecido para VIS – VIP – DIFVI";
                }
                else if (decimal.Parse(textBox.Text) > 70 && general.Modality.Contains("NO VIS"))
                {
                    textBox.Foreground = Brushes.Red;
                    textBox.ToolTip = "Porcentaje de Financiación supera el establecido para NO VIS";
                }
                else
                {
                    textBox.Foreground = Brushes.Black;
                    textBox.ToolTip = "";
                }
            }
        }
        public void RuleFrech(Grid gridGeneral, Grid gridFrech)
        {
            general = (GeneralInformation)gridGeneral.DataContext;
            validationFrech = (ValidationFrech)gridFrech.DataContext;
            if (general.Cession != null && general.Vda != null && validationFrech.HasFrech != null)
            {
                string A = general.Cession.ToString().ToUpper();
                string B = general.Vda.ToString().ToUpper();
                string C = validationFrech.HasFrech.ToString().ToUpper();
                if (A == "SI" && B == "USADO" && C == "SI")
                {
                    MessageBox.Show("Notificar compra de cartera con Frech a gestión y control de datos", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        public void RuleScheme(ComboBox cbScheme, string Modality, string Ticket, bool Message)
        {
            cbScheme.ItemsSource = null;
            cbScheme.Items.Clear();
            if (Modality != "")
            {
                CaseModality(Modality, cbScheme);
                if (GeneralInformation(Ticket) != null)
                {
                    cbScheme.Text = "";
                    cbScheme.Text = GeneralInformation(Ticket).Scheme;
                    if (cbScheme.SelectedIndex == -1)
                    {
                        cbScheme.Text = "";
                        CaseModality(Modality, cbScheme);
                        if (Message == true)
                        {
                            MessageBox.Show("Recuerde validar nuevamente el SCHEME CODE", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }
        public string Term(string Classification, string Coin, int term)
        {
            if (Classification != null && Coin != null && term.ToString() != "")
            {
                Query = "SELECT [Mensaje] FROM [Plazo$] WHERE [CLASIFICACION] = '" + Classification.ToUpper() + "' AND [MONEDA] = '" + Coin.ToUpper() + "' and [TOPEPLAZO] < " + term + "";
                return common.ExecuteQuery(Query);
            }
            return "";
        }
        public string RuleRate(string coin, string modality, string rate)
        {
            if (coin != "" && modality != "" && rate != "" && modality != "DIFVI")
            {
                decimal value = decimal.Parse(rate);
                Query = "SELECT [MENSAJE] FROM [TasaPolitica$] WHERE [MONEDA] = '" + coin + "' AND [MODALIDAD] = '" + modality + "' and [TOPE] < " + value + "";
                return common.ExecuteQuery(Query);
            }
            return "";
        }
        public void Delete(string Ticket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                var ToDelete = model.GeneralInformations.Where(X => X.IdTicket == Ticket).FirstOrDefault();
                if (ToDelete != null)
                {
                    model.Entry(ToDelete).State = EntityState.Deleted;
                    model.SaveChanges();
                }
            }
        }
        public void CaseModality(string Id, ComboBox cbScheme)
        {
            switch (Id)
            {
                case "VIS":
                    common.FillCombobox(comboBox: cbScheme, "VisVip", "TIPOS");
                    break;
                case "VIP":
                    common.FillCombobox(comboBox: cbScheme, "VisVip", "TIPOS");
                    break;
                case "NO VIS":
                    common.FillCombobox(comboBox: cbScheme, "NoVis", "TIPOS");
                    break;
                case "DIFVI":
                    common.FillCombobox(comboBox: cbScheme, "Difvi", "TIPOS");
                    break;
                default:
                    break;
            }
        }
    }
}
