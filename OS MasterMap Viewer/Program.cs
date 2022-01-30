//using System;
using MasterMapLib;


namespace OS_MasterMap_Viewer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // ** Create MasterMap object **
            string sourceFilePath = @"C:\TONY\5436275-SX9090";
            //string sourceFilePath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\ValidFeatureCollection.xml";
            //string sourceFilePath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\TopographicAreaFeatures.xml";
            MasterMap map = new MasterMap();

            Console.WriteLine("Generating Map...");
            if(map.LoadFeaturesFromXMLFile(sourceFilePath) == -1)
            {
                throw new Exception("Unable to Load Features...");
            }


            // ** Get metrics from file **
            Console.WriteLine("Updating Map metrics...\n");
            //if (map.UpdateMetrics() == -1)
            //{
            //    throw new Exception("Unable to Update Metrics...");
            //}

            // ** Post metric data **
            Console.WriteLine("Features: " + map.featureCount.ToString("#,###") + "\n");
            foreach(string member in map.Member_Metrics.Keys)
            {
                Console.WriteLine(member + ": " + map.Member_Metrics[member].ToString("#,###"));
            }
            Console.Write("\n");
            foreach (string feature in map.Feature_Metrics.Keys)
            {
                Console.WriteLine(feature + ": " + map.Feature_Metrics[feature].ToString("#,###"));
            }            
            Console.WriteLine("\nTOID Count: " + map.TOIDList.Count.ToString("#,###"));
            //foreach (string TOID in map.TOIDList)
            //{
            //    Console.WriteLine(TOID);
            //}


            Console.WriteLine("Building map image...");
            int result = map.BuildMapImage();
            if (result == -1)
            {
                throw new Exception("Error building map image...");
            }
            else if(result > 0)
            {
                Console.WriteLine(result + " TOIDs not generated correctly...");
            }
            map.mapImage.Save(@"C:\TONY\test.bmp");
            Console.WriteLine("Map generated...");

            //Console.WriteLine("Program ended...");
        }


    }

}


