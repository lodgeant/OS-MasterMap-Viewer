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
        public Dictionary<string, string> stylePalette = new Dictionary<string, string>()
        {
            {"structureFill","FFD7C3" },
            {"heritageFill","DCDCBE" },
            {"madeSurfaceFill","D2D2AA" },
            {"stepFill","D2D2AA" },
            {"roadFill","D7D7D7" },
            {"pathFill","CCCCCC" },
            {"railFill","CCCCCC" },
            {"buildingFill","FFDCAF" },
            {"glasshouseFill","FFCC99" },
            {"naturalSurfaceFill","D2FFB4" },
            {"naturalEnvironmentFill","DCFFBE" },
            {"inlandWaterFill","BEFFFF" },
            {"tidalWaterFill","BEFFFF" },
            {"multipleSurfaceFill","FFFFCC" },
            {"unclassifiedFill","FFFFFF" },
        };


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
                // ** Add namespace manager **
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(featureXMLDoc.NameTable);
                nsmgr.AddNamespace("osgb", "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb");
                nsmgr.AddNamespace("gml", "http://www.opengis.net/gml");

                // ** Get Topo data - TO BE REPLACED
                XmlNodeList nodeList_Area = featureXMLDoc.SelectNodes("//osgb:TopographicArea", nsmgr);
                XmlNodeList nodeList_Line = featureXMLDoc.SelectNodes("//osgb:TopographicLine", nsmgr);
                var nodeList = nodeList_Area.Cast<XmlNode>().Concat<XmlNode>(nodeList_Line.Cast<XmlNode>());


                // ** Generate mapImage **
                mapImage = new Bitmap(5000, 5000);
                float startPosX = 290000;
                float startPosY = 90000;
                int failedTOIDCount = 0;
                int unclassifiedCount = 0;
                using (var g = Graphics.FromImage(mapImage))
                {                    
                    foreach(XmlNode node in nodeList)
                    {
                        try
                        {
                            // ** Get Base Variables **                            
                            string featureType = node.LocalName;
                            string descriptiveGroup = "";
                            if (node.SelectSingleNode("osgb:descriptiveGroup", nsmgr) != null) descriptiveGroup = node.SelectSingleNode("osgb:descriptiveGroup", nsmgr).InnerXml;                            
                            string descriptiveTerm = "";
                            if (node.SelectSingleNode("osgb:descriptiveTerm", nsmgr) != null) descriptiveTerm = node.SelectSingleNode("osgb:descriptiveTerm", nsmgr).InnerXml;                            
                            string make = "";
                            if (node.SelectSingleNode("osgb:make", nsmgr) != null) make = node.SelectSingleNode("osgb:make", nsmgr).InnerXml;

                            // ** TopographicArea **
                            if (featureType.Equals("TopographicArea"))
                            {
                                // ** Get Specific Variables **
                                string[] lineString = node.SelectSingleNode("osgb:polygon/gml:Polygon/gml:outerBoundaryIs/gml:LinearRing/gml:coordinates", nsmgr).InnerXml.Split(" ");

                                // ** Derive brush to use for Feature **
                                string fillStyle = GetStyleName(featureType, descriptiveGroup, descriptiveTerm, make);
                                if (fillStyle.Equals("unclassifiedFill")) unclassifiedCount += 1;

                                // ** Generate polygon points and draw **
                                List<PointF> ptsList = new List<PointF>();
                                foreach (string pointString in lineString)
                                {
                                    float xPos = float.Parse(pointString.Split(",")[0]) - startPosX;
                                    float yPos = float.Parse(pointString.Split(",")[1]) - startPosY;
                                    ptsList.Add(new PointF(xPos, yPos));
                                }
                                g.FillPolygon(new SolidBrush(ColorTranslator.FromHtml("#" + stylePalette[fillStyle])), ptsList.ToArray());
                                //g.DrawPolygon(new Pen(Color.Gray, 1), ptsList.ToArray());
                            }
                            else if (featureType.Equals("TopographicLine"))
                            {
                                // ** Get Specific Variables **
                                string[] lineString = node.SelectSingleNode("osgb:polyline/gml:LineString/gml:coordinates", nsmgr).InnerXml.Split(" ");

                                // ** Generate line points **
                                List<PointF> ptsList = new List<PointF>();
                                foreach (string pointString in lineString)
                                {
                                    float xPos = float.Parse(pointString.Split(",")[0]) - startPosX;
                                    float yPos = float.Parse(pointString.Split(",")[1]) - startPosY;
                                    ptsList.Add(new PointF(xPos, yPos));
                                }

                                // ** Draw Line **
                                g.DrawLines(new Pen(Color.Black, 1), ptsList.ToArray());
                            }


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


        public string GetStyleName(string featureType, string descriptiveGroup, string descriptiveTerm, string make)
        {
            string styleName = "unclassifiedFill";
            if(featureType.Equals("TopographicArea"))
            {
                if(descriptiveGroup.Equals("Building"))
                {
                    styleName = "buildingFill";
                }
                else if (descriptiveTerm.Equals("Step"))
                {
                    styleName = "stepFill";
                }
                else if (descriptiveGroup.Equals("Glasshouse"))
                {
                    styleName = "glasshouseFill";
                }
                else if (descriptiveGroup.Equals("Historic Interest"))
                {
                    styleName = "heritageFill";
                }
                else if (descriptiveGroup.Equals("Inland Water"))
                {
                    styleName = "inlandWaterFill";
                }
                else if (descriptiveGroup.Equals("Natural Environment"))
                {
                    styleName = "naturalEnvironmentFill";
                }
                else if (descriptiveGroup.Equals("Path"))
                {
                    styleName = "pathFill";
                }
                else if (descriptiveGroup.Equals("Road Or Track"))
                {
                    styleName = "roadFill";
                }
                else if (descriptiveGroup.Equals("Structure"))
                {
                    styleName = "structureFill";
                }
                else if (descriptiveGroup.Equals("Tidal Water"))
                {
                    styleName = "tidalWaterFill";
                }
                else if (descriptiveGroup.Equals("Unclassified"))
                {
                    styleName = "unclassifiedFill";
                }
                else if (descriptiveGroup.Equals("Rail") && make.Equals("Manmade"))
                {
                    styleName = "railFill";
                }
                else if (make.Equals("Manmade"))
                {
                    styleName = "madeSurfaceFill";
                }
                else if (make.Equals("Natural"))
                {
                    styleName = "naturalSurfaceFill";
                }
                else if (make.Equals("Unknown"))
                {
                    styleName = "madeSurfaceFill";
                }
                else if (make.Equals("Multiple"))
                {
                    styleName = "multipleSurfaceFill";
                }
            }
            return styleName;
        }


    }
}