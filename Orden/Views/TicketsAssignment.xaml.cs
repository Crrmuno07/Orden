namespace Orden.Views
{

    using log4net;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Orden.Model;
    using System.Windows;
    using System.Collections.Generic;
    using Orden.ViewModels;
    using System.Windows.Data;
    using System.Data.SqlClient;

    public partial class TicketsAssignment : Window
    {

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<TicketsAssignment_ViewModel> ticketsAssignment;

        public TicketsAssignment()
        {
            InitializeComponent();
            Load();
        }
        private void Load()
        {
            string UserName = Environment.UserName;
            try
            {
                using (ModelOrder modeldb = new ModelOrder())
                {

                    ticketsAssignment = modeldb.Database.SqlQuery<TicketsAssignment_ViewModel>("SP_Order_TicketsAssignment @UserName",
                                                  new SqlParameter("UserName", UserName)
                                                  ).ToList();

                    gridTikets.ItemsSource = ticketsAssignment;
                }
            }
            catch (Exception exc)
            {
                if (exc.InnerException != null && exc.InnerException.InnerException != null)
                {
                    string msg = exc.InnerException.InnerException.Message;
                    Log.Fatal(msg, exc);
                }
            }            
        }

        private void Btn_Copy_Click(object sender, RoutedEventArgs e)
        {
            string IdTicket = (string)((System.Windows.Controls.Button)sender).CommandParameter;
            Clipboard.SetText(IdTicket);
            Close();
        }

        private void CancelUser_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

