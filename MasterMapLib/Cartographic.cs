using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;



namespace MasterMapLib
{

    [XmlRoot("cartographicMember", Namespace = osgbNS)]
    public class CartographicMember
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

        [XmlElement("CartographicText", Namespace = osgbNS)]
        public CartographicText cartographicText;

        [XmlElement("CartographicSymbol", Namespace = osgbNS)]
        public CartographicSymbol cartographicSymbol;

    }


    [XmlRoot("CartographicText", Namespace = osgbNS)]
    public class CartographicText : BaseFeature
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

        [XmlElement("textString", Namespace = osgbNS)]
        public string textString;

    }


    [XmlRoot("CartographicSymbol", Namespace = osgbNS)]
    public class CartographicSymbol : BaseFeature
    {
        private const string osgbNS = "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb";

    }


}
