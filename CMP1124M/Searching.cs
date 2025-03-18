using System;
using System.Collections.Generic;
using System.Linq;


namespace CMP1124M
{
    class Searching
    {
        public List<int> LinearSearch(int[] nums, int target)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == target)
                {
                    indexes.Add(i+1);
                }
            }
            return indexes;
        }

        public int BinarySearch(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left <= right)
            {
                int middle = left + (right - left) / 2;
                if (nums[middle] == target)
                {
                    return middle;
                }
                if (nums[middle] < target)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }
            return -1;
        }

    }
}
