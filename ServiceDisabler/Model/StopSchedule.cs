using System.Collections.Generic;
using System.Xml.Serialization;

namespace ServiceDisabler
{
    [XmlRoot("StopSchedule")]
    public class StopSchedule
    {
        [XmlElement("Record")]
        public List<StopTimeRecord> StopTimeRecords { get; set; }
    }
}
