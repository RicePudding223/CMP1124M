using System;
using System.Collections.Generic;
using System.IO;

/* 
 [Program Name] = Stock Exchange Volume Analyzer
 [Name] = Reece Sammons (29206858)
 [Hand in Date] = 27/03/2025

• Description
This program is a console-based application that reads, sorts, and
searches stock exchange volume data from multiple text files. The
program allows users to analyze stock trading activity by implementing
different sorting and searching algorithms.

• Main functions of the program
- Reads stock exchange volume data from multiple text files.
- Asks the user to select an array to sort and the sorting algorithm to use.
- Displays every 10th or 50th value the original and sorted arrays
  depending on size, along with the number of comparisons made.
- Searches for a user-defined value and returns its position(s).
- If the value is not found, the program returns the closest value(s) to the target.
- This is looped until the user decides to move on to the next step.
- Merges and processes datasets for advanced analysis.

• Input parameters
- Algorithm choice
- Sorting order
- Search target

• Expected output
- Original and sorted arrays
- Number of comparisons made
- Indexes of the target value in the original array
- Closest value(s) to the target if the value is not found

• Number of sorting and searching algorithms implemented
- Sorting: Bubble sort, Merge sort, Insertion sort, Quick sort
- Searching: Linear search, Binary search
*/

namespace CMP1124M
{
    /// <summary>
    /// Main class that contains the entry point of the program.
    /// Allows user to input choices for sorting and searching algorithms.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method, contains loop for user to process arrays and has the merging stage.
        /// </summary>
        static void Main(string[] args)
        {
            // File paths for the stock exchange volume data
            string[] filePaths = {
                "Share_1_256.txt", "Share_2_256.txt", "Share_3_256.txt",
                "Share_1_2048.txt", "Share_2_2048.txt", "Share_3_2048.txt"
            };

            // Dictionary that stores the arrays with an integer key
            Dictionary<int, int[]> arrays = LoadArrays(filePaths);

            // Sorting and searching objects
            Sorting sorting = new Sorting();
            Searching searching = new Searching();

            // Loop to process user input for sorting and searching
            bool continueLoop = true;
            while (continueLoop)
            {
                continueLoop = ProcessUserInput(arrays, sorting, searching, false, null);
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

            // Merging Share_1_256 and Share_3_256 arrays
            Console.WriteLine("\nMerging Share_1_256 and Share_3_256 arrays...");
            int mergeCount = 0;
            int[] mergedArray1 = sorting.Merge(arrays[1], arrays[3], false, ref mergeCount);

            // Process user input with the merged array
            Console.WriteLine("\nNow with this new array...\n");
            ProcessUserInput(arrays, sorting, searching, true, mergedArray1);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();

            // Merging Share_1_2048 and Share_3_2048 arrays
            Console.WriteLine("\nMerging Share_1_2048 and Share_3_2048 arrays...");
            int[] mergedArray2 = sorting.Merge(arrays[4], arrays[6], false, ref mergeCount);

            // Process user input with the merged array
            Console.WriteLine("\nNow with this new array...\n");
            ProcessUserInput(arrays, sorting, searching, true, mergedArray2);

            // End of program
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Loads arrays from text files into a dictionary.
        /// </summary>
        /// <param name="filePaths">String array of file paths</param>
        /// <returns>Returns the dictionary that contains an integer key and integer array</returns>
        static Dictionary<int, int[]> LoadArrays(string[] filePaths)
        {
            var arrays = new Dictionary<int, int[]>();
            // Loop through filePaths and read data into arrays
            for (int i = 0; i < filePaths.Length; i++)
            {
                arrays.Add(i + 1, ReadFileToArray(filePaths[i]));
            }
            return arrays;
        }

        /// <summary>
        /// Reads stock exchange volume data from a text file and converts it to an integer array.
        /// </summary>
        /// <param name="filePath">String array of file paths</param>
        /// <returns>Integer array containing stock exchange volume data</returns>
        static int[] ReadFileToArray(string filePath)
        {
            try
            {
                // Read all lines from the file and convert to integer array
                string[] lines = File.ReadAllLines(filePath);
                return Array.ConvertAll(lines, int.Parse);
            }
            // Catch exceptions for file not found and invalid data formats
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

        /// <summary>
        /// Method that calls other methods
        /// </summary>
        /// <param name="arrays">Dictionary of all arrays with integer key</param>
        /// <param name="sorting">Sorting class object</param>
        /// <param name="searching">Searching class object</param>
        /// <param name="isMerged">Bool to know if the user is on the merging part of the program</param>
        /// <param name="mergedArray">The integer array for the merging part of the program</param>
        /// <returns>Bool to show if the loop should continue in Main</returns>
        static bool ProcessUserInput(Dictionary<int, int[]> arrays, Sorting sorting, Searching searching, bool isMerged, int[] mergedArray)
        {
            int[] selectedArray; // Array selected by the user
            // If the user is not on the merging part of the program then ask what array to use
            if (!isMerged)
            {
                int choice = GetUserChoice();
                if (choice == 7)
                {
                    return false; // Exit the loop
                }
                selectedArray = arrays[choice];
            }
            else
            {
                selectedArray = mergedArray; // Uses merged array instead of asking user
            }

            // Asks user for sorting order and what algorithm to use
            bool isDescending = GetSortOrder();
            (int[] sortedArray, int sortCount) = SortArray(sorting, selectedArray, isDescending);

            // Displays the original and sorted arrays
            DisplayArrays(selectedArray, sortedArray, sortCount);

            // Asks user for search target and what algorithm to use
            int target = GetSearchTarget();
            SearchAndDisplayResults(searching, selectedArray, sortedArray, target);

            return true; // Continue the loop
        }

        /// <summary>
        /// Asks the user to pick an array to sort.
        /// </summary>
        /// <returns>An integer between 1 and 7</returns>
        static int GetUserChoice()
        {
            // Loop until the user picks a valid choice
            while (true)
            {
                Console.WriteLine("Pick which array you want to sort:");
                Console.WriteLine("1. Share_1_256\n2. Share_2_256\n3. Share_3_256");
                Console.Write("4. Share_1_2048\n5. Share_2_2048\n6. Share_3_2048\n7. Move on to next step\n: ");
                // Check if the input is an integer between 1 and 7
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 7)
                {
                    return choice;
                }
                Console.WriteLine("\nInvalid choice. Please try again.");
            }
        }

        /// <summary>
        /// Asks the user if they want to sort in ascending or descending order.
        /// </summary>
        /// <returns>A bool value to show if the user wants it to descend</returns>
        static bool GetSortOrder()
        {
            // Loop until the user picks a valid choice
            while (true)
            {
                Console.Write("Do you want to sort the array in ascending or descending order? (a/d)\n: ");
                // Check if the input is 'a' or 'd'
                string answer = Console.ReadLine().ToLower();
                if (answer == "d")
                {
                    return true;
                }
                else if (answer == "a")
                {
                    return false;
                }
                Console.WriteLine("\nInvalid choice. Please try again.");
            }
        }

        /// <summary>
        /// Allows user to pick a sorting algorithm and sorts the array.
        /// </summary>
        /// <param name="sorting">Sorting class object</param>
        /// <param name="selectedArray">Integer array of the selected array</param>
        /// <param name="isDescending">Bool to show if the user wants to sort in descending order</param>
        /// <returns>An integer array of the new sorted array and a count of how many comparrisons</returns>
        static (int[], int) SortArray(Sorting sorting, int[] selectedArray, bool isDescending)
        {
            // Loop until the user picks a valid choice
            while (true)
            {
                Console.WriteLine("What sorting algorithm do you want to use?");
                Console.Write("1. Bubble sort\n2. Merge sort\n3. Insertion sort\n4. Quick sort\n: ");
                // Check if the input is an integer between 1 and 4
                if (int.TryParse(Console.ReadLine(), out int sortChoice))
                {
                    switch (sortChoice)
                    {
                        case 1:
                            return sorting.BubbleSort(selectedArray, isDescending);
                        case 2:
                            return sorting.MergeSort(selectedArray, isDescending);
                        case 3:
                            return sorting.InsertionSort(selectedArray, isDescending);
                        case 4:
                            return sorting.QuickSort(selectedArray, 0, selectedArray.Length - 1, isDescending);
                    }
                }
                Console.WriteLine("\nInvalid choice. Please try again.");
            }
        }

        /// <summary>
        /// Displays the original and sorted arrays along with the number of comparisons made.
        /// </summary>
        /// <param name="originalArray">The original unsorted array that the user selected</param>
        /// <param name="sortedArray">The sorted array that the user wanted to sort</param>
        /// <param name="sortCount">The number of comparisons that the sorting algorithm made</param>
        static void DisplayArrays(int[] originalArray, int[] sortedArray, int sortCount)
        {
            Console.WriteLine("Original array:");
            PrintArray(originalArray);
            Console.WriteLine("\nSorted array:");
            PrintArray(sortedArray);
            Console.WriteLine($"\nNumber of comparisons made: {sortCount}");
        }

        /// <summary>
        /// Asks the user to pick a number to search for.
        /// </summary>
        /// <returns>An integer to search for</returns>
        static int GetSearchTarget()
        {
            // Loop until the user picks a valid choice
            while (true)
            {
                Console.Write("\nPick a number to search for\n: ");
                // Checks if the input is an integer
                if (int.TryParse(Console.ReadLine(), out int target))
                {
                    return target;
                }
                Console.WriteLine("\nInvalid choice. Please try again.");
            }
        }

        /// <summary>
        /// Asks the user to pick a searching algorithm and searches for the target number. 
        /// It then prints out all the indexes where the target number was found or the closest number(s) to the target.
        /// </summary>
        /// <param name="searching">Searching class object</param>
        /// <param name="originalArray">The original unsorted array that the user selected</param>
        /// <param name="sortedArray">The sorted array that the user wanted to sort</param>
        /// <param name="target">The target number the user wants to find</param>
        static void SearchAndDisplayResults(Searching searching, int[] originalArray, int[] sortedArray, int target)
        {
            // Variables to store target indexes, number of comparisons, and if the search was binary
            List<int> indexes;
            int count;
            bool isBinarySearch = false;

            // Loop until the user picks a valid choice
            while (true)
            {
                Console.WriteLine("What searching algorithm do you want to use?");
                Console.Write("1. Linear search\n2. Binary search\n: ");
                // Check if the input is an integer between 1 and 2
                if (int.TryParse(Console.ReadLine(), out int searchChoice))
                {
                    
                    switch (searchChoice)
                    {
                        case 1:
                            // If the user picked linear search then use the original array
                            // to show the difference between amount of comparisons made between the two algorithms
                            Console.WriteLine("Using original array as the array doesn't need to be sorted.");
                            (indexes, count) = searching.LinearSearch(originalArray, target);
                            break;
                        case 2:
                            // If the user picked binary search then use the sorted array
                            // this is because the array needs to be sorted for binary search to work
                            Console.WriteLine("Using sorted array as the array needs to be sorted.");
                            (indexes, count) = searching.BinarySearch(sortedArray, target);
                            isBinarySearch = true;
                            break;
                        default:
                            Console.WriteLine("\nInvalid choice. Please try again.");
                            continue;
                    }
                    break;
                }
                Console.WriteLine("\nInvalid choice. Please try again.");
            }

            // If the target number was not found then find the closest number(s) to the target
            if (indexes.Count == 0)
            {
                Console.WriteLine("The target number was not found in the array.");
                List<int> closestValues = FindClosest(originalArray, target); // List that stores the closest number(s)
                List<int> closestIndexes = new List<int>(); // List that stores their indexes
                // Loop through the closest values and find their indexes in the original array
                foreach (int value in closestValues)
                {
                    var (foundIndexes, _) = searching.LinearSearch(originalArray, value);
                    closestIndexes.AddRange(foundIndexes);
                }
                Console.WriteLine($"The closest number(s) to the target was {string.Join(", ", closestValues)}" +
                    $" and was found at the following indexe(s) in the original array: {string.Join(", ", closestIndexes)}");
            }
            // If the target was found with a binary search then show the indexes in the sorted array
            // and shows the amount of comparisons made
            else if (isBinarySearch)
            {
                Console.WriteLine($"The target number was found at the following indexes in the sorted array: {string.Join(", ", indexes)}");
                Console.WriteLine($"Number of comparisons made: {count}");
            }
            // If the target was found with a linear search then show the indexes in the original array
            // and shows the amount of comparisons made
            else
            {
                Console.WriteLine($"The target number was found at the following indexes in the original array: {string.Join(", ", indexes)}");
                Console.WriteLine($"Number of comparisons made: {count}");
            }
        }

        /// <summary>
        /// Prints out every 10th or 50th value of an array depending on its size.
        /// </summary>
        /// <param name="nums">The array in question</param>
        static void PrintArray(int[] nums)
        {
            List<int> values = new List<int>();
            // If the array is less than 257 then print every 10th value, if not then print every 50th value
            int everyX = nums.Length < 257 ? 10 : 50;
            for (int i = everyX - 1; i < nums.Length; i += everyX)
            {
                values.Add(nums[i]); // Add the value to list so can print on one line
            }
            Console.WriteLine(string.Join(", ", values));
        }

        /// <summary>
        /// Finds the closest number(s) to the target number in an array.
        /// </summary>
        /// <param name="nums">The array in question</param>
        /// <param name="target">The target number in question</param>
        /// <returns></returns>
        static List<int> FindClosest(int[] nums, int target)
        {
            List<int> closest = new List<int>();
            int minDifference = int.MaxValue; // Set to max value so any difference will be less

            for (int i = 0; i < nums.Length; i++)
            {
                int currentDifference = Math.Abs(target - nums[i]); // Finds difference between target and current number
                if (currentDifference < minDifference)
                {
                    //If the current difference is less than the minDifference then clear the list and add the new number
                    closest.Clear();
                    closest.Add(nums[i]);
                    minDifference = currentDifference;
                }
                else if (currentDifference == minDifference && !closest.Contains(nums[i]))
                {
                    //If the currentDifference is the same as the minDifference then add the number to the list
                    closest.Add(nums[i]);
                }
            }

            return closest;
        }
    }
}
