using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_Algorithm
{
    class Program
    {
        public static int count = 0;

        public static void MySort(int[] arr)
        {
            count = 0;
            int n = arr.Length;
            for (int i = 0; i < n; i++)
                for (int j = i; j > 0; j--)
                {
                    count++;
                    if (arr[j].CompareTo(arr[j - 1]) < 0)
                        MySortExchange(arr, j, j - 1);
                    else break;
                }
        }

        public static void MySelectionSort(int[] arr)
        {
            count = 0;
            int n = arr.Length;
            for (int i = 0; i < n; i++)
            {
                int min = i; // find the minimum
                for (int j = i + 1; j < n; j++)
                {
                    count++;
                    if (arr[j].CompareTo(arr[min]) < 0) min = j;
                }
                MySortExchange(arr, i, min);
            }
        }

        public static void MyInsertionSort(int[] arr)
        {
            count = 0;
            int n = arr.Length;
            for (int i = 0; i < n; i++)
                for (int j = i; j > 0; j--)                 // Compare current position with position before
                {
                    count++;
                    if (arr[j].CompareTo(arr[j - 1]) < 0)   //Finding minimum of this 2 elements and 
                        MySortExchange(arr, j, j - 1);      //swap them!
                    else break;
                }
        }

        public static void MyShellSort(int[] arr)
        {
            count = 0;
            int n = arr.Length;

            int h = 1;

            while (h < n / 3) h = 3 * h + 1;  // 3x + 1 increment sequence

            while (h >= 1)
            {
                for (int i = h; i < n; i++)
                    for (int j = i; j >= h && (arr[j].CompareTo(arr[j - h]) < 0); j--)
                    {
                        count++;
                        MySortExchange(arr, j, j - h);      //swap them - Insertion Sort              
                    }
                h = h / 3;                                // move to next increment
            }
        }

        public static void MySortExchange(int[] arr, int i, int j)
        {
            int swap = arr[i];
            arr[i] = arr[j];
            arr[j] = swap;
        }

        public static void MySortPrint(int[] arr)
        {
            foreach (int n in arr) Console.Write("{0} ", n);
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            bool exitFlag = true;
 
            int[] test = new int[] { 8, 1, 10, 7, 55, 24, 43, 88, 2, 313, 81, 134, 85, 17, 89, 255, 77, 13, 243, 454, 574, 446, 544, 4 };
            Console.WriteLine("UnSorted array:");
            MySortPrint(test);

            while (exitFlag)
            {
                string op = Console.ReadLine().Trim().ToLower();
                switch (op)
                {
                    case "e":
                        Console.WriteLine("EXIT!");
                        exitFlag = false;
                        break;
                    case "p":
                        Console.WriteLine("Sorted array:");
                        MySortPrint(test);
                        break;
                    case "1":
                        MySelectionSort(test);
                        Console.WriteLine("Selection Sort: Count = {0}", count);
                        break;
                    case "2":
                        MyInsertionSort(test);
                        Console.WriteLine("Insertion Sort: Count = {0}", count);
                        break;
                    case "3":
                        MyShellSort(test);
                        Console.WriteLine("Shell Sort: Count = {0}", count);
                        break;
                    default:
                        Console.WriteLine("Вы нажали неизвестную букву");
                        break;
                }                
            }
        }
    }
}
