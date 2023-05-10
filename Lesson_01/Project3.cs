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
    class Project3
    {

        /// <summary>
        /// 摄像头下车牌
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            VideoCapture cap = new VideoCapture(0);
            Mat img = new Mat();

            CascadeClassifier plateCascade = new CascadeClassifier();
            plateCascade.Load(@"C:\CodeLearning\Lesson_01\Lesson_01\Resources\haarcascade_russian_plate_number.xml");
            if (plateCascade.Empty())
            {
                Console.WriteLine("XML file not loaded");
            }
            Rect[] plates = new Rect[0];
            while (true)
            {
                cap.Read(img);
                plates = plateCascade.DetectMultiScale(img, 1.1, 10);
                for (int i = 0; i < plates.Length; i++)
                {
                    // Mat imgCrop = img(plates[i]); //C++中的写法
                    Mat imgCrop = new Mat(img, plates[i]); //截取图像
                    //Cv2.ImShow(i.ToString(), imgCrop);
                    Cv2.ImWrite("C:\\CodeLearning\\Lesson_01\\Lesson_01\\Resources\\plates\\" + i.ToString() + ".png", imgCrop);

                    Cv2.Rectangle(img, plates[i].TopLeft, plates[i].BottomRight, new Scalar(255, 0, 255), 3);
                }
                Cv2.ImShow("Pic", img);
                Cv2.WaitKey(1);

            }

        }

    }
}