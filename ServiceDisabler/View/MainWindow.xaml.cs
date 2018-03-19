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
            StopSettingsWindow.DataContext = StopSchedulerWindowManager.Instance;
            var viewModel = new MainViewModel();
            DataContext = viewModel;
            Closing += viewModel.OnWindowClosing;
        }
    }
}
