using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServiceDisabler.Helpers;

namespace ServiceDisabler.Services
{
    class ScheduleService : IScheduleService
    {
        public StopSchedule GetSchedule()
        {
            var schedule = new StopSchedule
            {
                StopTimeRecords = new List<StopTimeRecord>
                {
                    new StopTimeRecord
                    {
                        ServiceName = "test",
                        StopTime = DateTimeOffset.Now
                    }, 
                }
            };

            var directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var filename = Properties.Settings.Default["ScheduleFilename"].ToString();
            var fullPath = Path.Combine(directory, filename);
            if (File.Exists(fullPath))
            {
                schedule = XmlHelper.FromXmlFile<StopSchedule>(fullPath);
                return schedule;
            }

            var xml = XmlHelper.ToXml(schedule);
            XmlHelper.ToXmlFile(xml, fullPath);

            return schedule;
        }

        public void UpdateSchedule(StopSchedule currentSchedule, StopTimeRecord[] newStopTimeRecords)
        {
            var newRecList = newStopTimeRecords.ToList();

            foreach (var newRec in newRecList)
            {
                var curr = currentSchedule
                    .StopTimeRecords
                    .ToList()
                    .Find(x => x.ServiceName == newRec.ServiceName);

                if (curr != null)
                {
                    curr.StopTime = newRec.StopTime;
                }
                else
                {
                    currentSchedule.StopTimeRecords.Add(newRec);
                }
            }
        }

        public void SaveSchedule(StopSchedule currentSchedule)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var filename = Properties.Settings.Default["ScheduleFilename"].ToString();
            var fullPath = Path.Combine(directory, filename);
            if (Directory.Exists(directory))
            {
                XmlHelper.ToXmlFile(currentSchedule, fullPath);
            }
        }
    }
}
