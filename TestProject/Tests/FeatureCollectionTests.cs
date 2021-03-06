using NUnit.Framework;
using System.IO;
using MasterMapLib;

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
            string xmlString = File.ReadAllText(testValidFeaturefilePath);
            FeatureCollection fc = new FeatureCollection().DeserialiseFromXMLString(xmlString);
            Assert.AreEqual(3, fc.topographicMembers.Count);

            // ** TopographicLine [0] **
            Assert.AreEqual("osgb1000000347714966", fc.topographicMembers[0].topographicLine.fid);
            Assert.AreEqual("10046", fc.topographicMembers[0].topographicLine.featureCode);
            Assert.AreEqual(1, fc.topographicMembers[0].topographicLine.version);
            Assert.AreEqual("Buildings", fc.topographicMembers[0].topographicLine.theme[0]);
            Assert.AreEqual("Land", fc.topographicMembers[0].topographicLine.theme[1]);

            // ** TopographicArea [1] **
            Assert.AreEqual("osgb1000002062221594", fc.topographicMembers[1].topographicArea.fid);
            Assert.AreEqual("10056", fc.topographicMembers[1].topographicArea.featureCode);
            Assert.AreEqual(3, fc.topographicMembers[1].topographicArea.version);
            Assert.AreEqual("Land", fc.topographicMembers[1].topographicArea.theme[0]);

            // ** TopographicPoint [2] **
            Assert.AreEqual("osgb1000000732277248", fc.topographicMembers[2].topographicPoint.fid);
            Assert.AreEqual("10197", fc.topographicMembers[2].topographicPoint.featureCode);
            Assert.AreEqual(1, fc.topographicMembers[2].topographicPoint.version);
            Assert.AreEqual("Terrain And Height", fc.topographicMembers[2].topographicPoint.theme[0]);

        }

        [Test]
        public void canLoadBoundaryMembers()
        {
            string xmlString = File.ReadAllText(testValidFeaturefilePath);
            FeatureCollection fc = new FeatureCollection().DeserialiseFromXMLString(xmlString);
            Assert.AreEqual(1, fc.boundaryMembers.Count);

            // ** BoundaryLine [0] **
            Assert.AreEqual("osgb1000000738111002", fc.boundaryMembers[0].boundaryLine.fid);
            Assert.AreEqual("10128", fc.boundaryMembers[0].boundaryLine.featureCode);
            Assert.AreEqual(3, fc.boundaryMembers[0].boundaryLine.version);            
            Assert.AreEqual("Administrative Boundaries", fc.boundaryMembers[0].boundaryLine.theme[0]);

        }



    }
}