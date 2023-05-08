using System.IO;
using System.Linq;
using System.Numerics;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using static System.Net.Mime.MediaTypeNames;

namespace Lesson_01
{
    /// <summary>
    /// Warp Images
    /// </summary>
    class Chapter5
    {
        public const float w = 250, h = 350;
        
        static void Main(string[] args)
        {
            string path = @"C:\CodeLearning\Lesson_01\Lesson_01\Resources\cards.jpg";
            Mat img = Cv2.ImRead(path);
            Point2f[] src = new Point2f[]
            {
                new Point2f(529, 142),
                new Point2f(771, 190 ),
                new Point2f(405, 395 ),
                new Point2f(674, 457 )
            };
            Point2f[] dst = new Point2f[]
            {
                new Point2f(0.0f, 0.0f),
                new Point2f(w, 0.0f),
                new Point2f(0.0f, h),
                new Point2f(w, h)
            };
            
            //Point2f[] src = new Point2f[] { new Point2f(529, 142), new Point2f(771, 190 ), new Point2f(405, 395 ), new Point2f(674, 457 ) };
            //Point2f[] dst = new Point2f[] { new Point2f(0.0f, 0.0f), new Point2f(w, 0.0f), new Point2f(0.0f, h), new Point2f(w, h) };

            Mat matrix = new Mat(), imgWarp = new Mat();
            matrix = Cv2.GetPerspectiveTransform(src, dst);//参数需要是Point2f类型
            Cv2.WarpPerspective(img,imgWarp,matrix, new OpenCvSharp.Size(w,h));

            //将透视变换输入的图像要变换的四个点画出
             for(int i = 0; i < 4; i++)
            {
                //Console.WriteLine(src[i].X);
                float circle_xf = src[i].X;
                float circle_yf = src[i].Y;
                int circle_x = (int)circle_xf;
                int circle_y = (int)circle_yf;
                Cv2.Circle(img,circle_x, circle_y, 10 , new Scalar(0, 69, 255), 5);
            }

            Cv2.ImShow("Image", img);
            Cv2.ImShow("Image Warp", imgWarp);

            Cv2.WaitKey(0);

        }
    }
}
