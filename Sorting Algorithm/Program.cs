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

        public static bool Compare<T>(T first, T second)
        {
            return Comparer<T>.Default.Compare(first, second) < 0;
        }

        public static void MySortExchange<T>(T[] arr, int i, int j)
        {    //swap 2 element (index i and j) in array
            T swap = arr[i];
            arr[i] = arr[j];
            arr[j] = swap;
        }

        public static void MySortPrint<T>(T[] arr)
        {
            foreach (T n in arr) Console.Write("{0} ", n);
            Console.WriteLine();
        }

        public static void MySelectionSort<T>(T[] arr)
        {
            count = 0;
            int n = arr.Length;
            for (int i = 0; i < n; i++)
            {
                int min = i; // find the minimum
                for (int j = i + 1; j < n; j++)
                {
                    count++;
                    if (Compare(arr[j], arr[min])) min = j;
                }
                MySortExchange(arr, i, min);
            }
        }

        public static void MyInsertionSort<T>(T[] arr)
        {
            count = 0;
            int n = arr.Length;
            for (int i = 0; i < n; i++)
                for (int j = i; j > 0; j--)                 // Compare current position with position before
                {
                    count++;
                    if (Compare(arr[j], arr[j - 1]))
                        //if (arr[j].CompareTo(arr[j-1])<0) //Finding minimum of this 2 elements and 
                        MySortExchange(arr, j, j - 1);      //swap them!
                    else break;
                }
        }

        public static void MyShellSort<T>(T[] arr)
        {
            count = 0;
            int n = arr.Length;
            int[] shell = new int[] { 0, 1, 4, 10, 23, 57, 132, 301, 701, 1750 };  //последовательность Марцина Циура - A102549 в OEIS
            int shellNum = 0;
            int h = shell[1];

            while (shell[shellNum] < n)
            {
                if (shell[shellNum + 1] < n) h = shell[++shellNum];
                else break;
            }
            //h=3*h+1;  // 3x + 1 increment sequence - Standart Sequence

            while (h >= 1)
            {
                for (int i = h; i < n; i++)
                    for (int j = i; j >= h && Compare(arr[j], arr[j - h]); j--)
                    {
                        count++;
                        MySortExchange(arr, j, j - h);  //swap them - Insertion Sort              
                    }
                h = shell[--shellNum];                  // move to next increment
            }
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
