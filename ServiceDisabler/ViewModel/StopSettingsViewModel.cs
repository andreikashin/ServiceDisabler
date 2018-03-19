using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ServiceDisabler.Helpers;

namespace ServiceDisabler
{
    internal class StopSettingsViewModel : BaseViewModel
    {
        public event Action<Service> Closed;


        public RelayCommand ApplyClickCommand { get; set; }
        public RelayCommand CancelClickCommand { get; set; }

        public string SelectedItemName { get; set; }

        private string _hour;
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

        private string _minute;
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

        private string _second;
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

        private DateTime _selectedDate;
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

        //public StopSettingsViewModel()
        //{
        //}

        public StopSettingsViewModel()
        {
            
            //ApplyClickCommand = new RelayCommand(Apply);
            //CancelClickCommand = new RelayCommand(Cancel);
            //SelectedItemName = ((Service)selectedItem).Name;
        }

        private void Apply(object parameter)
        {
            var name = SelectedItemName == string.Empty
                ? "Unknown"
                : SelectedItemName;

            var date = SelectedDate
                .Equals(DateTime.MinValue)
                ? DateTime.Today
                : SelectedDate;

            if (Hour != string.Empty)
            {
                date = date.AddHours(Convert.ToInt32(Hour));
            }

            if (Minute != string.Empty)
            {
                date = date.AddHours(Convert.ToInt32(Minute));
            }

            if (Second != string.Empty)
            {
                date = date.AddHours(Convert.ToInt32(Second));
            }

            var newSchedule = new[]
            {
                new StopTimeRecord
                {
                    ServiceName = name,
                    StopTime = new DateTimeOffset(date)
                },
            };

            ScheduleService.UpdateSchedule(StopSchedule, newSchedule);
            RaisePropertyChanged(nameof(StopSchedule));
            CloseWindow();
        }

        private void Cancel(object parameter)
        {
            CloseWindow();
        }

        public void Show(Service service)
        {
            var vm = new SetStopViewModel(service);
            vm.Closed += StopSettingsWindow_Closed;
            StopSettingsWindowManager.Instance.ShowSettingsWindow(new SetStopView { DataContext = vm });
        }

        void StopSettingsWindow_Closed(Service service)
        {
            Closed?.Invoke(service);
            StopSettingsWindowManager.Instance.CloseSettingsWindow();
        }
    }
}
