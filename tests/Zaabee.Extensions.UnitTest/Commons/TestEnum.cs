using System;
using System.ComponentModel;

namespace Zaabee.Extensions.UnitTest.Commons
{

    [Flags]
    public enum TestEnum
    {
        [Description("A")] Create = 1,
        [Description("B")] Delete = 2,
        [Description("C")] Modify = 4,
        [Description("D")] Query = 8
    }
}