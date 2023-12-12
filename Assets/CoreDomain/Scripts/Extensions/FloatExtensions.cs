using System;

namespace CoreDomain.Scripts.Extensions
{
    public static class FloatExtensions
    {
        private const float Tolerance = 0.001f;

        public static bool EqualsWithTolerance(this float number, float otherNumber)
        {
            return Math.Abs(number - otherNumber) < Tolerance;
        }
     
        public static int GetSign(this float number)
        {
            if (number < 0)
            {
                return -1;
            }
            
            return number > 0 ? 1 : 0;
        }
    }
}