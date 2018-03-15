using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDisabler.Model
{
    internal class Service
    {
        public string Name { get; set; }
        public int ProcessId { get; set; }
        public string StartMode { get; set; } // Auto | Manual | Disabled
        public string State { get; set; } // Running | Stopped
        public string Status { get; set; } // OK | UNKNOWN

        public DateTime StopTime { get; set; }
    }
}
