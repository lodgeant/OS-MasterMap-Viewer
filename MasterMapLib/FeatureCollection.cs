using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;



namespace MasterMapLib
{
    [XmlRoot("FeatureCollection", Namespace = osgbNS)]
    public class FeatureCollection
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";
        private const string gmlNS = "http://www.opengis.net/gml";

        [XmlAttribute("fid", Namespace = osgbNS)]
        public string fid;

        [XmlElement("description", Namespace = gmlNS)]
        public string description;

        [XmlElement("queryTime", Namespace = osgbNS)]
        public string queryTime;

        [XmlElement("cartographicMember", Namespace = osgbNS)]
        public List<CartographicMember> cartographicMembers;

        [XmlElement("topographicMember", Namespace = osgbNS)]
        public List<TopographicMember> topographicMembers;

        [XmlElement("boundaryMember", Namespace = osgbNS)]
        public List<BoundaryMember> boundaryMembers;


        public FeatureCollection() { }

        public FeatureCollection DeserialiseFromXMLString(string XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (FeatureCollection)serializer.Deserialize(reader);
            }
        }


    }

    



   

    public class BaseFeature
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

        [XmlAttribute("fid", Namespace = osgbNS)]
        public string fid;

        [XmlElement("featureCode", Namespace = osgbNS)]
        public string featureCode;

        [XmlElement("version", Namespace = osgbNS)]
        public int version;

        [XmlElement("theme", Namespace = osgbNS)]
        public List<string> theme;

    }



}
