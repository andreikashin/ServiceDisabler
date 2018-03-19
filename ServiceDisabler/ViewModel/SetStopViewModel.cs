using System;
using System.Linq;
using Prism.Commands;
using ServiceDisabler.Helpers;

namespace ServiceDisabler
{
    internal class SetStopViewModel : MainViewModel
    {
        public event Action<Service> Closed;

        public Service StopService
        {
            get { return _stopService; }
            set
            {
                _stopService = value;
                RaisePropertyChanged(nameof(StopService));
            }
        }
        private Service _stopService;

        public string SelectedItemName { get; set; }

        /// <summary>
        /// Selected date
        /// </summary>
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(nameof(SelectedDate));
            }
        }
        private DateTime _selectedDate;

        /// <summary>
        /// User Hours
        /// </summary>
        public int Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                RaisePropertyChanged(nameof(Hour));
            }
        }
        private int _hour;

        /// <summary>
        /// User Minutes
        /// </summary>
        public int Minute
        {
            get { return _minute; }
            set
            {
                _minute = value;
                RaisePropertyChanged(nameof(Minute));
            }
        }
        private int _minute;

        /// <summary>
        /// User Seconds
        /// </summary>
        public int Second
        {
            get { return _second; }
            set
            {
                _second = value;
                RaisePropertyChanged(nameof(Second));
            }
        }
        private int _second;

        // Commands
        public DelegateCommand ApplyCommand { get; }
        public DelegateCommand CancelCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public SetStopViewModel(Service service)
        {
            ApplyCommand = new DelegateCommand(SaveStopDateTime);
            CancelCommand = new DelegateCommand(() => Closed?.Invoke(null));
            StopService = service;
            SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Store stop schedule for service in collection
        /// </summary>
        private void SaveStopDateTime()
        {
            if (Closed != null && StopService != null)
            {
                var service = StopService;
                var newDateTime = new DateTime(
                    SelectedDate.Year,
                    SelectedDate.Month,
                    SelectedDate.Day,
                    Hour, Minute, Second, 
                    DateTimeKind.Local);
                service.StopTime = new DateTimeOffset(newDateTime);
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
