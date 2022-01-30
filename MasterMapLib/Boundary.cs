using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;



namespace MasterMapLib
{

    [XmlRoot("boundaryMember", Namespace = osgbNS)]
    public class BoundaryMember
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

        [XmlElement("BoundaryLine", Namespace = osgbNS)]
        public BoundaryLine boundaryLine;

    }


    [XmlRoot("BoundaryLine", Namespace = osgbNS)]
    public class BoundaryLine : BaseFeature
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

    }


   


}
