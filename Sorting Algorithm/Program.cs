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

        public static void Swap<T>(T[] arr, int i, int j)
        {    //swap 2 element (index i and j) in array
            T swap = arr[i];
            arr[i] = arr[j];
            arr[j] = swap;
        }

        public static void SortPrint<T>(T[] arr)
        {
            foreach (T n in arr) Console.Write("{0} ", n);
            Console.WriteLine();
        }

        public static void Shuffle<T>(T[] arr)
        {
            count = 0;
            int n = arr.Length;
            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                count++;
                int shuffleIndex = rnd.Next(0, i);
                Swap(arr, i, shuffleIndex);
            }
        }

        public static void FillRandom(int[] arr, int min, int max)
        {
            int n = arr.Length;
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.Next(min, max);
            }
        }
        public static void FillRandom(double[] arr, int min, int max)
        {
            int n = arr.Length;
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.Next(min, max - 1) + rnd.NextDouble();
            }
        }

        public static void SelectionSort<T>(T[] arr)
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
                Swap(arr, i, min);
            }
        }

        public static void InsertionSort<T>(T[] arr)
        {
            count = 0;
            int n = arr.Length;
            for (int i = 0; i < n; i++)
                for (int j = i; j > 0; j--)             //Compare current position with position before
                {
                    count++;
                    if (Compare(arr[j], arr[j - 1]))   //Finding minimum of this 2 elements and 
                        Swap(arr, j, j - 1);            //swap them!
                    else break;
                }
        }

        public static void ShellSort<T>(T[] arr)
        {
            count = 0;
            int n = arr.Length;
            int[] shell = new int[] { 0, 1, 4, 10, 23, 57, 132, 301, 701, 1750 };  //Марцина Циура (последовательность A102549 в OEIS)
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
                        Swap(arr, j, j - h);    //swap them - Insertion Sort              
                    }
                h = shell[--shellNum];      // move to next increment
            }
        }


        public static void Menu()
        {
            Console.WriteLine("========== Test Sorting Algorithms ==========");
            Console.WriteLine(" S - Set array size;");
            Console.WriteLine(" F - Fill an array by random values;");
            Console.WriteLine(" 1 - Test Selection sort;");
            Console.WriteLine(" 2 - Test Insertion sort;");
            Console.WriteLine(" 3 - Test Shell sort;");
            Console.WriteLine(" 4 - Test");
            Console.WriteLine(" P - Print an array;");
            Console.WriteLine(" E - Exit;");
            Console.WriteLine(" C - Clear screen.");
        }

        static void Main(string[] args)
        {
            bool exitFlag = true;
 
            int[] test = new int[100];
            FillRandom(test, 0, 100);

            Menu();

            while (exitFlag)
            {
                Console.Write("Select an action: ");
                string op = Console.ReadLine().Trim().ToLower();
                switch (op)
                {
                    case "s":
                        Console.Write("Set array size: ");
                        string str = Console.ReadLine();
                        int size = 100; //default
                        if (int.TryParse(str, out size))
                            test = new int[size];
                        FillRandom(test, 0, 100);
                        break;
                    case "f":
                        Console.Write("Set min value: ");
                        string strMin = Console.ReadLine();
                        Console.Write("Set max value: ");
                        string strMax = Console.ReadLine();
                        int min = 0, max = 100; //default
                        if (int.TryParse(strMin, out min) && int.TryParse(strMax, out max))
                            FillRandom(test, min, max);
                        break;
                    case "e":
                        Console.WriteLine("EXIT!");
                        exitFlag = false;
                        break;
                    case "p":
                        Console.WriteLine("Print array:");
                        SortPrint(test);
                        break;
                    case "c":
                        Console.Clear();
                        Menu();                        
                        break;
                    case "1":
                        SelectionSort(test);
                        Console.WriteLine("Selection Sort: Count = {0}", count);
                        break;
                    case "2":
                        InsertionSort(test);
                        Console.WriteLine("Insertion Sort: Count = {0}", count);
                        break;
                    case "3":
                        ShellSort(test);
                        Console.WriteLine("Shell Sort: Count = {0}", count);
                        break;
                    default:
                        Console.WriteLine("Try Again.");
                        break;
                }                
            }
        }
    }
}
