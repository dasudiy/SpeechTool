using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TTSTool.Classes
{
    public static class Validator
    {
        public static bool IsNull(this object o) { return o == null; }
        public static bool IsOddInt(this int o) { return o / 2 * 2 != o; }
        public static bool IsSByte(this object o) { if (o == null) { return false; } sbyte i = 0; return sbyte.TryParse(o.ToString(), out i); }
        public static bool IsByte(this object o) { if (o == null) { return false; } byte i = 0; return byte.TryParse(o.ToString(), out i); }
        public static bool IsShort(this object o) { if (o == null) { return false; } short i = 0; return short.TryParse(o.ToString(), out i); }
        public static bool IsUShort(this object o) { if (o == null) { return false; } ushort i = 0; return ushort.TryParse(o.ToString(), out i); }
        public static bool IsInt(this object o) { if (o == null) { return false; } var i = 0; return int.TryParse(o.ToString(), out i); }
        public static bool IsUInt(this object o) { if (o == null) { return false; } uint i = 0; return uint.TryParse(o.ToString(), out i); }
        public static bool IsDouble(this object o) { if (o == null) { return false; } double i = 0; return double.TryParse(o.ToString(), out i); }
        public static bool IsULong(this object o) { if (o == null) { return false; } ulong i = 0; return ulong.TryParse(o.ToString(), out i); }
        public static bool IsFloat(this object o) { if (o == null) { return false; } float i = 0; return float.TryParse(o.ToString(), out i); }
        public static bool IsLong(this object o) { if (o == null) { return false; } long i = 0; return long.TryParse(o.ToString(), out i); }
        public static bool IsDecimal(this object o) { if (o == null) { return false; } decimal i = 0; return decimal.TryParse(o.ToString(), out i); }
        public static bool IsBool(this object o) { if (o == null) { return false; } bool i = false; return bool.TryParse(o.ToString(), out i); }
        public static bool IsGuid(this object o) { if (o == null) { return false; } Guid i = Guid.Empty; return Guid.TryParse(o.ToString(), out i); }
        public static bool IsDateTime(this object o) { if (o == null) { return false; } DateTime i = DateTime.MinValue; return DateTime.TryParse(o.ToString(), out i); }
        public static bool IsPastDateTime(this object o)
        {
            if (o == null) { return false; }
            DateTime i = DateTime.MinValue;
            if (!DateTime.TryParse(o.ToString(), out i)) { return false; }
            return i > DateTime.MinValue && i < DateTime.Now;
        }
        public static bool IsUserName(this object o) { return o != null && new Regex(@"^[a-zA-Z]\w+$").IsMatch(o.ToString()); }
        public static bool IsPostcode(this object o) { return o != null && new Regex(@"^[1-9]\d{5}$").IsMatch(o.ToString()); }
        public static bool IsAge(this object o) { if (!o.IsInt()) { return false; } var a = int.Parse(o.ToString()); return a >= 18 && a < 200; }
        public static bool IsNaturalNumber(this object o) { if (o == null) { return false; } int i = 0; if (!int.TryParse(o.ToString(), out i)) { return false; } return i >= 0; }
        public static bool IsEmpty(this Guid o) { return Guid.Empty.Equals(o); }
        public static bool IsEmptyString(this object o) { return o == null || string.IsNullOrWhiteSpace(o.ToString()); }

        public static void NotEmpty(this string input, string errorInfo = "不能为空")
        {
            if (!NotEmpty(input)) { throw new Exception(errorInfo); }
        }

        public static bool NotEmpty(this string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static void IsEmail(this string email, string errorInfo = "邮件格式不正确")
        {
            if (!IsEmail(email)) { throw new Exception(errorInfo); }
        }

        public static bool IsEmail(this string email)
        {
            return IsMatch(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", email);
        }

        public static bool IsIdentityNumber(this string Id)
        {
            if (!IsMatch(@"^\d{17}[\dxX]$", Id)) { return false; }
            long n = 0;

            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }

            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");

            DateTime time = new DateTime();

            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');

            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');

            char[] Ai = Id.Remove(17).ToCharArray();

            int sum = 0;

            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }

            int y = -1;

            Math.DivRem(sum, 11, out y);

            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }

            return true;//符合GB11643-1999标准
        }

        public static int GetAge(DateTime dayOfBirth, DateTime? checkDate)
        {
            DateTime today = (checkDate ?? DateTime.Now).Date;
            int age = today.Year - dayOfBirth.Year;
            if (dayOfBirth > today.AddYears(-age)) age--;
            return age;
        }

        public static bool IsPhoneNumber(this string phone)
        {
            return IsMobilePhoneNumber(phone) || IsLineTelephoneNumber(phone);
        }

        public static bool IsMobilePhoneNumber(this string phone)
        {
            return IsMatch(@"^1\d{10}$", phone);
        }

        public static bool IsLineTelephoneNumber(this string phone)
        {
            return IsMatch(@"^0\d{10,11}$", phone);
        }

        public static bool IsPostcode(this string postcode)
        {
            return IsMatch(@"^\d{6}$", postcode);
        }

        public static bool IsMatch(this string regex, string input)
        {
            return Regex.IsMatch(input, regex);
        }

        public static void IsMatch(this string regex, string input, string errorInfo = "输入格式不正确")
        {
            if (!IsMatch(regex, input))
            {
                throw new Exception(errorInfo);
            }
        }

        public static bool IsAllGB2312ChineseChars(this string input)
        {
            var encoding = System.Text.Encoding.GetEncoding("gbk");
            var result = true;
            foreach (var c in input)
            {
                var bytes = encoding.GetBytes(new char[] { c });
                if (bytes.Length < 2)
                {//跳过半角字符...
                    continue;
                }

                result &= bytes[0] >= 176 && bytes[0] <= 247 && bytes[1] >= 160 && bytes[1] <= 254;

                if (!result) { break; }
            }

            return result;
        }

        public static bool IsAllGBKChineseChars(this string input)
        {
            var encoding = System.Text.Encoding.GetEncoding("gbk");
            var result = true;
            foreach (var c in input)
            {
                var bytes = encoding.GetBytes(new char[] { c });
                if (bytes.Length < 2)
                {//跳过半角字符...
                    continue;
                }

                result &= bytes[0] >= 129 && bytes[0] <= 254 && bytes[1] >= 64 && bytes[1] <= 254;

                if (!result) { break; }
            }

            return result;
        }
    }

}
