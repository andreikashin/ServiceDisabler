using System;
using System.Globalization;
using System.Xml.Serialization;

namespace ServiceDisabler
{
    public class StopTimeRecord
    {
        public string ServiceName { get; set; }

        [XmlElement("StopTime")]
        public string StopTimeForXml
        {
            get { return StopTime.ToString(CultureInfo.InvariantCulture); }
            set { StopTime = DateTimeOffset.Parse(value, CultureInfo.InvariantCulture); }
        }

        [XmlIgnore]
        public DateTimeOffset StopTime { get; set; }
    }
}
