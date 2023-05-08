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
    class Chapter1
    {
        /// <summary>
        /// images
        /// </summary>
        /// <param name="args"></param>
        /*        static void Main(string[] args)
                {
                    //创建一张大小为300*300颜色为蓝色的三通道彩色图像
                    Mat img = new Mat(300, 300, MatType.CV_8UC3, new Scalar(255, 0, 0));
                    //显示图像
                    Cv2.ImShow("img", img);
                    //延时等待按键按下
                    Cv2.WaitKey(0);
                }
     
            //string path = @"C:\CodeLearning\Lesson_01\Lesson_01\Resources\test.png";   //OK 不加@的话，\会被当作转义字符处理
            //string path = "C:\\CodeLearning\\Lesson_01\\Lesson_01\\Resources\\test.png"; // OK
            string path = @"C:\CodeLearning\Lesson_01\Lesson_01\Resources\test.png";
            // string path = @"..\Lesson_01\Resources\test.png";
            Mat img = Cv2.ImRead(path);
            Cv2.ImShow("Pic", img);
            Cv2.WaitKey(0);
            */

        ///video
        //static void Main(string[] args)
        //{
        //    string path = @"C:\CodeLearning\Lesson_01\Lesson_01\Resources\test_video.mp4";
        //    VideoCapture cap = new VideoCapture(path);
        //    Mat img = new Mat();
        //    while (true)
        //    {
        //        cap.Read(img);
        //        if (!img.Empty())
        //        {
        //            Cv2.ImShow("Video", img);
        //        }
        //        else
        //        {
        //            Console.WriteLine("ERROR|NO IMG");
        //            break;
        //        }
        //        Cv2.WaitKey(1);
        //        Thread.Sleep(30);//可以控制视频播放速度
        //    }
        //}
        ///Webcam
        static void Main(string[] args)
        {
            
            VideoCapture cap = new VideoCapture(0);
            Mat img = new Mat();
            while (true)
            {
                cap.Read(img);
                if (!img.Empty())
                {
                    Cv2.ImShow("Video", img);
                }
                else
                {
                    Console.WriteLine("ERROR|NO IMG");
                    break;
                }
                Cv2.WaitKey(1);
            }

        }



    }
}