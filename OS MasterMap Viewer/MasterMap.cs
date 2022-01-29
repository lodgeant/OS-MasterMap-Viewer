using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_MasterMap_Viewer
{

    public class MasterMap
    {
        //public string sourceFilePath;
        //public int sourceFileLineCount;
        //public Dictionary<string, int> FeatureMembers = new Dictionary<string, int>();
        //public Dictionary<string, int> FeatureTypes = new Dictionary<string, int>();



        //public MasterMap() { }

        //public MasterMap(string sourceFilePath)
        //{ 
        //    this.sourceFilePath = sourceFilePath;
        //}




        //public void GenerateMetrics()
        //{
        //    // Get total line count
        //    // Get dictionary of all FeatureMembers and counts
        //    // Get dictionary of all Feature Types and counts

        //    string[] memberList = { "topographicMember" };



        //    // ** Get metrics **
        //    FeatureMembers = new Dictionary<string, int>();
        //    FeatureTypes = new Dictionary<string, int>();
        //    int lineCount = 0;
        //    foreach (string line in File.ReadLines(this.sourceFilePath))
        //    {           
        //        // ** Update FeatureMember metrics **
        //        if(memberList.Any(s => line.Contains("</osgb:" + s)))                
        //        {
        //            string memberString = line.Replace("</osgb:", "").Replace("<osgb:", "").Replace(">", "");
        //            if (FeatureMembers.ContainsKey(memberString) == false)
        //            {
        //                FeatureMembers.Add(memberString, 0);
        //            }
        //            FeatureMembers[memberString] += 1;
        //        }





        //        lineCount++;
        //    }
        //    this.sourceFileLineCount = lineCount;


        //}


    }


}
