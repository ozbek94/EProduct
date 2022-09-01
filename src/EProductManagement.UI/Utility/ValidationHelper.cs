using System;
using System.Linq;

namespace EProductManagement.UI.Utility
{
    public class ValidationHelper
    {
        public static bool BeAGuid(string Text)
        {
            Guid guid;

            if (Guid.TryParse(Text, out guid))
                return true;
            return false;

        }

        public static bool BeAllDigits(string Code)
        {
            if (string.IsNullOrEmpty(Code) || !Code.All(char.IsDigit))
                return false;
            return true;
        }
    }
}