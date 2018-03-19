using System.Windows;

namespace ServiceDisabler
{
    internal class StopSchedulerWindowManager : BaseViewModel
    {
        public StopSchedulerWindowManager()
        {
            Visibility = Visibility.Collapsed;
            Content = null;
        }

        private static StopSchedulerWindowManager _instance;

        public static StopSchedulerWindowManager Instance =>
            _instance ??
            (_instance = new StopSchedulerWindowManager());

        private FrameworkElement _content;
        public FrameworkElement Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                RaisePropertyChanged(nameof(Visibility));
            }
        }

        /// <summary>
        /// Show Scheduler
        /// </summary>
        /// <param name="content"></param>
        public void ShowSchedulerWindow(FrameworkElement content)
        {
            Content = content;
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Close scheduler
        /// </summary>
        public void CloseSchedulerWindow()
        {
            Visibility = Visibility.Collapsed;
            Content = null;
        }
    }
}
