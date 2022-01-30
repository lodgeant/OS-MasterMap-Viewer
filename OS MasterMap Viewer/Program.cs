//using System;
using MasterMapLib;


namespace OS_MasterMap_Viewer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // ** Create MasterMap object **
            //string sourceFilePath = @"C:\TONY\5436275-SX9090";            
            string sourceFilePath = @"C:\Source Code\CS\OS MasterMap Viewer\TestProject\Tests\Artefacts\ValidFeatureCollection.xml";
            MasterMap map = new MasterMap();

            Console.WriteLine("Generating Map...");
            map.LoadFeaturesFromXMLFile(sourceFilePath);


            // ** Get metrics from file **
            Console.WriteLine("Updating Map metrics...");
            map.UpdateMetrics();


            //Console.WriteLine("Found " + map.sourceFileLineCount.ToString("#,###") + " line(s)...");
            //Console.WriteLine("Program ended...");
        }


    }

}


