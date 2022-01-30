using NUnit.Framework;
using MasterMapLib;


namespace TestProject
{
    public class MasterMapTests
    {        
        private string testFeaturefilePath = @"C:\TONY\5436275-SX9090";
        private string testValidFeaturefilePath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\ValidFeatureCollection.xml";


        [Test]
        public void nothing()
        {
        }

        [Test]
        public void canCreateMasterMap()
        {
            MasterMap map = new MasterMap();
        }


        [Test]
        public void canLoadFeaturesFromXMLFile_fileNotExist()
        {
            MasterMap map = new MasterMap();

            int expected = -1;
            int result = map.LoadFeaturesFromXMLFile("");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void canLoadFeaturesFromXMLFile_usingFilePath_fileExists()
        {
            MasterMap map = new MasterMap();
            map.LoadFeaturesFromXMLFile(testValidFeaturefilePath);
            Assert.AreEqual(0, map.LoadFeaturesFromXMLFile(testValidFeaturefilePath));

            int expected = 0;
            int result = 0;
            //expected = 150;
            //result = map.featureXML_nodeCount;
            //Assert.AreEqual(expected, result);
            expected = 2;
            result = map.featureXMLDoc_nodeCount;
            Assert.AreEqual(expected, result);
        }
             

        [Test]
        public void LoadedXMLFileHasIncorrectXMLFormat()
        {
            string invalidFeatureXMLPath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\InvalidFeatureXML.xml";
            MasterMap map = new MasterMap();
            int expected = -1;
            int result = map.LoadFeaturesFromXMLFile(invalidFeatureXMLPath);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [Ignore("Ignore test")]
        public void LoadedXMLFileGeneratesFeatureCollection()
        {
            MasterMap map = new MasterMap();
            map.LoadFeaturesFromXMLFile(testValidFeaturefilePath);
            FeatureCollection fc = map.featureCollection;           
            Assert.AreEqual(3, fc.cartographicMembers.Count);
            Assert.AreEqual(3, fc.topographicMembers.Count);
            Assert.AreEqual(1, fc.boundaryMembers.Count);
        }

        [Test]
        public void canUpdateMetrics()
        {
            MasterMap map = new MasterMap();            
            int expected = 0;
            int result = map.UpdateMetrics();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Metrics_UpdatedCorrectly()
        {
            MasterMap map = new MasterMap();
            map.LoadFeaturesFromXMLFile(testValidFeaturefilePath);
            map.UpdateMetrics();
            int expected = 0;
            int result = 0;

            // ** Check total features count **
            expected = 7;
            result = map.featureCount;
            Assert.AreEqual(expected, result);

            // ** Check Member metrics **
            expected = 3;
            result = map.Member_Metrics["cartographicMember"];
            Assert.AreEqual(expected, result);
            expected = 3;
            result = map.Member_Metrics["topographicMember"];
            Assert.AreEqual(expected, result);
            expected = 1;
            result = map.Member_Metrics["boundaryMember"];
            Assert.AreEqual(expected, result);

            // ** Check Feature metrics **
            expected = 2;
            result = map.Feature_Metrics["CartographicText"];
            Assert.AreEqual(expected, result);
            expected = 1;
            result = map.Feature_Metrics["CartographicSymbol"];
            Assert.AreEqual(expected, result);
            expected = 1;
            result = map.Feature_Metrics["TopographicLine"];
            Assert.AreEqual(expected, result);
            expected = 1;
            result = map.Feature_Metrics["TopographicArea"];
            Assert.AreEqual(expected, result);
            expected = 1;
            result = map.Feature_Metrics["TopographicPoint"];
            Assert.AreEqual(expected, result);
            expected = 1;
            result = map.Feature_Metrics["BoundaryLine"];
            Assert.AreEqual(expected, result);

            // ** Check TOID count **
            //expected = 8;
            //result = map.TOIDList.Count;
            //Assert.AreEqual(expected, result);
        }

        [Test]
        [Ignore("Ignore test")]
        public void LoadedXMLFileGeneratesFeatureCollection_FullFile()
        {
            int expected = 0;
            int result = 0;

            MasterMap map = new MasterMap();
            map.LoadFeaturesFromXMLFile(testFeaturefilePath);
            FeatureCollection fc = map.featureCollection;
            Assert.AreEqual(21183, fc.cartographicMembers.Count);
            Assert.AreEqual(546484, fc.topographicMembers.Count);

            map.UpdateMetrics();

            // ** Check total features count **
            expected = 567824;
            result = map.featureCount;
            Assert.AreEqual(expected, result);

            // ** Check Member metrics **
            expected = 21183;
            result = map.Member_Metrics["cartographicMember"];
            Assert.AreEqual(expected, result);
            expected = 546484;
            result = map.Member_Metrics["topographicMember"];
            Assert.AreEqual(expected, result);
            expected = 157;
            result = map.Member_Metrics["boundaryMember"];
            Assert.AreEqual(expected, result);

            // ** Check Feature metrics **
            expected = 20299;
            result = map.Feature_Metrics["CartographicText"];
            Assert.AreEqual(expected, result);
            expected = 884;
            result = map.Feature_Metrics["CartographicSymbol"];
            Assert.AreEqual(expected, result);
            expected = 402355;
            result = map.Feature_Metrics["TopographicLine"];
            Assert.AreEqual(expected, result);
            expected = 141903;
            result = map.Feature_Metrics["TopographicArea"];
            Assert.AreEqual(expected, result);
            expected = 2226;
            result = map.Feature_Metrics["TopographicPoint"];
            Assert.AreEqual(expected, result);
            expected = 157;
            result = map.Feature_Metrics["BoundaryLine"];
            Assert.AreEqual(expected, result);

        }



        [Test]
        public void canBuildBlankMapImage()
        {
            int expected = 0;
            int result = 0;
            MasterMap map = new MasterMap();            
            Assert.AreSame(null, map.mapImage);
        }


        [Test]
        public void canBuildMapImage()
        {
            int expected = 0;
            int result = 0;
            MasterMap map = new MasterMap();
            expected = 0;
            result = map.BuildMapImage();
            Assert.AreEqual(expected, result);
            Assert.AreEqual(5000, map.mapImage.Width);
            Assert.AreEqual(5000, map.mapImage.Height);
        }


        [Test]
        public void canSeeStylePalette()
        {
            int expected = 0;
            int result = 0;
            MasterMap map = new MasterMap();

            Assert.AreEqual("FFD7C3", map.stylePalette["structureFill"]);
            Assert.AreEqual("DCDCBE", map.stylePalette["heritageFill"]);
            Assert.AreEqual("D2D2AA", map.stylePalette["madeSurfaceFill"]);
            Assert.AreEqual("D2D2AA", map.stylePalette["stepFill"]);
            Assert.AreEqual("D7D7D7", map.stylePalette["roadFill"]);
            Assert.AreEqual("CCCCCC", map.stylePalette["pathFill"]);
            Assert.AreEqual("CCCCCC", map.stylePalette["railFill"]);
            Assert.AreEqual("FFDCAF", map.stylePalette["buildingFill"]);
            Assert.AreEqual("FFCC99", map.stylePalette["glasshouseFill"]);
            Assert.AreEqual("D2FFB4", map.stylePalette["naturalSurfaceFill"]);
            Assert.AreEqual("DCFFBE", map.stylePalette["naturalEnvironmentFill"]);
            Assert.AreEqual("BEFFFF", map.stylePalette["inlandWaterFill"]);
            Assert.AreEqual("BEFFFF", map.stylePalette["tidalWaterFill"]);
            Assert.AreEqual("FFFFCC", map.stylePalette["multipleSurfaceFill"]);
            Assert.AreEqual("FFFFFF", map.stylePalette["unclassifiedFill"]);
        }


        [Test]
        public void canGetStyleName()
        {
            int expected = 0;
            int result = 0;
            MasterMap map = new MasterMap();

            // featureType
            // descriptiveGroup
            // descriptiveTerm
            // make
            Assert.AreEqual("unclassifiedFill", map.GetStyleName("", "", "", ""));
            Assert.AreEqual("buildingFill", map.GetStyleName("TopographicArea", "Building", "", ""));
            Assert.AreEqual("stepFill", map.GetStyleName("TopographicArea", "", "Step", ""));
            Assert.AreEqual("glasshouseFill", map.GetStyleName("TopographicArea", "Glasshouse", "", ""));
            Assert.AreEqual("heritageFill", map.GetStyleName("TopographicArea", "Historic Interest", "", ""));
            Assert.AreEqual("inlandWaterFill", map.GetStyleName("TopographicArea", "Inland Water", "", ""));
            Assert.AreEqual("naturalEnvironmentFill", map.GetStyleName("TopographicArea", "Natural Environment", "", ""));
            Assert.AreEqual("pathFill", map.GetStyleName("TopographicArea", "Path", "", ""));
            Assert.AreEqual("roadFill", map.GetStyleName("TopographicArea", "Road Or Track", "", ""));
            Assert.AreEqual("structureFill", map.GetStyleName("TopographicArea", "Structure", "", ""));
            Assert.AreEqual("tidalWaterFill", map.GetStyleName("TopographicArea", "Tidal Water", "", ""));
            Assert.AreEqual("unclassifiedFill", map.GetStyleName("TopographicArea", "Unclassified", "", ""));
            Assert.AreEqual("railFill", map.GetStyleName("TopographicArea", "Rail", "", "Manmade"));
            Assert.AreEqual("madeSurfaceFill", map.GetStyleName("TopographicArea", "", "", "Manmade"));
            Assert.AreEqual("naturalSurfaceFill", map.GetStyleName("TopographicArea", "", "", "Natural"));
            Assert.AreEqual("madeSurfaceFill", map.GetStyleName("TopographicArea", "", "", "Unknown"));
            Assert.AreEqual("multipleSurfaceFill", map.GetStyleName("TopographicArea", "", "", "Multiple"));

        }



    }
}