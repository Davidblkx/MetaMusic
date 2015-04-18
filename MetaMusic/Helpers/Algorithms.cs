using System;

namespace MetaMusic.Helpers
{
    public static class Algorithms
    {
        /// <summary>
        ///     Calculate the difference between 2 strings using the Levenshtein distance algorithm
        /// </summary>
        /// <param name="s1">First string</param>
        /// <param name="s2">Second string</param>
        /// <returns></returns>
        public static int LevenshteinDistance(string s1, string s2) //O(n*m)
        {
            var s1Length = s1.Length;
            var s2Length = s2.Length;

            var matrix = new int[s1Length + 1, s2Length + 1];

            // First calculation, if one entry is empty return full length
            if (s1Length == 0)
            {
                return s2Length;
            }

            if (s2Length == 0)
            {
                return s1Length;
            }

            // Initialization of matrix with row size S1Length and columns size S2Length
            for (var i = 0; i <= s1Length; matrix[i, 0] = i++)
            {
            }
            for (var j = 0; j <= s2Length; matrix[0, j] = j++)
            {
            }

            // Calculate rows and collumns distances
            for (var i = 1; i <= s1Length; i++)
            {
                for (var j = 1; j <= s2Length; j++)
                {
                    var cost = (s2[j - 1] == s1[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }
            // return result
            return matrix[s1Length, s2Length];
        }
    }
}