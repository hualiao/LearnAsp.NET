using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Algorithm.LeetCodeOj
{
    public class Solution
    {
        public int[] TwoSum(int[] nums, int target)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < nums.Length; i++)
            {
                int x = nums[i];
                if (ht.ContainsKey(target - x))
                {
                    int[] rs = { (int)ht[target - x] + 1, i + 1 };
                }
                else
                {
                    ht[x] = i;
                }
            }
            return null;
        }

        public int[] TwoSum1(int[] nums, int target)
        {
            Dictionary<int, int> dt = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dt.ContainsKey(target - nums[i]))
                    return new int[] { dt[target - nums[i]] + 1, i + 1 };
                else
                {
                    if (!dt.ContainsKey(target - nums[i]))
                        dt.Add(nums[i], i);
                }
            }
            return new int[2];
        }
    }
}
