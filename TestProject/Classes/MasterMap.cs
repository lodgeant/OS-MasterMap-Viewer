using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;


namespace TestProject
{
    public class MasterMap
    {        
        public XDocument featureXML = new XDocument();
        public int featureXML_nodeCount = 0;
        public XmlDocument featureXMLDoc = new XmlDocument();
        public int featureXMLDoc_nodeCount = 0;
        public FeatureCollection featureCollection = new FeatureCollection();        
        public Dictionary<string, int> Member_Metrics = new Dictionary<string, int>();
        public Dictionary<string, int> Feature_Metrics = new Dictionary<string, int>();

        public enum MemberType
        {
            cartographicMember,
            topographicMember
        }

        public enum FeatureType
        {
            CartographicText,
            CartographicSymbol,
            TopographicLine
        }


        public int LoadFeaturesFromXMLFile(string filePath)
        {
            try
            {
                // ** Check file exists **                
                if (File.Exists(filePath) == false)
                {
                    throw new Exception("File does not exist.");
                }

                // ** Load XML to XDocument **
                featureXMLDoc.Load(filePath);                
                featureXMLDoc_nodeCount = featureXMLDoc.ChildNodes.Count;
                featureXML = XDocument.Load(filePath);
                featureXML_nodeCount = featureXML.Descendants().Count();

                // ** Generate FeatureCollection **
                string xmlString = File.ReadAllText(filePath);
                featureCollection = new FeatureCollection().DeserialiseFromXMLString(xmlString);

                return 0;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public int UpdateMetrics()
        {            
            try
            {
                // ** Add namespace manager **
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(featureXMLDoc.NameTable);
                nsmgr.AddNamespace("osgb", "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb");


                // ** Generate Member_Metrics **                
                foreach (string member in Enum.GetNames(typeof(MemberType)))
                {
                    if (Member_Metrics.ContainsKey(member) == false) Member_Metrics.Add(member, 0);
                    Member_Metrics[member] = featureXMLDoc.SelectNodes("//osgb:" + member, nsmgr).Count;
                }

                // ** Generate Deature **                
                foreach (string feature in Enum.GetNames(typeof(FeatureType)))
                {
                    if (Feature_Metrics.ContainsKey(feature) == false) Feature_Metrics.Add(feature, 0);
                    Feature_Metrics[feature] = featureXMLDoc.SelectNodes("//osgb:" + feature, nsmgr).Count;
                }

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

    }
}
