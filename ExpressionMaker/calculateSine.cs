using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionMaker
{
    //static double test = 0;
    public static class calculateSine
    {
        public static double MathSine(double num)
        {
            double test = Convert.ToDouble(((decimal)num).ToString());
            return Math.Sin(test);
        }
        public static double MathCos(double num)
        {
            double test = Convert.ToDouble(((decimal)num).ToString());
            return Math.Cos(test);
        }
        public static double MathTan(double num)
        {
            double test = Convert.ToDouble(((decimal)num).ToString());
            return Math.Tan(test);
        }
    }
}
