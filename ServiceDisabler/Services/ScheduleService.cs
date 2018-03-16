using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ServiceDisabler.Helpers;

namespace ServiceDisabler.Services
{
    class ScheduleService : IScheduleService
    {
        public StopSchedule GetSchedule()
        {
            var schedule = new StopSchedule { StopTimeRecords = new StopTimeRecord[] { } };

            var directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var filename = Properties.Settings.Default["ScheduleFilename"].ToString();
            var fullPath = Path.Combine(directory, filename);
            if (File.Exists(fullPath))
            {
                schedule = XmlHelper.FromXmlFile<StopSchedule>(fullPath);
                return schedule;
            }

            return schedule;
        }

        public void UpdateSchedule(List<StopTimeRecord> stopTimeRecords)
        {
            throw new NotImplementedException();
        }
    }
}
