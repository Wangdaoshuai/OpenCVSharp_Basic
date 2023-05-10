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
    class Project4
    {

        /// <summary>
        /// 摄像头下人脸检测
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            VideoCapture cap = new VideoCapture(0);
            Mat img = new Mat();

            CascadeClassifier faceCasecade = new CascadeClassifier();
            faceCasecade.Load(@"C:\CodeLearning\Lesson_01\Lesson_01\Resources\haarcascade_frontalface_default.xml");
            if (faceCasecade.Empty())
            {
                Console.WriteLine("XML file not loaded");
            }
            Rect[] faces = new Rect[0];
            while (true)
            {
                cap.Read(img);
                faces = faceCasecade.DetectMultiScale(img, 1.1, 10);
                for (int i = 0; i < faces.Length; i++)
                {
                    Cv2.Rectangle(img, faces[i].TopLeft, faces[i].BottomRight, new Scalar(255, 0, 255), 3);
                }
                Cv2.ImShow("Pic", img);
                Cv2.WaitKey(1);

            }

        }

    }
}
