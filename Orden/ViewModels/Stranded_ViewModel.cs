using Orden.Helpers;
using Orden.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Orden.ViewModels
{
    public class Stranded_ViewModel : GenericRepository<CausesStranding>
    {
        private readonly CommonFunctions common = new CommonFunctions();
        public List<string> CausesStranding(string Case, string Type, string State)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.CausesStrandings.Where(P => P.IdTicket == Case && P.TypeName == State && P.StatusName == Type).Select(x => x.OptionName).ToList();
            }
        }
        public void DeleteDatagridStranded(DataGrid dataGrid, List<string> list, DataGrid dataGridManagement, List<string> listManagement, object sender, CanExecuteRoutedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            if (e.Command == DataGrid.DeleteCommand)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(string.Format("Seguro que desea pasar a gestionado la causal de varado: ", (dataGrid.SelectedItem as Check_ViewModel).OptionName), "Confirmar Proceso", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var value = list.Find(X => X == (dataGrid.SelectedItem as Check_ViewModel).OptionName);
                    listManagement.Add(value);
                    list.RemoveAll(X => X == (dataGrid.SelectedItem as Check_ViewModel).OptionName);
                    dataGrid.ItemsSource = null;
                    common.Check_ViewModel(dataGrid, list);
                    dataGridManagement.ItemsSource = null;
                    common.Check_ViewModel(dataGridManagement, listManagement);
                    if (dataGrid.Items.Count == 0) dataGrid.Visibility = Visibility.Collapsed;
                }
            }
        }
        public void Delete(string Ticket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                IList<CausesStranding> causes = model.CausesStrandings.Where(X => X.IdTicket == Ticket).ToList();
                model.CausesStrandings.RemoveRange(causes);
                model.SaveChanges();
            }
        }
        public bool Save(string Ticket,  bool validation, string Status, string Type, DataGrid dataGrid)
        {
            if (validation == false)
            {
                foreach (Check_ViewModel item in dataGrid.Items)
                {
                    CausesStranding causes = new CausesStranding
                    {
                        IdTicket = Ticket,
                        CreationDate = DateTime.Now,
                        StatusName = Status,
                        TypeName = Type,
                        OptionName = item.OptionName
                    };
                    Add(causes);
                }
            }
            return false;
        }
    }
}
