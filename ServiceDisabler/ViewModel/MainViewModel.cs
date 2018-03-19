using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Windows.Controls;
using System.Windows.Threading;
using Prism.Commands;
using ServiceDisabler.Helpers;
using ServiceDisabler.Services;

namespace ServiceDisabler
{
    internal class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Service> _services = new ObservableCollection<Service>();
        public ObservableCollection<Service> Services
        {
            get { return _services; }
            set
            {
                _services = value;
                RaisePropertyChanged(nameof(Services));
            }
        }

        public DelegateCommand ShowSetStopCommand { get; }

        internal DispatcherTimer updateServiceListTimer;

        //public StopSchedule StopSchedule { get; set; }
        public object SelectedItem { get; set; }
        public Service SelectedService { get; set; }

        public MainViewModel() : this(new ScheduleService())
        {
        }

        public MainViewModel(IScheduleService scheduleService)
        {
            ShowSetStopCommand = new DelegateCommand(ShowStopSettings);


            ScheduleService = scheduleService;

            

            Services = GetServiceList();
            //SelectedService = Services.FirstOrDefault(x => x.Name == ((ListViewItem)SelectedItem).Name);

            // load schedule
            StopSchedule = ScheduleService.GetSchedule();

            //  service list update timer setup
            updateServiceListTimer = new DispatcherTimer();
            updateServiceListTimer.Tick -= updateServiceListTimer_Tick;
            updateServiceListTimer.Tick += updateServiceListTimer_Tick;
            updateServiceListTimer.Interval = TimeSpan.FromSeconds(10);
            //updateServiceListTimer.Start();
            updateServiceListTimer_Tick(null, null);

            // stop service timer
            var stopServiceTimer = new DispatcherTimer();
            stopServiceTimer.Tick -= stopServiceTimer_Tick;
            stopServiceTimer.Tick += stopServiceTimer_Tick;
            stopServiceTimer.Interval = new TimeSpan(0, 0, 0, 1);
            //stopServiceTimer.Start();
        }

        private void updateServiceListTimer_Tick(object sender, EventArgs e)
        {
            Services = GetServiceList();
            foreach (var service in Services)
            {
                var records = StopSchedule.StopTimeRecords;
                var scheduledItem = records.Find(rec => rec.ServiceName == service.Name);
                if (scheduledItem != null)
                {
                    service.StopTime = scheduledItem.StopTime;
                }
            }
            //RaisePropertyChanged(nameof(Services));
            //RaisePropertyChanged(nameof(StopSchedule));
        }

        private void stopServiceTimer_Tick(object sender, EventArgs e)
        {
            var stopRecords = StopSchedule.StopTimeRecords.Where(
                x => x.StopTime > DateTimeOffset.Now &&
                     x.StopTime < DateTimeOffset.Now.AddSeconds(1));

            foreach (var rec in stopRecords)
            {
                var service = new ServiceController(rec.ServiceName);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }

        private static ObservableCollection<Service> GetServiceList()
        {
            var query = new SelectQuery("select * from Win32_Service");
            var result = new ObservableCollection<Service>();
            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (var o in searcher.Get())
                {
                    //var searcherProperties = o.Properties;
                    var service = (ManagementObject)o;
                    result.Add(new Service
                    {
                        Name = $"{service["Name"]}",
                        ProcessId = Convert.ToInt32($"{service["ProcessId"]}"),
                        StartMode = $"{service["StartMode"]}",
                        State = $"{service["State"]}",
                        Status = $"{service["Status"]}",

                        //StopTime = DateTime.Now,
                    });
                }
            }
            return result;
        }

        private void ShowStopSettings()
        {
            SelectedService = (Service)SelectedItem;

            var propertiesWindow = new StopSettingsViewModel();

            propertiesWindow.Closed += r =>
            {
                SelectedService.Name = r.Name;
                RaisePropertyChanged(nameof(Service.Name));

                SelectedService.ProcessId = r.ProcessId;
                RaisePropertyChanged(nameof(Service.ProcessId));

                SelectedService.StartMode = r.StartMode;
                RaisePropertyChanged(nameof(Service.StartMode));

                SelectedService.State = r.State;
                RaisePropertyChanged(nameof(Service.State));

                SelectedService.Status = r.Status;
                RaisePropertyChanged(nameof(Service.Status));

                SelectedService.StopTime = r.StopTime;
                RaisePropertyChanged(nameof(Service.StopTime));
            };

            propertiesWindow.Show(SelectedService);
        }
    }
}
