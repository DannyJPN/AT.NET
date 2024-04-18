using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthAppLib
{
    public class SimpleCalc
    {
        private double x, y, result;
        public void SetXY(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public void Add()
        {

            result = x + y;
        }
        private void Multiply()
        {
            result = x * y;

        }
        private void ShowResult()
        {
            Console.WriteLine(result);
        }

    }
}
