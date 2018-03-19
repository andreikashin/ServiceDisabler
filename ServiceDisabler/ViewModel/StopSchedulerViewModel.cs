using System;
using ServiceDisabler.Helpers;

namespace ServiceDisabler
{
    internal class StopSchedulerViewModel : BaseViewModel
    {
        public event Action<Service> Closed;

        // Commands
        public RelayCommand ApplyClickCommand { get; set; }
        public RelayCommand CancelClickCommand { get; set; }

        public string SelectedItemName { get; set; }

        /// <summary>
        /// Display Hours
        /// </summary>
        public string Hour
        {
            get { return _hour; }
            set
            {
                if (_hour != value)
                {
                    _hour = value;
                    RaisePropertyChanged(nameof(Hour));
                }
            }
        }
        private string _hour;

        /// <summary>
        /// Display minutes
        /// </summary>
        public string Minute
        {
            get { return _minute; }
            set
            {
                if (_minute != value)
                {
                    _minute = value;
                    RaisePropertyChanged(nameof(Minute));
                }
            }
        }
        private string _minute;

        /// <summary>
        /// Display Seconds
        /// </summary>
        public string Second
        {
            get { return _second; }
            set
            {
                if (_second != value)
                {
                    _second = value;
                    RaisePropertyChanged(nameof(Second));
                }
            }
        }
        private string _second;

        /// <summary>
        /// Selected Date
        /// </summary>
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (!_selectedDate.Equals(value))
                {
                    _selectedDate = value;
                    RaisePropertyChanged(nameof(SelectedDate));
                }
            }
        }
        private DateTime _selectedDate;
        
        /// <summary>
        /// Show modal scheduler window
        /// </summary>
        /// <param name="service"></param>
        public void Show(Service service)
        {
            var vm = new SetStopViewModel(service);
            vm.Closed += StopSchedulerWindow_Closed;
            StopSchedulerWindowManager.Instance.ShowSchedulerWindow(new SetStopView { DataContext = vm });
        }

        /// <summary>
        /// On scheduler window close event
        /// </summary>
        /// <param name="service"></param>
        private void StopSchedulerWindow_Closed(Service service)
        {
            if (service == null)
            {
                StopSchedulerWindowManager.Instance.CloseSchedulerWindow();
                return;
            }
            Closed?.Invoke(service);
            StopSchedulerWindowManager.Instance.CloseSchedulerWindow();

            RaisePropertyChanged(nameof(Services));
        }
    }
}
