using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogoBlocks
{
    public class FileMover
    {
        private List<string> _priorityList;
        public FileMover(List<string> priorityList)
        {
            _priorityList = priorityList;
            // takes in a priority list
        }

        public void ProcessFiles(List<ScanInfo> myScanResults, string destinationFolder)
        {
            foreach (ScanInfo currInfo in myScanResults)
            {
                string sourceLoc = currInfo.SourcePath;
                Debug.WriteLine("Begin move: " + sourceLoc);
                try
                {
                    string newLoc = destinationFolder + "/";
                    string fileNameNoExt = Path.GetFileNameWithoutExtension(sourceLoc);
                    string fileExt = Path.GetExtension(sourceLoc);
                    int count = 1;

                    for (int i = 0; i < _priorityList.Count; i++)
                    {
                        string currentCategorizor = _priorityList[i];
                        Debug.WriteLine("Categorizor: " + currentCategorizor);
                        if (currInfo.Properties.ContainsKey(currentCategorizor))
                        {
                            newLoc = newLoc + currInfo.Properties[currentCategorizor] + "/";
                            Debug.WriteLine("Next directory: " + newLoc);
                            System.IO.Directory.CreateDirectory(newLoc);
                        }
                    }

                    string newFileName = fileNameNoExt;
                    while (File.Exists(newLoc + newFileName + fileExt))
                    {
                        newFileName = fileNameNoExt + "(" + count++ + ")";
                    }

                    File.Copy(sourceLoc, newLoc + newFileName + fileExt);


                    /*
                    string fileName = Path.GetFileName(sourceLoc);
                    File.Copy(sourceLoc, newLoc + fileName); // doesn't check for duplicates
                    // move to the base destination folder
                    // BUILD PATH BEFORE COPYING
                    for (int i = 0; i < _priorityList.Count; i++)
                    {
                        string currentCategorizor = _priorityList[i];
                        Debug.WriteLine("Categorizor: " + currentCategorizor);
                        if (currInfo.Properties.ContainsKey(currentCategorizor))
                        {
                            string newerLoc = newLoc + currInfo.Properties[currentCategorizor] + "/";
                            Debug.WriteLine("Next directory: " + newerLoc);
                            System.IO.Directory.CreateDirectory(newerLoc);
                            File.Move(newLoc + fileName, newerLoc + fileName);
                            newLoc = newerLoc;
                        }
                    }
                    */
                }
                catch
                {
                    Debug.WriteLine("File already exists: " + sourceLoc);
                }
            }
        }
    }
}
