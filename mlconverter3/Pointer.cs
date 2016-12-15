using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mlconverter3
{
    class Pointer
    {
        /// <summary>
        /// converts a pointer to an unsigned integer
        /// </summary>
        public static int ToInt(int pointer)
        {
            pointer <<= 8;
            pointer >>= 8;
            return pointer & 0x00FFFFFF;
        }

        public static int ToGba(int pointer)
        {
            pointer &= 0x00FFFFFF;
            return pointer += 0x08000000;
        }
    }
}
