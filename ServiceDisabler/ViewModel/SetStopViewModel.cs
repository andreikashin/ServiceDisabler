using System;
using Prism.Commands;
using ServiceDisabler.Helpers;

namespace ServiceDisabler
{
    internal class SetStopViewModel : BaseViewModel
    {
        public event Action<Service> Closed;

        public Service StopService { get; set; }
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
            if (Closed != null)
            {
                var service = StopService;
                var newDateTime = new DateTimeOffset(SelectedDate);
                newDateTime.SetTime(Hour, Minute, Second);
                service.StopTime = newDateTime;

                Closed(service);
            }
        }
    }
}
