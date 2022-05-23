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
    public class Subrogation_ViewModel : GenericRepository<Subrogation>
    {
        readonly CommonFunctions common = new CommonFunctions();
        public ObservableCollection<Subrogation> Subrogation(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return new ObservableCollection<Subrogation>(model.Subrogations.Where(P => P.IdTicket == Case).ToList());
            }
        }
        public void DeleteDatagridSubrogation(DataGrid dataGrid, List<Subrogation> list, List<Subrogation> listRemove, Grid gridGeneric, object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                DataGrid grid = (DataGrid)sender;
                if (MessageBox.Show(string.Format("Seguro que desea eliminar la subrogación del usuario {0}", (grid.SelectedItem as Subrogation).IdentificationNumber), "Confirmar Eliminación", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    listRemove.Add(grid.SelectedItem as Subrogation);
                    list.RemoveAt(grid.SelectedIndex);
                    common.GenericCleaner(gridGeneric, true);
                    dataGrid.ItemsSource = list;
                }
            }
            if (dataGrid.Items.Count == 0) dataGrid.Visibility = Visibility.Collapsed;
        }
        public void SaveDatagridSubrogation(DataGrid dataGrid, List<Subrogation> list, Subrogation newsubrogation, int id)
        {
            dataGrid.ItemsSource = null;
            if (id != -1 && list.Count > 0) list.RemoveAt(id);
            list.Add(newsubrogation);
            dataGrid.ItemsSource = list;
        }
        public void Delete(string Ticket, List<Subrogation> List)
        {
            using (ModelOrder model = new ModelOrder())
            {
                foreach (var item in List)
                {
                    IList<Subrogation> subrogations = model.Subrogations.Where(X => X.IdTicket == Ticket && X.IdSubrogation == item.IdSubrogation).ToList();
                    model.Subrogations.RemoveRange(subrogations);
                    model.SaveChanges();
                }
            }
        }
        public bool Save(string Ticket, List<Subrogation> list, bool validation, bool Exists)
        {
            if (validation == false)
            {
                foreach (Subrogation item in list)
                {
                    Subrogation subrogation = new Subrogation
                    {
                        IdTicket = Ticket,
                        IdSubrogation = item.IdSubrogation,
                        SubrogationNumber = item.SubrogationNumber,
                        IdentificationType = item.IdentificationType,
                        IdentificationNumber = item.IdentificationNumber,
                        ObligationNumber = item.ObligationNumber,
                        Value = item.Value,
                        Gmf = item.Gmf
                    };
                    if (Exists == false)
                    {
                        subrogation.IdSubrogation = 0;
                    }
                    if (subrogation.IdSubrogation == 0) { Add(subrogation); } else { Update(subrogation); }
                }
            }
            return false;
        }
    }
}
