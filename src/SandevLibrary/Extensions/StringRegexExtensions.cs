using System.Text.RegularExpressions;

namespace SandevLibrary.Extensions
{
    public static class StringRegexExtensions
    {
        private const string EMAIL_PATTERN = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
        private const string PHONE_PATTERN = "^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$";
        private const string URL_PATTERN = "^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$";
        private const string PASSWORD_PATTERN = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*()_]).{8,}$";

        public static bool EmailCheckValidation(this string email)
        {
            Regex regex = new Regex(EMAIL_PATTERN);
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool PhoneCheckValidation(this string phone)
        {
            Regex regex = new Regex(PHONE_PATTERN);
            Match match = regex.Match(phone);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool UrlCheckValidation(this string url)
        {
            Regex regex = new Regex(URL_PATTERN);
            Match match = regex.Match(url);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool PasswordCheckValidation(this string password)
        {
            Regex regex = new Regex(PASSWORD_PATTERN);
            Match match = regex.Match(password);
            if (match.Success)
                return true;
            else
                return false;
        }
    }
}
