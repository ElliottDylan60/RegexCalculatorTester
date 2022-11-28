using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionMaker
{
    //static double test = 0;
    public static class MyMath
    {
        public static double sin(double num)
        {
            double test = Convert.ToDouble(((decimal)num).ToString());
            return Math.Sin(test);
        }
        public static double cos(double num)
        {
            double test = Convert.ToDouble(((decimal)num).ToString());
            return Math.Cos(test);
        }
        public static double tan(double num)
        {
            double test = Convert.ToDouble(((decimal)num).ToString());
            return Math.Tan(test);
        }
    }
}
