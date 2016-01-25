using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Algorithm.LeetCodeOj
{
    /// <summary>
    /// https://leetcode.com/discuss/76935/c%23-182ms-two-pointers-solutions
    /// https://leetcode.com/discuss/45220/explained-solution-direct-address-instead-builtin-dictionary
    /// </summary>
    public class Solution
    {
        public int LengthOfLongestSubstring(string s)
        {
            if (s == null)
                return 0;
            // Let us create a hashtable where char is the key and position in the
            // string is the value. We have 256 different chracters
            int[] hashtable = new int[256];
            // Let us initialize the hash table value to -1
            int i = 0;
            for (i = 0; i < hashtable.Length; ++i)
            {
                hashtable[i] = -1;
            }

            int longest = 0;
            int substringStartPosition = 0;
            i = 0;
            while (i < s.Length)
            {
                char c = s[i];
                if (hashtable[c] != -1)
                {
                    // found a repeat character
                    // keep the length if more than the previous value
                    longest = System.Math.Max(longest, i - substringStartPosition);

                    // reset the hashtable for all caracters that are
                    // before previous c position
                    for (int j = substringStartPosition; j < hashtable[j]; ++j)
                    {
                        hashtable[j] = -1;
                    }

                    // Move the start position
                    substringStartPosition = hashtable[c] + 1;
                }

                hashtable[c] = i;
                ++i;
            }

            // keep the length if more than the previous value
            return System.Math.Max(longest,i-substringStartPosition);
        }
    }
}
