using System;

namespace ServiceDisabler
{
    internal class Service
    {
        public string Name { get; set; }
        public int ProcessId { get; set; }
        public string StartMode { get; set; } // Auto | Manual | Disabled
        public string State { get; set; } // Running | Stopped
        public string Status { get; set; } // OK | UNKNOWN

        public DateTimeOffset? StopTime { get; set; }
    }
}
