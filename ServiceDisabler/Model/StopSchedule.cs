using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ServiceDisabler
{
    [XmlRoot("StopSchedule")]
    public class StopSchedule
    {
        [XmlElement("Record")]
        public StopTimeRecord[] StopTimeRecords { get; set; }
    }
}
