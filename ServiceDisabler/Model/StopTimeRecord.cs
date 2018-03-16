using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDisabler
{
    internal class StopTimeRecord
    {
        public string ServiceName { get; set; }
        public DateTimeOffset StopTime { get; set; }
    }
}
