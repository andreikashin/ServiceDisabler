using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDisabler.Services
{
    class ScheduleService : IScheduleService
    {
        public StopSchedule GetSchedule()
        {
            throw new NotImplementedException();
        }

        public void UpdateSchedule(List<StopTimeRecord> stopTimeRecords)
        {
            throw new NotImplementedException();
        }
    }
}
