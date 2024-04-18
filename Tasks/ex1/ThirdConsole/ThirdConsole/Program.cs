using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Bitmap bmp = new Bitmap(800, 620);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.Gainsboro),0,0,bmp.Width,bmp.Height);
            g.DrawLine(new Pen(Color.BlueViolet,10),0, bmp.Width/2-5 , bmp.Height, bmp.Width / 2 - 5);
            string name = "Dan";
            g.DrawString(name, new Font("Arial", 50), new SolidBrush(Color.BlueViolet), bmp.Width / 2 - g.MeasureString(name, new Font("Arial", 50)).Width / 2, bmp.Height / 3);
            string surname = "Dan";
            g.DrawString(surname, new Font("Arial", 50), new SolidBrush(Color.BlueViolet), bmp.Width / 2 - g.MeasureString(surname, new Font("Arial", 50)).Width / 2, bmp.Height / 3);
         


            Image flower = Image.FromFile("../../../flower_gold.png");
            g.DrawImage(flower,bmp.Width*3/4, 50, bmp.Height*3/4, bmp.Height * 3 / 4);
            string login = "KRU0142";
            g.DrawString(login, new Font("Arial", 50), new SolidBrush(Color.BlueViolet), bmp.Width * 3 / 4, flower.Height + 55);


            flower.Dispose();
            g.Dispose();
            bmp.Save("Meishi.png", ImageFormat.Png);
            bmp.Save("Meishi.jpeg", ImageFormat.Jpeg);

            bmp.Dispose();








        }
    }
}
