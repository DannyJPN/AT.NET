using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwelfthConsoleApp
{
    public class Valider
    {

        public static bool Validate(int ICO)
        {
            string ICOtext = ICO.ToString();
            if (ICOtext.Length != 8)
            { return false; }
            if (ICO <= 0)
            { return false; }
            int sum = 0;

            for (int i = 0, m = 8; i < ICOtext.Length - 1; i++, m--)
            {
                sum += Convert.ToInt32(ICOtext[i].ToString()) * m;
            }

            int remain = sum % 11;

            if (remain == 0 && Convert.ToInt32(ICOtext[ICOtext.Length - 1].ToString()) == 1)
            {
                return true;
            }

            else if (remain == 1 && Convert.ToInt32(ICOtext[ICOtext.Length - 1].ToString()) == 0)
            {
                return true;
            }

            else if (remain != 0 && remain != 0 && Convert.ToInt32(ICOtext[ICOtext.Length - 1].ToString()) == 11 - remain)
            {
                return true;
            }
            else
            { return false; }




        }
    }
}
