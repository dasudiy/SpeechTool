using System;
using System.Collections.Generic;
using System.Text;

namespace TTSTool.Classes
{
    public static class ArgumentsHelper
    {
        public static T ArgumentRead<T>(this string[] arguments, string argumentName, T defaultValue = default(T))
        {
            var index = Array.IndexOf(arguments, argumentName);
            if (index >= 0 && arguments.Length >= index + 2)
            {
                return arguments[index + 1].To<T>();
            }
            return defaultValue;
        }

        public static bool ArgumentExist(this string[] arguments, string argumentName) => Array.IndexOf(arguments, argumentName) >= 0;
    }
}
