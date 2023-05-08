using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using static System.Net.Mime.MediaTypeNames;

namespace Lesson_01
{
    /// <summary>
    /// Color Detection
    /// </summary>
    class Chapter6
    {

        static void Main(string[] args)
        {

            string path = @"C:\CodeLearning\Lesson_01\Lesson_01\Resources\lambo.png";
            Mat img = Cv2.ImRead(path);

            Mat imgHSV = new Mat();
            Cv2.CvtColor(img, imgHSV, ColorConversionCodes.BGR2HSV);//H色调，S饱和度，V明度

            int hmin = 0, smin = 110, vmin = 153;
            int hmax = 19, smax = 240, vmax = 255;
            Mat mask = new Mat();

            


            Scalar lower = new Scalar(hmin, smin, vmin);
            Scalar upper = new Scalar(hmax, smax, vmax);
            Cv2.InRange(imgHSV,lower, upper,mask);

            Cv2.ImShow("Image", img);
            Cv2.ImShow("Image HSV", imgHSV);
            Cv2.ImShow("Image Mask", mask);
            Cv2.WaitKey(0);

        }
    }
}