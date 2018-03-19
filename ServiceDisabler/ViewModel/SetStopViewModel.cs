using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;

namespace ServiceDisabler
{
    internal class SetStopViewModel : BaseViewModel
    {
        public event Action<Service> Closed;

        public Service StopService { get; set; }
        public string SelectedItemName { get; set; }
        public DateTimeOffset? SelectedDate { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public DelegateCommand ApplyCommand { get; }

        public DelegateCommand CancelCommand { get; }

        public SetStopViewModel(Service service)
        {
            ApplyCommand = new DelegateCommand(SaveStopDateTime);
            CancelCommand = new DelegateCommand(() => this.CloseWindow());
            StopService = service;
        }

        private void SaveStopDateTime()
        {
            if (Closed != null)
            {
                var service = StopService;
                service.StopTime = new DateTimeOffset();
                Closed(service);
            }
        }
    }
}
