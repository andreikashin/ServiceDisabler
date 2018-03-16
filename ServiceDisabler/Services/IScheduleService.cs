using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDisabler.Services
{
    internal interface IScheduleService
    {
        StopSchedule GetSchedule();
        void UpdateSchedule(StopTimeRecord[] stopTimeRecords);
    }
}
