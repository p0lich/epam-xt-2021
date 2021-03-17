using System;
using ExtendedText;

namespace EPAM_Task2._1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Constructors examples

            CustomString EmptyString = new CustomString();
            ShowInfo(EmptyString);

            CustomString StringFromSymbol = new CustomString('w');
            ShowInfo(StringFromSymbol);

            CustomString OldToNew = new CustomString("this is new string");
            ShowInfo(OldToNew);

            char[] charStr = new char[] { 'h', 'e', 'l', 'l', 'o' };
            CustomString StringFromChar = new CustomString(charStr);
            ShowInfo(StringFromChar);



            // String comparsion example

            // Strings with different length
            Console.WriteLine(CustomString.Compare(OldToNew, StringFromChar));

            // String with same lengths but different symbols
            CustomString StringForComparsion = new CustomString("this is big shrimp");
            //ShowInfo(stringForComparsion);

            Console.WriteLine(CustomString.Compare(OldToNew, StringForComparsion));

            // Equal strings
            Console.WriteLine(CustomString.Compare(OldToNew, OldToNew) + "\n");



            // Concat strings examples
            CustomString strAppend = new CustomString("hello ");
            ShowInfo(strAppend);

            // Using + operator

            // With custom string
            ShowInfo(strAppend + new CustomString("World"));

            // With normal string
            ShowInfo(strAppend + "wOrld");

            // With char array
            ShowInfo(strAppend + new char[] { 'w', 'o', 'R', 'l', 'd' });

            // With char symbol
            ShowInfo(strAppend + '!');



            // Using append method

            // With custom string
            strAppend.Append(new CustomString("big"));
            ShowInfo(strAppend);

            // With normal string
            strAppend.Append(" shiny ");
            ShowInfo(strAppend);

            // With char array
            strAppend.Append(new char[] { 'w', 'o', 'r', 'l', 'd' });
            ShowInfo(strAppend);

            // With char symbol
            strAppend.Append('!');
            ShowInfo(strAppend);



            // Index search exaple

            char indSymbol = 'l';
            Console.WriteLine("String: {0}\nSymbol: {1}\n",
                StringFromChar.ConvertToString(), indSymbol);

            // In all string
            Console.WriteLine(StringFromChar.IndexOf(indSymbol));

            // Search from 2 index
            Console.WriteLine(StringFromChar.IndexOf(indSymbol, 3));

            // Search from 1 to 3
            Console.WriteLine(StringFromChar.IndexOf(indSymbol, 1, 3) + "\n");



            // Convert to normal string
            Console.WriteLine("To string: {0}\nValue: {1}",
                StringFromChar.ConvertToString(),
                StringFromChar.ConvertToString().GetType() + "\n");

            // Convert to char array

            char[] CharArray = StringFromChar.ConvertToCharArray();

            Console.Write("To char array: ");
            for (int i = 0; i < StringFromChar.Length; i++)
            {
                Console.Write(CharArray[i]);
            }
            Console.WriteLine();

            Console.WriteLine("Value: " + StringFromChar.ConvertToCharArray().GetType() + "\n");



            // New custom methods

            // Method Repeat. String will be repeated multiple times
            CustomString strRepeat = new CustomString("hello");
            CustomString strRepeatCopy = new CustomString("hello");

            // Without parameters(repeat 1 time)
            strRepeat.Repeat();
            ShowInfo(strRepeat);

            // With repeat count
            strRepeat.Repeat(3);
            ShowInfo(strRepeat);

            // With repeat count and symbol separator
            strRepeatCopy.Repeat(4, '-');
            ShowInfo(strRepeatCopy);



            // New overloaded operator *. Work like Repeat, but return new string
            ShowInfo(StringFromChar * 3);



            // Method Reverse. String will be reversed in the specified range
            CustomString strReverse = new CustomString("string for reverse");

            // Without parameters(reverse all string)
            strReverse.Reverse();
            ShowInfo(strReverse);
            strReverse.Reverse();

            // Reverse start from 3 index
            strReverse.Reverse(3);
            ShowInfo(strReverse);
            strReverse.Reverse(3);

            // Reverse start from 3 index and end on 8 index
            strReverse.Reverse(3, 8);
            ShowInfo(strReverse);
            strReverse.Reverse(3, 8);


            // Method Encoding. Return encoded/decoded string using XOR encryption
            // Can change output of other strings

            CustomString OriginalText = new CustomString("secret message");
            CustomString EncodedText = OriginalText.Encoding("key");
            CustomString DecodedText = EncodedText.Encoding("key");

            Console.WriteLine("Original text: {0}\nEncoded text: {1}\nDecoded text: {2}",
                OriginalText.ConvertToString(),
                EncodedText.ConvertToString(),
                DecodedText.ConvertToString());
        }

        public static void ShowInfo(CustomString str)
        {
            Console.WriteLine("Custom string: {0}\nString length: {1}\nString capacity: {2}",
                str.ConvertToString(),
                str.Length,
                str.GetCapacity());

            Console.WriteLine();
        }
    }
}
