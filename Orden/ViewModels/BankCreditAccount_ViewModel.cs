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
    public class BankCreditAccount_ViewModel : GenericRepository<BankCreditAccount>
    {
        readonly CommonFunctions common = new CommonFunctions();

        public ObservableCollection<BankCreditAccount> BankCreditAccount(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return new ObservableCollection<BankCreditAccount>(model.BankCreditAccounts.Where(P => P.IdTicket == Case).ToList());
            }
        }
        public void DeleteDatagridAccount(DataGrid dataGrid, List<BankCreditAccount> list, List<BankCreditAccount> listRemove, Grid gridGeneric, object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                DataGrid grid = (DataGrid)sender;
                if (MessageBox.Show(string.Format("Seguro que desea eliminar la cuenta {0}", (grid.SelectedItem as BankCreditAccount).AccountNumber), "Confirmar Eliminación", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    listRemove.Add(grid.SelectedItem as BankCreditAccount);
                    list.RemoveAt(grid.SelectedIndex);
                    common.GenericCleaner(gridGeneric, true);
                    dataGrid.ItemsSource = list;
                }
            }
            if (dataGrid.Items.Count == 0) dataGrid.Visibility = Visibility.Collapsed;
        }
        public void SaveDatagridAccount(DataGrid dataGrid, List<BankCreditAccount> list, BankCreditAccount newbankCreditAccount, int id)
        {
            dataGrid.ItemsSource = null;
            if (id != -1 && list.Count > 0) list.RemoveAt(id);
            list.Add(newbankCreditAccount);
            dataGrid.ItemsSource = list;
        }
        public void Delete(string Ticket, List<BankCreditAccount> List)
        {
            using (ModelOrder model = new ModelOrder())
            {
                foreach (var item in List)
                {
                    IList<BankCreditAccount> bankCreditAccounts = model.BankCreditAccounts.Where(X => X.IdTicket == Ticket && X.IdCreditAccount == item.IdCreditAccount).ToList();
                    model.BankCreditAccounts.RemoveRange(bankCreditAccounts);
                    model.SaveChanges();
                }
            }
        }
        public bool Save(string Ticket, List<BankCreditAccount> list, bool validation, bool Exists)
        {
            if (validation == false)
            {
                foreach (BankCreditAccount item in list)
                {
                    BankCreditAccount bankCreditAccount = new BankCreditAccount
                    {
                        IdTicket = Ticket,
                        IdCreditAccount = item.IdCreditAccount,
                        IdentificationNumber = item.IdentificationNumber,
                        IdentificationType = item.IdentificationType,
                        AccountType = item.AccountType,
                        AccountNumber = item.AccountNumber,
                        Value = item.Value,
                        Gmf = item.Gmf
                    };
                    if (Exists == false)
                    {
                        bankCreditAccount.IdCreditAccount = 0;
                    }
                    if (bankCreditAccount.IdCreditAccount == 0) { Add(bankCreditAccount); } else { Update(bankCreditAccount); }
                }
            }
            return false;
        }
    }
}
