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
    class Project2
    {
        public static Mat imgOriginal = new Mat(), imgThre = new Mat();
        public static List<Point> initialPoints = new List<Point>();
        public static List<Point> docPoints = new List<Point>();
        public static Mat imgWarp = new Mat();
        public static Mat imgCrop = new Mat();
        public static Mat preProcessing(Mat img)
        {
            Mat imgGray = new Mat();
            Mat imgBlur = new Mat();
            Mat imgCanny = new Mat();
            Mat imgDia = new Mat();
            Cv2.CvtColor(img, imgGray, ColorConversionCodes.BGR2GRAY);//色彩转换
            Cv2.GaussianBlur(img, imgBlur, new OpenCvSharp.Size(3, 3), 3, 0); //高斯模糊
            Cv2.Canny(imgBlur, imgCanny, 25, 75);//canny边缘检测

            Mat kernel1 = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            Cv2.Dilate(imgCanny, imgDia, kernel1);//膨胀
            return imgDia;
        }
        public static List<Point> getContours(Mat imgDia)
        {


            Point[][] contours;
            HierarchyIndex[] hierarchy;//轮廓的层级

            Cv2.FindContours(imgDia, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            //Cv2.DrawContours(img, contours,-1, new Scalar(255,0,255),2);//-1表示画出所有轮廓

            Point[][] conPoly = new Point[contours.Length][];//角点
            Rect[] boundRect = new Rect[contours.Length];//外接矩形集合
            List<Point> biggest = new List<Point>(); 
            double maxArea = 0;
            //过滤器
            for (int i = 0; i < contours.Length; i++)
            {
                double area = Cv2.ContourArea(contours[i]);
                // Console.WriteLine(area);

                if (area > 1000)
                {
                    double peri = Cv2.ArcLength(contours[i], true);//轮廓逼近参数
                    conPoly[i] = Cv2.ApproxPolyDP(contours[i], 0.02 * peri, true);
                    if(area > maxArea && conPoly[i].Length == 4)
                    {
                        //Cv2.DrawContours(imgOriginal, contours, i, new Scalar(255, 0, 255), 2); //画轮廓
                        biggest = new List<Point> { conPoly[i][0],conPoly[i][1],conPoly[i][2],conPoly[i][3] };
                        maxArea = area;
                    }
                    
                }
            }
            return biggest;
        }

        public static void drawPoints(List<Point> points, Scalar color)
        {
            for(int i = 0; i < points.Count; i++)
            {
                Cv2.Circle(imgOriginal, points[i], 5, color, -1);
                Cv2.PutText(imgOriginal, i.ToString(), points[i], HersheyFonts.HersheyPlain, 2, color, 2);
            }
        }
        public static List<Point> reorder(List<Point> points)
        {
            List<Point> newPoints = new List<Point>();
            List<int> sumPoints = new List<int>();
            List<int> subPoints = new List<int>();
            for(int i = 0; i < 4; i++)
            {
                sumPoints.Add(points[i].X + points[i].Y);
                subPoints.Add(points[i].X - points[i].Y);
            }
            /*
             * 在这个例子中，num => num == numbers.Min()是一个lambda表达式，它表示一个接受一个int类型参数num并返回一个bool类型值的匿名函数。
             * 这个函数用于比较传入的数字num是否等于numbers集合中的最小值，如果相等则返回true，否则返回false。
               在lambda表达式中，箭头“=>”左侧的部分表示输入参数，右侧的部分表示函数体。箭头左侧的部分可以省略类型声明，
               因为C#编译器可以根据上下文自动推断类型。
               在这个例子中，我们使用lambda表达式作为FindIndex()方法的参数，FindIndex()方法会遍历List集合中的元素，对于每个元素，
               将其传递给lambda表达式并执行匿名函数，如果函数返回true，则表示该元素是我们要找的元素，FindIndex()方法返回该元素的索引号。
               因此，通过使用lambda表达式，我们可以在一行代码中查找集合中的最小值或最大值，并返回其索引号。
             */
            //四个点按照左上、右上、左下、右下顺序
            int Index1 = sumPoints.FindIndex(num => num == sumPoints.Min());
            newPoints.Add(points[Index1]);
            int Index2 = subPoints.FindIndex(num => num == subPoints.Max());
            newPoints.Add(points[Index2]);
            int Index3 = subPoints.FindIndex(num => num == subPoints.Min());
            newPoints.Add(points[Index3]);
            int Index4 = sumPoints.FindIndex(num => num == sumPoints.Max());
            newPoints.Add(points[Index4]);
            return newPoints;
        }
        public static Mat getWarp(Mat img,List<Point> points,float w,float h)
        {
            Point2f[] src = new Point2f[]{points[0], points[1], points[2], points[3]};
            Point2f[] dst = new Point2f[]
            {
                new Point2f(0.0f, 0.0f),
                new Point2f(w, 0.0f),
                new Point2f(0.0f, h),
                new Point2f(w, h)
            };

            Mat matrix = new Mat();
            matrix = Cv2.GetPerspectiveTransform(src, dst);//参数需要是Point2f类型
            Cv2.WarpPerspective(img, imgWarp, matrix, new OpenCvSharp.Size(w, h));
            return imgWarp;
        }

        static void Main(string[] args)
        {
            string path = @"C:\CodeLearning\Lesson_01\Lesson_01\Resources\paper.jpg";   //OK 不加@的话，\会被当作转义字符处理
            imgOriginal = Cv2.ImRead(path);
            Cv2.Resize(imgOriginal, imgOriginal, new OpenCvSharp.Size(), 0.5, 0.5);

            //Preprpcessing
            imgThre = preProcessing(imgOriginal);
            //Get Contours - Biggest
            
            initialPoints = getContours(imgThre);
            //drawPoints(initialPoints, new Scalar(0, 0, 255));  //这种方式四个点序号会乱
            docPoints = reorder(initialPoints);
            drawPoints(docPoints, new Scalar(0, 255, 0));   //四个点按照左上、右上、左下、右下顺序
            //Warp
            int w = 400, h = 500;
            imgWarp = getWarp(imgOriginal, docPoints, w, h);

            //Crop
            Rect roi = new Rect(5,5,w-(2*5),h-(2*5));
            //imgCrop = imgWarp(roi);
            Cv2.ImShow("Pic", imgOriginal);
            Cv2.ImShow("Image Thre", imgThre);
            Cv2.ImShow("Image Warp", imgWarp);
            //Cv2.ImShow("Image Crop", imgCrop);
            Cv2.WaitKey(0);
        }


    }
}
    