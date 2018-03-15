using System;
using System.Collections.Generic;
using System.Management;

namespace ServiceDisabler
{
    internal class ViewModelMain : ViewModelBase
    {
        public List<Service> Services { get; set; }

        public ViewModelMain()
        {
            Services = GetServiceList();
        }

        private List<Service> GetServiceList()
        {
            var query = new SelectQuery("select * from Win32_Service");
            var result = new List<Service>();
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

                        StopTime = DateTime.Now,
                    });
                }
            }
            return result;
        }
    }
}
