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
    public class BanckDebit_ViewModel : GenericRepository<BankDebitAccount>
    {
        readonly CommonFunctions common = new CommonFunctions();
        public ObservableCollection<BankDebitAccount> BankDebitAccounts(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return new ObservableCollection<BankDebitAccount>(model.BankDebitAccounts.Where(P => P.IdTicket == Case).ToList());
            }
        }
        public void DeleteDatagridDebit(DataGrid dataGrid, List<BankDebitAccount> list, List<BankDebitAccount> listRemove, Grid gridGeneric, object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                DataGrid grid = (DataGrid)sender;
                if (MessageBox.Show(string.Format("Seguro que desea eliminar el la cuenta debito {0}", (grid.SelectedItem as BankDebitAccount).AccountNumber), "Confirmar Eliminación", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    listRemove.Add(grid.SelectedItem as BankDebitAccount);
                    list.RemoveAt(grid.SelectedIndex);
                    common.GenericCleaner(gridGeneric, true);
                    dataGrid.ItemsSource = list;
                }
            }
            if (dataGrid.Items.Count == 0) dataGrid.Visibility = Visibility.Collapsed;
        }
        public void SaveDatagridDebit(DataGrid dataGrid, List<BankDebitAccount> list, BankDebitAccount newbankDebitAccount, int id)
        {
            double? Value = 0;
            dataGrid.ItemsSource = null;
            if (id != -1 && list.Count > 0) list.RemoveAt(id);
            list.Add(newbankDebitAccount);
            foreach (var item in list)
            {
                Value += item.Porcentage;
            }
            if (Value > 100)
            {
                MessageBox.Show("La suma de los porcentajes de los debitos automaticos supera el maximo permitido del 100%", "Debito Automatico", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                list.RemoveAll(X => X.AccountNumber == newbankDebitAccount.AccountNumber && X.AccountType == newbankDebitAccount.AccountType);
            }
            dataGrid.ItemsSource = list;
        }
        public void Delete(string Ticket, List<BankDebitAccount> List)
        {
            using (ModelOrder model = new ModelOrder())
            {
                foreach (var item in List)
                {
                    IList<BankDebitAccount> bankDebits = model.BankDebitAccounts.Where(X => X.IdTicket == Ticket && X.IdDebitAccount == item.IdDebitAccount).ToList();
                    model.BankDebitAccounts.RemoveRange(bankDebits);
                    model.SaveChanges();
                }
            }
        } 
        public string ValidateSum(List<BankDebitAccount> list)
        {
            double? Value = 0;
            foreach (var item in list)
            {
                Value += item.Porcentage;
            }
            if (Value > 100)
            {
                return "La suma de los porcentajes de los debitos automaticos supera el maximo permitido del 100%";              
            }
            return "";
        }
        public bool Save(string Ticket, List<BankDebitAccount> list, bool validation, bool Exists)
        {
            if (validation == false)
            {
                foreach (BankDebitAccount item in list)
                {
                    BankDebitAccount bankDebitAccount = new BankDebitAccount
                    {
                        IdTicket = Ticket,
                        IdDebitAccount = item.IdDebitAccount,
                        AccountNumber = item.AccountNumber,
                        AccountType = item.AccountType,
                        Porcentage = item.Porcentage
                    };
                    if (Exists == false)
                    {
                        bankDebitAccount.IdDebitAccount = 0;
                    }
                    if (bankDebitAccount.IdDebitAccount == 0) { Add(bankDebitAccount); } else { Update(bankDebitAccount); }
                }
            }
            return false;
        }
    }
}
