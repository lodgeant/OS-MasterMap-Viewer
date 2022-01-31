using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Drawing;
using System.Xml.XPath;



namespace MasterMapLib
{
    public class MasterMap
    {
        public XmlDocument featureCollectionXML = new XmlDocument();        
        public int featureCount = 0;
        XmlNamespaceManager nsmgr;
        public Dictionary<string, int> Member_Metrics = new Dictionary<string, int>();
        public Dictionary<string, int> Feature_Metrics = new Dictionary<string, int>();
        public Bitmap mapImage;        
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

        private XmlNodeList nodeList_TopographicArea;
        private XmlNodeList nodeList_TopographicLine;
        private List<XmlNode> OSFeatureList = new List<XmlNode>();

        //public XDocument featureXML = new XDocument();
        //public FeatureCollection featureCollection = new FeatureCollection();        
        //public HashSet<string> TOIDList = new HashSet<string>();
        //private Dictionary<string, OSFeature> OSFeatureDict = new Dictionary<string, OSFeature>();
        //private List<OSFeature> OSFeatureList = new List<OSFeature>();



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


        public MasterMap()
        {
            nsmgr = new XmlNamespaceManager(featureCollectionXML.NameTable);
            nsmgr.AddNamespace("osgb", "http://www.ordnancesurvey.co.uk/xml/namespaces/osgb");
            nsmgr.AddNamespace("gml", "http://www.opengis.net/gml");
        }



        public int LoadXML(string filePath)
        {            
            try
            {
                // ** Check file exists **
                if (File.Exists(filePath) == false) throw new Exception("File does not exist.");
                
                // ** Load XML **
                featureCollectionXML.Load(filePath);

                // ** Update hit lists **
                nodeList_TopographicArea = featureCollectionXML.SelectNodes("//osgb:TopographicArea", nsmgr);
                nodeList_TopographicLine = featureCollectionXML.SelectNodes("//osgb:TopographicLine", nsmgr);
                OSFeatureList = nodeList_TopographicArea.Cast<XmlNode>().Concat<XmlNode>(nodeList_TopographicLine.Cast<XmlNode>()).ToList();


                //OSFeatureDict = new Dictionary<string, OSFeature>();
                //OSFeatureList = new List<OSFeature>();
                //foreach (XmlNode node in allNodes)
                //{
                //    // ** Get Base Variables **
                //    OSFeature f = new OSFeature();
                //    f.fid = node.SelectSingleNode("@fid", nsmgr).InnerXml;
                //    f.featureType = node.LocalName;
                //    f.descriptiveGroup = node.SelectSingleNode("osgb:descriptiveGroup", nsmgr).InnerXml;
                //    string make = "";
                //    if (node.SelectSingleNode("osgb:make", nsmgr) != null) make = node.SelectSingleNode("osgb:make", nsmgr).InnerXml;
                //    f.make = make;
                //    OSFeatureDict.Add(f.fid, f);
                //    OSFeatureList.Add(f);
                //}


                // ** Generate FeatureCollection **
                //string xmlString = File.ReadAllText(filePath);
                //featureCollection = new FeatureCollection().DeserialiseFromXMLString(featureCollectionXML.OuterXml);

                //featureXML = XDocument.Load(filePath);
                //featureXML_nodeCount = featureXML.Descendants().Count();

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
                // ** Reset metrics dictionaries **               
                Member_Metrics = new Dictionary<string, int>();
                Feature_Metrics = new Dictionary<string, int>();

                // ** Update feature count **                            
                XPathNavigator nav = featureCollectionXML.CreateNavigator();
                foreach (string member in Enum.GetNames(typeof(MemberType)))
                {
                    int memberCount = Convert.ToInt32(nav.Evaluate("count(//osgb:" + member + ")", nsmgr));
                    //int memberCount = featureCollectionXML.SelectNodes("//osgb:" + member, nsmgr).Count;
                    featureCount += memberCount;

                    if (Member_Metrics.ContainsKey(member) == false) Member_Metrics.Add(member, 0);
                    Member_Metrics[member] = memberCount;
                }

                // ** Generate Feature_Metrics **                
                foreach (string feature in Enum.GetNames(typeof(FeatureType)))
                {
                    int featureCount = Convert.ToInt32(nav.Evaluate("count(//osgb:" + feature + ")", nsmgr));
                    if (Feature_Metrics.ContainsKey(feature) == false) Feature_Metrics.Add(feature, 0);
                    Feature_Metrics[feature] = featureCount;
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
                // ** Get Topo data - TO BE REPLACED
                //XmlNodeList nodeList_TopographicArea = featureCollectionXML.SelectNodes("//osgb:TopographicArea", nsmgr);
                //XmlNodeList nodeList_TopographicLine = featureCollectionXML.SelectNodes("//osgb:TopographicLine", nsmgr);
                //var nodeList = nodeList_TopographicArea.Cast<XmlNode>().Concat<XmlNode>(nodeList_TopographicLine.Cast<XmlNode>());
                //var nodeList = allNodes;


                // ** Generate mapImage **
                mapImage = new Bitmap(5000, 5000);
                float startPosX = 290000;
                //float startPosX = 287000;
                float startPosY = 90000;
                int failedTOIDCount = 0;
                int unclassifiedCount = 0;
                using (var g = Graphics.FromImage(mapImage))
                {                    
                    foreach(XmlNode node in OSFeatureList)
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

                            //string fid = node.SelectSingleNode("@fid", nsmgr).InnerXml;
                            //string featureType = OSFeatureDict[fid].featureType;
                            //string descriptiveGroup = OSFeatureDict[fid].descriptiveGroup;
                            //string make = OSFeatureDict[fid].make;
                            
                            // ** TopographicArea **
                            if (featureType.Equals("TopographicArea"))
                            {
                                // ** Get Specific Variables **
                                string[] lineString = node.SelectSingleNode("osgb:polygon/gml:Polygon/gml:outerBoundaryIs/gml:LinearRing/gml:coordinates", nsmgr).InnerXml.Split(" ");

                                // ** Derive brush to use for Feature **
                                string fillStyle = GetStyleName(featureType, descriptiveGroup, descriptiveTerm, make);
                                if (fillStyle.Equals("unclassifiedFill")) unclassifiedCount += 1;

                                // ** Generate polygon points and draw **                                
                                List<PointF> ptsList = ConvertStringArrayToPointFList(lineString, startPosX, startPosY);
                                g.FillPolygon(new SolidBrush(ColorTranslator.FromHtml("#" + stylePalette[fillStyle])), ptsList.ToArray());
                                //g.DrawPolygon(new Pen(Color.Gray, 1), ptsList.ToArray());
                            }
                            else if (featureType.Equals("TopographicLine"))
                            {
                                // ** Get Specific Variables **
                                string[] lineString = node.SelectSingleNode("osgb:polyline/gml:LineString/gml:coordinates", nsmgr).InnerXml.Split(" ");

                                // ** Generate line points and draw **                                
                                List<PointF> ptsList = ConvertStringArrayToPointFList(lineString, startPosX, startPosY);
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

        public List<PointF> ConvertStringArrayToPointFList(string[] lineString, float startPosX, float startPosY)
        {
            List<PointF> ptsList = new List<PointF>();
            foreach (string pointString in lineString)
            {
                string[] coordsString = pointString.Split(",");
                float xPos = float.Parse(coordsString[0]) - startPosX;
                float yPos = float.Parse(coordsString[1]) - startPosY;
                ptsList.Add(new PointF(xPos, yPos));
            }
            return ptsList;
        }


    }


    //public class OSFeature
    //{
    //    public string fid;
    //    public string featureType;
    //    public string descriptiveGroup;
    //    public string make = "";
    //}

    

}