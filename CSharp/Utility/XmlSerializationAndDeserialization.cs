using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace CSharp.Utility
{
    /// <summary>
    /// http://stackoverflow.com/questions/14722492/xml-string-deserialization-into-c-sharp-object
    /// </summary>

    public class XmlSerializationAndDeserialization
    {
        public static void Test()
        {
            var r = radio.FromXmlFile("");
            Eg2();
        }

        public static void Eg2()
        {
            string xmlString = @"<AddressDetails>
                                    <HouseNo></HouseNo>
                                    <StreetName>Rohini</StreetName>
                                    <City>ee</City>
                                </AddressDetails>";
            XmlSerializer deserializer = new XmlSerializer(typeof(Address));
            TextReader reader = new StringReader(xmlString);
            Address address = (Address)deserializer.Deserialize(reader);
            reader.Close();
        }
    }

    #region Example 1

    [XmlRoot("radio")]
    public sealed class radio
    {
        [XmlElement("channel", Type = typeof(channel))]
        public channel[] channels { get; set; }

        [XmlElement("programme", Type = typeof(programme))]
        public programme[] programmes { get; set; }

        public radio()
        {
            channels = null;
            programmes = null;
        }

        public static radio FromXmlString(string xmlString)
        {
            var reader = new StringReader(xmlString);
            var serializer = new XmlSerializer(typeof(radio));
            var instance = (radio)serializer.Deserialize(reader);
            reader.Close();
            return instance;
        }

        public static radio FromXmlFile(string xmlPath)
        {
            using (FileStream fileStream = new FileStream("Document\\radio.xml", FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(radio));
                var instance = (radio)serializer.Deserialize(fileStream);
                return instance;
            }
        }
    }

    [Serializable]
    public class channel
    {
        [XmlAttribute("id")]
        public string id { get; set; }

        [XmlElement("display-name")]
        public string displayName { get; set; }

        [XmlElement("icon")]
        public string icon { get; set; }

        public channel() { }
    }

    [Serializable]
    public sealed class programme
    {
        [XmlAttribute("channel")]
        public string channel { get; set; }

        [XmlAttribute("start")]
        public string start { get; set; }

        [XmlAttribute("stop")]
        public string stop { get; set; }

        [XmlAttribute("duration")]
        public string duration { get; set; }

        [XmlElement("title")]
        public string title { get; set; }

        [XmlElement("desc")]
        public string desc { get; set; }

        [XmlElement("category")]
        public string category { get; set; }

        [XmlElement("date")]
        public string date { get; set; }

        public programme()
        {
        }
    }

    #endregion

    [XmlRoot("AddressDetails")]
    public class Address
    {
        /// <summary>
        /// 1.Class variable/property should always be declared as public 
        /// 2.We need to have Default/ Non Parameterised Constructor in order to deserialize.
        /// </summary>
        [XmlElement("HouseNo")]
        public Nullable<int> HouseNo { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
    }
}
