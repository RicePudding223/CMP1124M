using System.Collections.Generic;

namespace CMP1124M
{
    /// <summary>
    /// Class that contains the searching algorithms
    /// </summary>
    class Searching
    {
        /// <summary>
        /// Linear search algorithm
        /// </summary>
        /// <param name="nums">The array it is searching</param>
        /// <param name="target">The target value it is looking for</param>
        /// <returns>Returns a list of indexes in the array where it is found and a count of comparisons</returns>
        public (List<int>, int) LinearSearch(int[] nums, int target)
        {
            int count = 0;
            List<int> indexes = new List<int>();
            for (int i = 0; i < nums.Length; i++) // Loops through array
            {
                count++; // Increment the count
                if (nums[i] == target)
                {
                    // If the current item is the target, add it to the list
                    indexes.Add(i + 1);
                }
            }
            return (indexes, count);
        }

        /// <summary>
        /// Starts the binary search.
        /// This is so it can start a count reference.
        /// </summary>
        /// <param name="nums">The array it is searching</param>
        /// <param name="target">The target value it is looking for</param>
        /// <returns>Returns a list of indexes in the array where it is found and a count of comparisons</returns>
        public (List<int>, int) BinarySearch(int[] nums, int target)
        {
            int count = 0;
            List<int> indexes = new List<int>();
            BinarySearchHelper(nums, target, 0, nums.Length - 1, indexes, ref count);
            return (indexes, count);
        }

        /// <summary>
        /// Binary search algorithm
        /// </summary>
        /// <param name="nums">The array it is searching</param>
        /// <param name="target">The target value it is looking for</param>
        /// <param name="left">The left part of the array</param>
        /// <param name="right">The right part of the array</param>
        /// <param name="indexes">List to store where the numbers are</param>
        /// <param name="count">A reference count</param>
        private void BinarySearchHelper(int[] nums, int target, int left, int right, List<int> indexes, ref int count)
        {
            if (left > right)
            {
                return;
            }

            int middle = left + (right - left) / 2; // Finds the middle
            count++; // Increments the comparison count

            if (nums[middle] == target)
            {
                // If the middle value is the target add it to the list
                indexes.Add(middle); 
                // Look in the left and right arrays to find any more targets
                BinarySearchHelper(nums, target, left, middle - 1, indexes, ref count);
                BinarySearchHelper(nums, target, middle + 1, right, indexes, ref count);
            }
            // If the target isnt't the middle, look either left or right if the targer is larger or smaller
            else if (nums[middle] > target)
            {
                BinarySearchHelper(nums, target, left, middle - 1, indexes, ref count);
            }
            else
            {
                BinarySearchHelper(nums, target, middle + 1, right, indexes, ref count);
            }
        }
    }
}
