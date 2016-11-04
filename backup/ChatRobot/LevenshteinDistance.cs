using System;

namespace ChatRobot
{
    /// <summary>
    /// Levenshtein distance (LD) is a measure of the similarity between two strings, which we will refer to as the source string (s) and the target string (t). 
    /// The distance is the number of deletions, insertions, or substitutions required to transform s into t. 
    /// The greater the Levenshtein distance, the more different the strings are.
    /// Step 	Description
    ///1 	Set n to be the length of s.
    ///Set m to be the length of t.
    ///If n = 0, return m and exit.
    ///If m = 0, return n and exit.
    ///Construct a matrix containing 0..m rows and 0..n columns.
    ///2 	Initialize the first row to 0..n.
    ///Initialize the first column to 0..m.
    ///3 	Examine each character of s (i from 1 to n).
    ///4 	Examine each character of t(j from 1 to m).
    ///5 	If s[i] equals t[j], the cost is 0.
    ///If s[i] doesn't equal t[j], the cost is 1.
    ///6 	Set cell d[i, j] of the matrix equal to the minimum of:
    ///a.The cell immediately above plus 1: d[i - 1, j] + 1.
    ///b.The cell immediately to the left plus 1: d[i, j - 1] + 1.
    ///c.The cell diagonally above and to the left plus the cost: d[i - 1, j - 1] + cost.
    ///7 	After the iteration steps(3, 4, 5, 6) are complete, the distance is found in cell d[n, m].
    ///http://people.cs.pitt.edu/~kirk/cs1501/Pruhs/Fall2006/Assignments/editdistance/Levenshtein%20Distance.htm
    /// </summary>
    public static class LevenshteinDistance
    {
        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

    }
}
