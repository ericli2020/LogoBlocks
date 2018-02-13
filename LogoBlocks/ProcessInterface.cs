using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Multi.QrCode;

namespace LogoBlocks
{
    public class ProcessInterface
    {
        private string _sourceFolder;
        private string _destFolder;
        private ImageScanner _imageScanner;
        private IdTranslator _idTranslator;
        private FileMover _fileMover;
        

        public ProcessInterface(char deliminatorChar, List<string> priorityList, string sourceFolder, string destFolder, string pathToDict)
        {
            _sourceFolder = sourceFolder;
            _destFolder = destFolder;
            _idTranslator = new IdTranslator(pathToDict);
            _imageScanner = new ImageScanner(deliminatorChar, _idTranslator);
            _fileMover = new FileMover(priorityList);
        }

        public void Begin()
        {
            // uses image scanner to get every single scaninfo
            // sends all the scaninfos to the idtranslator which returns a series of moveinfos
            // filemover processes all the moveinfos based on their types
        }

        public void Test()
        {
            List<ScanInfo> scanResults = _imageScanner.ScanImages(_sourceFolder);
            _fileMover.ProcessFiles(scanResults, _destFolder);
        }
    }
}
