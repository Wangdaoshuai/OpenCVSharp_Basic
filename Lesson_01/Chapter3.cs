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
    /// 调整图像大小与裁剪
    /// </summary>
    class Chapter3
    {

        static void Main(string[] args)
        {

            string path = @"C:\CodeLearning\Lesson_01\Lesson_01\Resources\test.png";
            Mat img = Cv2.ImRead(path);
            Mat imgResize = new Mat();
            Console.WriteLine(img.Size());
            //图像大小
            Cv2.Resize(img, imgResize, new OpenCvSharp.Size(640, 480));//改变图片大小
            //Cv2.Resize(img, imgResize, new OpenCvSharp.Size(), 0.5f, 0.5f);//在x,y轴以倍数改变图片大小
          
            //裁剪(ROI)
            Mat imgCrop = new Mat();
            imgCrop = img[new OpenCvSharp.Rect(100, 100, 300, 250)];


            Cv2.ImShow("Image", img);
            Cv2.ImShow("Image Resize", imgResize);
            Cv2.ImShow("Image Crop", imgCrop);

            Cv2.WaitKey(0);

        }
    }
}