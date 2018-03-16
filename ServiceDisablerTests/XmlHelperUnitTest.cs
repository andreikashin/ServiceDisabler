using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceDisabler;
using ServiceDisabler.Helpers;

namespace ServiceDisablerTests
{
    [TestClass]
    public class XmlHelperUnitTest
    {
        [TestMethod]
        public void FromObjectToXmlConversionTest()
        {
            // Arrange
            var schedule = new StopSchedule
            {
                StopTimeRecords = new[]
                {
                    new StopTimeRecord
                    {
                        ServiceName = "test1",
                        StopTime = new DateTimeOffset(2000,1,1,0,0,0, new TimeSpan(2,0,0))
                    },
                    new StopTimeRecord
                    {
                        ServiceName = "test2",
                        StopTime = new DateTimeOffset(2000,1,1,0,0,0, new TimeSpan(2,0,0))
                    },
                }
            };

            var expected = @"
<?xml version=""1.0"" encoding=""utf-16""?>
<StopSchedule xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
<Record><ServiceName>test1</ServiceName><StopTime>01/01/2000 00:00:00 +02:00</StopTime></Record>
<Record><ServiceName>test2</ServiceName><StopTime>01/01/2000 00:00:00 +02:00</StopTime></Record>
</StopSchedule>";

            // Act
            var xml = XmlHelper.ToXml(schedule);
            xml = xml.Replace("\n", string.Empty);
            xml = xml.Replace("\r", string.Empty);
            xml = xml.Replace("\t", string.Empty);

            expected = expected.Replace("\n", string.Empty);
            expected = expected.Replace("\r", string.Empty);
            expected = expected.Replace("\t", string.Empty);

            // Assert
            Assert.AreEqual(expected, xml);
        }

        [TestMethod]
        public void FromXmlToObjectConversionTest()
        {
            // Arrange
            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?>
                        <StopSchedule xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                        <Record>
                        <ServiceName>test1</ServiceName>
                        <StopTime>01/02/2000 01:02:03 +02:00</StopTime>
                        </Record>
                        <Record>
                        <ServiceName>test2</ServiceName>
                        <StopTime>01/02/2000 01:02:03 +00:00</StopTime>
                        </Record>
                        </StopSchedule>";

            // Act
            var sch = XmlHelper.FromXml<StopSchedule>(xml);

            // Assert
            Assert.AreEqual(2, sch.StopTimeRecords.Length);
            Assert.AreEqual("test1", sch.StopTimeRecords[0].ServiceName);
            Assert.AreEqual("test2", sch.StopTimeRecords[1].ServiceName);
        }
    }
}
