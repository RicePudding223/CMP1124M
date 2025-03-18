using System;
using System.Data.SqlTypes;
using System.Linq;

namespace CMP1124M
{
    class Sorting
    {

        public int[] BubbleSort(int[] nums, bool descending)
        {
            int[] sortedArray = new int[nums.Length];
            Array.Copy(nums, sortedArray, nums.Length);

            for (int i = 0; i < sortedArray.Length - 1; i++)
            {
                for (int j = 0; j < sortedArray.Length - i - 1; j++)
                {
                    if ((descending == true && sortedArray[j] < sortedArray[j + 1])
                        || (descending == false && sortedArray[j] > sortedArray[j + 1]))
                    {
                        int temp = sortedArray[j];
                        sortedArray[j] = sortedArray[j + 1];
                        sortedArray[j + 1] = temp;
                    }
                }
            }
            return sortedArray;
        }


        public int[] MergeSort(int[] nums, bool descending)
        {
            if (nums.Length <= 1)
            {
                return nums;
            }
            int middle = nums.Length / 2;
            int[] left = new int[middle];
            int[] right = new int[nums.Length - middle];
            for (int i = 0; i < middle; i++)
            {
                left[i] = nums[i];
            }
            for (int i = middle; i < nums.Length; i++)
            {
                right[i - middle] = nums[i];
            }
            left = MergeSort(left, descending);
            right = MergeSort(right, descending);

            return Merge(left, right, descending);
        }

        public int[] Merge(int[] left, int[] right, bool descending)
        {
            int[] result = new int[left.Length + right.Length];
            int leftCounter = 0;
            int rightCounter = 0;
            int resultCounter = 0;

            while (leftCounter < left.Length && rightCounter < right.Length)
            {
                if (((descending == false && left[leftCounter] < right[rightCounter]) ||
                   (descending == true && left[leftCounter] > right[rightCounter])))
                {
                    result[resultCounter] = left[leftCounter];
                    leftCounter++;
                }
                else
                {
                    result[resultCounter] = right[rightCounter];
                    rightCounter++;
                }
                resultCounter++;
            }

            while (leftCounter < left.Length)
            {
                result[resultCounter] = left[leftCounter];
                leftCounter++;
                resultCounter++;
            }

            while (rightCounter < right.Length)
            {
                result[resultCounter] = right[rightCounter];
                rightCounter++;
                resultCounter++;
            }

            return result;
        }

        public int[] InsertionSort(int[] nums, bool descending)
        {
            int[] sorted = new int[nums.Length];
            sorted[0] = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                int key = nums[i];
                int j = i - 1;

                while ((descending == false && j >= 0 && sorted[j] > key) ||
                       (descending == true && j >= 0 && sorted[j] < key))
                {
                    sorted[j + 1] = sorted[j];
                    j--;
                }

                sorted[j + 1] = key;
            }

            return sorted;
        }

        public int[] QuickSort(int[] nums, int low, int high, bool descending)
        {
            int[] sortedArray = new int[nums.Length];
            Array.Copy(nums, sortedArray, nums.Length);

            QuickSortInternal(sortedArray, low, high, descending);
            return sortedArray;
        }

        private void QuickSortInternal(int[] nums, int low, int high, bool descending)
        {
            if (low < high)
            {
                int partitionIndex = Partition(nums, low, high, descending);
                QuickSortInternal(nums, low, partitionIndex - 1, descending);
                QuickSortInternal(nums, partitionIndex + 1, high, descending);
            }
        }

        private int Partition(int[] nums, int low, int high, bool descending)
        {
            int pivot = nums[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if ((descending == false && nums[j] < pivot) ||
                    (descending == true && nums[j] > pivot))
                {
                    i++;
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;
                }
            }

            int temp2 = nums[i + 1];
            nums[i + 1] = nums[high];
            nums[high] = temp2;
            return i + 1;
        }
    }
}

