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
            double convert = Convert.ToDouble(((decimal)num).ToString());
            return Math.Sin(convert);
        }
        public static double cos(double num)
        {
            double convert = Convert.ToDouble(((decimal)num).ToString());
            return Math.Cos(convert);
        }
        public static double tan(double num)
        {
            double convert = Convert.ToDouble(((decimal)num).ToString());
            return Math.Tan(convert);
        }
        public static double cot(double num) {
            double convert = Convert.ToDouble(((decimal)num).ToString());
            return (Math.Cos(convert)) / (Math.Sin(convert));
        }
    }
}
