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
