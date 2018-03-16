using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ServiceDisabler.View;

namespace ServiceDisabler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenPropertiesWindow();
        }

        private void ServiceItemContextMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenPropertiesWindow();
        }

        private void OpenPropertiesWindow()
        {
            var item = ServiceListView.SelectedItem;
            if (item != null)
            {
                var popup = new ServiceProperties();
                popup.ShowDialog();
                //var selectedService = (Service)ServiceListView.SelectedItem;
                //var service = new ServiceController(selectedService.Name);
                //service.Stop();
                //service.WaitForStatus(ServiceControllerStatus.Stopped);



            }
        }
    }
}
