using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Multi.QrCode;
using System.Drawing;
using ZXing.Common;
using ZXing.QrCode;

namespace LogoBlocks
{
    public class ImageScanner // scans each image
    {
        // private QRCodeMultiReader _myQrReader;
        private IdTranslator _idParser;
        private QRCodeReader _myQrSingleReader;
        private Char _stringDelimiter;
        private Dictionary<DecodeHintType, object> _myHints;
        // private BarcodeReader _myBarcodeReader;
        // private BarcodeReader _myBarcodeReader2;
        // private BarcodeReader _myBarcodeReader3;

        public ImageScanner(char stringDelimiter, IdTranslator idParser)
        {
            _idParser = idParser;
            _myHints = new Dictionary<DecodeHintType, object>();
            _myHints.Add(DecodeHintType.TRY_HARDER, true);
            _stringDelimiter = stringDelimiter;
            // _myQrReader = new QRCodeMultiReader();
            _myQrSingleReader = new QRCodeReader();
            // _myBarcodeReader = new BarcodeReader(null, bitmap => new BitmapLuminanceSource(bitmap), luminance => new GlobalHistogramBinarizer(luminance)) { AutoRotate = true, Options = new DecodingOptions { TryHarder = true, PossibleFormats = new List<BarcodeFormat>{BarcodeFormat.QR_CODE} }, TryInverted = true };
            // _myBarcodeReader2 = new BarcodeReader(null, bitmap => new BitmapLuminanceSource(bitmap), luminance => new GlobalHistogramBinarizer(luminance)) { AutoRotate = true, Options = new DecodingOptions {TryHarder = true, PossibleFormats = new List<BarcodeFormat> {BarcodeFormat.CODE_128}}, TryInverted = true };
            // _myBarcodeReader3 = new BarcodeReader {AutoRotate = true, Options = new DecodingOptions {TryHarder = true, PossibleFormats = new List<BarcodeFormat> {BarcodeFormat.CODE_128}}, TryInverted = true};
        }

        public List<ScanInfo> ScanImages(string sourcePath) // take every sourcePath and pair it with the QR codes it came with
        {
            string[] myPaths = Directory.GetFiles(sourcePath); // this should filter for jpg, gif, bmp, and png
            List<ScanInfo> toReturn = new List<ScanInfo>();
            

            foreach (string imagePath in myPaths)
            {
                Debug.WriteLine("ImageScanner begin: " + imagePath);
                ScanInfo newInfo = new ScanInfo(imagePath);
                Result currentResult = _myQrSingleReader.decode(
                    new BinaryBitmap(
                        new HybridBinarizer(
                            new BitmapLuminanceSource(
                                (Bitmap) Bitmap.FromFile(imagePath)
                            )
                        )
                    ),
                    _myHints);

                if (currentResult != null)
                {
                    Debug.WriteLine("ImageScan raw: " + currentResult.Text);
                    string[] splitString = currentResult.Text.Split(_stringDelimiter);
                    if (splitString != null)
                    {
                        // translate these
                        
                        foreach (string currCode in splitString)
                        {
                            CsvDataNode translatedCode = _idParser.translateId(currCode);
                            if (translatedCode != null)
                            {
                                newInfo.Properties[translatedCode.type] = translatedCode.name; // this should never be null
                            }
                        }
                        
                    }
                    else
                    {
                        Debug.WriteLine("ImageScanner invalid raw");
                    }
                }
                else
                {
                    Debug.WriteLine("ImageScan null scan");
                }
                toReturn.Add(newInfo);
            }

            return toReturn;
        }
    }
}
