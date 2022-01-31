//using System;
using MasterMapLib;
using System.Diagnostics;

namespace OS_MasterMap_Viewer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            try
            {
                // ** Create MasterMap object **
                string sourceFilePath = @"C:\TONY\5436275-SX9090";
                //string sourceFilePath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\ValidFeatureCollection.xml";
                //string sourceFilePath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\TopographicAreaFeatures.xml";
                MasterMap map = new MasterMap();

                // ** Load map **
                Console.Write("Loading Map...");
                stopWatch = new Stopwatch(); stopWatch.Start();
                if (map.LoadXML(sourceFilePath) == -1)
                {
                    throw new Exception("Unable to Load Features...");
                }
                stopWatch.Stop();
                Console.WriteLine("Map loaded - took " + stopWatch.Elapsed.TotalSeconds.ToString("#,###") + " secs...");

                // ** Update Metrics **
                Console.Write("Updating metrics...");
                stopWatch = new Stopwatch(); stopWatch.Start();
                if (map.UpdateMetrics() == -1)
                {
                    throw new Exception("Unable to Update Metrics...");
                }
                stopWatch.Stop();
                Console.WriteLine("Metrics updated - took " + stopWatch.Elapsed.TotalSeconds.ToString("#,###") + " secs...");

                // ** Post metric data **
                Console.Write("\n");
                Console.WriteLine("Features: " + map.featureCount.ToString("#,###") + "\n");
                foreach (string member in map.Member_Metrics.Keys)
                {
                    Console.WriteLine(member + ": " + map.Member_Metrics[member].ToString("#,###"));
                }
                Console.Write("\n");
                foreach (string feature in map.Feature_Metrics.Keys)
                {
                    Console.WriteLine(feature + ": " + map.Feature_Metrics[feature].ToString("#,###"));
                }
                //Console.WriteLine("\nTOID Count: " + map.TOIDList.Count.ToString("#,###"));
                //foreach (string TOID in map.TOIDList)
                //{
                //    Console.WriteLine(TOID);
                //}



                // ** Generate map image **
                Console.Write("\n");
                Console.WriteLine("Building map image...");
                stopWatch = new Stopwatch(); stopWatch.Start();
                int result = map.BuildMapImage();
                if (result == -1)
                {
                    throw new Exception("Error building map image...");
                }
                else if(result > 0)
                {
                    Console.WriteLine(result + " TOIDs not generated correctly...");
                }
                stopWatch.Stop();
                Console.WriteLine("Map generated - took " + stopWatch.Elapsed.TotalSeconds.ToString("#,###") + " secs...");
                map.mapImage.Save(@"C:\TONY\test.bmp");
                



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }

}


