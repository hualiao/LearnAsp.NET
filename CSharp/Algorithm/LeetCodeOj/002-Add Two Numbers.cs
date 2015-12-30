using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Algorithm.LeetCodeOj
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }

    public class Solution
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null)
                return l2;
            if (l2 == null)
                return l1;

            // write each digit part of sum digits
            ListNode head = null;
            ListNode prev = null;
            int val = 0;
            int carryover = 0;
            // move and add digit by digit
            while (l1 != null || l2 != null || carryover == 1)
            {
                if (l1 != null && l2 != null)
                {
                    val = l1.val + l2.val + carryover;
                    l1 = l1.next;
                    l2 = l2.next;
                }
                else if (l1 != null)
                {
                    val = l1.val + carryover;
                    l1 = l1.next;
                }
                else if (l2 != null)
                {
                    val = l2.val + carryover;
                    l2 = l2.next;
                }
                else
                {
                    // There is a carryover
                    val = carryover;
                }

                carryover = (val >= 10 ? 1 : 0);
                var newNode = new ListNode(val % 10);
                if (head == null)
                {
                    head = newNode;
                }
                else
                {
                    prev.next = newNode;
                }
                prev = newNode;
            }
            
            return head;
        }
    }
}
