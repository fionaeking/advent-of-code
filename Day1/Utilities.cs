using System;

namespace Utilities
{
    class Utilities
    {
        public static int roundDown(float varOne)
        {
            return Convert.ToInt32(varOne);
        }

        public static float divideByNum(int input_num, int divisor)
        {
            return input_num/divisor;
        }

        public static int subtractNum(int original_num, int num_to_subtract)
        {
            return original_num - num_to_subtract;
        }
    }
}