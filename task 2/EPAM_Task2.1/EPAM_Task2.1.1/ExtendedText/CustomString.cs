using System;

namespace ExtendedText
{
    public class CustomString
    {
        private char[] Data;
        private int length;

        public int Length
        {
            get
            {
                return length;
            }
        }

        #region Constructors

        public CustomString()
        {
            this.Data = new char[1];
            this.length = 0;
        }

        public CustomString(char symbol)
        {
            this.Data = new char[1] { symbol };
            this.length = 1;
        }

        public CustomString(string oldString)
        {
            this.Data = oldString.ToCharArray();
            this.length = oldString.Length;
        }

        public CustomString(char[] charArray)
        {
            this.Data = charArray;
            this.length = charArray.Length;
        }

        #endregion

        public char this[int index]
        {
            get
            {
                return Data[index];
            }

            set
            {
                Data[index] = value;
            }
        }

        #region Private methods

        private void IncreaseCapacity(int newLength)
        {
            Array.Resize(ref Data, (int)Math.Pow(2, 1 + (int)Math.Log2(newLength)));
            length = newLength;
        }

        private void DataIncrease(char[] charArray, int oldLength, int newLength)
        {
            if (newLength > Data.Length)
            {
                IncreaseCapacity(newLength);
            }

            else
            {
                length = newLength;
            }

            for (int i = oldLength, strInd = 0; i < newLength; i++, strInd++)
            {
                Data[i] = charArray[strInd];
            }
        }

        private static CustomString DataSum(CustomString str1, CustomString str2)
        {
            char[] ConcatString = new char[str1.Length + str2.Length];
            int counter = 0;

            for (int i = 0; i < str1.Length; i++, counter++)
            {
                ConcatString[counter] = str1[i];
            }

            for (int i = 0; i < str2.Length; i++, counter++)
            {
                ConcatString[counter] = str2[i];
            }

            return new CustomString(ConcatString);
        }

        private static void IndexCorrector(int strLength, ref int startIndex, ref int endIndex)
        {
            if (startIndex < 0)
            {
                startIndex = 0;
            }

            if (startIndex > endIndex)
            {
                startIndex = strLength - 1;
            }

            if (endIndex < 0)
            {
                endIndex = 0;
            }

            if (endIndex > strLength)
            {
                endIndex = strLength - 1;
            }
        }

        #endregion

        #region Standart methods and operators

        public int GetCapacity()
        {
            return Data.Length;
        }

        public string ConvertToString()
        {
            return new string(Data);
        }

        public char[] ConvertToCharArray()
        {
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = Data[i];
            }

            return result;
        }

        public void Append(CustomString str)
        {
            int newLength = length + str.Length;
            DataIncrease(str.ConvertToCharArray(), length, newLength);
        }

        public void Append(string str)
        {
            int newLength = length + str.Length;
            DataIncrease(str.ToCharArray(), length, newLength);
        }

        public void Append(char[] charArray)
        {
            int newLength = length + charArray.Length;
            DataIncrease(charArray, length, newLength);
        }

        public void Append(char symbol)
        {
            int newLength = length + 1;
            DataIncrease(new char[] { symbol }, length, newLength);
        }

        public static CustomString operator +(CustomString str1, CustomString str2)
        {
            return DataSum(str1, str2);
        }

        public static CustomString operator +(CustomString str1, string str2)
        {
            return DataSum(str1, new CustomString(str2));
        }

        public static CustomString operator +(CustomString str1, char[] str2)
        {
            return DataSum(str1, new CustomString(str2));
        }

        public static CustomString operator +(CustomString str1, char symbol)
        {
            return DataSum(str1, new CustomString(new char[] { symbol }));
        }

        public static bool Compare(CustomString str1, CustomString str2)
        {
            if (str1.Length != str2.Length)
            {
                return false;
            }

            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int IndexOf(char symbol, int startIndex = 0, int endIndex = int.MaxValue)
        {
            IndexCorrector(length, ref startIndex, ref endIndex);

            for (int i = startIndex; i <= endIndex; i++)
            {
                if (Data[i] == symbol)
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #region New methods and operators

        public void Reverse(int startIndex = 0, int endIndex = int.MaxValue)
        {
            IndexCorrector(length, ref startIndex, ref endIndex);

            int diff = endIndex - startIndex;

            int iterCount = (diff % 2 == 0) ? diff : diff + 1;

            for (int i = 0; i < iterCount / 2; i++)
            {
                char temp = Data[startIndex + i];
                Data[startIndex + i] = Data[endIndex - i];
                Data[endIndex - i] = temp;
            }
        }

        public void Repeat()
        {
            int newLength = length * 2;
            DataIncrease(Data, length, newLength);
        }

        public void Repeat(int repCount)
        {
            if (repCount <= 1)
            {
                return;
            }

            int newLength = length * repCount;
            char[] newData = new char[newLength - length];

            for (int i = 0; i < newData.Length; i++)
            {
                newData[i] = Data[i % length];
            }

            DataIncrease(newData, length, newLength);
        }

        public void Repeat(int repCount, char separator)
        {
            if (repCount <= 1)
            {
                return;
            }

            int newLength = (length + 1) * repCount;
            char[] newData = new char[newLength - length];

            int charInd = 0;

            for (int i = 0; i < newData.Length - repCount; i++)
            {
                if ((i % length == 0) && (i != newLength - repCount - 1))
                {
                    newData[i + charInd] = separator;
                    charInd++;
                }
                newData[i + charInd] = Data[i % length];
            }

            DataIncrease(newData, length, newLength);
        }

        public static CustomString operator *(CustomString str, int repCount)
        {
            int newLength = str.Length * repCount;
            char[] newData = new char[newLength];

            for (int i = 0; i < newLength; i++)
            {
                newData[i] = str[i % str.Length];
            }

            return new CustomString(newData);
        }

        public CustomString Encoding(string key)
        {
            CustomString EncodedMessage = new CustomString();

            for (int i = 0; i < length; i++)
            {
                EncodedMessage.Append((char)(Data[i] ^ key[i % key.Length]));
            }

            return EncodedMessage;
        }

        #endregion
    }
}
