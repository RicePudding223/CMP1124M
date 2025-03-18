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

            Dictionary<int, int[]> arrays = new Dictionary<int, int[]>
            {
                { 1, ReadFileToArray(filePaths[0]) },
                { 2, ReadFileToArray(filePaths[1]) },
                { 3, ReadFileToArray(filePaths[2]) },
                { 4, ReadFileToArray(filePaths[3]) },
                { 5, ReadFileToArray(filePaths[4]) },
                { 6, ReadFileToArray(filePaths[5]) }
            };

            Sorting sorting = new Sorting();
            Searching searching = new Searching();

            Console.WriteLine("Pick which array you want to sort:");
            Console.WriteLine("1. Share_1_256\n2. Share_2_256\n3. Share_3_256");
            Console.Write("4. Share_1_2048\n5. Share_2_2048\n6. Share_3_2048\n: ");

            int choice = int.Parse(Console.ReadLine());
            int[] selectedArray = arrays[choice];

            Console.Write("Do you want to sort the array in ascending or descending order? (a/d)\n: ");
            bool isDescending = Console.ReadLine().ToLower() == "d";

            int[] sortedArray = choice <= 3
                ? sorting.BubbleSort(selectedArray, isDescending)
                : sorting.MergeSort(selectedArray, isDescending);

            PrintArray(sortedArray);

            Console.Write("Pick a number to search for in the array\n: ");
            int target = int.Parse(Console.ReadLine());

            List<int> indexes = searching.LinearSearch(selectedArray, target);
            if (indexes.Count == 0)
            {
                Console.WriteLine("The target number was not found in the array.");
            }
            else
            {
                Console.WriteLine($"The target number was found at the following indexes in the original array: {string.Join(", ", indexes)}");
            }


            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }

        static void PrintArray(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (i % 10 == 0)
                {
                    Console.WriteLine(nums[i]);
                }
            }
        }
    }
}