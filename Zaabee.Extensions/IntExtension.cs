using System.Collections.Generic;
using System.Text;
using Zaabee.Extensions.Commons;

namespace Zaabee.Extensions
{
    public static class IntExtension
    {
        public static string ToString(this int dec, NumberSystem numberSystem)
        {
            var stack = new Stack<int>();
            var i = (int) numberSystem;
            var sb = new StringBuilder();
            while (dec > 0)
            {
                stack.Push(dec % i);
                dec /= i;
            }
            while (stack.Count > 0) sb.Append(Consts.Chars[stack.Pop()]);
            return sb.ToString();
        }
    }
}