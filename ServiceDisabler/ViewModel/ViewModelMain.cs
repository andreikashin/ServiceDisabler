using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using ServiceDisabler.Services;

namespace ServiceDisabler
{
    internal class ViewModelMain : ViewModelBase
    {
        private readonly IScheduleService _scheduleService;

        public ObservableCollection<Service> Services { get; set; }
        public StopSchedule StopSchedule { get; set; }


        public ViewModelMain() : this(new ScheduleService())
        {
        }

        public ViewModelMain(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;

            Services = GetServiceList();

            // load schedule
            StopSchedule = _scheduleService.GetSchedule();

            //  service list update timer setup
            var updateServiceListTimer = new System.Windows.Threading.DispatcherTimer();
            updateServiceListTimer.Tick += updateServiceListTimer_Tick;
            updateServiceListTimer.Interval = new TimeSpan(0, 0, 0, 10);
            updateServiceListTimer.Start();
            updateServiceListTimer_Tick(null, null);

            // stop service timer
            var stopServiceTimer = new System.Windows.Threading.DispatcherTimer();
            stopServiceTimer.Tick += stopServiceTimer_Tick;
            stopServiceTimer.Interval = new TimeSpan(0, 0, 0, 1);
            stopServiceTimer.Start();
        }

        private void updateServiceListTimer_Tick(object sender, EventArgs e)
        {
            Services = GetServiceList();
            foreach (var service in Services)
            {
                var records = StopSchedule.StopTimeRecords.ToList();
                var scheduledItem = records.Find(rec => rec.ServiceName == service.Name);
                if (scheduledItem != null)
                {
                    service.StopTime = scheduledItem.StopTime;
                }
            }
            RaisePropertyChanged(nameof(Services));
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

        private ObservableCollection<Service> GetServiceList()
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
    }
}
