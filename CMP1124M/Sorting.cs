using System;

namespace CMP1124M
{
    /// <summary>
    /// Class that contains the sorting algorithms.
    /// </summary>
    class Sorting
    {
        /// <summary>
        /// Bubble sort algorithm.
        /// </summary>
        /// <param name="nums">The array being sorted</param>
        /// <param name="descending">Bool to decide if to sort in descending order</param>
        /// <returns>Returns sorted array along with the comparison count</returns>
        public (int[], int) BubbleSort(int[] nums, bool descending)
        {
            // Variable to count number of comparisons
            int count = 0;
            // Makes a copy of the original array to not modify it
            int[] sortedArray = new int[nums.Length];
            Array.Copy(nums, sortedArray, nums.Length);

            // Goes through the array and swaps adjacent elements if they are in the wrong order
            for (int i = 0; i < sortedArray.Length - 1; i++)
            {
                for (int j = 0; j < sortedArray.Length - i - 1; j++)
                {
                    count++; // Increment the count
                    // Depending on the descending bool, it will decide which order is wrong
                    if ((descending == true && sortedArray[j] < sortedArray[j + 1])
                        || (descending == false && sortedArray[j] > sortedArray[j + 1]))
                    {
                        (sortedArray[j + 1], sortedArray[j]) = (sortedArray[j], sortedArray[j + 1]); // Swaps the elements
                    }
                }
            }
            return (sortedArray, count);
        }

        /// <summary>
        /// Starts the merge sort algorithm.
        /// This is so it can make the counter variable and keep track of it.
        /// </summary>
        /// <param name="nums">The array being sorted</param>
        /// <param name="descending">Bool to decide if to sort in descending order</param>
        /// <returns>Returns sorted array along with the comparison count</returns>
        public (int[], int) MergeSort(int[] nums, bool descending)
        {
            int count = 0;
            int[] sortedArray = MergeSortInternal(nums, descending, ref count);
            return (sortedArray, count);
        }

        /// <summary>
        /// The part of the merge sort algorithm that splits the array into smaller parts.
        /// </summary>
        /// <param name="nums">The array being split</param>
        /// <param name="descending">Bool to decide if to sort in descending order</param>
        /// <param name="count">The count reference</param>
        /// <returns></returns>
        private int[] MergeSortInternal(int[] nums, bool descending, ref int count)
        {
            // If the array is of length 1 or less, it is as small as it can go.
            if (nums.Length <= 1)
            {
                return nums;
            }
            // Splits the array into two halves
            int middle = nums.Length / 2;
            int[] left = new int[middle];
            int[] right = new int[nums.Length - middle];
            // Fills the two halves with the appropriate elements
            for (int i = 0; i < middle; i++)
            {
                left[i] = nums[i];
            }
            for (int i = middle; i < nums.Length; i++)
            {
                right[i - middle] = nums[i];
            }
            // Recursively calls itself to split the halves further
            left = MergeSortInternal(left, descending, ref count);
            right = MergeSortInternal(right, descending, ref count);

            // Merges the split arrays back together in the correct order
            return Merge(left, right, descending, ref count);
        }

        /// <summary>
        /// Merges two arrays together in the correct order.
        /// </summary>
        /// <param name="left">The array on the left</param>
        /// <param name="right">The array on the right</param>
        /// <param name="descending">Bool to decide if to sort in descending order</param>
        /// <param name="count">The count reference</param>
        /// <returns></returns>
        public int[] Merge(int[] left, int[] right, bool descending, ref int count)
        {
            // Creates a new array to store the merged arrays
            int[] result = new int[left.Length + right.Length];
            // Counter variables to keep track of the elements in the arrays
            int leftCounter = 0;
            int rightCounter = 0;
            int resultCounter = 0;

            // While both left and right arrays have elements to be merged
            while (leftCounter < left.Length && rightCounter < right.Length)
            {
                count++; // Increment the count
                         // Compare elements from left and right arrays based on the sorting order
                if (((descending == false && left[leftCounter] < right[rightCounter]) ||
                   (descending == true && left[leftCounter] > right[rightCounter])))
                {
                    // Place the smaller/larger element into the result array
                    result[resultCounter] = left[leftCounter];
                    leftCounter++; // Move to the next element in the left array
                }
                else
                {
                    // Place the smaller/larger element into the result array
                    result[resultCounter] = right[rightCounter];
                    rightCounter++; // Move to the next element in the right array
                }
                resultCounter++; // Move to the next position in the result array
            }

            // If there are remaining elements in the left array, add them to the result array
            while (leftCounter < left.Length)
            {
                result[resultCounter] = left[leftCounter];
                leftCounter++;
                resultCounter++;
            }

            // If there are remaining elements in the right array, add them to the result array
            while (rightCounter < right.Length)
            {
                result[resultCounter] = right[rightCounter];
                rightCounter++;
                resultCounter++;
            }

            return result;
        }

        /// <summary>
        /// Insertion Sort algorithm
        /// </summary>
        /// <param name="nums">The array being sorted</param>
        /// <param name="descending">Bool to decide if to sort in descending order</param>
        /// <returns>Returns sorted array along with the comparison count</returns>
        public (int[], int) InsertionSort(int[] nums, bool descending)
        {
            // Variable to count number of comparisons
            int count = 0;
            // Makes a new array and sets the first element to be the same as the array being sorted
            int[] sorted = new int[nums.Length];
            sorted[0] = nums[0];

            // Loops through the array
            for (int i = 1; i < nums.Length; i++)
            {
                // Sets the current element as the key to be compared
                int key = nums[i];
                int j = i - 1;

                // Compare and shift elements based on the sorting order
                while ((descending == false && j >= 0 && sorted[j] > key) ||
                       (descending == true && j >= 0 && sorted[j] < key))
                {
                    count++;
                    sorted[j + 1] = sorted[j];
                    j--;
                }

                sorted[j + 1] = key;
            }

            return (sorted, count);
        }

        /// <summary>
        /// Starts the merge sort algorithm.
        /// This is so it can start a count variable and a new array to not modify the original.
        /// </summary>
        /// <param name="nums">The array being sorted</param>
        /// <param name="low">The starting index</param>
        /// <param name="high">The ending index</param>
        /// <param name="descending">Bool to decide if to sort in descending order</param>
        /// <returns>Returns sorted array along with the comparison count</returns>
        public (int[], int) QuickSort(int[] nums, int low, int high, bool descending)
        {
            // Variable to count number of comparisons
            int count = 0;
            // Makes a copy of the original array to not modify it
            int[] sortedArray = new int[nums.Length];
            Array.Copy(nums, sortedArray, nums.Length);

            QuickSortInternal(sortedArray, low, high, descending, ref count);
            return (sortedArray, count);
        }

        /// <summary>
        /// Internal Quick Sort algorithm
        /// </summary>
        /// <param name="nums">The array being sorted</param>
        /// <param name="low">The starting index</param>
        /// <param name="high">The ending index</param>
        /// <param name="descending">Bool to decide if to sort in descending order</param>
        /// <param name="count">The count reference</param>
        private void QuickSortInternal(int[] nums, int low, int high, bool descending, ref int count)
        {
            if (low < high)
            {
                // Partition the array to get the partition index
                int partitionIndex = Partition(nums, low, high, descending, ref count);
                // Recursively sort elements before and after partition
                QuickSortInternal(nums, low, partitionIndex - 1, descending, ref count);
                QuickSortInternal(nums, partitionIndex + 1, high, descending, ref count);
            }
        }

        /// <summary>
        /// Partition the array for Quick Sort
        /// </summary>
        /// <param name="nums">The array being sorted</param>
        /// <param name="low">The starting index</param>
        /// <param name="high">The ending index</param>
        /// <param name="descending">Bool to decide if to sort in descending order</param>
        /// <param name="count">The count reference</param>
        /// <returns>Returns the partition index</returns>
        private int Partition(int[] nums, int low, int high, bool descending, ref int count)
        {
            int pivot = nums[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                // Compare elements based on the sorting order
                if ((descending == false && nums[j] < pivot) ||
                    (descending == true && nums[j] > pivot))
                {
                    count++;
                    i++;
                    (nums[j], nums[i]) = (nums[i], nums[j]); // Swaps the elements
                }
            }

            (nums[high], nums[i + 1]) = (nums[i + 1], nums[high]); // Swaps the elements
            return i + 1;
        }
    }
}
