using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EPAM_Task3._3
{
    // Task 3.3.2

    public static class StringExtension
    {
        // UTF 16 numbers

        // english capital letters:
        // decimal numbers: 65 - 90
        // hexadecimals numbers: 0041 - 005A

        // russian capital letters
        // decimal numbers: 1025(Ё), 1040 - 1071
        // hexadecimals numbers: 0401(Ё), 0410 - 042F

        public static string GetLanguage(this string str)
        {
            if (str == "")
            {
                return "String is empty";
            }

            str = str.ToUpper();

            if (str.All(c => c >= 65 && c <= 90))
            {
                return "English language";
            }

            if (str.All(c => c == 1025 || (c >= 1040 && c <= 1071)))
            {
                return "Russian language";
            }

            return "Mixed string";
        }
    }
}
