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
    public class BankCheck_ViewModel : GenericRepository<BankCheck>
    {
        readonly CommonFunctions common = new CommonFunctions();
        public ObservableCollection<BankCheck> BankCheck(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return new ObservableCollection<BankCheck>(model.BankChecks.Where(P => P.IdTicket == Case).ToList());
            }
        }
        public void DeleteDatagridCheck(DataGrid dataGrid, List<BankCheck> list, List<BankCheck> listRemove, Grid gridGeneric, object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                DataGrid grid = (DataGrid)sender;
                if (MessageBox.Show(string.Format("Seguro que desea eliminar el cheque del beneficiario {0}", (grid.SelectedItem as BankCheck).Beneficiary), "Confirmar Eliminación", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    listRemove.Add(grid.SelectedItem as BankCheck);
                    list.RemoveAt(grid.SelectedIndex);
                    common.GenericCleaner(gridGeneric, true);
                    dataGrid.ItemsSource = list;
                }
            }
            if (dataGrid.Items.Count == 0) dataGrid.Visibility = Visibility.Collapsed;
        }
        public void SaveDatagridCheck(DataGrid dataGrid, List<BankCheck> list, BankCheck newbankCheck, int id)
        {
            dataGrid.ItemsSource = null;
            if (id != -1 && list.Count > 0) list.RemoveAt(id);
            list.Add(newbankCheck);
            dataGrid.ItemsSource = list;
        }
        public void Delete(string Ticket, List<BankCheck> List)
        {
            using (ModelOrder model = new ModelOrder())
            {
                foreach (var item in List)
                {
                    IList<BankCheck> bankChecks = model.BankChecks.Where(X => X.IdTicket == Ticket && X.IdCheck == item.IdCheck).ToList();
                    model.BankChecks.RemoveRange(bankChecks);
                    model.SaveChanges();
                }
            }
        }
        public string Save(string Ticket, List<BankCheck> list, bool validation, bool Exists)
        {
            int flag = list.Where(l => l.Office == "" || l.Office == null).Count();
            if (flag != 0 && validation == true)
            {
                return "Recuerde ingresar la oficina para todos los cheques que se encuentren en la orden";
            }
            if (validation == false)
            {
                foreach (BankCheck item in list)
                {
                    BankCheck bankCheck = new BankCheck
                    {
                        IdTicket = Ticket,
                        IdCheck = item.IdCheck,
                        Office = item.Office,
                        IdentificationType = item.IdentificationType,
                        IdentificationNumber = item.IdentificationNumber,
                        Beneficiary = item.Beneficiary,
                        Value = item.Value,
                        Gmf = item.Gmf
                    };
                    if (Exists == false)
                    {
                        bankCheck.IdCheck = 0;
                    }
                    if (bankCheck.IdCheck == 0) { Add(bankCheck); } else { Update(bankCheck); }
                }
            }
            return "";
        }
    }
}
