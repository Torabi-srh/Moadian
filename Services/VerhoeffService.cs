using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Services
{
    public static class VerhoeffService
    {
        private static readonly int[,] MULTIPLICATION_TABLE = new int[,]
        {
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            { 1, 2, 3, 4, 0, 6, 7, 8, 9, 5 },
            { 2, 3, 4, 0, 1, 7, 8, 9, 5, 6 },
            { 3, 4, 0, 1, 2, 8, 9, 5, 6, 7 },
            { 4, 0, 1, 2, 3, 9, 5, 6, 7, 8 },
            { 5, 9, 8, 7, 6, 0, 4, 3, 2, 1 },
            { 6, 5, 9, 8, 7, 1, 0, 4, 3, 2 },
            { 7, 6, 5, 9, 8, 2, 1, 0, 4, 3 },
            { 8, 7, 6, 5, 9, 3, 2, 1, 0, 4 },
            { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }
        };

        private static readonly int[,] PERMUTATION_TABLE = new int[,]
        {
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            { 1, 5, 7, 6, 2, 8, 3, 0, 9, 4 },
            { 5, 8, 0, 3, 7, 9, 6, 1, 4, 2 },
            { 8, 9, 1, 6, 0, 4, 3, 5, 2, 7 },
            { 9, 4, 5, 3, 1, 2, 6, 8, 7, 0 },
            { 4, 2, 8, 6, 5, 7, 3, 9, 0, 1 },
            { 2, 7, 9, 3, 8, 0, 6, 4, 1, 5 },
            { 7, 0, 4, 6, 9, 1, 3, 2, 5, 8 }
        };

        private static readonly int[] INVERSE_TABLE = new int[] { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };

        public static int CheckSum(string number)
        {
            int c = 0;
            int len = number.Length;

            for (int i = 0; i < len; ++i)
            {
                c = MULTIPLICATION_TABLE[c, PERMUTATION_TABLE[((i + 1) % 8), number[len - i - 1] - '0']];
            }

            return INVERSE_TABLE[c];
        }

        public static bool validate(string number)
        {
            var c = 0;
            var len = number.Length;
            for (int i = 0; i < len; i++)
            {
                c = MULTIPLICATION_TABLE[c, PERMUTATION_TABLE[(i % 8), number[len - i - 1] - '0']];
            }
            return c == 0;
        }
    }
}
