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
    public class Seller_ViewModel : GenericRepository<Seller>
    {
        readonly CommonFunctions common = new CommonFunctions();

        public ObservableCollection<Seller> Sellers(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return new ObservableCollection<Seller>(model.Sellers.Where(P => P.IdTicket == Case).ToList());
            }
        }
        public void DeleteDatagridSellers(DataGrid dataGrid, List<Seller> list, List<Seller> listRemove, Grid gridGeneric, object sender, CanExecuteRoutedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            if (e.Command == DataGrid.DeleteCommand)
            {
                if (MessageBox.Show(string.Format("Seguro que desea eliminar el vendedor {0}", (grid.SelectedItem as Seller).IdentificationNumber), "Confirmar Eliminación", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    listRemove.Add(grid.SelectedItem as Seller);
                    list.RemoveAt(grid.SelectedIndex);
                    common.GenericCleaner(gridGeneric, true);
                    dataGrid.ItemsSource = list;
                }
            }
            if (dataGrid.Items.Count == 0) dataGrid.Visibility = Visibility.Collapsed;
        }
        public void SaveDatagridSellers(DataGrid dataGrid, List<Seller> list, Seller newseller, int id)
        {
            dataGrid.ItemsSource = null;
            if (id != -1 && list.Count > 0) list.RemoveAt(id);
            list.Add(newseller);
            dataGrid.ItemsSource = list;
        }
        public void Delete(string Ticket, List<Seller> List)
        {
            using (ModelOrder model = new ModelOrder())
            {
                foreach (var item in List)
                {
                    IList<Seller> sellers = model.Sellers.Where(X => X.IdTicket == Ticket && X.IdSeller == item.IdSeller).ToList();
                    model.Sellers.RemoveRange(sellers);
                    model.SaveChanges();
                }
            }
        }
        public bool Save(string Ticket, List<Seller> list, bool validation, bool Exists)
        {
            if (validation == false)
            {
                foreach (Seller item in list)
                {
                    Seller seller = new Seller
                    {
                        IdTicket = Ticket,
                        IdSeller = item.IdSeller,
                        IdentificationType = item.IdentificationType,
                        IdentificationNumber = item.IdentificationNumber,
                        NameSeller = item.NameSeller
                    };
                    if (Exists == false)
                    {
                        seller.IdSeller = 0;
                    }
                    if (seller.IdSeller == 0) { Add(seller); } else { Update(seller); }
                }
            }
            return false;
        }
    }
}
