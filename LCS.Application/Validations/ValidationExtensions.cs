using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LCS.Application.Validations
{
    internal static class ValidationExtensions
    {
        static string phoneNoPattern = @"\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}";
        static string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
       public static bool EmailValid(this string email)
        {
            return Regex.IsMatch(email, emailPattern);
        }
        public static bool PhoneNoValid(this string phone)
        {
            return Regex.IsMatch(phone, phoneNoPattern);
        }

        public static bool FutureDate(this DateTime date, DateTime? max = null)
        {
            if (max == null)
                return date > DateTime.Now;
            return date> DateTime.Now&& date <= max;
        }
        public static bool PastDate(this DateTime date,DateTime? min=null)
        {
            if (min == null)
                return date < DateTime.Now;
            return date < DateTime.Now && date >= min;

        }

        public static bool StringMaxLength(this string str, int maxlength=150)
        {
            return str.Length <= maxlength && str.Trim().Length>0;
        }
    }
}
