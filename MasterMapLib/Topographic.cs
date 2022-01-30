using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;



namespace MasterMapLib
{

    [XmlRoot("topographicMember", Namespace = osgbNS)]
    public class TopographicMember
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

        [XmlElement("TopographicLine", Namespace = osgbNS)]
        public TopographicLine topographicLine;

        [XmlElement("TopographicArea", Namespace = osgbNS)]
        public TopographicArea topographicArea;

        [XmlElement("TopographicPoint", Namespace = osgbNS)]
        public TopographicPoint topographicPoint;


    }


    [XmlRoot("TopographicLine", Namespace = osgbNS)]
    public class TopographicLine : BaseFeature
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

    }


    [XmlRoot("TopographicArea", Namespace = osgbNS)]
    public class TopographicArea : BaseFeature
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

    }

    [XmlRoot("TopographicPoint", Namespace = osgbNS)]
    public class TopographicPoint : BaseFeature
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

    }


}
