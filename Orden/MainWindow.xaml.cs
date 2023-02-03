using log4net;
using Orden.Helpers;
using Orden.Model;
using Orden.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Orden
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// Declaracón de variables
        /// <summary>
        /// Log de error de la aplicación: \bin\Debug\MiAsistenteEnProcesos_LogdeErrores.log cuando no esya instalada
        /// //**********************************************************************************************************
        /// Log de error de la aplicación: \bin\Debug\MiAsistenteEnProcesos_LogdeErrores.log cuando esta instalada
        /// C:\Users\UserName\AppData\Local\Apps\2.0\*
        /// </summary
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CommonFunctions common = new CommonFunctions();
        public readonly Order mainWindow = new Order();
        private int IdOrder = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        public void FindStates(string name, int NumOrder)
        {
            try
            {
                using (ModelOrder model = new ModelOrder())
                {
                    IdOrder = name != "" ? model.Orders.Where(x => x.NameOrder == name).FirstOrDefault().IdOrders : NumOrder;
                    foreach (var item in model.OrderStatesGets.Where(x => x.IdOrder == IdOrder && x.Active == true))
                    {
                        if (mainWindow.FindStates == "")
                        {
                            mainWindow.FindStates = item.IdState.ToString();
                        }
                        else
                        {
                            mainWindow.FindStates = mainWindow.FindStates + "," + item.IdState.ToString();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);              
                Application.Current.Shutdown();
            }
        }
        public void LoadStatesOrder(int Id)
        {
            try
            {
                mainWindow.name = common.NameOrder(Id);
                common.FillStates(mainWindow.cbStateOrder, Id);
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);               
                Application.Current.Shutdown();
            }
        }
        public void Display()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(3000);
                    Dispatcher.Invoke(() =>
                    {
                        common.LoadCombobox(mainWindow);
                        mainWindow.Clear();
                        mainWindow.Show();
                        this.Close();
                        return Task.CompletedTask;
                    });
                });
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);               
                Application.Current.Shutdown();
            }
        }
        private void CbOrders_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbOrders.Text != "")
                {
                    if (cbOrders.Text == "Orden Auxiliar de Análisis")
                    {
                        mainWindow.Assignment.Visibility = Visibility.Visible;
                        mainWindow.PrintUser.Visibility = Visibility.Visible;
                    }
                    if (cbOrders.Text == "Consulta Orden")
                    {
                        mainWindow.Print.Visibility = Visibility.Visible;
                        mainWindow.Save.Visibility = Visibility.Collapsed;
                    }
                    mainWindow.name = cbOrders.Text;
                    gridSelect.Visibility = Visibility.Collapsed;
                    FindStates(cbOrders.Text, 0);
                    Display();
                    LoadStatesOrder(IdOrder);
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("La aplicación presento un error controlado, comuniquese con el administrador", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
    }
}
