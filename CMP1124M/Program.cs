using System;
using System.Collections.Generic;
using System.IO;

namespace CMP1124M
{
    class Program
    {
        static int[] ReadFileToArray(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                return Array.ConvertAll(lines, int.Parse);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {filePath}");
                return new int[0];
            }
            catch (FormatException)
            {
                Console.WriteLine($"Invalid data format in file: {filePath}");
                return new int[0];
            }
        }

        static void Main(string[] args)
        {
            string[] filePaths = {
                "Share_1_256.txt", "Share_2_256.txt", "Share_3_256.txt",
                "Share_1_2048.txt", "Share_2_2048.txt", "Share_3_2048.txt"
            };

            Dictionary<int, int[]> arrays = LoadArrays(filePaths);

            Sorting sorting = new Sorting();
            Searching searching = new Searching();

            AskUserQuestions(arrays, false, null);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("\nMerging Share_1_256 and Share_3_256 arrays...");
            int[] merge1 = sorting.Merge(arrays[1], arrays[3], false);

            Console.WriteLine("\nNow with this new array...\n");
            AskUserQuestions(arrays, true, merge1);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("\nMerging Share_1_2048 and Share_3_2048 arrays...");
            int[] merge2 = sorting.Merge(arrays[4], arrays[6], false);

            Console.WriteLine("\nNow with this new array...\n");
            AskUserQuestions(arrays, true, merge2);

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }

        static void AskUserQuestions(Dictionary<int, int[]> arrays, bool merge, int[] merge1)
        {
            Sorting sorting = new Sorting();
            Searching searching = new Searching();

            int[] selectedArray;
            int choice = 0;

            if (!merge)
            {
                choice = GetUserChoice();
                selectedArray = arrays[choice];
            }
            else
            {
                selectedArray = merge1;
            }

            bool isDescending = GetSortOrder();

            int[] sortedArray = SortArray(sorting, selectedArray, choice, isDescending);

            DisplayArrays(selectedArray, sortedArray);

            int target = GetSearchTarget();

            SearchAndDisplayResults(searching, selectedArray, target);
        }

        static Dictionary<int, int[]> LoadArrays(string[] filePaths)
        {
            return new Dictionary<int, int[]>
            {
                { 1, ReadFileToArray(filePaths[0]) },
                { 2, ReadFileToArray(filePaths[1]) },
                { 3, ReadFileToArray(filePaths[2]) },
                { 4, ReadFileToArray(filePaths[3]) },
                { 5, ReadFileToArray(filePaths[4]) },
                { 6, ReadFileToArray(filePaths[5]) }
            };
        }

        static int GetUserChoice()
        {
            Console.WriteLine("Pick which array you want to sort:");
            Console.WriteLine("1. Share_1_256\n2. Share_2_256\n3. Share_3_256");
            Console.Write("4. Share_1_2048\n5. Share_2_2048\n6. Share_3_2048\n: ");
            return int.Parse(Console.ReadLine());
        }

        static bool GetSortOrder()
        {
            Console.Write("Do you want to sort the array in ascending or descending order? (a/d)\n: ");
            return Console.ReadLine().ToLower() == "d";
        }

        static int[] SortArray(Sorting sorting, int[] selectedArray, int choice, bool isDescending)
        {
            return choice <= 3
                ? sorting.BubbleSort(selectedArray, isDescending)
                : sorting.MergeSort(selectedArray, isDescending);
        }

        static void DisplayArrays(int[] originalArray, int[] sortedArray)
        {
            Console.WriteLine("Original array:");
            PrintArray(originalArray);
            Console.WriteLine("\nSorted array:");
            PrintArray(sortedArray);
        }

        static int GetSearchTarget()
        {
            Console.Write("\nPick a number to search for in the original array\n: ");
            return int.Parse(Console.ReadLine());
        }

        static void SearchAndDisplayResults(Searching searching, int[] selectedArray, int target)
        {
            List<int> indexes = searching.LinearSearch(selectedArray, target);
            if (indexes.Count == 0)
            {
                Console.WriteLine("The target number was not found in the array.");
                List<int> newTargets = FindClosest(selectedArray, target);
                List<int> newIndexes = new List<int>();
                foreach (int newTarget in newTargets)
                {
                    newIndexes.AddRange(searching.LinearSearch(selectedArray, newTarget));
                }
                Console.WriteLine($"The closest number(s) to the target was {string.Join(", ", newTargets)}" +
                    $" and was found at the following indexe(s) in the original array: {string.Join(", ", newIndexes)}");
            }
            else
            {
                Console.WriteLine($"The target number was found at the following indexes in the original array: {string.Join(", ", indexes)}");
            }
        }

        static void PrintArray(int[] nums)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int everyX = nums.Length < 257 ? 10 : 50;
                if (i % everyX == 0)
                {
                    values.Add(nums[i]);
                }
            }
            Console.WriteLine(string.Join(", ", values));
        }

        static List<int> FindClosest(int[] nums, int target)
        {
            List<int> closest = new List<int>();
            int minDifference = int.MaxValue;

            for (int i = 0; i < nums.Length; i++)
            {
                int currentDifference = Math.Abs(target - nums[i]);
                if (currentDifference < minDifference)
                {
                    closest.Clear();
                    closest.Add(nums[i]);
                    minDifference = currentDifference;
                }
                else if (currentDifference == minDifference && !closest.Contains(nums[i]))
                {
                    closest.Add(nums[i]);
                }
            }

            return closest;
        }
    }
}
