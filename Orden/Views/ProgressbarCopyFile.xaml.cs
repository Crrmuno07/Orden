using System;
using System.Windows;

namespace Orden.Views
{
    /// <summary>
    /// Lógica de interacción para ProgressbarCopyFile.xaml
    /// </summary>
    public partial class ProgressbarCopyFile : Window
    {
        public ProgressbarCopyFile()
        {
            InitializeComponent();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Window_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Activate();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (App.Current.MainWindow != null)
            {
                App.Current.MainWindow.ShowInTaskbar = true;
            }
        }
    }
}
