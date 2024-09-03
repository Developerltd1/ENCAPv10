using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Extensions
{
    public static class DoubleExtensions
    {
      
        public static double ToDouble(this string value)
        {
            if (double.TryParse(value, out double result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid double format");
            }
        }
    }
}
