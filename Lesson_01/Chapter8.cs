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
    class Chapter8
    {
        static void Main(string[] args)
        {

            string path = @"C:\CodeLearning\Lesson_01\Lesson_01\Resources\test.png";
            // string path = @"..\Lesson_01\Resources\test.png";
            Mat img = Cv2.ImRead(path);

            CascadeClassifier faceCasecade = new CascadeClassifier();
            faceCasecade.Load(@"C:\CodeLearning\Lesson_01\Lesson_01\Resources\haarcascade_frontalface_default.xml");
            if (faceCasecade.Empty())
            {
                Console.WriteLine("XML file not loaded");
            }

            //List<Rect> faces = new List<Rect>();
            //Rect[] faces = new Rect[];
            Rect[] faces = faceCasecade.DetectMultiScale(img, 1.1, 10);

            for (int i = 0; i < faces.Length; i++)
            {
                Cv2.Rectangle(img, faces[i].TopLeft, faces[i].BottomRight, new Scalar(255, 0, 255), 3);
            }

            Cv2.ImShow("Pic", img);
            Cv2.WaitKey(0);

        }



    }
}