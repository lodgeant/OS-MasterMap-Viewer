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
                        
            int expected = 150;
            int result = map.featureXML_nodeCount;
            Assert.AreEqual(expected, result);
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
            expected = 8;
            result = map.TOIDList.Count;
            Assert.AreEqual(expected, result);
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
            expected = 0;
            //result = map.BuildMapImage();
            Assert.AreEqual(expected, result);
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

            Assert.AreEqual(1000, map.mapImage.Width);
            Assert.AreEqual(1000, map.mapImage.Height);
        }


    }
}