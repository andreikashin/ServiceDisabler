using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ServiceDisabler.Services;

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
            var viewModel = new MainViewModel();
            DataContext = viewModel;
            Closing += viewModel.OnWindowClosing;
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
    }
}
