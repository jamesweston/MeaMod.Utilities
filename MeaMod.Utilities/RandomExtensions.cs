using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MeaMod.Utilities
{
    /// <summary>
    /// Extensions to Random Class
    /// </summary>
    public class RandomExtensions : Random
    {
        /// <summary>
        /// Create Random BigInteger with range
        /// </summary>
        /// <param name="min">Lower Limit</param>
        /// <param name="max">Upper Limit</param>
        /// <returns></returns>
        public BigInteger CreateRandomInRange(BigInteger min, BigInteger max)
        {
            string numeratorString, denominatorString;
            double fraction = this.NextDouble();
            BigInteger inRange;

            //Maintain all 17 digits of precision, 
            //but remove the leading zero and the decimal point;
            numeratorString = fraction.ToString("G17").Remove(0, 2);

            //Use the length instead of 17 in case the random
            //fraction ends with one or more zeros
            denominatorString = string.Format("1E{0}", numeratorString.Length);

            inRange = (max - min) * BigInteger.Parse(numeratorString) /
               BigInteger.Parse(denominatorString,
               System.Globalization.NumberStyles.AllowExponent)
               + min;
            return inRange;
        }
    }
}
