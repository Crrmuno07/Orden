using log4net;
using Orden.Helpers;
using Orden.Model;
using Orden.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wipro.Utils;
using Microsoft.Win32;
namespace Orden.Views
{
    public partial class Order : Window
    {
        #region Variables
        /// Declaracón de variables
        /// <summary>
        /// Log de error de la aplicación: \bin\Debug\MiAsistenteEnProcesos_LogdeErrores.log cuando no esya instalada
        /// //**********************************************************************************************************
        /// Log de error de la aplicación: \bin\Debug\MiAsistenteEnProcesos_LogdeErrores.log cuando esta instalada
        /// C:\Users\UserName\AppData\Local\Apps\2.0\*
        /// </summary
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<string> ltsManagement, ltsPending, ltsReturn, ltsManagementM, ltsPendingM, ltsReturnM;
        public int IndexCheck, IndexAccount, IndexDebitAccount, IndexInsurance, IndexSeller, IndexParticipant, IndexSubroga;
        public int IdParticipant, Idinsurances, IdbankCreditAccounts, IdbankChecks, Idsubrogations, Iddisbursement, Idseller, IdbankDebitAccounts;

        private readonly PropertyInformation_ViewModel propertyInformation_ViewModel = new PropertyInformation_ViewModel();
        private readonly GeneralInformation_ViewModel generalInformation_ViewModel = new GeneralInformation_ViewModel();
        private readonly BankCreditAccount_ViewModel bankCreditAccount_ViewModel = new BankCreditAccount_ViewModel();
        private readonly ValidationFrech_ViewModel validationFrech_ViewModel = new ValidationFrech_ViewModel();
        private readonly DiscountConcept_ViewModel discountConcept_ViewModel = new DiscountConcept_ViewModel();
        private readonly Disbursement_ViewModel disbursement_ViewModel = new Disbursement_ViewModel();
        private readonly AuditProcess_ViewModel auditProcess_ViewModel = new AuditProcess_ViewModel();
        private readonly Subrogation_ViewModel subrogation_ViewModel = new Subrogation_ViewModel();
        private readonly Participant_ViewModel participant_ViewModel = new Participant_ViewModel();
        private readonly FinacleData_ViewModel finacleData_ViewModel = new FinacleData_ViewModel();
        private readonly AuditTicket_ViewModel auditTicket_ViewModel = new AuditTicket_ViewModel();
        private readonly BanckDebit_ViewModel banckDebit_ViewModel = new BanckDebit_ViewModel();
        private readonly Insurance_ViewModel insurance_ViewModel = new Insurance_ViewModel();
        private readonly BankCheck_ViewModel bankCheck_ViewModel = new BankCheck_ViewModel();
        private readonly Stranded_ViewModel stranded_ViewModel = new Stranded_ViewModel();
        private readonly Ticket_ViewModel ticket_ViewModel = new Ticket_ViewModel();
        private readonly Seller_ViewModel seller_ViewModel = new Seller_ViewModel();
        private readonly CommonFunctions common = new CommonFunctions();
        private GeneralInformation general = new GeneralInformation();
        private TransactionLog transactionLog = new TransactionLog();

        public List<BankCreditAccount> ltsRemovebankCreditAccounts, ltsbankCreditAccounts;
        public List<BankDebitAccount> ltsRemovebankDebitAccounts, ltsbankDebitAccounts;
        public List<Disbursement> ltsRemovedisbursement, ltsdisbursement;
        public List<Subrogation> ltsRemovesubrogations, ltssubrogations;
        public List<Participant> ltsRemoveParticipants, ltsParticipants;
        public List<Insurance> ltsRemoveinsurances, ltsinsurances;
        public List<BankCheck> ltsRemovebankChecks, ltsbankChecks;
        public List<Seller> ltsRemoveseller, ltsseller;
        public DateTime StartDate;
        private Ticket ticketFind;
        private Ticket AllticketFind;

        public string delivery;
        public string FindStates = "";
        public string Modality = "";
        public string IdTicket;
        public string message;
        public string region;
        public string name = "";
        public string msg = "";
        public string City = "";
        public string IdTransaction = "";
        public bool Exists = true;
        public bool Trc = false;
        public bool blnFlag = false;
        public bool blnInsert = false;
        public int StartState;
        #endregion

        #region Funciones
        public Order()
        {
            InitializeComponent();
        }

        /// <summary>
        /// LIMPIA LA ORDEN
        /// </summary>
        public void Clear()
        {
            cbFrechType.ItemsSource = null;
            ltsPending = new List<string>();
            ltsReturn = new List<string>();
            ltsManagement = new List<string>();
            ltsPendingM = new List<string>();
            ltsReturnM = new List<string>();
            ltsManagementM = new List<string>();

            IndexCheck = IndexAccount = IndexDebitAccount = IndexInsurance = IndexSeller = IndexParticipant = IndexSubroga = -1;
            IdParticipant = Idinsurances = IdbankCreditAccounts = IdbankChecks = Idsubrogations = Iddisbursement = Idseller = IdbankDebitAccounts = 0;

            ltsRemoveParticipants = new List<Participant>();
            ltsRemoveinsurances = new List<Insurance>();
            ltsRemovebankCreditAccounts = new List<BankCreditAccount>();
            ltsRemovebankChecks = new List<BankCheck>();
            ltsRemovesubrogations = new List<Subrogation>();
            ltsRemovedisbursement = new List<Disbursement>();
            ltsRemoveseller = new List<Seller>();
            ltsRemovebankDebitAccounts = new List<BankDebitAccount>();

            ltsParticipants = new List<Participant>();
            ltsinsurances = new List<Insurance>();
            ltsbankCreditAccounts = new List<BankCreditAccount>();
            ltsbankChecks = new List<BankCheck>();
            ltssubrogations = new List<Subrogation>();
            ltsdisbursement = new List<Disbursement>();
            ltsseller = new List<Seller>();
            ltsbankDebitAccounts = new List<BankDebitAccount>();

            common.GenericCleaner(gridFrech, true);
            common.GenericCleaner(gridGeneral, true);
            common.GenericCleaner(gridProperty, true);
            common.GenericCleaner(gridParticipants, true);
            common.GenericCleaner(gridInsuranceTwo, true);
            common.GenericCleaner(gridAccount, true);
            common.GenericCleaner(gridCheck, true);
            common.GenericCleaner(gridSubro, true);
            common.GenericCleaner(gridSeller, true);
            common.GenericCleaner(gridContingency, true);
            common.GenericCleaner(gridManagement, true);
            common.GenericCleaner(gridStranded, true);
            common.GenericCleaner(gridAudit, true);
            common.GenericCleaner(gridAutomatic, true);
            common.GenericCleaner(gridHome, true);

            txtFinancing.Foreground = Brushes.Black;
            gridCheck.DataContext = new BankCheck();
            gridSubro.DataContext = new Subrogation();
            gridSeller.DataContext = new Seller();
            gridAccount.DataContext = new BankCreditAccount();
            gridContingency.DataContext = new Disbursement();
            gridInsuranceTwo.DataContext = new Insurance();
            gridInsuranceOne.DataContext = new DiscountConcept();
            gridFinacle.DataContext = new FinacleData();
            gridFrech.DataContext = new ValidationFrech();
            gridGeneral.DataContext = new GeneralInformation();
            gridInsuranceOne.DataContext = new DiscountConcept();
            gridProperty.DataContext = new PropertyInformation();
            gridAudit.DataContext = new AuditProcess();
            gridAutomatic.DataContext = new BankDebitAccount();

            common.ViewDataGrid(datagridStrandedP);
            common.ViewDataGrid(datagridStrandedD);
            common.ViewDataGrid(datagridStrandedG);

            cbCaseReversed.Text = "NO";
            txtSubsidy.Text = "NO";
            txtObservationOrder.Text = "";
            txtCase.IsReadOnly = false;
        }
        private void CleanSheet_Click(object sender, RoutedEventArgs e)
        {
            if (txtCase.Text.Trim() != "")
            {
                common.DeleteLocked(txtCase.Text.Trim());
                //Reporte en nazar exitoso
                transactionLog.WriteTransactionLog("EUCDCH01", "53", "13", IdTransaction, "1", "1180");
            }
            Clear();
        }

        /// <summary>
        /// CARGA LA INFORMACIÓN EN LA ORDEN
        /// </summary>
        public void Find()
        {
            try
            {
                region = "";
                StartDate = DateTime.Now;
                IdTicket = txtCase.Text.Trim();
                if (common.ExistTicket(IdTicket).IdState == 1)
                {
                    MessageBox.Show("El ticket ingresado no puede ser visualizado porque aun no ha sido atendido por ANALISIS", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                    common.DeleteLocked(txtCase.Text.Trim());
                    return;
                }
                ticketFind = ticket_ViewModel.Ticket(IdTicket, FindStates);
                if (ticketFind != null)
                {
                    region = ticketFind.Region;
                    GeneralInformation generalFind = generalInformation_ViewModel.GeneralInformation(IdTicket);
                    if (generalFind != null)
                    {
                        txtCase.IsReadOnly = true;
                        if (ticketFind.IdState == common.IdState("Analizado"))
                        {
                            try
                            {
                                IdTransaction = transactionLog.CreateTransactionLog("EUCDCH01", Environment.UserName, Environment.MachineName, "1180", "https://rpa-autoanywhere.apps.bancolombia.corp");
                            }
                            catch (Exception exc)
                            {
                                Log.Fatal(exc.Message, exc);
                            }
                        }
                        StartState = ticketFind.IdState;
                        gridFinacle.DataContext = null;
                        gridGeneral.DataContext = null;
                        gridFrech.DataContext = null;
                        gridProperty.DataContext = null;
                        gridInsuranceTwo.DataContext = null;
                        gridAccount.DataContext = null;
                        gridCheck.DataContext = null;
                        gridSubro.DataContext = null;
                        gridAutomatic.DataContext = null;

                        Modality = generalFind.Modality;
                        txtObservation.Text = ticketFind.CausesObservations;
                        txtObservationOrder.Text = ticketFind.GeneralObservations;
                        cbCaseReversed.Text = ticketFind.Reversed == true ? "SI" : "NO";
                        generalInformation_ViewModel.RuleScheme(cbScheme, Modality, IdTicket, false);

                        gridFinacle.DataContext = (finacleData_ViewModel.FinacleData(IdTicket) is null) ? new FinacleData() : finacleData_ViewModel.FinacleData(IdTicket);
                        gridGeneral.DataContext = (generalFind is null) ? new GeneralInformation() : generalFind;
                        txtBuilder.Text = generalFind.Builder;
                        txtProyectBuilder.Text = generalFind.Project;
                        delivery = txtDelivery.Text = generalFind.Delivery.ToString();
                        txtValueDelivery.Text = generalFind.DeliveryValue.ToString();
                        cbBRP.Text = generalFind.Brp;
                        ValidationFrech validation = validationFrech_ViewModel.ValidationFrech(IdTicket);
                        gridFrech.DataContext = (validationFrech_ViewModel.ValidationFrech(IdTicket) is null) ? new ValidationFrech() : validation;
                        gridProperty.DataContext = (propertyInformation_ViewModel.PropertyInformation(IdTicket) is null) ? new PropertyInformation() : propertyInformation_ViewModel.PropertyInformation(IdTicket);
                        gridAudit.DataContext = (auditProcess_ViewModel.AuditProcess(IdTicket) is null) ? new AuditProcess() : auditProcess_ViewModel.AuditProcess(IdTicket);
                        gridInsuranceOne.DataContext = (discountConcept_ViewModel.DiscountConcept(IdTicket) is null) ? new DiscountConcept() : discountConcept_ViewModel.DiscountConcept(IdTicket);
                        City = propertyInformation_ViewModel.PropertyInformation(IdTicket).City.ToString();
                        if (City != "")
                        {
                            common.FillComboboxDptoCity(cbCity, cbDeparment.Text.ToUpper(), City);
                        }

                        ltsPending = stranded_ViewModel.CausesStranding(IdTicket, "Pendiente", "Varado");
                        common.Check_ViewModel(datagridStrandedP, ltsPending);
                        ltsReturn = stranded_ViewModel.CausesStranding(IdTicket, "Devolución", "Varado");
                        common.Check_ViewModel(datagridStrandedD, ltsReturn);
                        ltsManagement = stranded_ViewModel.CausesStranding(IdTicket, "GestionInterna", "Varado");
                        common.Check_ViewModel(datagridStrandedG, ltsManagement);

                        ltsPendingM = stranded_ViewModel.CausesStranding(IdTicket, "Pendiente", "Gestionado");
                        common.Check_ViewModel(datagridManagementP, ltsPendingM);
                        ltsReturnM = stranded_ViewModel.CausesStranding(IdTicket, "Devolución", "Gestionado");
                        common.Check_ViewModel(datagridManagementD, ltsReturnM);
                        ltsManagementM = stranded_ViewModel.CausesStranding(IdTicket, "GestionInterna", "Gestionado");
                        common.Check_ViewModel(datagridManagementG, ltsManagementM);

                        ltsParticipants = participant_ViewModel.Participants(IdTicket).ToList();
                        datagridParticipant.ItemsSource = ltsParticipants;
                        ltsinsurances = insurance_ViewModel.Insurance(IdTicket).ToList();
                        datagridInsurance.ItemsSource = ltsinsurances;
                        ltsbankCreditAccounts = bankCreditAccount_ViewModel.BankCreditAccount(IdTicket).ToList();
                        datagridAccount.ItemsSource = ltsbankCreditAccounts;
                        ltsbankChecks = bankCheck_ViewModel.BankCheck(IdTicket).ToList();
                        datagridCheck.ItemsSource = ltsbankChecks;
                        ltssubrogations = subrogation_ViewModel.Subrogation(IdTicket).ToList();
                        datagridSubro.ItemsSource = ltssubrogations;
                        ltsdisbursement = disbursement_ViewModel.Disbursements(IdTicket).ToList();
                        datagridContingency.ItemsSource = ltsdisbursement;
                        ltsseller = seller_ViewModel.Sellers(IdTicket).ToList();
                        datagridSeller.ItemsSource = ltsseller;
                        ltsbankDebitAccounts = banckDebit_ViewModel.BankDebitAccounts(IdTicket).ToList();
                        datagridAutomatic.ItemsSource = ltsbankDebitAccounts;

                        generalInformation_ViewModel.RuleFinancing(txtValueApprove.Text, txtAppraisalValue.Text, txtFinancing, gridGeneral);
                        RuleModality();

                        if (txtVtoApprove.Text == "VENCIDO") { dpDateRevived.IsEnabled = true; }
                        if (txtModality.Text == "DIFVI")
                        {
                            message = generalInformation_ViewModel.RuleRate(cbCoin.Text, txtModality.Text, txtRate.Text);
                            if (message != "")
                            {
                                MessageBox.Show(message);
                                txtRate.Text = "";
                            }
                        }
                        RuleFrechOld();
                        cbFrechType.Text = validation != null ? validation.TypeFrech : "";
                        RuleMCYMiCasaYa();
                        common.ViewDataGrid(datagridInsurance);
                        common.ViewDataGrid(datagridAccount);
                        common.ViewDataGrid(datagridCheck);
                        common.ViewDataGrid(datagridSubro);
                        common.ViewDataGrid(datagridContingency);
                        common.ViewDataGrid(datagridSeller);
                        common.ViewDataGrid(datagridStrandedD);
                        common.ViewDataGrid(datagridStrandedP);
                        common.ViewDataGrid(datagridStrandedG);
                        common.ViewDataGrid(datagridAutomatic);
                    }
                    else
                    {
                        MessageBox.Show("El ticket ingresado no tiene la información necesaria porfavor diligencielo todo manualmente", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                        common.DeleteTicket(txtCase.Text.Trim());
                        txtCase.Text = "";
                    }
                }
                else
                {
                    common.DeleteLocked(txtCase.Text.Trim());
                    MessageBox.Show("El ticket ingresado no puede ser visualizado en esta orden de desembolso", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
        private void FindCase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (common.ExistTicket(txtCase.Text.Trim()) != null)
                {
                    common.DeleteLocked(txtCase.Text.Trim());
                    if (common.ValidateTicket(txtCase.Text.Trim()) == true)
                    {
                        Loading loading = new Loading();
                        loading.lbInformation.Content = "Buscando.....";
                        Thread.Sleep(300);
                        loading.Show();
                        Thread thread = new Thread(() =>
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                Find();
                                loading.Close();
                            }));
                        });
                        thread.Start();
                    }
                    else
                    {
                        string NameUser = "";
                        using (ModelOrder model = new ModelOrder())
                        {
                            NameUser = (from a in model.TicketsUsers
                                        join b in model.Users on a.IdUser equals b.IdUser
                                        where a.IdTicket == txtCase.Text.Trim()
                                        select b.NameUser).FirstOrDefault();
                        }
                        MessageBox.Show("El ticket que trata de consultar se encuentra bloqueado en base de datos, puede estar siendo trabajado por otro usuario: " + NameUser + "", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("El ticket: " + txtCase.Text + " no se encuentra en base de datos", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtCase.Text = "";
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// VALIDACIONES ANTES DE GUARDAR LA INFORMACIÓN EN LA BASE DE DATOS
        /// </summary>
        public bool ValidationsSave()
        {
            try
            {
                string result = "";
                StringBuilder CompletResult = new StringBuilder();
                txtDelivery.Text = txtDelivery.Text == "" ? "0" : txtDelivery.Text;
                switch (cbCaseReversed.Text)
                {
                    case "SI":
                        IdTicket = txtCase.Text.Trim().Replace("_R", "") + "_R";
                        break;
                    default:
                        if (int.Parse(txtDelivery.Text) > 1)
                        {
                            string Id = "_" + txtDelivery.Text;
                            if (txtCase.Text.Trim().ToArray().Where(x => x.Equals('_')).Count() == 3)
                            {
                                IdTicket = IdTicket.Substring(0, IdTicket.LastIndexOf("_", IdTicket.Length - 1));
                            }
                            IdTicket += Id;
                        }
                        else
                        {
                            IdTicket = txtCase.Text.Trim();
                        }
                        break;
                }
                ticketFind = ticket_ViewModel.Ticket(IdTicket, FindStates);
                if (!(ticketFind is null) && txtCase.Text.Trim() != IdTicket)
                {
                    result = "El ticket ingresado, ya tiene un registro duplicado, compruebe buscandolo en la base de datos como reversado y/o otras entregas ejemplo: " + IdTicket;
                    CompletResult.AppendFormat("- {0}\n", result);
                }
                if (cbStateOrder.Text != "Pendiente Auxiliar" && cbStateOrder.Text != "Wait Analisis")
                {
                    general = (GeneralInformation)gridGeneral.DataContext;
                    general.Brp = cbBRP.Text;
                    general.Delivery = int.Parse(txtDelivery.Text);
                    general.DeliveryValue = txtValueDelivery.Text != "" ? decimal.Parse(txtValueDelivery.Text, System.Globalization.NumberStyles.Currency) : 0;
                    ValidationFrech validation = (ValidationFrech)gridFrech.DataContext;
                    validation.Income = txtIncome.Text != "" ? decimal.Parse(txtIncome.Text, System.Globalization.NumberStyles.Currency) : 0;

                    CompletResult = common.ModelValidator(general, CompletResult);
                    CompletResult = common.ModelValidator((ValidationFrech)gridFrech.DataContext, CompletResult);
                    CompletResult = common.ModelValidator((PropertyInformation)gridProperty.DataContext, CompletResult);
                    CompletResult = common.ModelValidator((FinacleData)gridFinacle.DataContext, CompletResult);
                    CompletResult = common.ModelValidator((AuditProcess)gridAudit.DataContext, CompletResult);

                    result = Points();
                    if (result != "") CompletResult.AppendFormat("- {0}\n", result);
                    result = TRC();
                    if (result != "") CompletResult.AppendFormat("- {0}\n", result);
                    if (cbFrechType.Text == "")
                    {
                        result = "Debe seleccionar el tipo del FRECH";
                        CompletResult.AppendFormat("- {0}\n", result);
                    }
                    if (ltssubrogations.Count > 0 && txtProject.Text == "")
                    {
                        result = "El campo project Id no puede quedar vacio ya que se tiene información en la subrogación";
                        CompletResult.AppendFormat("- {0}\n", result);
                    }
                    if (txtVtoApprove.Text == "VENCIDO" || txtVtoApprove.Text == "")
                    {
                        result = "El ticket no permite ser guardado porque el campo Vto.Aprobación esta vació o vencido";
                        CompletResult.AppendFormat("- {0}\n", result);
                    }
                    if (cbStateOrder.Text == "Predesembolso" || cbStateOrder.Text == "PreDesembolso Manual" || cbStateOrder.Text == "PosDesembolso Manual" || cbStateOrder.Text == "Posdesembolso" || cbStateOrder.Text == "Desembolso" || cbStateOrder.Text == "Finalizado" || cbStateOrder.Text == "Finalizado Manual" || cbStateOrder.Text == "Pendiente Verificar")
                    {
                        if (cbState.Text == "VARADO" || cbState.Text == "DESVARADO PARCIAL")
                        {
                            result = "Para el estado: " + cbStateOrder.Text + " la orden no se puede guardar con el estado de auditoria: " + cbState.Text + "";
                            CompletResult.AppendFormat("- {0}\n", result);
                        }
                        if (cbState.Text == "DESVARADO TOTAL" || cbState.Text == "COMPLETADO EXITOSO")
                        {
                            if (datagridStrandedP.Items.Count >= 1 || datagridStrandedG.Items.Count >= 1 || datagridStrandedD.Items.Count >= 1)
                            {
                                result = "Verifique la sección de varados, recuerde que el ticket debe pasar sin pendientes";
                                CompletResult.AppendFormat("- {0}\n", result);
                            }
                        }
                        if (cbState.Text == "DESVARADO TOTAL" && (txtUserTotal.Text == "" || dpDateTotal.Text == ""))
                        {
                            result = "Los campos referentes a Usuario Desvarado Total y Fecha Desvarado Total deben estar diligenciados ";
                            CompletResult.AppendFormat("- {0}\n", result);
                        }
                        if (cbState.Text == "COMPLETADO EXITOSO")
                        {
                            if (datagridManagementP.Items.Count >= 1 || datagridManagementG.Items.Count >= 1 || datagridManagementD.Items.Count >= 1)
                            {
                                result = "Verifique la sección de varados, recuerde que se gestionaron pendientes para este ticket por tal motivo su estado debe ser DESVARADO TOTAL";
                                CompletResult.AppendFormat("- {0}\n", result);
                            }
                        }
                        if (txtUserAnalysis.Text == "" || dpDateAnalysis.Text == "")
                        {
                            result = "Los campos referentes a Fecha Analisis Boleta y Usuario Analisis deben estar diligenciados ";
                            CompletResult.AppendFormat("- {0}\n", result);
                        }
                    }
                    if (cbStateOrder.Text == "Pendiente Auxiliar" && cbState.Text == "DESVARADO PARCIAL" && (txtUserPartial.Text == "" || dpDatePartial.Text == ""))
                    {
                        result = "Los campos referentes a Usuario Desvarado Parcial y Fecha Desvarado Parcial deben estar diligenciados para dejar el ticket en varado ";
                        CompletResult.AppendFormat("- {0}\n", result);
                    }
                    if (cbStateOrder.Text == "Pendiente Auxiliar" && cbState.Text == "VARADO" && (txtUserAnalysis.Text == "" || dpDateAnalysis.Text == ""))
                    {
                        result = "Los campos referentes a Fecha Analisis Boleta y Usuario Analisis deben estar diligenciados ";
                        CompletResult.AppendFormat("- {0}\n", result);
                    }
                    if (cbStateOrder.Text == "Pendiente Auxiliar" && cbState.Text == "DESVARADO TOTAL" && (txtUserTotal.Text == "" || dpDateTotal.Text == ""))
                    {
                        result = "Los campos referentes a Usuario Desvarado Total y Usuario Desvarado Total deben estar diligenciados ";
                        CompletResult.AppendFormat("- {0}\n", result);
                    }
                    foreach (var item in ltsinsurances)
                    {
                        if (ltsParticipants.Where(X => X.IdentificationNumber == item.IdentificationNumber).FirstOrDefault() == null)
                        {
                            result = "La cedula de los seguro no se encuentra en los participantes relacionados en el caso";
                            CompletResult.AppendFormat("- {0}\n", result);
                        }
                    }
                    if (ltsbankChecks.Where(x => x.Gmf == "" || x.Gmf == null).Count() > 0 || ltsbankCreditAccounts.Where(x => x.Gmf == "" || x.Gmf == null).Count() > 0 || ltssubrogations.Where(x => x.Gmf == "" || x.Gmf == null).Count() > 0)
                    {
                        CompletResult.AppendFormat("- {0}\n", "Todos los GMF deben estar seleccionados");
                    }
                    result = banckDebit_ViewModel.ValidateSum(ltsbankDebitAccounts);
                    if (result != "") CompletResult.AppendFormat("- {0}\n", result);
                    result = participant_ViewModel.Save(IdTicket, ltsParticipants, true, false);
                    if (result != "") CompletResult.AppendFormat("- {0}\n", result);
                    result = insurance_ViewModel.Save(IdTicket, ltsinsurances, true, false, cbScheme.Text);
                    if (result != "") CompletResult.AppendFormat("- {0}\n", result);
                    result = bankCheck_ViewModel.Save(IdTicket, ltsbankChecks, true, false);
                    if (result != "") CompletResult.AppendFormat("- {0}\n", result);
                    if (common.OfficeActive(txtSolId.Text) == false) { CompletResult.AppendFormat("- {0}\n", "La oficina ingresada no se encuentra activa"); }
                    if (CompletResult.ToString() != "")
                    {
                        MessageBox.Show(CompletResult.ToString(), "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                }
                else
                {
                    CompletResult = common.ModelValidator((AuditProcess)gridAudit.DataContext, CompletResult);
                    if (cbStateOrder.Text == "Pendiente Auxiliar" && cbState.Text == "DESVARADO PARCIAL" && (txtUserPartial.Text == "" || dpDatePartial.Text == ""))
                    {
                        result = "Los campos referentes a Usuario Descarado Parcial y Fecha Desvarado Parcial deben estar diligenciados para dejar el ticket en varado ";
                    }
                    if (cbStateOrder.Text == "Pendiente Auxiliar" && cbState.Text == "VARADO" && (txtUserAnalysis.Text == "" || dpDateAnalysis.Text == ""))
                    {
                        result = "Los campos referentes a Fecha Analisis Boleta y Usuario Analisis deben estar diligenciados ";
                    }
                    if (cbStateOrder.Text == "Pendiente Auxiliar" && cbState.Text == "DESVARADO TOTAL" && (txtUserTotal.Text == "" || dpDateTotal.Text == ""))
                    {
                        result = "Los campos referentes a Usuario Desvarado Total y Usuario Desvarado Total deben estar diligenciados ";
                    }
                    if (result != "") CompletResult.AppendFormat("- {0}\n", result);
                    if (CompletResult.ToString() != "")
                    {
                        MessageBox.Show(CompletResult.ToString(), "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
            return true;
        }
        public void SaveGeneralInformation()
        {
            try
            {
                AllticketFind = ticket_ViewModel.FindTicket(IdTicket);
                ticketFind = ticket_ViewModel.Ticket(IdTicket, FindStates);
                if (AllticketFind != null && ticketFind == null)
                {
                    MessageBox.Show("El ticket que esta ingresando no fue buscado previamente y este se encuentra registrado en base de datos", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (AllticketFind == null && !name.Contains("Análisis"))
                {
                    MessageBox.Show("Para ingresar un ticket que no este registrado en base de datos se debe hacer desde la orden de analisis", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (cbStateOrder.Text != "")
                {
                    if (region == "" || region == null)
                    {
                        region = cbSegment.Text == "PREFERENCIAL" ? "ESPECIALIZADA" : "OTRA";
                    }
                    if (ticketFind is null)
                    {
                        StartDate = DateTime.Now;
                        StartState = 1;
                        Ticket ticketNew = new Ticket
                        {
                            IdTicket = IdTicket,
                            IdState = common.IdState(cbStateOrder.Text),
                            Region = region,
                            CreationDate = DateTime.Now,
                            Reversed = cbCaseReversed.Text == "SI",
                            CausesObservations = txtObservation.Text,
                            GeneralObservations = "Ticket Ingresado Manualmente desde la Orden " + txtObservationOrder.Text,
                            Priority = false,
                            Locked = false
                        };
                        ticket_ViewModel.Add(ticketNew);
                        Exists = false;
                    }
                    else
                    {
                        ticketFind.CausesObservations = txtObservation.Text;
                        ticketFind.GeneralObservations = txtObservationOrder.Text;
                        ticketFind.Reversed = false;
                        ticketFind.IdState = common.IdState(cbStateOrder.Text);
                        ticket_ViewModel.Update(ticketFind);
                        Exists = true;
                    }

                    participant_ViewModel.Delete(IdTicket, ltsRemoveParticipants);
                    participant_ViewModel.Save(IdTicket, ltsParticipants, false, Exists);

                    generalInformation_ViewModel.Delete(IdTicket);
                    GeneralInformation generalInformation = (GeneralInformation)gridGeneral.DataContext;
                    generalInformation.Brp = cbBRP.Text;
                    generalInformation.IdTicket = IdTicket;
                    generalInformation.Builder = txtBuilder.Text;
                    generalInformation.Project = txtProyectBuilder.Text;
                    generalInformation.Delivery = txtDelivery.Text != "" ? int.Parse(txtDelivery.Text) : 0;
                    generalInformation.DeliveryValue = txtValueDelivery.Text != "" ? decimal.Parse(txtValueDelivery.Text, System.Globalization.NumberStyles.Currency) : 0;
                    generalInformation_ViewModel.Add(generalInformation);

                    validationFrech_ViewModel.Delete(IdTicket);
                    ValidationFrech validationFrech = (ValidationFrech)gridFrech.DataContext;
                    validationFrech.IdTicket = IdTicket;
                    validationFrech.points = txtPoints.Text != "" ? decimal.Parse(txtPoints.Text) : 0;
                    validationFrech.LivingPlace = cbPlace.Text;
                    validationFrech.TypeFrech = cbFrechType.Text;
                    validationFrech_ViewModel.Add(validationFrech);

                    propertyInformation_ViewModel.Delete(IdTicket);
                    PropertyInformation property = (PropertyInformation)gridProperty.DataContext;
                    property.IdTicket = IdTicket;
                    propertyInformation_ViewModel.Add(property);

                    finacleData_ViewModel.Delete(IdTicket);
                    FinacleData finacleData = (FinacleData)gridFinacle.DataContext;
                    finacleData.IdTicket = IdTicket;
                    finacleData_ViewModel.Add(finacleData);

                    discountConcept_ViewModel.Delete(IdTicket);
                    DiscountConcept discountConcept = (DiscountConcept)gridInsuranceOne.DataContext;
                    discountConcept.IdTicket = IdTicket;
                    discountConcept_ViewModel.Add(discountConcept);

                    insurance_ViewModel.Delete(IdTicket, ltsRemoveinsurances);
                    insurance_ViewModel.Save(IdTicket, ltsinsurances, false, Exists, cbScheme.Text);

                    bankCreditAccount_ViewModel.Delete(IdTicket, ltsRemovebankCreditAccounts);
                    bankCreditAccount_ViewModel.Save(IdTicket, ltsbankCreditAccounts, false, Exists);

                    bankCheck_ViewModel.Delete(IdTicket, ltsRemovebankChecks);
                    bankCheck_ViewModel.Save(IdTicket, ltsbankChecks, false, Exists);

                    subrogation_ViewModel.Delete(IdTicket, ltsRemovesubrogations);
                    subrogation_ViewModel.Save(IdTicket, ltssubrogations, false, Exists);

                    disbursement_ViewModel.Delete(IdTicket, ltsRemovedisbursement);
                    disbursement_ViewModel.Save(IdTicket, ltsdisbursement, false, Exists);

                    seller_ViewModel.Delete(IdTicket, ltsRemoveseller);
                    seller_ViewModel.Save(IdTicket, ltsseller, false, Exists);

                    banckDebit_ViewModel.Delete(IdTicket, ltsRemovebankDebitAccounts);
                    banckDebit_ViewModel.Save(IdTicket, ltsbankDebitAccounts, false, Exists);

                    stranded_ViewModel.Delete(IdTicket);
                    stranded_ViewModel.Save(IdTicket, false, "Pendiente", "Varado", datagridStrandedP);
                    stranded_ViewModel.Save(IdTicket, false, "GestionInterna", "Varado", datagridStrandedG);
                    stranded_ViewModel.Save(IdTicket, false, "Devolución", "Varado", datagridStrandedD);

                    stranded_ViewModel.Save(IdTicket, false, "Pendiente", "Gestionado", datagridManagementP);
                    stranded_ViewModel.Save(IdTicket, false, "GestionInterna", "Gestionado", datagridManagementG);
                    stranded_ViewModel.Save(IdTicket, false, "Devolución", "Gestionado", datagridManagementD);

                    AuditTicket auditTicket = new AuditTicket()
                    {
                        IdTicket = txtCase.Text.Trim(),
                        IdUser = auditTicket_ViewModel.IdUser(),
                        StartDate = Convert.ToDateTime(StartDate.ToString("yyyy-MM-dd HH:mm:ss")),
                        EndDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                        StartState = StartState,
                        EndState = common.IdState(cbStateOrder.Text),
                        Description = "Caso llamada desde la orden de desembolso "
                    };
                    auditTicket_ViewModel.Add(auditTicket);

                    auditProcess_ViewModel.Delete(IdTicket);
                    AuditProcess audit = (AuditProcess)gridAudit.DataContext;
                    audit.IdTicket = IdTicket;
                    auditProcess_ViewModel.Add(audit);

                    common.ChangeTicket(txtCase.Text.Trim(), cbStateOrder.Text);
                    //Reporte en nazar exitoso
                    transactionLog.WriteTransactionLog("EUCDCH01", "2", "13", IdTransaction, "1", "1180");
                    txtCase.IsReadOnly = false;
                    Clear();
                }
                else
                {
                    MessageBox.Show("Para guardar el ticket debe seleccionar el estado en el que desea guardarlo", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception exc)
            {
                //Reporte en nazar Error
                transactionLog.WriteTransactionLog("EUCDCH01", "5", "1", IdTransaction, "1", "1180");
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            IdTicket = txtCase.Text.Trim();
            if (IdTicket != "")
            {
                if (participant_ViewModel.GetTitular(IdTicket) != 0 || ltsParticipants.Where(X => X.TypeParticipant == "TITULAR").Count() != 0)
                {
                    if (ValidationsSave() == true)
                    {
                        Loading loading = new Loading();
                        loading.lbInformation.Content = "Guardando.....";
                        Thread.Sleep(300);
                        loading.Show();
                        Thread thread = new Thread(() =>
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                SaveGeneralInformation();
                                loading.Close();
                            }));
                        });
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }
                }
                else
                {
                    MessageBox.Show("El ticket debe tener un titular asociado", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Ingrese el ticket que desea buscar o ingresar en base de datos", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// CALCULAR LA TASA POLITICA FINAL
        /// </summary>
        /// <param name="Agreement" "VALOR DEL CONVENIO></param>
        /// <param name="Attributions" "VALOR DE LAS ATRIBUCIONES></param>
        /// <param name="Property" "VALOR DE CONSERVACION DEL INMUEBLE></param>
        /// <param name="Package" "VALOR DE LOS PAQUETES></param>
        private void CalculateRate(decimal Property, string NameAgr, string NameAttri, string Inmueble, string NamePack)
        {
            try
            {
                if (txtRate.Text != "")
                {
                    decimal Agreement, Attributions, Package, sum;
                    if (cbEmployee.Text == "SI")
                    {
                        RateEmployee();
                    }
                    else
                    {
                        txtTotalRate.Text = txtRate.Text;
                        if (common.PorcentageRate(NameAgr, NameAttri, NamePack, Inmueble, "Convenio") != 0 || common.PorcentageRate(NameAgr, NameAttri, NamePack, Inmueble, "Atribuciones") != 0 || common.PorcentageRate(NameAgr, NameAttri, NamePack, Inmueble, "Paquete") != 0)
                        {
                            Agreement = common.PorcentageRate(NameAgr, NameAttri, NamePack, Inmueble, "Convenio");
                            txtAgreenmentValue.Text = Agreement.ToString();
                            Attributions = common.PorcentageRate(NameAgr, NameAttri, NamePack, Inmueble, "Atribuciones");
                            txtAttributionsValue.Text = Attributions.ToString();
                            Package = common.PorcentageRate(NameAgr, NameAttri, NamePack, Inmueble, "Paquete");
                            txtPackegeValue2.Text = Package.ToString();
                            sum = Attributions + Agreement + Package + Property;
                            if (sum >= decimal.Parse("0.65"))
                            {
                                Property = common.PorcentageRate(NameAgr, NameAttri, NamePack, Inmueble, "Conservacion");
                                txtPackageValue.Text = Property.ToString();
                            }
                        }
                        else
                        {
                            txtAgreenmentValue.Text = discountConcept_ViewModel.AgreementPoints(cbAgreement.Text);
                            Agreement = decimal.Parse(txtAgreenmentValue.Text);
                            txtAttributionsValue.Text = discountConcept_ViewModel.AttributionsPercentage(cbAttributions.Text);
                            Attributions = decimal.Parse(txtAttributionsValue.Text);
                            txtPackegeValue2.Text = discountConcept_ViewModel.PackagePercentage(cbPackage.Text);
                            Package = decimal.Parse(txtPackegeValue2.Text);
                        }
                        txtTotalRate.Text = (decimal.Parse(txtTotalRate.Text) - Property - Agreement - Attributions - Package).ToString();
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }

        }
        public void RateEmployee()
        {
            try
            {
                if (txtRate.Text != "")
                {
                    txtTotalRate.Text = txtRate.Text;
                    if (cbEmployee.Text == "SI")
                    {
                        txtAgreenmentValue.Text = "0";
                        txtAttributionsValue.Text = "0";
                        txtPackageValue.Text = "0";
                        txtPackegeValue2.Text = "0";
                        common.FillCombobox(cbPackage, "Paquete", "TIPOS");
                        common.FillCombobox(cbAgreement, "Convenio", "TIPOS");
                        common.FillCombobox(cbAttributions, "Atribuciones", "TIPOS");
                        common.FillCombobox(cbProperty, "Inmueble", "TIPOS");
                        txtTotalRate.Text = discountConcept_ViewModel.EmployeePercentage(cbCoin.Text, txtModality.Text);
                        if (txtRate.Text != "" && txtRate.Text != "0" && txtTotalRate.Text != "" && txtTotalRate.Text != "0")
                        {
                            txtEmployeeValue.Text = (decimal.Parse(txtRate.Text) - decimal.Parse(txtTotalRate.Text)).ToString();
                        }
                    }
                    else
                    {
                        txtTotalRate.Text = txtRate.Text;
                        txtEmployeeValue.Text = "0";
                        if (txtRate.Text != "" && txtRate.Text != "0" && txtEmployeeValue.Text != "" && txtEmployeeValue.Text != "0")
                        {
                            txtTotalRate.Text = (decimal.Parse(txtRate.Text) - decimal.Parse(txtEmployeeValue.Text)).ToString();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Imprime la orden y la guarda en pdf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCase.Text.Trim()))
                {
                    UIElement element = scrollHome.Content as UIElement;
                    string filePath = common.GetPathPdf() + txtCase.Text.Trim() + ".png";
                    Uri path = new Uri(filePath);
                    if (File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    CaptureScreen(element, path);
                    common.ConvertPdf(filePath, true);
                 }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message, ex);
            }
        }

        /// <summary>
        /// Imprime la orden y la guarda en pdf, por pedido del usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCase.Text.Trim()))
                {
                    SaveFileDialog showdialog = new SaveFileDialog()
                    {
                        Title = "Guardar como",
                        FileName = txtCase.Text.Trim(),
                        Filter = "PDF | *.pdf",
                        AddExtension = true
                    };
                    showdialog.ShowDialog();
                    if (showdialog.FileName != "")
                    {
                        if (File.Exists(showdialog.FileName))
                        {
                            System.IO.File.Delete(showdialog.FileName);
                        }
                        UIElement element = scrollHome.Content as UIElement;
                        string filePath = showdialog.FileName.Replace(".pdf", ".png");
                        Uri path = new Uri(filePath); 
                        if (File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        CaptureScreen(element, path);
                        common.ConvertPdf(filePath, true);
                        MessageBox.Show("Se ha generado correctamente la impresión de la pantalla","Confirmación");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Ver la lista de casos asignados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Assignment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TicketsAssignment winTickets = new TicketsAssignment()
                {
                    Width = 800,
                    Height = 550
                };
                winTickets.ShowDialog();
                winTickets.Close();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message, ex);
            }
        }

        public void CaptureScreen(UIElement source, Uri destination)
        {
            try
            {
                double Height, renderHeight, Width, renderWidth;
                Height = renderHeight = source.RenderSize.Height;
                Width = renderWidth = source.RenderSize.Width;
                RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
                VisualBrush visualBrush = new VisualBrush(source);
                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(0, 0), new Point(Width, Height)));
                }
                renderTarget.Render(drawingVisual);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                using (FileStream stream = new FileStream(destination.LocalPath, FileMode.Create, FileAccess.Write))
                {
                    encoder.Save(stream);
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// REGLAS GENERALES
        /// </summary>
        public string Points()
        {
            if ((cbFrechType.Text == "FRECH V - MI CASA YA" || cbFrechType.Text == "FRECH II") && (txtPoints.Text == "0" || txtPoints.Text == ""))
            {
                return "Verifique los puntos de cobertura el caso no puede continuar sino se le asignan los puntos ";
            }
            return "";
        }
        public string TRC()
        {
            try
            {
                decimal suma = 0;
                if (txtValueTRC.Text == "" || txtValueTRC.Text == "$0.00") txtValueTRC.Text = "0";
                if (txtCosts.Text == "" || txtCosts.Text == "$0.00") txtCosts.Text = "0";

                if (ltsinsurances.Where(x => x.PolicyType == "COLECTIVA" && x.InsuranceType == "TRC").Count() > 0)
                {
                    if (decimal.Parse(txtCosts.Text, System.Globalization.NumberStyles.Currency) == 0 || decimal.Parse(txtValueTRC.Text, System.Globalization.NumberStyles.Currency) == 0)
                    {
                        return "Recuerde que los costos directos y el valor de la prima TRC son obligatorios porque existe un registro de seguros TRC + COLECTIVA";
                    }
                }
                if (cbBRP.Text == "NO")
                {
                    suma = (decimal)ltsbankCreditAccounts.Sum(x => x.Value) + (decimal)ltsdisbursement.Sum(x => x.Value) + (decimal)ltssubrogations.Sum(x => x.Value) + (decimal)ltsbankChecks.Sum(x => x.Value);
                    suma += decimal.Parse(txtValueTRC.Text, System.Globalization.NumberStyles.Currency);
                    if (suma != decimal.Parse(txtValueDelivery.Text, System.Globalization.NumberStyles.Currency))
                    {
                        return "Recuerde que el valor a entraga debe ser igual a las suma de las formas de desembolso y el valor prima TRC cuando aplique ";
                    }
                    if (dpDateAppraisal.SelectedDate > DateTime.Now)
                    {
                        return "Verificar la fecha de avaluó, esta no puede superar la fecha actual ";
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
            return "";
        }
        public void RuleModality()
        {
            try
            {
                decimal approve = 0;
                cbScheme.Text = "";
                if (txtAppraisalValue.Text != "")
                {
                    approve = decimal.Parse(txtAppraisalValue.Text, System.Globalization.NumberStyles.Currency);
                }
                if (cbClassification.Text == "VIVDA")
                {
                    txtModality.Text = generalInformation_ViewModel.Modality(cbCity.Text, cbDeparment.Text, approve);
                }
                else
                {
                    txtModality.Text = "DIFVI";
                }
                generalInformation_ViewModel.RuleScheme(cbScheme, txtModality.Text, IdTicket, false);
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
        public void RuleTerm()
        {
            try
            {
                if (txtTerm.Text != "")
                {
                    int flag = int.Parse(txtTerm.Text.Replace("Meses", ""));
                    if (cbBussines.Text != "SI")
                    {
                        string message = generalInformation_ViewModel.Term(cbClassification.Text, cbCoin.Text, flag);
                        if (message != "")
                        {
                            MessageBox.Show(message, "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                            txtTerm.Text = "";
                            txtTerm.Focus();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
        public void RuleMCY()
        {
            try
            {
                txtMCY.Foreground = Brushes.Black;
                if (cbFrechType.Text == "MI CASA YA CP STNBLE" || cbFrechType.Text == "MI CASA YA CP" || cbFrechType.Text == "FRECH V - MI CASA YA")
                {
                    string ID = ltsParticipants.Where(X => X.TypeParticipant == "TITULAR").Select(X => X.IdentificationNumber).FirstOrDefault();
                    string Result = validationFrech_ViewModel.GetYear(ID);
                    if (Result == "Cliente no se encuentra en la base de MCY" || Result == "Cliente Sin Asignacion de MCY" || Result == "Cliente se encuentra en MCY pero no tiene año de asignación")
                    {
                        txtMCY.Text = "0";
                        txtMCY.Foreground = Brushes.Red;
                        txtMCY.ToolTip = Result;
                    }
                    else
                    {
                        if (Result != "")
                        {
                            string information = validationFrech_ViewModel.MCY(cbCity.Text, cbDeparment.Text, decimal.Parse(txtAppraisalValue.Text, System.Globalization.NumberStyles.Currency), int.Parse(Result));
                            if (information != "")
                            {
                                MessageBox.Show(information, "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                            }
                            txtMCY.ToolTip = "";
                            txtMCY.Text = Result;
                        }
                    }
                    RuleIncome();
                }
                else
                {
                    txtMCY.ToolTip = "";
                    txtMCY.Text = "";
                }
                txtPoints.Text = "0";
                if (cbFrechType.Text == "FRECH II")
                {
                    txtPoints.Text = txtModality.Text == "VIS" ? "4" : "5";
                }
                if (cbFrechType.Text == "FRECH V - MI CASA YA")
                {
                    RuleMCYMiCasaYa();
                }
                if (cbFrechType.Text == "FRECH CP ECOBERTURA" && cbCpEco.Text == "NO")
                {
                    MessageBox.Show("La selección FRECH CP ECOBERTURA no esta permitida cuando el campo VIVIENDO ECOBERTURA es NO", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                    cbFrechType.Text = "FRECH CP";
                }
                if (cbFrechType.Text == "FRECH CP" && cbCpEco.Text == "SI")
                {
                    MessageBox.Show("La selección FRECH CP no esta permitida cuando el campo VIVIENDO ECOBERTURA es SI", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                    cbFrechType.Text = "FRECH CP ECOBERTURA";
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
        public void RuleMCYMiCasaYa()
        {
            try
            {
                if (cbFrechType.Text == "FRECH V - MI CASA YA")
                {
                    string ID = ltsParticipants.Where(X => X.TypeParticipant == "TITULAR").Select(X => X.IdentificationNumber).FirstOrDefault();
                    string Result = validationFrech_ViewModel.GetYear(ID);
                    if (Result == "Cliente no se encuentra en la base de MCY" || Result == "Cliente Sin Asignacion de MCY" || Result == "Cliente se encuentra en MCY pero no tiene año de asignación")
                    {
                        txtMCY.Text = "0";
                        txtMCY.Foreground = Brushes.Red;
                        txtMCY.ToolTip = Result;
                    }
                    else
                    {
                        if (Result != "")
                        {
                            if (int.Parse(Result) > 2019)
                            {
                                string Flag = validationFrech_ViewModel.PointsModalityMCY(cbCity.Text, cbDeparment.Text, decimal.Parse(txtAppraisalValue.Text, System.Globalization.NumberStyles.Currency), int.Parse(Result));
                                txtPoints.Text = Flag == "VIS" ? "4" : "5";
                            }
                            else
                            {
                                decimal Appraisal = decimal.Parse(txtAppraisalValue.Text, System.Globalization.NumberStyles.Currency);
                                int Year = int.Parse(txtMCY.Text);
                                txtPoints.Text = validationFrech_ViewModel.PointsSalary(Appraisal, Year).ToString();
                            }
                            txtMCY.Foreground = Brushes.Black;
                            txtMCY.ToolTip = "";
                            txtMCY.Text = Result;
                        }
                    }
                    RuleIncome();
                }
                else
                {
                    txtMCY.ToolTip = "";
                    txtMCY.Foreground = Brushes.Black;
                    txtMCY.Text = "";
                }
                if (cbFrechType.Text == "FRECH II")
                {
                    txtPoints.Text = txtModality.Text == "VIS" ? "4" : "5";
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
        public void RuleFrechOld()
        {
            try
            {
                double salary = 0;
                cbFrechType.Items.Clear();
                cbFrechType.IsEnabled = true;
                txtFrechResult.Text = "";
                txtPoints.Text = "";
                txtMCY.Text = "";
                txtMCY.Foreground = Brushes.Black;

                if (txtIncome.Text != "")
                {
                    salary = double.Parse(txtIncome.Text, System.Globalization.NumberStyles.Currency);
                }
                if (txtAppraisalValue.Text != "")
                {
                    if (cbClassification.Text == "VIVDA" && (decimal.Parse(txtAppraisalValue.Text, System.Globalization.NumberStyles.Currency) <= validationFrech_ViewModel.Salary() * 500) && cbScheme.Text == "AVM (Vivienda > a VIS)" && cbVda.Text == "Nuevo" && (cbCoin.Text == "COP" || cbCoin.Text == "UVR") && txtModality.Text == "NO VIS" && cbPlan.Text == "Si - Normal Equivalente" && cbFrech.Text == "NO" && (cbFormat.Text == "SI" || cbFormat.Text == "NO") && cbPropertyType.Text == "URBANO" && (cbClass.Text == "APARTAMENTO" || cbClass.Text == "CASA"))
                    {
                        txtFrechResult.Text = "FRECH CP - FRECH CP ECOBERTURA";
                        cbFrechType.Items.Add("FRECH CP");
                        cbFrechType.Items.Add("FRECH CP ECOBERTURA");
                        return;
                    }
                    if (cbScheme.Text == "AVV (Vivienda VIS)" && salary > 1 && salary <= validationFrech_ViewModel.Salary() * 8 && cbVda.Text == "Nuevo" && (txtModality.Text == "VIS" || txtModality.Text == "VIP") && cbPlan.Text == "Si - Normal Equivalente" && cbFrech.Text == "NO" && cbFonvivienda.Text == "NO" && (cbFormat.Text == "SI" || cbFormat.Text == "NO") && cbPropertyType.Text == "URBANO" && (cbClass.Text == "APARTAMENTO" || cbClass.Text == "CASA"))
                    {
                        if (salary > 1 && salary <= validationFrech_ViewModel.Salary() * 8)
                        {
                            txtFrechResult.Text = "NO APLICA";
                            cbFrechType.Items.Add(txtFrechResult.Text);
                            cbFrechType.Text = txtFrechResult.Text;
                        }
                        if (salary > 1 && salary <= validationFrech_ViewModel.Salary() * 4)
                        {
                            if (txtFrechResult.Text == "" || cbFrechType.Text == "NO APLICA")
                            {
                                cbFrechType.Items.Clear();
                                txtFrechResult.Text = "FRECH V - MI CASA YA";
                                cbFrechType.Items.Add("FRECH V - MI CASA YA");
                                cbFrechType.Text = txtFrechResult.Text;
                            }
                        }
                    }
                    else
                    {
                        txtFrechResult.Text = "NO APLICA";
                        cbFrechType.Items.Add(txtFrechResult.Text);
                        cbFrechType.Text = txtFrechResult.Text;
                        cbCpEco.Text = "NO";
                    }
                }
                generalInformation_ViewModel.RuleFrech(gridGeneral, gridFrech);
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
        public void RuleIncome()
        {
            try
            {
                if (txtMCY.Text != "0" && txtMCY.Text != "Cliente Sin Asignacion de MCY")
                {
                    int salary = validationFrech_ViewModel.HistoricalSalary(int.Parse(txtMCY.Text));
                    if (txtIncome.Text != "")
                    {
                        if (decimal.Parse(txtIncome.Text, System.Globalization.NumberStyles.Currency) > (salary * 4))
                        {
                            MessageBox.Show("Rango de ingresos no cumple para el año de asignación de MCY", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                            txtFrechResult.Text = "";
                            cbFrechType.Text = "";
                            txtMCY.Foreground = Brushes.Red;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("Se presento un error controlado por la aplicación, pongase en contacto con el administrador de la aplicación", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
        public int NewRegister(DataGrid dataGrid, Grid grid)
        {
            dataGrid.UnselectAll();
            common.GenericCleaner(grid, false);
            return -1;
        }
        public void ValidationKeyDown(KeyEventArgs e, Control control)
        {
            if (e.Key == Key.Tab)
            {
                control.Focus();
            }
            common.ValidationTextNumber(e);
        }
        public void ResetCombobox(object sender)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.SelectedIndex = -1;
        }
        #endregion

        #region Eventos Controles
        /// <summary>
        /// EVENTO QUE SE DISPARA CUANDO SE CIERRA LAS OPCIONES DE LOS COMBOBOX
        /// </summary>
        private void CbClassification_DropDownClosed(object sender, EventArgs e)
        {
            RuleFrechOld();
            RuleModality();
            RuleTerm();
        }
        private void CbCoin_DropDownClosed(object sender, EventArgs e)
        {
            RuleFrechOld();
            RuleTerm();
            message = generalInformation_ViewModel.RuleRate(cbCoin.Text, txtModality.Text, txtRate.Text);
            if (message != "")
            {
                MessageBox.Show(message, "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                txtRate.Text = "";
            }
        }
        private void CbDeparment_DropDownClosed(object sender, EventArgs e)
        {
            if (cbDeparment.Text != "" && cbDeparment.Text != null)
            {
                if (propertyInformation_ViewModel.PropertyInformation(IdTicket) != null)
                {
                    common.FillComboboxDptoCity(cbCity, cbDeparment.Text.ToUpper(), propertyInformation_ViewModel.PropertyInformation(IdTicket).City);
                }
                else
                {
                    common.FillComboboxDptoCity(cbCity, cbDeparment.Text.ToUpper(), "");
                }
            }
        }
        private void CbCity_DropDownClosed(object sender, EventArgs e)
        {
            RuleModality();
            RuleFrechOld();
        }
        private void CbBussines_DropDownClosed(object sender, EventArgs e) => generalInformation_ViewModel.RuleFrech(gridGeneral, gridFrech);
        private void CbVda_DropDownClosed(object sender, EventArgs e) => RuleFrechOld();
        private void CbFrech_DropDownClosed(object sender, EventArgs e) => RuleFrechOld();
        private void CbClass_DropDownClosed(object sender, EventArgs e) => RuleFrechOld();
        private void CbPropertyType_DropDownClosed(object sender, EventArgs e) => RuleFrechOld();
        private void CbScheme_DropDownClosed(object sender, EventArgs e) => RuleFrechOld();
        private void CbPlan_DropDownClosed(object sender, EventArgs e) => RuleFrechOld();
        private void CbFonvivienda_DropDownClosed(object sender, EventArgs e) => RuleFrechOld();
        private void CbFormat_DropDownClosed(object sender, EventArgs e) => RuleFrechOld();

        /// <summary>
        /// DESCUENTOS
        /// </summary>
        private void CbFrechType_DropDownClosed(object sender, EventArgs e) => RuleMCY();
        private void CbEmployee_DropDownClosed(object sender, EventArgs e) => RateEmployee();
        private void CbAgreement_DropDownClosed(object sender, EventArgs e)
        {
            txtAgreenmentValue.Text = discountConcept_ViewModel.AgreementPoints(cbAgreement.Text);
            CalculateRate(decimal.Parse(txtPackageValue.Text), cbAgreement.Text, cbAttributions.Text, cbProperty.Text, cbPackage.Text);
        }
        private void CbAttributions_DropDownClosed(object sender, EventArgs e)
        {
            txtAttributionsValue.Text = discountConcept_ViewModel.AttributionsPercentage(cbAttributions.Text);
            CalculateRate(decimal.Parse(txtPackageValue.Text), cbAgreement.Text, cbAttributions.Text, cbProperty.Text, cbPackage.Text);
        }
        private void CbProperty_DropDownClosed(object sender, EventArgs e)
        {
            txtPackageValue.Text = discountConcept_ViewModel.PropertyPercentage(cbProperty.Text);
            CalculateRate(decimal.Parse(txtPackageValue.Text), cbAgreement.Text, cbAttributions.Text, cbProperty.Text, cbPackage.Text);
            if (decimal.Parse(txtTotalRate.Text) < 1)
            {
                cbProperty.Focusable = true;
                MessageBox.Show("La tasa total no permitida", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }
        private void CbPackage_DropDownClosed(object sender, EventArgs e)
        {
            txtPackegeValue2.Text = discountConcept_ViewModel.PackagePercentage(cbPackage.Text);
            CalculateRate(decimal.Parse(txtPackageValue.Text), cbAgreement.Text, cbAttributions.Text, cbProperty.Text, cbPackage.Text);
        }
        private void CbPlace_DropDownClosed(object sender, EventArgs e)
        {
            if (txtFrechResult.Text != "FRECH VI COBERTURA PLANA")
            {
                common.TypeFrech(cbFrechType, cbPlace.Text);
            }
        }
        private void CbTypeAutomatic_DropDownClosed(object sender, EventArgs e)
        {
            if (cbScheme.Text != null && cbScheme.Text != "")
            {
                if (new[] { "AID", "CID", "RID " }.Any(c => cbScheme.Text.Contains(c)))
                {
                    if (cbTypeAutomatic.Text == "AFC")
                    {
                        MessageBox.Show("El tipo de cuanto AFC no esta permitida por el tipo de scheme code que se tiene seleccionado", "Debitos Automaticos", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                        cbTypeAutomatic.Text = "";
                    }
                }
            }
        }
        private void CbCpEco_DropDownClosed(object sender, EventArgs e)
        {
            if (txtFrechResult.Text == "FRECH CP - FRECH CP ECOBERTURA")
            {
                if (cbCpEco.Text == "NO")
                {
                    cbFrechType.Text = "FRECH CP";
                }
                else if (cbCpEco.Text == "SI")
                {
                    cbFrechType.Text = "FRECH CP ECOBERTURA" +
                        "";
                }
            }
            if (txtFrechResult.Text == "NO APLICA")
            {
                cbCpEco.Text = "NO";
            }
        }

        /// <summary>
        /// EVENTOS QUE SE GENERAN CUANDO PIERDEN EL FOCO
        /// </summary>
        private void TxtIncome_LostFocus(object sender, RoutedEventArgs e)
        {
            txtIncome.Text = ValidateFormat(txtIncome.Text);
            RuleFrechOld();
        }
        private void TxtValueApprove_LostFocus(object sender, RoutedEventArgs e)
        {
            txtValueApprove.Text = ValidateFormat(txtValueApprove.Text);
            generalInformation_ViewModel.RuleFinancing(txtValueApprove.Text, txtAppraisalValue.Text, txtFinancing, gridGeneral);
            RuleModality();
        }
        private void TxtAppraisalValue_LostFocus(object sender, RoutedEventArgs e)
        {
            txtAppraisalValue.Text = ValidateFormat(txtAppraisalValue.Text);
            generalInformation_ViewModel.RuleFinancing(txtValueApprove.Text, txtAppraisalValue.Text, txtFinancing, gridGeneral);
            RuleModality();
            RuleFrechOld();
        }
        private void TxtCosts_LostFocus(object sender, RoutedEventArgs e)
        {
            txtCosts.Text = ValidateFormat(txtCosts.Text);
            if (txtCosts.Text != "")
            {
                double value = double.Parse(txtCosts.Text, System.Globalization.NumberStyles.Currency);
                txtValueTRC.Text = Math.Truncate(value * 1.6739 / 1000 * 1.19).ToString();
            }
            else
            {
                txtValueTRC.Text = "";
            }
        }
        private void TxtValueTRC_LostFocus(object sender, RoutedEventArgs e)
        {
            txtValueTRC.Text = ValidateFormat(txtValueTRC.Text);
        }
        private void TxtSolId_LostFocus(object sender, RoutedEventArgs e)
        {
            general = (GeneralInformation)gridGeneral.DataContext;
            if (txtSolId.Text != "")
            {
                string aux = txtSolId.Text.Replace("BC", "");
                if (common.OfficeActive(txtSolId.Text) == true)
                {
                    int num = 6 - aux.Length;
                    txtSolId.Text = aux.PadLeft(num + aux.Length, '0').ToString();
                    txtSolId.Text = "BC" + txtSolId.Text;
                    general.Office = txtSolId.Text;
                }
                else
                {
                    MessageBox.Show("La oficina ingresada no se encuentra activa", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtSolId.Text = "";
                    general.Office = "";
                }
            }
        }
        private void TxtTerm_LostFocus(object sender, RoutedEventArgs e)
        {
            general = (GeneralInformation)gridGeneral.DataContext;
            if (txtTerm.Text != "")
            {
                txtTerm.Text = txtTerm.Text.Replace(" Meses", "") + " Meses";
            }
            RuleTerm();
        }
        private void TxtRate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtRate.Text != "")
            {
                double val = Convert.ToDouble(txtRate.Text.Replace(",", "."));
                txtRate.Text = val.ToString("N2");
                message = "";
                message = generalInformation_ViewModel.RuleRate(cbCoin.Text, txtModality.Text, txtRate.Text);
                if (message != "")
                {
                    MessageBox.Show(message, "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    txtRate.Text = "";
                }
                CalculateRate(decimal.Parse(txtPackageValue.Text), cbAgreement.Text, cbAttributions.Text, cbProperty.Text, cbPackage.Text);
            }
        }
        private void DpDateRevived_LostFocus(object sender, RoutedEventArgs e)
        {
            txtVtoApprove.Text = "";
            if (dpDateRevived.Text != "")
            {
                if (dpDateRevived.SelectedDate.Value.AddMonths(9) < DateTime.Now) { txtVtoApprove.Text = "VENCIDO"; } else { txtVtoApprove.Text = "VIGENTE"; }
                general._ApprovedVto = txtVtoApprove.Text;
            }
        }
        private void TxtNumAccount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNumAccount.Text != "")
            {
                int num = 11 - txtNumAccount.Text.Length;
                txtNumAccount.Text = txtNumAccount.Text.PadLeft(num + txtNumAccount.Text.Length, '0').ToString();
            }
        }
        private void TxtOfficeCheck_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtOfficeCheck.Text != "")
            {
                string aux = txtOfficeCheck.Text.Replace("BC", "");
                if (common.OfficeActive(txtOfficeCheck.Text) == true)
                {
                    int num = 6 - aux.Replace("BC", "").Length;
                    txtOfficeCheck.Text = aux.PadLeft(num + aux.Length, '0').ToString();
                    txtOfficeCheck.Text = "BC" + txtOfficeCheck.Text;
                }
                else
                {
                    MessageBox.Show("La oficina ingresada no se encuentra activa", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtOfficeCheck.Text = "";
                }
            }
        }
        private void DpDateAppraisal_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dpDateAppraisal.Text == "")
            {
                PropertyInformation property = (PropertyInformation)gridProperty.DataContext;
                property.AppraisalDate = null;
            }
        }
        private void TxtValueDelivery_LostFocus(object sender, RoutedEventArgs e) => txtValueDelivery.Text = common.ValueConvert(ValidateFormat(txtValueDelivery.Text));
        private void TxtAccountValue_LostFocus(object sender, RoutedEventArgs e) => txtAccountValue.Text = common.ValueConvert(ValidateFormat(txtAccountValue.Text));
        private void TxtCheckValue_LostFocus(object sender, RoutedEventArgs e) => txtCheckValue.Text = common.ValueConvert(ValidateFormat(txtCheckValue.Text));
        private void TxtSubroValue_LostFocus(object sender, RoutedEventArgs e) => txtSubroValue.Text = common.ValueConvert(ValidateFormat(txtSubroValue.Text));
        private void TxtValueContingency_LostFocus(object sender, RoutedEventArgs e) => txtValueContingency.Text = common.ValueConvert(ValidateFormat(txtValueContingency.Text));

        /// <summary>
        /// EVENTO QUE SE GENERA CUANDO OBTIENE EL FOCO
        /// </summary>
        private void TxtFrechResult_GotFocus(object sender, RoutedEventArgs e) => RuleFrechOld();
        private void TxtTerm_GotFocus(object sender, RoutedEventArgs e) => txtTerm.Text = txtTerm.Text.Replace(" Meses", "");

        /// <summary>
        /// EVENTOS CUANDO CABIAN EL TEXTO DEL TEXTBOX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSolId_TextChanged(object sender, TextChangedEventArgs e)
        {
            int solId = txtSolId.Text.Replace("BC", "").Length;
            if (solId > 6)
            {
                MessageBox.Show("El campo supera el numero maximo de caracteres (6)", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtSolId.Text = "";
                txtSolId.Focus();
            }
        }
        private void TxtOfficeCheck_TextChanged(object sender, TextChangedEventArgs e)
        {
            int solId = txtOfficeCheck.Text.Replace("BC", "").Length;
            if (solId > 6)
            {
                MessageBox.Show("El campo supera el numero maximo de caracteres (6)", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtOfficeCheck.Text = "";
                txtOfficeCheck.Focus();
            }
        }
        private void TxtIdParticipant_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Id = txtIdParticipant.Text.Length;
            if (Id > 15)
            {
                MessageBox.Show("El campo supera el numero maximo de (15) caracteres", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtIdParticipant.Text = "";
            }
        }
        private void TxtModality_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Modality != txtModality.Text)
            {
                generalInformation_ViewModel.RuleScheme(cbScheme, txtModality.Text, IdTicket, true);
                message = generalInformation_ViewModel.RuleRate(cbCoin.Text, txtModality.Text, txtRate.Text);
                if (message != "")
                {
                    MessageBox.Show(message, "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    txtRate.Text = "";
                }
            }
        }

        /// <summary>
        /// EVANTOS DE LOS DATAGRID QUE PERMITEN CONTROLAR EL INGRESO DE LA INFORMACIÓN EN BASE DE DATOS
        /// </summary>
        private void DatagridParticipant_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => participant_ViewModel.DeleteDatagridParticipant(datagridParticipant, ltsParticipants, ltsRemoveParticipants, gridParticipants, sender, e);
        private void DatagridParticipant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IdParticipant = 0;
            DataGrid grid = (DataGrid)sender;
            Participant participant = (Participant)datagridParticipant.SelectedItem;
            gridParticipants.DataContext = participant;
            if (participant != null) IdParticipant = participant.IdParticipant;
            IndexParticipant = grid.SelectedIndex;
        }
        private void DatagridInsurance_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => insurance_ViewModel.DeleteDatagridInsurance(datagridInsurance, ltsinsurances, ltsRemoveinsurances, gridInsuranceTwo, sender, e);
        private void DatagridInsurance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Idinsurances = 0;
            DataGrid grid = (DataGrid)sender;
            Insurance insurance = (Insurance)datagridInsurance.SelectedItem;
            gridInsuranceTwo.DataContext = insurance;
            if (insurance != null) Idinsurances = insurance.IdInsurance;
            IndexInsurance = grid.SelectedIndex;
        }
        private void DatagridAccount_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => bankCreditAccount_ViewModel.DeleteDatagridAccount(datagridAccount, ltsbankCreditAccounts, ltsRemovebankCreditAccounts, gridAccount, sender, e);
        private void DatagridAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IdbankCreditAccounts = 0;
            DataGrid grid = (DataGrid)sender;
            BankCreditAccount bankCreditAccount = (BankCreditAccount)datagridAccount.SelectedItem;
            gridAccount.DataContext = bankCreditAccount;
            if (bankCreditAccount != null) IdbankCreditAccounts = bankCreditAccount.IdCreditAccount;
            IndexAccount = grid.SelectedIndex;
        }
        private void DatagridCheck_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => bankCheck_ViewModel.DeleteDatagridCheck(datagridCheck, ltsbankChecks, ltsRemovebankChecks, gridCheck, sender, e);
        private void DatagridCheck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IdbankChecks = 0;
            DataGrid grid = (DataGrid)sender;
            BankCheck bankCheck = (BankCheck)datagridCheck.SelectedItem;
            gridCheck.DataContext = bankCheck;
            if (bankCheck != null) IdbankChecks = bankCheck.IdCheck;
            IndexCheck = grid.SelectedIndex;
        }
        private void DatagridSubro_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => subrogation_ViewModel.DeleteDatagridSubrogation(datagridSubro, ltssubrogations, ltsRemovesubrogations, gridSubro, sender, e);
        private void DatagridSubro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Idsubrogations = 0;
            DataGrid grid = (DataGrid)sender;
            Subrogation subrogation = (Subrogation)datagridSubro.SelectedItem;
            gridSubro.DataContext = subrogation;
            if (subrogation != null) Idsubrogations = subrogation.IdSubrogation;
            IndexSubroga = grid.SelectedIndex;
        }
        private void DatagridContingency_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => disbursement_ViewModel.DeleteDatagridDisbursement(datagridContingency, ltsdisbursement, ltsRemovedisbursement, gridContingency, sender, e);
        private void DatagridContingency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Iddisbursement = 0;
            Disbursement disbursement = (Disbursement)datagridContingency.SelectedItem;
            gridContingency.DataContext = disbursement;
            Iddisbursement = disbursement.IdDisbursement;
        }
        private void DatagridSeller_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => seller_ViewModel.DeleteDatagridSellers(datagridSeller, ltsseller, ltsRemoveseller, gridSeller, sender, e);
        private void DatagridSeller_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Idseller = 0;
            DataGrid grid = (DataGrid)sender;
            Seller seller = (Seller)datagridSeller.SelectedItem;
            gridSeller.DataContext = seller;
            if (seller != null) Idseller = seller.IdSeller;
            IndexSeller = grid.SelectedIndex;
        }
        private void DatagridAutomatic_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => banckDebit_ViewModel.DeleteDatagridDebit(datagridAutomatic, ltsbankDebitAccounts, ltsRemovebankDebitAccounts, gridAutomatic, sender, e);
        private void DatagridAutomatic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IdbankDebitAccounts = 0;
            DataGrid grid = (DataGrid)sender;
            BankDebitAccount bankDebit = (BankDebitAccount)datagridAutomatic.SelectedItem;
            gridAutomatic.DataContext = bankDebit;
            if (bankDebit != null) IdbankDebitAccounts = bankDebit.IdDebitAccount;
            IndexDebitAccount = grid.SelectedIndex;
        }
        private void DatagridStrandedP_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => stranded_ViewModel.DeleteDatagridStranded(datagridStrandedP, ltsPending, datagridManagementP, ltsPendingM, sender, e);
        private void DatagridStrandedD_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => stranded_ViewModel.DeleteDatagridStranded(datagridStrandedD, ltsReturn, datagridManagementD, ltsReturnM, sender, e);
        private void DatagridStrandedG_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e) => stranded_ViewModel.DeleteDatagridStranded(datagridStrandedG, ltsManagement, datagridManagementG, ltsManagementM, sender, e);
        private void CbStrandedPendindg_SelectionChanged(object sender, SelectionChangedEventArgs e) => ResetCombobox(sender);
        private void CbStrandedReturn_SelectionChanged(object sender, SelectionChangedEventArgs e) => ResetCombobox(sender);
        private void CbStrandedInternal_SelectionChanged(object sender, SelectionChangedEventArgs e) => ResetCombobox(sender);

        /// <summary>
        /// GUARDA LA INFORMACIÓN DE CADA UNA DE LAS SECCIONES DE LA ORDEN
        /// </summary>
        private void SaveParticipant_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbType.Text != null && txtIdParticipant.Text != "" && txtNameParticipant.Text != "" && cbTypeParticipant.Text != "")
                {
                    Participant newParticipant = new Participant
                    {
                        IdParticipant = IdParticipant,
                        IdTicket = IdTicket,
                        IdentificationType = cbType.Text,
                        TypeParticipant = cbTypeParticipant.Text,
                        IdentificationNumber = txtIdParticipant.Text == "" ? "0" : txtIdParticipant.Text,
                        NameParticipant = txtNameParticipant.Text
                    };
                    participant_ViewModel.SaveDatagridParticipant(datagridParticipant, ltsParticipants, cbTypeParticipant.Text, newParticipant, IndexParticipant);
                    common.GenericCleaner(gridParticipants, false);
                    IndexParticipant = -1;
                }
                else
                {
                    MessageBox.Show("Todos los campos deben estar diligenciados", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                App.Current.Shutdown();
            }
        }
        private void SaveInsurance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTypePol.Text.Contains("NO SEGURO"))
                {
                    if (txtIdInsurance.Text == "" || txtIdInsurance.Text == null || cbTypeSure.Text == "")
                    {
                        MessageBox.Show("Todos los campos deben estar diligenciados para agregar un nuevo seguro", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                        return;
                    }
                }
                else
                {
                    if (txtIdInsurance.Text == "" || txtIdInsurance.Text == null || cbTypePol.Text == "" || cbTypeSure.Text == "" || cbTypeInsurance.Text == "")
                    {
                        MessageBox.Show("Todos los campos deben estar diligenciados para agregar un nuevo seguro", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                        return;
                    }
                }
                Insurance insurance = new Insurance
                {
                    IdInsurance = Idinsurances,
                    IdTicket = IdTicket,
                    IdentificationNumber = txtIdInsurance.Text,
                    PolicyType = cbTypePol.Text,
                    InsuranceType = cbTypeSure.Text,
                    InsuranceCarrier = cbTypeInsurance.Text
                };
                if (ltsParticipants.Where(X => X.IdentificationNumber == insurance.IdentificationNumber).FirstOrDefault() == null)
                {
                    MessageBox.Show("La cedula ingresada en el seguro no se encuentra en los participantes relacionados en el caso", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    return;
                }
                if (dtStartDate.SelectedDate.ToString() != "") insurance.StartDate = DateTime.Parse(dtStartDate.SelectedDate.Value.ToString("dd/MM/yyyy"));
                if (dtEndDate.SelectedDate.ToString() != "") insurance.EndDate = DateTime.Parse(dtEndDate.SelectedDate.Value.ToString("dd/MM/yyyy"));
                if (cbTypePol.Text == "ENDOSADA" && (dtStartDate.SelectedDate.ToString() == "" || dtEndDate.SelectedDate.ToString() == ""))
                {
                    MessageBox.Show("Las fechas son obligatorias", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    return;
                }
                if (cbTypePol.Text == "COLECTIVA" && cbTypeSure.Text == "TRC" && dtEndDate.SelectedDate.ToString() == "")
                {
                    MessageBox.Show("La fecha fin es obligatoria", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    return;
                }
                if (cbTypePol.Text == "COLECTIVA" && cbTypeSure.Text == "TRC" && dtEndDate.SelectedDate.ToString() != "")
                {
                    insurance.InsuranceCarrier = "AXACOLPATRIA";
                }
                insurance_ViewModel.SaveDatagridInsurance(ltsinsurances, datagridInsurance, insurance, IndexInsurance);
                datagridInsurance.Visibility = Visibility.Visible;
                IndexInsurance = -1;
                common.GenericCleaner(gridInsuranceTwo, false);

            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        private void SaveAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTypeIdAccount.Text != null && txtIdAccount.Text != "" && txtNumAccount.Text != null && cbTypeAccount.Text != "" && txtAccountValue.Text != "" && (cbTypeAccountGmf.Text != null && cbTypeAccountGmf.Text != ""))
                {
                    decimal value = decimal.Parse(txtAccountValue.Text, System.Globalization.NumberStyles.Currency);
                    BankCreditAccount bankCreditAccount = new BankCreditAccount
                    {
                        IdCreditAccount = IdbankCreditAccounts,
                        IdentificationType = cbTypeIdAccount.Text,
                        IdentificationNumber = txtIdAccount.Text,
                        IdTicket = IdTicket,
                        AccountNumber = long.Parse(txtNumAccount.Text),
                        AccountType = cbTypeAccount.Text,
                        Value = value,
                        Gmf = cbTypeAccountGmf.Text
                    };
                    bankCreditAccount_ViewModel.SaveDatagridAccount(datagridAccount, ltsbankCreditAccounts, bankCreditAccount, IndexAccount);
                    datagridAccount.Visibility = Visibility.Visible;
                    IndexAccount = -1;
                    common.GenericCleaner(gridAccount, false);
                }
                else
                {
                    MessageBox.Show("Todos los campos deben estar diligenciados para agregar una nueva cuenta", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        private void SaveCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtOfficeCheck.Text != null && cbTypeIdCheck.Text != "" && txtIdCheck.Text != "" && txtbeneficiaryCheck.Text != "" && txtCheckValue.Text != "" && (cbTypeCheckGmf.Text != "" && cbTypeCheckGmf.Text != null))
                {
                    decimal value = decimal.Parse(txtCheckValue.Text, System.Globalization.NumberStyles.Currency);
                    BankCheck bankCheck = new BankCheck
                    {
                        IdCheck = IdbankChecks,
                        IdTicket = IdTicket,
                        Office = txtOfficeCheck.Text,
                        IdentificationType = cbTypeIdCheck.Text,
                        IdentificationNumber = txtIdCheck.Text,
                        Beneficiary = txtbeneficiaryCheck.Text,
                        Value = value,
                        Gmf = cbTypeCheckGmf.Text
                    };
                    bankCheck_ViewModel.SaveDatagridCheck(datagridCheck, ltsbankChecks, bankCheck, IndexCheck);
                    datagridCheck.Visibility = Visibility.Visible;
                    IndexCheck = -1;
                    common.GenericCleaner(gridCheck, false);
                }
                else
                {
                    MessageBox.Show("Todos los campos deben estar diligenciados para agregar un nuevo Cheque", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        private void SaveSubro_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNroSubro.Text != null && cbTypeIdSubro.Text != "" && txtIdSubro.Text != "" && txtObligationSubro.Text != "" && txtSubroValue.Text != "" && (cbTypeSubroGmf.Text != "" && cbTypeSubroGmf.Text != null))
                {
                    decimal value = decimal.Parse(txtSubroValue.Text, System.Globalization.NumberStyles.Currency);
                    Subrogation subrogation = new Subrogation
                    {
                        IdSubrogation = Idsubrogations,
                        IdTicket = IdTicket,
                        SubrogationNumber = long.Parse(txtNroSubro.Text),
                        IdentificationType = cbTypeIdSubro.Text,
                        IdentificationNumber = txtIdSubro.Text,
                        ObligationNumber = long.Parse(txtObligationSubro.Text),
                        Value = value,
                        Gmf = cbTypeSubroGmf.Text
                    };
                    subrogation_ViewModel.SaveDatagridSubrogation(datagridSubro, ltssubrogations, subrogation, IndexSubroga);
                    datagridSubro.Visibility = Visibility.Visible;
                    IndexSubroga = -1;
                    common.GenericCleaner(gridSubro, false);
                }
                else
                {
                    MessageBox.Show("Todos los campos deben estar diligenciados para agregar una nueva subrogación", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        private void SaveSeller_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTypeSeller.Text != null && txtIdSeller.Text != "" && txtNameSeller.Text != "")
                {
                    Seller seller = new Seller
                    {
                        IdSeller = Idseller,
                        IdentificationType = cbTypeSeller.Text,
                        IdentificationNumber = txtIdSeller.Text,
                        NameSeller = txtNameSeller.Text
                    };
                    seller_ViewModel.SaveDatagridSellers(datagridSeller, ltsseller, seller, IndexSeller);
                    datagridSeller.Visibility = Visibility.Visible;
                    IndexSeller = -1;
                    common.GenericCleaner(gridSeller, false);
                }
                else
                {
                    MessageBox.Show("Todos los campos deben estar diligenciados para agregar un nuevo vendedor", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        private void SaveContingency_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTypeContingency.Text != null && txtValueContingency.Text != "" && cbGmfContingency.Text != "")
                {
                    decimal value = decimal.Parse(txtValueContingency.Text, System.Globalization.NumberStyles.Currency);
                    Disbursement disbursement = new Disbursement
                    {
                        IdDisbursement = Iddisbursement,
                        Contingency = cbTypeContingency.Text,
                        Value = value,
                        Gmf = cbGmfContingency.Text
                    };
                    disbursement_ViewModel.SaveDatagridDisbursement(datagridContingency, ltsdisbursement, disbursement);
                    datagridContingency.Visibility = Visibility.Visible;
                    common.GenericCleaner(gridContingency, false);
                }
                else
                {
                    MessageBox.Show("Todos los campos deben estar diligenciados para agregar una nueva contingencia", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        private void SaveStranded_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Verifique las causales de varado antes de guardar una vez guardado solo sera posible pasar a gestionadas: ", "Confirmar Proceso", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    if (ltsPending.Count != 0)
                    {
                        common.Check_ViewModel(datagridStrandedP, ltsPending);
                        datagridStrandedP.Visibility = Visibility.Visible;
                    }
                    if (ltsReturn.Count != 0)
                    {
                        common.Check_ViewModel(datagridStrandedD, ltsReturn);
                        datagridStrandedD.Visibility = Visibility.Visible;
                    }
                    if (ltsManagement.Count != 0)
                    {
                        common.Check_ViewModel(datagridStrandedG, ltsManagement);
                        datagridStrandedG.Visibility = Visibility.Visible;
                    }
                    common.FillComboboxStranded(cbStrandedPendindg, "Varados", "TIPOS");
                    common.FillComboboxStranded(cbStrandedReturn, "Varados", "TIPOS");
                    common.FillComboboxStranded(cbStrandedInternal, "VaradosInternos", "TIPOS");
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                Application.Current.Shutdown();
            }
        }
        private void SaveAutomatic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTypeAutomatic.Text != null && txtNumAutomatic.Text != "" && txtPercent.Text != "")
                {
                    BankDebitAccount bankDebit = new BankDebitAccount
                    {
                        IdDebitAccount = IdbankDebitAccounts,
                        AccountType = cbTypeAutomatic.Text,
                        AccountNumber = long.Parse(txtNumAutomatic.Text),
                        Porcentage = double.Parse(txtPercent.Text)
                    };
                    banckDebit_ViewModel.SaveDatagridDebit(datagridAutomatic, ltsbankDebitAccounts, bankDebit, IndexDebitAccount);
                }
                else
                {
                    MessageBox.Show("Todos los campos deben estar diligenciados para agregar una nueva cuenta debito automatico", "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
                datagridAutomatic.Visibility = Visibility.Visible;
                IndexDebitAccount = -1;
                common.GenericCleaner(gridAutomatic, false);
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// CAMPOS QUE SOLO PERMITEN NUMEROS
        /// </summary>
        private void TxtSolId_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, txtModality);
        private void TxtValueApprove_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, txtTerm);
        private void TxtTerm_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, cbCoin);
        private void TxtRate_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, txtIncome);
        private void TxtIncome_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, cbFrech);
        private void TxtMCY_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, txtDelivery);
        private void TxtDelivery_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, txtValueDelivery);
        private void TxtValueDelivery_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, cbBRP);
        private void TxtAppraisalValue_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, txtCosts);
        private void TxtCosts_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, txtValueTRC);
        private void TxtValueTRC_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, txtCIF);
        private void TxtValueContingency_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, cbGmfContingency);
        private void TxtNroSubro_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, cbTypeIdSubro);
        private void TxtObligationSubro_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, txtSubroValue);
        private void TxtSubroValue_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, cbTypeSubroGmf);
        private void TxtCheckValue_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, cbTypeCheckGmf);
        private void TxtNumAccount_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, txtAccountValue);
        private void TxtAccountValue_KeyDown(object sender, KeyEventArgs e) => ValidationKeyDown(e, cbTypeAccountGmf);
        private void TxtAgreenmentValue_KeyDown(object sender, KeyEventArgs e) => common.ValidationTextNumber(e);
        private void TxtEmployeeValue_KeyDown(object sender, KeyEventArgs e) => common.ValidationTextNumber(e);
        private void TxtAttributionsValue_KeyDown(object sender, KeyEventArgs e) => common.ValidationTextNumber(e);
        private void TxtPackageValue_KeyDown(object sender, KeyEventArgs e) => common.ValidationTextNumber(e);
        private void TxtPackegeValue2_KeyDown(object sender, KeyEventArgs e) => common.ValidationTextNumber(e);
        private void TxtTotalRate_KeyDown(object sender, KeyEventArgs e) => common.ValidationTextNumber(e);
        private void TxtNumAutomatic_KeyDown(object sender, KeyEventArgs e) => common.ValidationTextNumber(e);
        private void TxtPercent_KeyDown(object sender, KeyEventArgs e) => common.ValidationTextNumber(e);

        /// <summary>
        /// LIMPIA LOS CAMPOS DE CADA UNA DE LAS SECCIONES  DE MODALIDAD DE DESEMBOLSO, SEGUROS Y PARTICIPANTES.
        /// </summary>
        private void NewParticipant_Click(object sender, RoutedEventArgs e) => IndexParticipant = NewRegister(datagridParticipant, gridParticipants);
        private void NewInsurance_Click(object sender, RoutedEventArgs e) => IndexInsurance = NewRegister(datagridInsurance, gridInsuranceTwo);
        private void NewAccount_Click(object sender, RoutedEventArgs e) => IndexAccount = NewRegister(datagridAccount, gridAccount);
        private void NewCheck_Click(object sender, RoutedEventArgs e) => IndexCheck = NewRegister(datagridCheck, gridCheck);
        private void NewSubro_Click(object sender, RoutedEventArgs e) => IndexSubroga = NewRegister(datagridSubro, gridSubro);

        private void NewSeller_Click(object sender, RoutedEventArgs e) => IndexSeller = NewRegister(datagridSeller, gridSeller);
        private void NewContingency_Click(object sender, RoutedEventArgs e) => common.GenericCleaner(gridContingency, false);
        private void NewAutomatic_Click(object sender, RoutedEventArgs e) => IndexDebitAccount = NewRegister(datagridAutomatic, gridAutomatic);

        /// <summary>
        /// Permite el control de las causales de varados
        /// </summary>
        private void ChkPending_Checked(object sender, RoutedEventArgs e) => ltsPending = common.Check(cbStrandedPendindg, ltsPending);
        private void ChkPending_Unchecked(object sender, RoutedEventArgs e) => ltsPending = common.Check(cbStrandedPendindg, ltsPending);
        private void ChkReturn_Checked(object sender, RoutedEventArgs e) => ltsReturn = common.Check(cbStrandedReturn, ltsReturn);
        private void ChkReturn_Unchecked(object sender, RoutedEventArgs e) => ltsReturn = common.Check(cbStrandedReturn, ltsReturn);
        private void ChkInternal_Checked(object sender, RoutedEventArgs e) => ltsManagement = common.Check(cbStrandedInternal, ltsManagement);
        private void ChkInternal_Unchecked(object sender, RoutedEventArgs e) => ltsManagement = common.Check(cbStrandedInternal, ltsManagement);

        /// <summary>
        /// EVENTOS QUE SE DISPARAN CUANDO LOS DATAGRID OBTIENEN EN FOCO
        /// </summary>
        private void DatagridAccount_GotFocus(object sender, RoutedEventArgs e) => datagridAccount.UnselectAll();
        private void DatagridInsurance_GotFocus(object sender, RoutedEventArgs e) => datagridInsurance.UnselectAll();
        private void DatagridCheck_GotFocus(object sender, RoutedEventArgs e) => datagridCheck.UnselectAll();
        private void DatagridSubro_GotFocus(object sender, RoutedEventArgs e) => datagridSubro.UnselectAll();
        private void DatagridContingency_GotFocus(object sender, RoutedEventArgs e) => datagridContingency.UnselectAll();
        private void DatagridSeller_GotFocus(object sender, RoutedEventArgs e) => datagridSeller.UnselectAll();
        private void DatagridParticipant_GotFocus(object sender, RoutedEventArgs e) => datagridParticipant.UnselectAll();
        private void DatagridAutomatic_GotFocus(object sender, RoutedEventArgs e) => datagridAutomatic.UnselectAll();

        /// <summary>
        /// RECARGA LOS COMBOBOX DE DESCUENTOS
        /// </summary>
        private void CbAgreement_DropDownOpened(object sender, EventArgs e) => common.FillCombobox(cbAgreement, "Convenio", "TIPOS");
        private void CbEmployee_DropDownOpened(object sender, EventArgs e) => common.FillCombobox(cbEmployee, "Empleado", "TIPOS");
        private void CbAttributions_DropDownOpened(object sender, EventArgs e) => common.FillCombobox(cbAttributions, "Atribuciones", "TIPOS");
        private void CbProperty_DropDownOpened(object sender, EventArgs e) => common.FillCombobox(cbProperty, "Inmueble", "TIPOS");
        private void CbPackage_DropDownOpened(object sender, EventArgs e) => common.FillCombobox(cbPackage, "Paquete", "TIPOS");
        private void CbPlace_DropDownOpened(object sender, EventArgs e) => common.FillCombobox(cbPlace, "Generico", "TIPOS");

        /// <summary>
        /// CENTRA LA VENTANA CUANDO ESTA SE MINIMIZA
        /// </summary>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState != WindowState.Minimized)
            {
                WindowState = WindowState.Maximized;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtCase.Text.Trim() != "")
            {
                common.DeleteLocked(txtCase.Text.Trim());
                //Reporte en nazar exitoso
                transactionLog.WriteTransactionLog(Environment.UserName, "53", "13", IdTransaction, "1", "0");
            }
            Application.Current.Shutdown();
        }
        public string ValidateFormat(string text)
        {
            double value;
            try
            {
                value = double.Parse(text, System.Globalization.NumberStyles.Currency);
            }
            catch (Exception)
            {
                return text.Replace("$", "").Replace(".", "").Replace(",", ".");
            }
            return text;
        }
        #endregion
    }
}