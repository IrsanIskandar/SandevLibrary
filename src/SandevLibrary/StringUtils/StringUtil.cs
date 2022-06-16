using System;
using System.Collections.Generic;
using System.Text;

namespace SandevLibrary.StringUtils
{
    public class StringUtil
    {
        private static readonly Random random = new Random();
        private static readonly string _numbers = "0123456789";
        private static readonly string _letterLowercase = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string _letterUppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatSize(Int64 bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }

            return string.Format("{0:n1} {1}", number, suffixes[counter]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateNumber(int length)
        {
            StringBuilder builder = new StringBuilder(length);
            string generate = string.Empty;

            for (int i = 1; i <= length; i++)
            {
                char repeat = _numbers[random.Next(0, _numbers.Length)];
                builder.Append(repeat);
            }

            generate = builder.ToString();

            return generate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateStringLowercase(int length)
        {
            StringBuilder builder = new StringBuilder(length);
            string generate = string.Empty;

            for (int i = 1; i <= length; i++)
            {
                char repeat = _letterLowercase[random.Next(0, _letterLowercase.Length)];
                builder.Append(repeat);
            }

            generate = builder.ToString();

            return generate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateStringUppercase(int length)
        {
            StringBuilder builder = new StringBuilder(length);
            string generate = string.Empty;

            for (int i = 1; i <= length; i++)
            {
                char repeat = _letterUppercase[random.Next(0, _letterUppercase.Length)];
                builder.Append(repeat);
            }

            generate = builder.ToString();

            return generate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateAlphanumeric(int length)
        {
            StringBuilder builder = new StringBuilder(length);
            string alphanumeric = string.Join(_letterLowercase, _letterUppercase, _numbers);
            string generate = string.Empty;

            for (int i = 1; i <= length; i++)
            {
                char repeat = alphanumeric[random.Next(0, alphanumeric.Length)];
                builder.Append(repeat);
            }

            generate = builder.ToString();

            return generate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="lastCount"></param>
        /// <param name="padChar"></param>
        /// <returns></returns>
        public static string PaddingLeft(int length, string lastCount, char padChar)
        {
            int totalWidth = length;
            string lastCounter = lastCount;

            return lastCounter.PadLeft(totalWidth, padChar);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="lastCount"></param>
        /// <param name="padChar"></param>
        /// <returns></returns>
        public static string PaddingRight(int length, string lastCount, char padChar)
        {
            int totalWidth = length;
            string lastCounter = lastCount;

            return lastCounter.PadRight(totalWidth, padChar);
        }
    }
}
