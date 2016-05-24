using System;
using System.IO;
using System.Drawing;

namespace AliceApi.Common
{
    public class ImageTools
    {
        


        #region  Other Image Functions
        // This function creates the Thumbnail image and returns the image created in Byte() format
        public byte[] createThumnail(Stream ImageStream, double tWidth, double tHeight)
        {
            System.Drawing.Image g = System.Drawing.Image.FromStream(ImageStream);
            Size thumbSize = new Size();
            thumbSize = NewthumbSize(g.Width, g.Height, tWidth, tHeight);

            Bitmap imgOutput = new Bitmap(g, thumbSize.Width, thumbSize.Height);
            MemoryStream imgStream = new MemoryStream();

            System.Drawing.Imaging.ImageFormat thisFormat = g.RawFormat;

            imgOutput.Save(imgStream, thisFormat);

            byte[] imgbin = new byte[imgStream.Length];

            imgStream.Position = 0;
            int n = imgStream.Read(imgbin, 0, imgbin.Length);
            g.Dispose();
            imgOutput.Dispose();
            return imgbin;
        }

        public Size NewthumbSize(double currentwidth, double currentheight, double newWidth, Double newHeight)
        {
            // Calculate the Size of the New image 
            double tempMultiplier;
            if (currentheight > currentwidth)
                // Portrait 
                tempMultiplier = newHeight / currentheight;
            else
                // Landscape
                tempMultiplier = newWidth / currentwidth;
            Size NewSize = new Size(Convert.ToInt32(currentwidth * tempMultiplier), Convert.ToInt32(currentheight * tempMultiplier));
            return NewSize;
        }
        #endregion










    }
}
