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
    /// Draw Shapes and Text
    /// </summary>
    class Chapter4
    {

        static void Main(string[] args)
        {

            //Blank Image
            //CV_8UC3:其中8是8bit图 U是无符号 C是color通道数为3
            //OpenCV的Scalar用的是BGR通道,Blue Green Red
            Mat img = new Mat(512,512,MatType.CV_8UC3,new Scalar(255,0,0));
            //画圆
            Cv2.Circle(img, new OpenCvSharp.Point(256, 256), 155, new Scalar(0, 69, 255),10);//10是粗度 也可以不写
            //Cv2.Circle(img, new OpenCvSharp.Point(256, 256), 155, new Scalar(0, 69, 255), -1);//当为负数的时候，进行填充圆形，C++用FILLED进行代替
           
            //画矩形
            Cv2.Rectangle(img, new OpenCvSharp.Point(130, 226), new OpenCvSharp.Point(382, 286), new Scalar(255, 255, 255), 3);

            //画线
            Cv2.Line(img, new OpenCvSharp.Point(130, 226), new OpenCvSharp.Point(382, 296), new Scalar(0, 255, 255), 3);
            //绘制文字   中文字体需要使用GDI+接口，通过GDI操作BitMap来绘制
            Cv2.PutText(img, "A Bad Apple!", new OpenCvSharp.Point(137, 262), HersheyFonts.HersheyDuplex, 0.75, new Scalar(255, 20, 245));

            Cv2.ImShow("Image", img);


            Cv2.WaitKey(0);

        }
    }
}
