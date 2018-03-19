using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            StopSettingsWindow.DataContext = StopSettingsWindowManager.Instance;
            DataContext = new MainViewModel();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;

            var vm = item?.DataContext as MainViewModel;

            if (vm?.ShowSetStopCommand != null && 
                vm.ShowSetStopCommand.CanExecute())
            {
                vm.ShowSetStopCommand.Execute();
            }
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
                var name = ((ListViewItem) item).Name;
                var properties = new SetStopView
                {
                    DataContext = new StopSettingsViewModel()
                };

                //var popup = new StopSettingsWindow();
                //popup.ShowDialog();
                //var selectedService = (Service)ServiceListView.SelectedItem;
                //var service = new ServiceController(selectedService.Name);
                //service.Stop();
                //service.WaitForStatus(ServiceControllerStatus.Stopped);



            }
        }

        internal MainViewModel Vm => (MainViewModel)DataContext;
        
    }
}
