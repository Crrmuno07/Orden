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
    public class Disbursement_ViewModel : GenericRepository<Disbursement>
    {
        readonly CommonFunctions common = new CommonFunctions();
        public ObservableCollection<Disbursement> Disbursements(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return new ObservableCollection<Disbursement>(model.Disbursements.Where(P => P.IdTicket == Case).ToList());
            }
        }
        public void DeleteDatagridDisbursement(DataGrid dataGrid, List<Disbursement> list, List<Disbursement> listRemove, Grid gridGeneric, object sender, CanExecuteRoutedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            if (e.Command == DataGrid.DeleteCommand)
            {
                if (MessageBox.Show(string.Format("Seguro que desea eliminar la contingencia {0}", (grid.SelectedItem as Disbursement).Contingency), "Confirmar Eliminación", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    listRemove.Add(grid.SelectedItem as Disbursement);
                    list.RemoveAt(grid.SelectedIndex);
                    common.GenericCleaner(gridGeneric, true);
                    dataGrid.ItemsSource = list;
                }
            }
            if (dataGrid.Items.Count == 0) dataGrid.Visibility = Visibility.Collapsed;
        }
        public void SaveDatagridDisbursement(DataGrid dataGrid, List<Disbursement> list, Disbursement newdisbursement)
        {
            dataGrid.ItemsSource = null;
            list.RemoveAll(X =>
            {
                bool a = X.Contingency == newdisbursement.Contingency;
                bool b = X.Value == newdisbursement.Value;
                bool c = X.Gmf == newdisbursement.Gmf;
                return a && b && c;
            });
            list.Add(newdisbursement);
            dataGrid.ItemsSource = list;
        }
        public void Delete(string Ticket, List<Disbursement> List)
        {
            using (ModelOrder model = new ModelOrder())
            {
                foreach (var item in List)
                {
                    IList<Disbursement> disbursements = model.Disbursements.Where(X => X.IdTicket == Ticket && X.IdDisbursement == item.IdDisbursement).ToList();
                    model.Disbursements.RemoveRange(disbursements);
                    model.SaveChanges();
                }
            }
        }
        public bool Save(string Ticket, List<Disbursement> list, bool validation, bool Exists)
        {
            if (validation == false)
            {
                foreach (Disbursement item in list)
                {
                    Disbursement disbursement = new Disbursement
                    {
                        IdTicket = Ticket,
                        IdDisbursement = item.IdDisbursement,
                        Contingency = item.Contingency,
                        Value = item.Value,
                        Gmf = item.Gmf
                    };
                    if (Exists == false)
                    {
                        disbursement.IdDisbursement = 0;
                    }
                    if (disbursement.IdDisbursement == 0) { Add(disbursement); } else { Update(disbursement); }
                }
            }
            return false;
        }
    }
}
