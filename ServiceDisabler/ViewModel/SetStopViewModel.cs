using System;
using System.Linq;
using Prism.Commands;
using ServiceDisabler.Helpers;

namespace ServiceDisabler
{
    internal class SetStopViewModel : MainViewModel
    {
        public event Action<Service> Closed;

        private Service _stopService;
        public Service StopService
        {
            get { return _stopService; }
            set
            {
                _stopService = value;
                RaisePropertyChanged(nameof(StopService));
            }
        }

        public string SelectedItemName { get; set; }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(nameof(SelectedDate));
            }
        }

        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public DelegateCommand ApplyCommand { get; }

        public DelegateCommand CancelCommand { get; }

        public SetStopViewModel(Service service)
        {
            ApplyCommand = new DelegateCommand(SaveStopDateTime);
            CancelCommand = new DelegateCommand(() => Closed?.Invoke(null));
            StopService = service;
            SelectedDate = DateTime.Now;
        }

        private void SaveStopDateTime()
        {
            if (Closed != null && StopService != null)
            {
                var service = StopService;
                var newDateTime = new DateTimeOffset(SelectedDate);
                newDateTime.SetTime(Hour, Minute, Second);
                service.StopTime = newDateTime;
                if (StopSchedule.StopTimeRecords.Any(x => x.ServiceName == service.Name))
                {
                    StopSchedule.StopTimeRecords.Distinct().ToList().Add(
                        new StopTimeRecord
                        {
                            ServiceName = service.Name,
                            StopTime = service.StopTime
                        });
                }
                RaisePropertyChanged(nameof(StopSchedule));

                Closed?.Invoke(service);
            }
        }
    }
}
