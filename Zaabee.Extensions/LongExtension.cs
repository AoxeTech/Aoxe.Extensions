using System.Collections.Generic;
using System.Text;
using Zaabee.Extensions.Commons;

namespace Zaabee.Extensions
{
    public static class LongExtension
    {
        public static string ToString(this long dec, NumberSystem numberSystem)
        {
            var stack = new Stack<int>();
            var i = (int) numberSystem;
            while (dec > 0)
            {
                stack.Push((int) (dec % i));
                dec /= i;
            }
            var sb = new StringBuilder();
            while (stack.Count > 0) sb.Append(Consts.Chars[stack.Pop()]);
            return sb.ToString();
        }
    }
}