using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace LogoBlocks
{
    public class IdTranslator
    {
        private string _dataPath;
        private CsvReader _dataReader;
        private Dictionary<string, CsvDataNode> _translatorDict;
        public IdTranslator(string pathToCsv)
        {
            _dataPath = pathToCsv;
            _dataReader = new CsvReader(new StreamReader(_dataPath));
            _translatorDict = new Dictionary<string, CsvDataNode>();
            while (_dataReader.Read())
            {
                CsvDataNode newRecord = _dataReader.GetRecord<CsvDataNode>();
                // Debug.WriteLine("New Record: " + newRecord.code + " " + newRecord.name);
                _translatorDict[newRecord.code] = newRecord; // THIS SHOULD THROW IF THERE IS A NULL CODE
            }
            // load in the CSV
        }

        public CsvDataNode translateId(string idToTranslate)
        {
            // looks into the CSV and returns the necessary info
            if (_translatorDict.ContainsKey(idToTranslate))
            {
                return _translatorDict[idToTranslate];
            }
            Debug.WriteLine("Issue in id translation: " + idToTranslate);
            return null;
        }
    }
}
