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

        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenPropertiesWindow(sender);
        }

        private void ServiceItemContextMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenPropertiesWindow(sender);
        }

        private void OpenPropertiesWindow(object sender)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
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
