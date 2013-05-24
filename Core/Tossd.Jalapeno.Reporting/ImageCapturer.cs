using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tossd.Jalapeno.Reporting
{
    public class ImageCapturer
    {
        private readonly string _screenCaptureImagesPath;

        public ImageCapturer(string screenCaptureImagesPath, int testNumber)
        {
            _screenCaptureImagesPath = screenCaptureImagesPath + "_Test " + testNumber;
            Directory.CreateDirectory(_screenCaptureImagesPath);
        }

        public void CaptureScreen(string fileName)
        {
            var destinationImagePath = _screenCaptureImagesPath + "\\" + fileName;
            var screenWidth = Screen.GetBounds(new Point(0, 0)).Width;
            var screenHeight = Screen.GetBounds(new Point(0, 0)).Height;
            var screenBitmap = new Bitmap(screenWidth, screenHeight);
            var screenBitmapGFX = Graphics.FromImage(screenBitmap);
            screenBitmapGFX.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(screenWidth, screenHeight));
            screenBitmap.Save(destinationImagePath, ImageFormat.Png);
        }
    }
}
