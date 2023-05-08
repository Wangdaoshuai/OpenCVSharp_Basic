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
    /// 5个基础函数
    /// </summary>
    class Chapter2
    {

        static void Main(string[] args)
        {

            string path = @"C:\CodeLearning\Lesson_01\Lesson_01\Resources\test.png";
            Mat img = Cv2.ImRead(path);

            Mat imgGray = new Mat();
            Cv2.CvtColor(img, imgGray,ColorConversionCodes.BGR2GRAY);//色彩转换
            Mat imgBlur = new Mat();
            Cv2.GaussianBlur(img, imgBlur,new OpenCvSharp.Size(3, 3), 3, 0); //高斯模糊
            Mat imgCanny = new Mat();
            Cv2.Canny(imgBlur, imgCanny, 50, 150);//canny边缘检测

            ///腐蚀和膨胀
            Mat imgDia = new Mat();
            Mat imgErode = new Mat();
            //定义一个自定义膨胀的内核
            Mat kernel1 = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(5, 5));
            Cv2.Dilate(imgCanny, imgDia, kernel1);//膨胀
            Cv2.Erode(imgDia, imgErode, kernel1);//腐蚀

            Cv2.ImShow("Image", img);
            Cv2.ImShow("Image Gray", imgGray);
            Cv2.ImShow("Image Blur", imgBlur);
            Cv2.ImShow("Image Canny", imgCanny);
            Cv2.ImShow("Image Dilataion", imgDia);
            Cv2.ImShow("Image Erode", imgErode);
            Cv2.WaitKey(0);

        }
    }
}
