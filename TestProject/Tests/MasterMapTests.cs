using NUnit.Framework;


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

            
            int expected = 108;
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
            Assert.AreEqual(2, fc.topographicMembers.Count);
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
        public void Member_Metrics_UpdatedCorrectly()
        {
            MasterMap map = new MasterMap();
            map.LoadFeaturesFromXMLFile(testValidFeaturefilePath);
            map.UpdateMetrics();
            int expected = 0;
            int result = 0;


            expected = 3;
            result = map.Member_Metrics["cartographicMember"];
            Assert.AreEqual(expected, result);

            expected = 2;
            result = map.Member_Metrics["topographicMember"];
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Feature_Metrics_UpdatedCorrectly()
        {
            MasterMap map = new MasterMap();
            map.LoadFeaturesFromXMLFile(testValidFeaturefilePath);
            map.UpdateMetrics();
            int expected = 0;
            int result = 0;

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
        }



        [Test]
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
            expected = 21183;
            result = map.Member_Metrics["cartographicMember"];
            Assert.AreEqual(expected, result);
            expected = 546484;
            result = map.Member_Metrics["topographicMember"];
            Assert.AreEqual(expected, result);

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

        }





    }
}