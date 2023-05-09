using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using static System.Net.Mime.MediaTypeNames;

namespace Lesson_01
{
    class Project1
    {
        public static Mat img = new Mat();
        public static List<List<int>> newPoints = new List<List<int>>();

        //hmin, smin, vmin ;hmax , smax , vmax
        public static List<List<int>> myColors = new List<List<int>>()
        {
            new List<int>{124,48,117,143,170,255 },//purple
            new List<int>{68,72,156,102,126,255 },//green

        };
        public static List<Scalar> myColorValues = new List<Scalar>()
        {
            new Scalar(255, 0, 255), new Scalar(0, 255, 0)
        };//purple green 
          //myColors.Add(new List<int> { 255, 0, 0 });  // 添加红色
        static void Main(string[] args)
        {

            VideoCapture cap = new VideoCapture(0);
            //Mat img = new Mat();
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

                newPoints = findColor(img);

                drawOnCanvas();
                Cv2.WaitKey(1);
            }
        }


        public static List<List<int>> findColor(Mat img)
        {
            Mat imgHSV = new Mat();
            Cv2.CvtColor(img, imgHSV, ColorConversionCodes.BGR2HSV);//H色调，S饱和度，V明度
            for(int i = 0;i < myColors.Count; i++)
            {
                Scalar lower = new Scalar(myColors[i][0], myColors[i][1], myColors[i][2]);
                Scalar upper = new Scalar(myColors[i][3], myColors[i][4], myColors[i][5]);
                Mat mask = new Mat();
                Cv2.InRange(imgHSV, lower, upper, mask);
                //Cv2.ImShow(i.ToString(), mask);
                Point myPoint = getContours(mask);
                if(myPoint.X !=0 && myPoint.Y !=0)
                {
                    newPoints.Add(new List<int> { myPoint.X, myPoint.Y, i });
                }
            }
            return newPoints;
        }
        //Chapter7中的函数修改了一些
        public static Point getContours(Mat imgDia)
        {

             
            Point[][] contours;
            HierarchyIndex[] hierarchy;//轮廓的层级

            Cv2.FindContours(imgDia, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            //Cv2.DrawContours(img, contours,-1, new Scalar(255,0,255),2);//-1表示画出所有轮廓

            Point[][] conPoly = new Point[contours.Length][];//角点
            Rect[] boundRect = new Rect[contours.Length];//外接矩形集合
            Point myPoint = new Point(0, 0);
            //过滤器
            for (int i = 0; i < contours.Length; i++)
            {
                double area = Cv2.ContourArea(contours[i]);
               // Console.WriteLine(area);

                if (area > 1000)
                {
                    double peri = Cv2.ArcLength(contours[i], true);//轮廓逼近参数
                    conPoly[i] = Cv2.ApproxPolyDP(contours[i], 0.02 * peri, true);
                    //轮廓  外接矩形
                    boundRect[i] = Cv2.BoundingRect(conPoly[i]);
                    myPoint.X = boundRect[i].X + boundRect[i].Width / 2;
                    myPoint.Y = boundRect[i].Y;


                    Cv2.Rectangle(img, boundRect[i].TopLeft, boundRect[i].BottomRight, new Scalar(0, 255, 0), 5);

                    Cv2.DrawContours(img, contours, -1, new Scalar(255, 0, 255), 2); //画轮廓
                }
            }
            return myPoint;
        }

        public static void drawOnCanvas()
        {
            for(int i = 0;i<newPoints.Count;i++)
            {
                Cv2.Circle(img, new OpenCvSharp.Point(newPoints[i][0], newPoints[i][1]), 10, myColorValues[newPoints[i][2]],-1);
            }
        }


    }
}