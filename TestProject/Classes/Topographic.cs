using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;



namespace TestProject
{

    [XmlRoot("topographicMember", Namespace = osgbNS)]
    public class TopographicMember
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

        [XmlElement("TopographicLine", Namespace = osgbNS)]
        public TopographicLine topographicLine;



    }


    [XmlRoot("TopographicLine", Namespace = osgbNS)]
    public class TopographicLine : BaseFeature
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";




    }





}
