using NUnit.Framework;
using System.IO;

namespace TestProject
{
    public class FeatureCollectionTests
    {        
        private string testFullFeaturefilePath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\5436275-SX9090";
        private string testValidFeaturefilePath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\ValidFeatureCollection.xml";



        [Test]
        public void nothing()
        {
        }

        [Test]
        public void canCreateFeatureCollection()
        {
            FeatureCollection fc = new FeatureCollection();
        }


        [Test]
        public void canLoadFromXMLString()
        {
            //string xmlString = File.ReadAllText(testFullFeaturefilePath);
            string xmlString = File.ReadAllText(testValidFeaturefilePath);            
            FeatureCollection fc = new FeatureCollection().DeserialiseFromXMLString(xmlString);
            Assert.AreEqual("LOCAL_ID_0", fc.fid);
            Assert.AreEqual("Ordnance Survey, (c) Crown Copyright. All rights reserved, 2021-08-26", fc.description);
            Assert.AreEqual("2021-08-26T00:10:00", fc.queryTime);
        }

        [Test]
        public void canLoadCartographicMembers()
        {
            //string xmlString = File.ReadAllText(testFullFeaturefilePath);
            string xmlString = File.ReadAllText(testValidFeaturefilePath);
            FeatureCollection fc = new FeatureCollection().DeserialiseFromXMLString(xmlString);
            Assert.AreEqual(3, fc.cartographicMembers.Count);

            // ** CartographicText [0] **
            Assert.AreEqual("osgb5000005277336613", fc.cartographicMembers[0].cartographicText.fid);
            Assert.AreEqual("10178", fc.cartographicMembers[0].cartographicText.featureCode);
            Assert.AreEqual(1, fc.cartographicMembers[0].cartographicText.version);            
            Assert.AreEqual("Roads Tracks And Paths", fc.cartographicMembers[0].cartographicText.theme[0]);            
            Assert.AreEqual("Post", fc.cartographicMembers[0].cartographicText.textString);
            // [1]
            Assert.AreEqual("osgb5000005277336611", fc.cartographicMembers[1].cartographicText.fid);
            Assert.AreEqual("10178", fc.cartographicMembers[1].cartographicText.featureCode);

            // ** CartographicSymbol **
            Assert.AreEqual("osgb5000005241809433", fc.cartographicMembers[2].cartographicSymbol.fid);
            Assert.AreEqual("10130", fc.cartographicMembers[2].cartographicSymbol.featureCode);
            Assert.AreEqual(1, fc.cartographicMembers[2].cartographicSymbol.version);
            Assert.AreEqual("Administrative Boundaries", fc.cartographicMembers[2].cartographicSymbol.theme[0]);
           
        }



        [Test]
        public void canLoadTopographicMembers()
        {
            //string xmlString = File.ReadAllText(testFullFeaturefilePath);
            string xmlString = File.ReadAllText(testValidFeaturefilePath);
            FeatureCollection fc = new FeatureCollection().DeserialiseFromXMLString(xmlString);
            Assert.AreEqual(1, fc.topographicMembers.Count);

            // ** TopographicLine [0] **
            Assert.AreEqual("osgb1000000347714966", fc.topographicMembers[0].topographicLine.fid);
            Assert.AreEqual("10046", fc.topographicMembers[0].topographicLine.featureCode);
            Assert.AreEqual(1, fc.topographicMembers[0].topographicLine.version);
            Assert.AreEqual("Buildings", fc.topographicMembers[0].topographicLine.theme[0]);
            Assert.AreEqual("Land", fc.topographicMembers[0].topographicLine.theme[1]);



        }



    }
}