using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoBlocks
{
    public class ScanInfo
    {
        public ScanInfo(string sourcePath)
        {
            // this should be based on a priority system
            // THE FILEMOVER SHOULD DECIDE PRIORITY TO LOOK AT IN EACH FILE, AND TO MOVE ALL FILES (INCLUDING UNBARCODED) TO BASE FOLDER
            // THIS SHOULD BE MADE BY THE IDTRANSLATOR NESTED IN THE IMAGESCANNER
            SourcePath = sourcePath;
            Properties = new Dictionary<string, string>();
        }

        public string SourcePath { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}
