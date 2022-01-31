using NUnit.Framework;
using MasterMapLib;
using System.Diagnostics;

namespace TestProject
{
    public class MasterMapTests
    {        
        private string testFeaturefilePath = @"C:\TONY\5436275-SX9090";
        private string testValidFeaturefilePath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\ValidFeatureCollection.xml";
        private Stopwatch stopWatch;

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
            Assert.AreEqual(-1, map.LoadXML(""));
        }

        [Test]
        public void canLoadFeaturesFromXMLFile_usingFilePath_fileExists()
        {
            MasterMap map = new MasterMap();
            Assert.AreEqual(0, map.LoadXML(testValidFeaturefilePath));
            Assert.AreEqual(0, map.UpdateMetrics());
            Assert.AreEqual(7, map.featureCount);
        }
            
        [Test]
        public void LoadedXMLFileHasIncorrectXMLFormat()
        {
            string invalidFeatureXMLPath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\InvalidFeatureXML.xml";
            MasterMap map = new MasterMap();            
            Assert.AreEqual(-1, map.LoadXML(invalidFeatureXMLPath));
        }

        [Test]
        [Ignore("Ignore test")]
        public void LoadedXMLFileGeneratesFeatureCollection()
        {
            MasterMap map = new MasterMap();
            map.LoadXML(testValidFeaturefilePath);
            //FeatureCollection fc = map.featureCollection;           
            //Assert.AreEqual(3, fc.cartographicMembers.Count);
            //Assert.AreEqual(3, fc.topographicMembers.Count);
            //Assert.AreEqual(1, fc.boundaryMembers.Count);
        }

        [Test]
        public void canUpdateMetrics()
        {
            MasterMap map = new MasterMap();
            Assert.AreEqual(0, map.UpdateMetrics());
        }

        [Test]
        public void Metrics_UpdatedCorrectly()
        {
            MasterMap map = new MasterMap();            
            Assert.AreEqual(0, map.LoadXML(testValidFeaturefilePath));
            Assert.AreEqual(0, map.UpdateMetrics());

            // ** Check Member metrics **
            Assert.AreEqual(3, map.Member_Metrics["cartographicMember"]);
            Assert.AreEqual(3, map.Member_Metrics["topographicMember"]);
            Assert.AreEqual(1, map.Member_Metrics["boundaryMember"]);

            // ** Check Feature metrics **
            Assert.AreEqual(2, map.Feature_Metrics["CartographicText"]);
            Assert.AreEqual(1, map.Feature_Metrics["CartographicSymbol"]);
            Assert.AreEqual(1, map.Feature_Metrics["TopographicLine"]);
            Assert.AreEqual(1, map.Feature_Metrics["TopographicArea"]);
            Assert.AreEqual(1, map.Feature_Metrics["TopographicPoint"]);
            Assert.AreEqual(1, map.Feature_Metrics["BoundaryLine"]);

            // ** Check TOID count **
            //expected = 8;
            //result = map.TOIDList.Count;
            //Assert.AreEqual(expected, result);
        }

        [Test]
        //[Ignore("Ignore test")]
        public void LoadedXMLFileGeneratesFeatureCollection_FullFile()
        {            
            MasterMap map = new MasterMap();
            Assert.AreEqual(0, map.LoadXML(testFeaturefilePath));
            Assert.AreEqual(0, map.UpdateMetrics());
            Assert.AreEqual(567824, map.featureCount);

            // ** Check Member metrics **
            Assert.AreEqual(21183, map.Member_Metrics["cartographicMember"]);
            Assert.AreEqual(546484, map.Member_Metrics["topographicMember"]);
            Assert.AreEqual(157, map.Member_Metrics["boundaryMember"]);
            //
            // ** Check Feature metrics **
            Assert.AreEqual(20299, map.Feature_Metrics["CartographicText"]);
            Assert.AreEqual(884, map.Feature_Metrics["CartographicSymbol"]);
            Assert.AreEqual(402355, map.Feature_Metrics["TopographicLine"]);
            Assert.AreEqual(141903, map.Feature_Metrics["TopographicArea"]);
            Assert.AreEqual(2226, map.Feature_Metrics["TopographicPoint"]);
            Assert.AreEqual(157, map.Feature_Metrics["BoundaryLine"]);


            //FeatureCollection fc = map.featureCollection;
            //Assert.AreEqual(21183, fc.cartographicMembers.Count);
            //Assert.AreEqual(546484, fc.topographicMembers.Count);

        }

        [Test]
        public void canBuildMapImage()
        {            
            MasterMap map = new MasterMap();
            Assert.AreSame(null, map.mapImage);            
            Assert.AreEqual(0, map.BuildMapImage());
            Assert.AreEqual(5000, map.mapImage.Width);
            Assert.AreEqual(5000, map.mapImage.Height);
        }

        [Test]
        public void canSeeStylePalette()
        {
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
            MasterMap map = new MasterMap();

            // featureType, descriptiveGroup, descriptiveTerm, make
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


        [Test]
        [Ignore("Ignore test")]
        public void Performance_LoadXML()
        {
            MasterMap map = new MasterMap();
            stopWatch = new Stopwatch(); stopWatch.Start();
            Assert.AreEqual(0, map.LoadXML(testFeaturefilePath));
            stopWatch.Stop();            
            Assert.Less(stopWatch.Elapsed.TotalSeconds, 15);
        }

        [Test]
        [Ignore("Ignore test")]
        public void Performance_UpdateMetrics()
        {
            MasterMap map = new MasterMap();
            Assert.AreEqual(0, map.LoadXML(testFeaturefilePath));           
            stopWatch = new Stopwatch(); stopWatch.Start();
            Assert.AreEqual(0, map.UpdateMetrics());
            stopWatch.Stop();
            Assert.Less(stopWatch.Elapsed.TotalSeconds, 15);
        }

        [Test]
        [Ignore("Ignore test")]
        public void Performance_BuildMapImage()
        {
            MasterMap map = new MasterMap();
            Assert.AreEqual(0, map.LoadXML(testFeaturefilePath));
            stopWatch = new Stopwatch(); stopWatch.Start();
            Assert.AreEqual(0, map.BuildMapImage());
            stopWatch.Stop();
            Assert.Less(stopWatch.Elapsed.TotalSeconds, 1);
        }


    }
}