using log4net;
using Orden.Helpers;
using Orden.Model;
using Orden.Views;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Orden
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// Declaracón de variables
        /// <summary>
        /// Log de error de la aplicación: \bin\Debug\MiAsistenteEnProcesos_LogdeErrores.log cuando no esya instalada
        /// //**********************************************************************************************************
        /// Log de error de la aplicación: \bin\Debug\MiAsistenteEnProcesos_LogdeErrores.log cuando esta instalada
        /// C:\Users\UserName\AppData\Local\Apps\2.0\*
        /// </summary
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public string FileParameterServer = Path.Combine(ConfigurationManager.AppSettings["PathServer"], ConfigurationManager.AppSettings["Parametros"]);
        public string FileOfficeServer = Path.Combine(ConfigurationManager.AppSettings["PathServer"], ConfigurationManager.AppSettings["Sucursales"]);
        public string FileDptoCityServer = Path.Combine(ConfigurationManager.AppSettings["PathServer"], ConfigurationManager.AppSettings["DptoCity"]);
        public string FileMCYServer = Path.Combine(ConfigurationManager.AppSettings["PathServer"], ConfigurationManager.AppSettings["PathFileTxt"]);
        private CommonFunctions common;
        private MainWindow splashMain;
        public bool blnActive = false;
        private int IdOrder = 0;
        public string name = "";
        public bool ExistFile()
        {
            long fileLength = 0;
            long fileLengthTwo = 1;
            if (File.Exists(FileParameterServer) == true && File.Exists(FileOfficeServer) == true && File.Exists(FileDptoCityServer) == true && File.Exists(FileMCYServer))
            {
                string CreatePath = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)), "Archivos");
                string FileParameter = Path.Combine(CreatePath, ConfigurationManager.AppSettings["Parametros"]);
                string FileOffice = Path.Combine(CreatePath, ConfigurationManager.AppSettings["Sucursales"]);
                string FileDptoCity = Path.Combine(CreatePath, ConfigurationManager.AppSettings["DptoCity"]);
                string FileMCY = Path.Combine(CreatePath, ConfigurationManager.AppSettings["PathFileTxt"]);
                if (File.Exists(FileMCY) && File.Exists(FileMCYServer))
                {
                    using (FileStream stream = new FileStream(FileMCY, FileMode.Open, FileAccess.Read))
                    {
                        fileLength = stream.Length;
                        stream.Dispose();
                    }
                    using (FileStream stream = new FileStream(FileMCYServer, FileMode.Open, FileAccess.Read))
                    {
                        fileLengthTwo = stream.Length;
                        stream.Dispose();
                    }
                }
                if (!Directory.Exists(CreatePath)) Directory.CreateDirectory(CreatePath);
                if (File.Exists(FileParameter))
                {
                    File.Delete(FileParameter);
                }
                if (File.Exists(FileOffice))
                {
                    File.Delete(FileOffice);
                }
                if (File.Exists(FileDptoCity))
                {
                    File.Delete(FileDptoCity);
                }
                File.Copy(FileParameterServer, Path.Combine(CreatePath, ConfigurationManager.AppSettings["Parametros"]));
                File.Copy(FileOfficeServer, Path.Combine(CreatePath, ConfigurationManager.AppSettings["Sucursales"]));
                File.Copy(FileDptoCityServer, Path.Combine(CreatePath, ConfigurationManager.AppSettings["DptoCity"]));
                if (fileLength != fileLengthTwo)
                {
                    blnActive = true;
                    ProgressbarCopyFile progressbarCopyFile = new ProgressbarCopyFile();
                    Task.Factory.StartNew(() =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            progressbarCopyFile.Show();
                            progressbarCopyFile.ShowActivated = true;
                            progressbarCopyFile.Focus();
                        });
                    });
                    Task.Run(() =>
                    {
                        Copy(FileMCYServer, FileMCY, progressbarCopyFile);
                    });
                    if (File.Exists(FileParameter) == false || File.Exists(FileOffice) == false || File.Exists(FileDptoCity) == false) return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
                common = new CommonFunctions();
                splashMain = new MainWindow();
                splashMain.cbOrders.ItemsSource = common.ListOrders();
                if (splashMain.cbOrders.Items.Count != 0)
                {
                    if (ExistFile() == true)
                    {
                        if (splashMain.cbOrders.Items.Count == 1)
                        {
                            IdOrder = common.IdOrder();
                            if (IdOrder == 1)
                            {
                                splashMain.mainWindow.Assignment.Visibility = Visibility.Visible;
                                splashMain.mainWindow.PrintUser.Visibility = Visibility.Visible;
                            }
                            if (IdOrder == 5)
                            {
                                splashMain.mainWindow.Print.Visibility = Visibility.Visible;
                                splashMain.mainWindow.Save.Visibility = Visibility.Collapsed;
                            }
                            splashMain.gridSelect.Visibility = Visibility.Collapsed;
                            splashMain.FindStates(name, IdOrder);
                            splashMain.LoadStatesOrder(IdOrder);
                            if (blnActive == false) splashMain.Display();
                        }
                        splashMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("La aplicación no se puede iniciar, hay documentos de configuración que no se encontraron", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                        Current.Shutdown();
                    }
                }
                else
                {
                    MessageBox.Show("El usuario: " + Environment.UserName.ToUpper() + " no tiene asignados permisos sobre ninguna orden comuniquese con el administrador", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                    Current.Shutdown();
                }
            }
            catch (Exception exc)
            {
                Log.Fatal(exc.Message, exc);
                MessageBox.Show("La aplicación no logro iniciar correctamente revisar log de errores", "Mensaje de Informativo", MessageBoxButton.OK, MessageBoxImage.Information);
                Current.Shutdown();
            }
        }
        public void Copy(string PathFileSource, string PathFileDest, ProgressbarCopyFile progressbarCopyFile)
        {
            if (File.Exists(PathFileDest)) File.Delete(PathFileDest);

            byte[] buffer = new byte[1024 * 1024];
            using (FileStream fileSource = new FileStream(PathFileSource, FileMode.Open, FileAccess.Read))
            {
                long fileLength = fileSource.Length;
                using (FileStream filedest = new FileStream(PathFileDest, FileMode.CreateNew, FileAccess.Write))
                {
                    long Bytes = 0;
                    int CurrentByte = 0;
                    while ((CurrentByte = fileSource.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        Bytes += CurrentByte;
                        double persentage = (double)Bytes * 100.0 / fileLength;
                        filedest.Write(buffer, 0, CurrentByte);
                    }
                    if (splashMain.cbOrders.Items.Count == 1)
                    {
                        splashMain.Display();
                    }
                }
            }
            Dispatcher.Invoke(() =>
            {
                progressbarCopyFile.Close();
            });
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                splashMain.Activate();
            }));
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
