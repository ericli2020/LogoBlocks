using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoBlocks
{
    class Program
    {
        static void Main(string[] args)
        {
            // add search in subfolders option
            // look online for the CSV file
            // create user interface for the inputs below
            // learn github and set it up for version control
            // 1st get this on github

            // test comment in main

            char myDeliminator = '|';
            string mySourceFolder = @"../../../TestMove";
            string myDestFolder = @"../../../TestOutput";
            string myCsvSource = @"../../../logodict.csv";
            List<string> myMovePriority = new List<string>() {"student", "class"};

            ProcessInterface myProcess = new ProcessInterface(myDeliminator, myMovePriority, mySourceFolder, myDestFolder, myCsvSource);
            myProcess.Test();
        }
    }
}
