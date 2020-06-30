using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ZXing;
using ZXing.Common;
namespace IMS_Client_2.Barcode
{
    class clsBarCodeUtility
    {



        public static Bitmap GenerateBarCode(string strValue)
        {
            ZXing.BarcodeWriter barcodeWriter = new ZXing.BarcodeWriter();
            barcodeWriter.Format = ZXing.BarcodeFormat.CODE_128;
            EncodingOptions encodingOptions = new EncodingOptions();
            // dont put the data on barcode.
            encodingOptions.PureBarcode = false;
            encodingOptions.Height = 6;

            barcodeWriter.Options = encodingOptions;

            Bitmap bitmap = barcodeWriter.Write(strValue);

            return bitmap;

        }

        public static string ReadBarCode(string strFilepath)
        {
            ZXing.BarcodeReader barcodeReader = new ZXing.BarcodeReader();

            Image bitmap = Bitmap.FromFile(strFilepath);

            Bitmap bmp = new Bitmap(bitmap);

            Result result = barcodeReader.Decode(bmp);

            return result.Text;
        }
    }
}
