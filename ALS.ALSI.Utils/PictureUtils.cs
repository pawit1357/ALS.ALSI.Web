using System;
using System.Drawing;
using System.IO;

namespace ALS.ALSI.Utils
{
    public class PictureUtils
    {
        public static void convertTifToJpg(String destinationFileTif,String outputFileJpg)
        {
          
            Stream _stream = new FileStream(destinationFileTif, (FileMode)FileAccess.ReadWrite);
            MemoryStream storeStream = new MemoryStream();
            storeStream.SetLength(_stream.Length);
            _stream.Read(storeStream.GetBuffer(), 0, (int)_stream.Length);
            byte[] byteArray = storeStream.ToArray();
            Bitmap bm = new Bitmap(_stream);
            bm.Save(outputFileJpg, System.Drawing.Imaging.ImageFormat.Jpeg);
            bm.Dispose();

            storeStream.Flush();
            storeStream.Close();
            _stream.Close();
   
        }
    }
}
