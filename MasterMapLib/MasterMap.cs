using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Drawing;



namespace MasterMapLib
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
        public int featureCount = 0;
        public HashSet<string> TOIDList = new HashSet<string>();
        public Bitmap mapImage = null;

        


        public enum MemberType
        {
            cartographicMember,
            topographicMember,
            boundaryMember
        }

        public enum FeatureType
        {
            CartographicText,
            CartographicSymbol,
            TopographicLine,
            TopographicArea,
            TopographicPoint,
            BoundaryLine
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
                //featureXML = XDocument.Load(filePath);
                //featureXML_nodeCount = featureXML.Descendants().Count();

                // ** Generate FeatureCollection **
                //string xmlString = File.ReadAllText(filePath);
                //featureCollection = new FeatureCollection().DeserialiseFromXMLString(xmlString);

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int UpdateMetrics()
        {
            try
            {
                // ** Reset variables **
                featureCount = 0;
                Member_Metrics = new Dictionary<string, int>();
                Feature_Metrics = new Dictionary<string, int>();

                // ** Add namespace manager **
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(featureXMLDoc.NameTable);
                nsmgr.AddNamespace("osgb", "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb");


                // ** Generate Member_Metrics **                
                foreach (string member in Enum.GetNames(typeof(MemberType)))
                {
                    if (Member_Metrics.ContainsKey(member) == false) Member_Metrics.Add(member, 0);
                    Member_Metrics[member] = featureXMLDoc.SelectNodes("//osgb:" + member, nsmgr).Count;
                    featureCount += Member_Metrics[member];
                }

                // ** Generate Feature_Metrics **                
                foreach (string feature in Enum.GetNames(typeof(FeatureType)))
                {
                    if (Feature_Metrics.ContainsKey(feature) == false) Feature_Metrics.Add(feature, 0);
                    Feature_Metrics[feature] = featureXMLDoc.SelectNodes("//osgb:" + feature, nsmgr).Count;
                }

                // ** Update TOID value list **
                //TOIDList = new HashSet<string>();
                //var nodes = featureXMLDoc.SelectNodes("//@fid", nsmgr);
                //foreach(XmlNode node in nodes)
                //{
                //    TOIDList.Add(node.Value);
                //}


                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public int BuildMapImage()
        {
            try
            {
                mapImage = new Bitmap(5000, 5000);
                float startPosX = 290000;
                float startPosY = 90000;


                Color t = ColorTranslator.FromHtml("#FFD7C3");
                //Color t = Color.FromArgb(255, 215, 195);

                Brush b = new SolidBrush(t);

                // ** Add namespace manager **
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(featureXMLDoc.NameTable);
                nsmgr.AddNamespace("osgb", "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb");
                nsmgr.AddNamespace("gml", "http://www.opengis.net/gml");

                

                // ** Get Topo data - TO BE REPLACED
                XmlNodeList nodeList = featureXMLDoc.SelectNodes("//osgb:TopographicArea//gml:LinearRing/gml:coordinates", nsmgr);


                //List<string> failedTOIDList = new List<string>();
                int failedTOIDCount = 0;
                using (var graphics = Graphics.FromImage(mapImage))
                {                    
                    foreach(XmlNode node in nodeList)
                    {
                        try
                        {
                            string[] lineString = node.InnerXml.Split(" ");

                            List<PointF> ptsList = new List<PointF>();
                            foreach (string pointString in lineString)
                            {
                                float xPos = float.Parse(pointString.Split(",")[0]) - startPosX;
                                float yPos = float.Parse(pointString.Split(",")[1]) - startPosY;
                                ptsList.Add(new PointF(xPos, yPos));
                            }
                            //graphics.DrawLines(new Pen(Color.Black, 1), ptsList.ToArray());                            
                            graphics.FillPolygon(b, ptsList.ToArray());
                            graphics.DrawPolygon(new Pen(Color.Black, 1), ptsList.ToArray());
                        }
                        catch(Exception ex)
                        {
                            failedTOIDCount += 1;
                        }                        
                    }
                }


                return failedTOIDCount;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

    }
}