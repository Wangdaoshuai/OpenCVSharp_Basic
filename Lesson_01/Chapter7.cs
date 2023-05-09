using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Formats.Asn1;
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
    class Chapter7
    {

        static void Main(string[] args)
        {

            string path = @"C:\CodeLearning\Lesson_01\Lesson_01\Resources\shapes.png";
            Mat img = Cv2.ImRead(path);

            //图像预处理
            Mat imgGray = new Mat();
            Cv2.CvtColor(img, imgGray, ColorConversionCodes.BGR2GRAY);//色彩转换
            Mat imgBlur = new Mat();
            Cv2.GaussianBlur(img, imgBlur, new OpenCvSharp.Size(3, 3), 3, 0); //高斯模糊
            Mat imgCanny = new Mat();
            Cv2.Canny(imgBlur, imgCanny, 25, 75);//canny边缘检测
            Mat imgDia = new Mat();///膨胀
            //定义一个自定义膨胀的内核
            Mat kernel1 = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            Cv2.Dilate(imgCanny, imgDia, kernel1);//膨胀

            getContours(imgDia,img);

            Cv2.ImShow("Image", img);
            //Cv2.ImShow("Image Gray", imgGray);
            //Cv2.ImShow("Image Blur", imgBlur);
            //Cv2.ImShow("Image Canny", imgCanny);
            //Cv2.ImShow("Image Dilataion", imgDia);
           

            Cv2.WaitKey(0);
        }
        static public void getContours(Mat imgDia,Mat img)
        {
            /*
             * contours 的类型是Point[][]，它相当于OpenCV中的Vector<Vector<Point>> contours,
             * 存储多个轮廓，每个轮廓是由若干个点组成，可以在该函数前声明Point[][] contours;，
             * 在C#中没有赋值的变量在用的时候是不允许的，因为它是输出的结果，可以不需要给它new空间，
             * 但必须在函数的参数中声明是out；参数hierarchy为包含图像拓扑结构的信息，
             * 它是HierarchyIndex[]类型，这是输入的结果，同样要在函数的参数中声明为out。
             */
            Point[][] contours;
            HierarchyIndex[] hierarchy;//轮廓的层级

            Cv2.FindContours(imgDia,out contours,out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            //Cv2.DrawContours(img, contours,-1, new Scalar(255,0,255),2);//-1表示画出所有轮廓

            Point[][] conPoly = new Point[contours.Length][];//角点
            Rect[] boundRect = new Rect[contours.Length];//外接矩形集合
            string objectType = "";
            //过滤器
            for (int i = 0; i < contours.Length; i++)
            {
                double area = Cv2.ContourArea(contours[i]);
                Console.WriteLine(area);

               // Point[][] conPoly = new Point[contours.Length][];//角点
                if (area > 1000)
                {
                    double peri = Cv2.ArcLength(contours[i], true);//轮廓逼近参数
                    conPoly[i] = Cv2.ApproxPolyDP(contours[i], 0.02 * peri, true);

                    Cv2.DrawContours(img, contours, -1, new Scalar(255, 0, 255), 2); //画轮廓
                    //Cv2.DrawContours(img, conPoly[i], i, new Scalar(255, 0, 255), 2); // 报错

                    //轮廓  外接矩形
                    boundRect[i] = Cv2.BoundingRect(conPoly[i]);
                    Cv2.Rectangle(img, boundRect[i].TopLeft, boundRect[i].BottomRight, new Scalar(0, 255, 0), 5);
                    //判断不同角点集合属于什么图形
                    int objCor = conPoly[i].Length;
                    if(objCor == 3) { objectType = "Tri"; }
                    else if(objCor == 4) 
                    { 
                        float aspRatio = (float)boundRect[i].Width / (float)boundRect[i].Height;//宽高比
                        if(aspRatio > 0.95 && aspRatio < 1.05) { objectType = "Square"; }
                        else objectType = "Rect"; 
                    }
                    else if(objCor>4) { objectType = "Circle"; }
                    Cv2.PutText(img, objectType, new OpenCvSharp.Point(boundRect[i].X,boundRect[i].Y - 5), HersheyFonts.HersheyDuplex, 0.75, new Scalar(0, 69, 245),2);
                    //new OpenCvSharp.Point(137, 262)
                    //Console.WriteLine(conPoly[i].Length);
                }
            }
        }
    }
}