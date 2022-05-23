using Orden.Helpers;
using Orden.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Orden.ViewModels
{
    public class Insurance_ViewModel : GenericRepository<Insurance>
    {
        private readonly CommonFunctions common = new CommonFunctions();
        public ObservableCollection<Insurance> Insurance(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return new ObservableCollection<Insurance>(model.Insurances.Where(P => P.IdTicket == Case).ToList());
            }
        }
        public void DeleteDatagridInsurance(DataGrid dataGrid, List<Insurance> list, List<Insurance> listRemove, Grid gridGeneric, object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                DataGrid grid = (DataGrid)sender;
                if (MessageBox.Show(string.Format("Seguro que desea eliminar el seguro {0}", (grid.SelectedItem as Insurance).IdentificationNumber), "Confirmar Eliminación", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    listRemove.Add(grid.SelectedItem as Insurance);
                    list.RemoveAt(grid.SelectedIndex);
                    common.GenericCleaner(gridGeneric, true);
                    dataGrid.ItemsSource = list;
                }
            }
            if (dataGrid.Items.Count == 0) dataGrid.Visibility = Visibility.Collapsed;
        }
        public void SaveDatagridInsurance(List<Insurance> list, DataGrid dataGrid, Insurance insurance, int id)
        {
            dataGrid.ItemsSource = null;
            if (id != -1 && list.Count > 0) list.RemoveAt(id);
            list.Add(insurance);
            dataGrid.ItemsSource = list;
        }
        public string Save(string Ticket, List<Insurance> list, bool validation, bool Exists, string scheme)
        {
            string[] values = { "RVV", "CVV", "CVM", "RVM" };
            if (list.Where(X => X.InsuranceType == "VIDA" || X.InsuranceType == "IYT").Count() < 2)
            {
                return "El ticket debe tener los “seguro de VIDA e IYT";
            }
            if(scheme != "")
            {
                if (values.Contains(scheme.Substring(0, 3)) == true && list.Where(X => X.InsuranceType == "TRC").Count() == 0)
                {
                    return "El ticket debe tener un registro TRC en seguros por el tipo de scheme code que se tiene seleccionado";
                }
            }
            if (validation == false)
            {
                foreach (var item in list)
                {
                    Insurance insurance = new Insurance
                    {
                        IdTicket = Ticket,
                        IdInsurance = item.IdInsurance,
                        IdentificationNumber = item.IdentificationNumber,
                        PolicyType = item.PolicyType,
                        InsuranceType = item.InsuranceType,
                        InsuranceCarrier = item.InsuranceCarrier,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate
                    };
                    if (Exists == false)
                    {
                        insurance.IdInsurance = 0;
                    }
                    if (insurance.IdInsurance == 0) { Add(insurance); } else { Update(insurance); }
                }
            }
            return "";
        }
        public void Delete(string Ticket, List<Insurance> List)
        {
            using (ModelOrder model = new ModelOrder())
            {
                foreach (var item in List)
                {
                    IList<Insurance> insurances = model.Insurances.Where(X => X.IdTicket == Ticket && X.IdInsurance == item.IdInsurance).ToList();
                    model.Insurances.RemoveRange(insurances);
                    model.SaveChanges();
                }
            }
        }
    }
}
