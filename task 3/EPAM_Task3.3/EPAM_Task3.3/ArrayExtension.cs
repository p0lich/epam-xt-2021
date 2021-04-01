using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EPAM_Task3._3
{
    // Task 3.3.1

    public static class ArrayExtension
    {
        public static void UpdateData(this int[] array, Func<int, int> func)
        {
            if (func != null)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = func.Invoke(array[i]);
                }
            }
        }

        public static int GetSum(this int[] array)
        {
            int sum = 0;

            foreach (var item in array)
            {
                sum += item;
            }

            return sum;
        }

        public static int GetAverage(this int[] array)
        {
            int average = 0;

            foreach (var item in array)
            {
                average += item;
            }

            return average / array.Length;
        }

        public static int GetMostCommon(this int[] array)
        {
            return array.GroupBy(value => value).
                OrderByDescending(group => group.Count()).
                First().
                First();
        }
    }
}
