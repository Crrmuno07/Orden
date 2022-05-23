using log4net;
using Orden.Model;
using Orden.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataTable = System.Data.DataTable;
using TextBox = System.Windows.Controls.TextBox;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Orden.Helpers
{
    public class CommonFunctions : NotifyPropertyChangedImpl
    {
        /// <summary>
        /// Log de error de la aplicación: \bin\Debug\MiAsistenteEnProcesos_LogdeErrores.log cuando no esya instalada
        /// //**********************************************************************************************************
        /// Log de error de la aplicación: \bin\Debug\MiAsistenteEnProcesos_LogdeErrores.log cuando esta instalada
        /// C:\Users\UserName\AppData\Local\Apps\2.0\*
        /// </summary
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string FileParameter = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)), "Archivos", ConfigurationManager.AppSettings["Parametros"]);
        private readonly string FileOffice = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)), "Archivos", ConfigurationManager.AppSettings["Sucursales"]);
        private readonly string FileDptoCity = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)), "Archivos", ConfigurationManager.AppSettings["DptoCity"]);
        public int idUser = 0;

        public string ExecuteQuery(string Query)
        {
            using (OleDbConnection connection = new OleDbConnection(string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter)))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(Query, connection))
                {
                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                return dr[0].ToString();
                            }
                        }
                    }
                }
            }
            return "";
        }
        public StringBuilder ModelValidator(object obj, StringBuilder Result)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, context, validationResults, true);
            if (validationResults.Count != 0)
            {
                if (Result.ToString() == "")
                {
                    Result.AppendLine("La validación de datos genero los siguientes Errores:");
                    Result.AppendLine("");
                }
                foreach (ValidationResult item in validationResults)
                {
                    Result.AppendFormat("-- {0}\n", item.ErrorMessage);
                }
            }
            return Result;
        }
        public void FillComboboxStranded(ComboBox comboBox, string Sheet, string column)
        {
            try
            {
                DataTable dt = new DataTable();
                using (OleDbConnection connection = new OleDbConnection(string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter)))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand("Select [" + column + "] From [" + Sheet + "$]", connection))
                    {
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }
                comboBox.ItemsSource = (from DataRow item in dt.Rows
                                        let row = new Check_ViewModel
                                        {
                                            OptionName = item["Tipos"].ToString(),
                                            IsSelect = false
                                        }
                                        select row).ToList();
            }
            catch (Exception exc)
            {
                Log.Fatal("Se ha presento un Error en FillComboboxStranded función de commonFuntions" + exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        public void FillStates(ComboBox comboBox, int id)
        {
            using (ModelOrder model = new ModelOrder())
            {
                comboBox.ItemsSource = (from a in model.OrderStates
                                        join b in model.States on a.IdState equals b.IdState
                                        where a.IdOrder == id && a.Active == true
                                        select new { a.IdState, b.NameState }).ToList();
            }
        }
        public void FillCombobox(ComboBox comboBox, string Sheet, string column)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter)))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand("Select [" + column + "] From [" + Sheet + "$] ORDER BY [" + column + "] ASC", connection))
                    {
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);
                            comboBox.ItemsSource = ((IListSource)dt).GetList();
                            comboBox.DisplayMemberPath = "TIPOS";
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal("Se ha presento un Error en FillCombobox función de commonFuntions" + exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        public void FillComboboxDptoCity(ComboBox comboBox, string dpto, string city)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileDptoCity)))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand("Select [Municipio] From [Ciudades$] Where [Departamento] = '" + dpto + "' ORDER BY [Municipio] ASC", connection))
                    {
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);
                            comboBox.Text = "";
                            comboBox.ItemsSource = null;
                            comboBox.ItemsSource = ((IListSource)dt).GetList();
                            comboBox.DisplayMemberPath = "Municipio";
                            comboBox.Text = city;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal("Se ha presento un Error en FillComboboxDptoCity función de commonFuntions" + exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        public void ValidationTextNumber(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }
        public void GenericCleaner(Grid grid, bool? Flag)
        {
            try
            {
                foreach (object item in grid.Children)
                {
                    if (item.GetType().Name.ToUpper() == "TEXTBOX")
                    {
                        TextBox textbox = (TextBox)item;
                        textbox.Text = "";
                    }
                    if (item.GetType().Name.ToUpper() == "COMBOBOX")
                    {
                        ComboBox comboBox = (ComboBox)item;
                        comboBox.Text = "";
                    }
                    if (item.GetType().Name.ToUpper() == "DATEPICKER")
                    {
                        DatePicker datePicker = (DatePicker)item;
                        datePicker.SelectedDate = null;
                    }
                    if (item.GetType().Name.ToUpper() == "DATAGRID" && Flag == true)
                    {
                        DataGrid dataGrid = (DataGrid)item;
                        dataGrid.ItemsSource = null;
                    }
                }

            }
            catch (Exception exc)
            {
                Log.Fatal("Se ha presento un Error en GenericCleaner función de commonFuntions" + exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        public string GetPathPdf()
        {
            using (OleDbConnection connection = new OleDbConnection(string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter)))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand("SELECT [Ruta] FROM[RutaPDF$]", connection))
                {
                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                return CreateDir(dr[0].ToString());
                            }
                        }
                    }
                }
            }
            return "";
        }
        private string CreateDir(string basePath)
        {
            string result = string.Empty;
            string sYear = DateTime.Now.Year.ToString();
            string sDay = DateTime.Now.Day.ToString();
            try
            {
                string sMonth = "";
                switch (DateTime.Now.Month)
                {
                    case 1:
                        sMonth = "Enero";
                        break;
                    case 2:
                        sMonth = "Febrero";
                        break;
                    case 3:
                        sMonth = "Marzo";
                        break;
                    case 4:
                        sMonth = "Abril";
                        break;
                    case 5:
                        sMonth = "Mayo";
                        break;
                    case 6:
                        sMonth = "Junio";
                        break;
                    case 7:
                        sMonth = "Julio";
                        break;
                    case 8:
                        sMonth = "Agosto";
                        break;
                    case 9:
                        sMonth = "Septiembre";
                        break;
                    case 10:
                        sMonth = "Octubre";
                        break;
                    case 11:
                        sMonth = "Noviembre";
                        break;
                    case 12:
                        sMonth = "Diciembre";
                        break;
                    default:
                        break;
                }
                if (sDay.Length == 1)
                {
                    sDay = "0" + sDay;
                }
                string sFullPath = basePath + sYear + @"\" + sMonth + @"\" + sDay + @"\";
                if (!Directory.Exists(sFullPath))
                {
                    Directory.CreateDirectory(sFullPath);
                }
                result = sFullPath;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return result;

        }
        public List<string> Check(ComboBox comboBox, List<string> check_ViewModels)
        {
            ActiveCheck(comboBox, check_ViewModels);
            check_ViewModels = new List<string>();
            foreach (Check_ViewModel cm in comboBox.Items)
            {
                if (cm.IsSelect == true)
                {
                    int value = check_ViewModels.FindIndex(item => item == cm.OptionName);
                    if (value == -1)
                    {
                        check_ViewModels.Add(cm.OptionName);
                    }
                }
            }
            return check_ViewModels;
        }
        public void ActiveCheck(ComboBox comboBox, List<string> list)
        {
            foreach (Check_ViewModel cm in comboBox.Items)
            {
                if (list.Where(x => x == cm.OptionName).Count() > 0)
                {
                    cm.IsSelect = true;
                }
            }
        }
        public void Check_ViewModel(DataGrid dataGrid, List<string> check_ViewModels)
        {
            List<Check_ViewModel> modelCheck = new List<Check_ViewModel>();
            foreach (var item in check_ViewModels)
            {
                Check_ViewModel row = new Check_ViewModel
                {
                    OptionName = item,
                };
                modelCheck.Add(row);
            }
            foreach (Check_ViewModel item in dataGrid.Items)
            {
                Check_ViewModel row = new Check_ViewModel
                {
                    OptionName = item.OptionName,
                };
                if (check_ViewModels.FindAll(x => x.Contains(item.OptionName)).Count == 0)
                {
                    modelCheck.Add(row);
                }
            }
            dataGrid.ItemsSource = modelCheck;
        }
        public void ViewDataGrid(DataGrid datagrid)
        {
            datagrid.Visibility = datagrid.Items.Count != 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        public bool OfficeActive(string Office)
        {
            try
            {
                if(Office != "" && Office != null)
                {
                    int Aux = int.Parse(Office.Replace("BC", ""));
                    using (OleDbConnection connection = new OleDbConnection(string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileOffice)))
                    {
                        connection.Open();
                        using (OleDbCommand command = new OleDbCommand("Select [CODIGO_NUEVO] From [Sucursal$] WHERE [CODIGO_NUEVO] = " + Aux + "", connection))
                        {
                            using (DbDataReader dr = command.ExecuteReader())
                            {
                                DataTable dt = new DataTable();
                                dt.Load(dr);
                                if (dt.Rows.Count == 1)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal("Se ha presento un Error en OfficeActive función de commonFuntions" + exc.Message, exc);
                Application.Current.Shutdown();
            }
            return false;
        }
        public decimal PorcentageRate(string NameAgreement, string NameAttri, string NamePackege, string Inmueble, string column)
        {
            string cod = "";
            decimal value = 0;
            if (Inmueble != null && Inmueble != "")
            {
                if (cod == "") { cod = "Conservacion"; }
            }
            if (NameAgreement != null && NameAgreement != "")
            {
                if (cod == "") { cod = "Convenio"; } else { cod = "Convenio + " + NameAgreement; }
            }
            if (NameAttri != null && NameAttri != "")
            {
                if (cod == "") { cod = NameAttri; } else { cod = cod + " + " + NameAttri; }
            }
            if (NamePackege != null && NamePackege != "")
            {
                if (cod == "") { cod = NamePackege; } else { cod = cod + " + " + NamePackege; }
            }
            if (cod != "")
            {
                using (OleDbConnection connection = new OleDbConnection(string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter)))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand("Select [" + column + "] From [TablaDescuentos$] WHERE [Combinacion] = '" + cod + "'", connection))
                    {
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    value = decimal.Parse(dr[0].ToString());
                                    return value;
                                }
                            }
                        }
                    }
                }
            }
            return value;
        }
        public void TypeFrech(ComboBox comboBox, string Column)
        {
            try
            {
                if (Column != null && Column != "")
                {
                    using (OleDbConnection connection = new OleDbConnection(string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter)))
                    {
                        connection.Open();
                        using (OleDbCommand command = new OleDbCommand("Select [" + Column + "] From [TipoFrech$]", connection))
                        {
                            using (DbDataReader dr = command.ExecuteReader())
                            {
                                DataTable dt = new DataTable();
                                dt.Load(dr);
                                comboBox.ItemsSource = ((IListSource)dt).GetList();
                                comboBox.DisplayMemberPath = Column;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal("Se ha presento un Error en TypeFrech función de commonFuntions" + exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        public List<string> ListOrders()
        {
            using (ModelOrder model = new ModelOrder())
            {
                return (from O in model.Orders
                        join ou in model.OrderUsers on O.IdOrders equals ou.IdOrder
                        where ou.IdUser == model.Users.Where(x => x.NameUser.ToUpper() == Environment.UserName.ToUpper()).FirstOrDefault().IdUser && ou.Active == true
                        select O.NameOrder).ToList();
            }
        }
        public string NameOrder(int id)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.Orders.Where(x => x.IdOrders == id).FirstOrDefault().NameOrder;
            }
        }
        public int IdOrder()
        {
            using (ModelOrder model = new ModelOrder())
            {
                return (from O in model.Orders
                        join ou in model.OrderUsers on O.IdOrders equals ou.IdOrder
                        where ou.IdUser == model.Users.Where(x => x.NameUser.ToUpper() == Environment.UserName.ToUpper()).FirstOrDefault().IdUser && ou.Active == true
                        select O.IdOrders).FirstOrDefault();
            }
        }
        public int IdState(string State)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.States.Where(x => x.NameState == State).FirstOrDefault().IdState;
            }
        }
        public void SaveConnectionString(string connectionStringName, string connectionString)
        {
            try
            {
                Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                appconfig.ConnectionStrings.ConnectionStrings[connectionStringName].ConnectionString = connectionString;
                appconfig.Save();
            }
            catch (Exception exc)
            {
                Log.Fatal("Se ha presento un Error en SaveConnectionString función de commonFuntions" + exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        public bool ConvertPdf(string pngFilePath, bool deleteOriginal)
        {
            bool result;
            try
            {
                if (File.Exists(pngFilePath))
                {
                    using (ImageMagick.MagickImageCollection colletion = new ImageMagick.MagickImageCollection())
                    {
                        colletion.Add(pngFilePath);
                        colletion.Write(pngFilePath.Replace("png", "pdf"));
                        if (deleteOriginal)
                        {
                            try
                            {
                                File.Delete(pngFilePath);
                            }
                            catch (Exception)
                            {
                                //MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Log.Fatal("Se ha presento un Error en ConvertPdf función de commonFuntions" + ex.Message, ex);
            }
            return result;
        }
        public void LoadCombobox(Views.Order order)
        {
            try
            {
                //Información general casos reversado
                FillCombobox(order.cbCaseReversed, "Generico", "TIPOS");
                FillCombobox(order.cbClass, "Garantia", "TIPOS");
                FillCombobox(order.cbPlace, "Generico", "TIPOS");
                //Participantes
                FillCombobox(order.cbTypeParticipant, "TipoParticipante", "TIPOS");
                FillCombobox(order.cbType, "Documentos", "TIPOS");
                FillCombobox(order.cbClassification, "Clasificacion", "TIPOS");
                FillCombobox(order.cbBussines, "Generico", "TIPOS");
                FillCombobox(order.cbVda, "Vivienda", "TIPOS");
                FillCombobox(order.cbCoin, "Moneda", "TIPOS");
                FillCombobox(order.cbPlan, "Plan", "TIPOS");
                FillCombobox(order.cbSegment, "Segmento", "TIPOS");
                FillCombobox(order.cbFrech, "Frech", "TIPOS");
                FillCombobox(order.cbFonvivienda, "GENERICO", "TIPOS");
                FillCombobox(order.cbFormat, "Generico", "TIPOS");
                FillCombobox(order.cbBRP, "BRP", "TIPOS");
                FillCombobox(order.cbPropertyType, "Propiedad", "TIPOS");
                FillCombobox(order.cbDeparment, "Departamentos", "TIPOS");
                //Descuentos
                FillCombobox(order.cbPackage, "Paquete", "TIPOS");
                FillCombobox(order.cbEmployee, "Empleado", "TIPOS");
                FillCombobox(order.cbAgreement, "Convenio", "TIPOS");
                FillCombobox(order.cbAttributions, "Atribuciones", "TIPOS");
                FillCombobox(order.cbProperty, "Inmueble", "TIPOS");
                //Seguros
                FillCombobox(order.cbTypePol, "TipoPoliza", "TIPOS");
                FillCombobox(order.cbTypeSure, "TiposSeguro", "TIPOS");
                FillCombobox(order.cbTypeInsurance, "Aseguradora", "TIPOS");
                //Cuentas
                FillCombobox(order.cbTypeIdAccount, "Cheques", "TIPOS");
                FillCombobox(order.cbTypeAccountGmf, "GMF", "TIPOS");
                FillCombobox(order.cbTypeAccount, "Cuenta", "TIPOS");
                //Cheques
                FillCombobox(order.cbTypeCheckGmf, "GMF", "TIPOS");
                FillCombobox(order.cbTypeIdCheck, "Cheques", "TIPOS");
                //Subrogaciones
                FillCombobox(order.cbTypeSubroGmf, "GMF", "TIPOS");
                FillCombobox(order.cbTypeIdSubro, "Subrogacion", "TIPOS");
                //Contingencia
                FillCombobox(order.cbTypeContingency, "FormaDesembolso", "TIPOS");
                FillCombobox(order.cbGmfContingency, "GMF", "TIPOS");
                //Vendedores
                FillCombobox(order.cbTypeSeller, "Subrogacion", "TIPOS");
                //Varados
                FillComboboxStranded(order.cbStrandedPendindg, "Varados", "TIPOS");
                FillComboboxStranded(order.cbStrandedReturn, "Varados", "TIPOS");
                FillComboboxStranded(order.cbStrandedInternal, "VaradosInternos", "TIPOS");
                //Varados
                FillCombobox(order.cbTypeAutomatic, "DebitoAutomatico", "TIPOS");
                //Estados Auditoria
                FillCombobox(order.cbState, "EstadosAuditoria", "TIPOS");
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                throw exc;
            }
        }
        public string ValueConvert(string FlagValue)
        {
            if (FlagValue != "")
            {
                return string.Format("{0:C2}", decimal.Parse(FlagValue, System.Globalization.NumberStyles.Currency));
            }
            return "";
        }
        public bool ValidateTicket(string IdTicket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                idUser = model.Users.Where(x => x.NameUser == Environment.UserName).FirstOrDefault().IdUser;
                Ticket Result = model.Tickets.Where(x => x.IdTicket == IdTicket && x.Locked == false).FirstOrDefault();
                if (Result != null)
                {
                    TicketsUsers ResultTicketsUsers = model.TicketsUsers.Where(x => x.IdTicket == Result.IdTicket).FirstOrDefault();
                    if (ResultTicketsUsers == null)
                    {
                        TicketsUsers ticketsUsers = new TicketsUsers
                        {
                            IdTicket = Result.IdTicket,
                            IdUser = idUser
                        };
                        model.TicketsUsers.Add(ticketsUsers);
                        model.Entry(ticketsUsers).State = EntityState.Added;
                        Result.Locked = true;
                        model.Tickets.Add(Result);
                        model.Entry(Result).State = EntityState.Modified;
                        model.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }
        public bool ChangeTicket(string IdTicket, string State)
        {
            using (ModelOrder model = new ModelOrder())
            {
                idUser = model.Users.Where(x => x.NameUser == Environment.UserName).FirstOrDefault().IdUser;
                Ticket Result = model.Tickets.Where(x => x.IdTicket == IdTicket).FirstOrDefault();
                if (Result != null)
                {
                    TicketsUsers ticketsUsers = model.TicketsUsers.Where(x => x.IdTicket == IdTicket && x.IdUser == idUser).FirstOrDefault();
                    if (ticketsUsers != null)
                    {
                        model.Entry(ticketsUsers).State = EntityState.Deleted;
                        model.SaveChanges();
                    }
                    if (State == "Finalizado Manual") Result.ExecutionDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Result.Locked = false;
                    model.Tickets.Add(Result);
                    model.Entry(Result).State = EntityState.Modified;
                    model.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public void DeleteLocked(string IdTicket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                idUser = model.Users.Where(x => x.NameUser == Environment.UserName).FirstOrDefault().IdUser;
                TicketsUsers ticketsUsers = model.TicketsUsers.Where(x => x.IdTicket == IdTicket && x.IdUser == idUser).FirstOrDefault();
                if (ticketsUsers != null)
                {
                    Ticket Result = model.Tickets.Where(x => x.IdTicket == IdTicket).FirstOrDefault();
                    if(Result != null)
                    {
                        Result.Locked = false;
                    }
                    model.Tickets.Add(Result);
                    model.Entry(Result).State = EntityState.Modified;
                    model.Entry(ticketsUsers).State = EntityState.Deleted;
                    model.SaveChanges();
                }
            }
        }
        public Ticket ExistTicket(string IdTicket)
        {
            Ticket Result = null;
            using (ModelOrder model = new ModelOrder())
            {
                Result = model.Tickets.Where(x => x.IdTicket == IdTicket).FirstOrDefault();
            }
            return Result;
        }
        public void DeleteTicket(string IdTicket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                Ticket Result = model.Tickets.Where(x => x.IdTicket == IdTicket).FirstOrDefault();
                model.Entry(Result).State = EntityState.Deleted;
                model.SaveChanges();
            }
        }
    }
}
