using System.Text;
using System.Text.RegularExpressions;

namespace SandevLibrary.SecurityAlgorithm
{
    public class ROT13Algorithm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string ROT13(string plainText)
        {
            StringBuilder result = new StringBuilder();
            Regex regex = new Regex("[A-Za-z]");
            foreach (char c in plainText)
            {
                if (regex.IsMatch(c.ToString()))
                {
                    int code = ((c & 223) - 52) % 26 + (c & 32) + 65;
                    result.Append((char)code);
                }
                else
                    result.Append(c);
            }
            return result.ToString();
        }
    }
}
