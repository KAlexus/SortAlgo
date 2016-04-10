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

        public static int CompareTo<T>(T first, T second)
        {
            return Comparer<T>.Default.Compare(first, second);
        }

        public static bool IsSorted<T>(T[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (Compare(arr[i], arr[i - 1]))
                    return false;
            }
            return true;
        }

        public static bool IsSorted<T>(T[] arr, int lo, int hi)
        {
            for (int i = lo; i < hi; i++)
            {
                if (Compare(arr[i], arr[i + 1]))
                    return false;
            }
            return true;
        }

        public static void Swap<T>(T[] arr, int i, int j)
        {   //swap 2 element (index i and j) in array
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
                    if (Compare(arr[j], arr[min]))
                        min = j;
                }
                Swap(arr, i, min);
            }
        }

        public static void InsertionSort<T>(T[] arr)
        {
            count = 0;
            int n = arr.Length;
            for (int i = 0; i < n; i++)
                for (int j = i; j > 0; j--)            //Compare current position with position before
                {
                    count++;
                    if (Compare(arr[j], arr[j - 1]))   //Finding minimum of this 2 elements and 
                        Swap(arr, j, j - 1);           //swap them!
                    else break;
                }
        }

        public static void ShellSort<T>(T[] arr)
        {
            count = 0;
            int n = arr.Length;
            int[] shell = new int[] { 0, 1, 4, 10, 23, 57, 132, 301, 701,
                                    1750, 3671, 5519, 7919, 10957, 14753,
                                    19403, 24809, 31319, 38873, 47657, 57559,
                                    69031, 81799, 96137, 112291, 130073, 149717,
                                    171529, 195043, 220861, 248851, 279431, 312583,
                                    347707, 386093, 427169, 470933, 517553, 1391376,
                                    3402672, 8382192, 21479367, 49095696, 114556624,
                                    343669872, 852913488, 2085837936 };
            // A102549+A055875+A036569 from OEIS - Marcin Ciura (1-9 elements); 10-37 - ; 
            // 38 -46 -  Sedgewick-Incerpi upper bound

            int shellNum = 0;
            int h = shell[1];

            while (shell[shellNum] < n)
            {
                if (shell[shellNum + 1] < n)
                    h = shell[++shellNum];
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
                h = shell[--shellNum];          // move to next increment
            }
        }

        public static T[] MergeSort<T>(T[] arr)
        {
            int n = arr.Length;
            if (n == 1)
                return arr;
            int mid = n / 2;

            return Merge(MergeSort(arr.Take(mid).ToArray()), MergeSort(arr.Skip(mid).ToArray()));
        }

        public static T[] Merge<T>(T[] left, T[] right)
        {
            int lo = 0, hi = 0;
            T[] merged = new T[left.Length + right.Length];
            for (int i = 0; i < left.Length + right.Length; i++)
            {
                count++;
                if (hi.CompareTo(right.Length) < 0 && lo.CompareTo(left.Length) < 0)
                    if (Compare(right[hi], left[lo]))
                        merged[i] = right[hi++];
                    else
                        merged[i] = left[lo++];
                else
                    if (hi < right.Length)
                        merged[i] = right[hi++];
                    else
                        merged[i] = left[lo++];
            }
            return merged;
        }

        public static void MergeRecursive<T>(T[] arr, T[] aux, int lo, int mid, int hi)
        {
            for (int k = lo; k <= hi; k++)
                aux[k] = arr[k];

            int i = lo, j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                count++;
                if      (i > mid)                   arr[k] = aux[j++];
                else if (j > hi)                    arr[k] = aux[i++];
                else if (Compare(aux[j], aux[i]))   arr[k] = aux[j++];
                else                                arr[k] = aux[i++];
            }
        }

        public static void MergeSortRecursive<T>(T[] arr, T[] aux, int lo, int hi)
        {
            if (hi <= lo) return;

            int mid = lo + (hi - lo) / 2;
            MergeSortRecursive(arr, aux, lo, mid);
            MergeSortRecursive(arr, aux, mid + 1, hi);

            if (!Compare(arr[mid + 1], arr[mid])) return;
            MergeRecursive(arr, aux, lo, mid, hi);
        }
        public static void MergeSortRecursive<T>(T[] arr)
        {
            T[] aux = new T[arr.Length];
            MergeSortRecursive(arr, aux, 0, arr.Length - 1);
        }

        public static int QSPartition<T>(T[] arr, int lo, int hi)
        {
            int left = lo, right = hi + 1;
            while (true)
            { //true
                while (Compare(arr[++left], arr[lo]))
                    if (left == hi) break;
                while (Compare(arr[lo], arr[--right]))
                    if (right == lo) break;
                if (left >= right) break;
                Swap(arr, left, right);
            }
            Swap(arr, lo, right);
            return right;
        }
        public static void QuickSort<T>(T[] arr)
        {
            Shuffle(arr);
            QuickSort(arr, 0, arr.Length - 1);
        }
        public static void QuickSort<T>(T[] arr, int lo, int hi)
        {
            if (hi <= lo) return;

            int mid = lo + (hi - lo) / 2;
            mid = QSMedian(arr, lo, mid, hi);
            Swap(arr, lo, mid);

            int index = QSPartition(arr, lo, hi);
            QuickSort(arr, lo, index - 1);
            QuickSort(arr, index + 1, hi);
        }
        public static int QSMedian<T>(T[] arr, int lo, int mid, int hi)
        {
            if (Compare(arr[lo], arr[mid]))
                Swap(arr, lo, mid);
            if (Compare(arr[mid], arr[hi]))
                Swap(arr, lo, mid);
            if (Compare(arr[lo], arr[hi]))
                Swap(arr, lo, mid);
            return mid;
        }

        public static T Select<T>(T[] arr, int k)
        {
            Shuffle(arr);
            int lo = 0, hi = arr.Length - 1;

            while (hi > lo)
            {
                int mid = QSPartition(arr, lo, hi);

                if      (mid < k) lo = mid + 1;
                else if (mid > k) hi = mid - 1;
                else return arr[k];
            }
            return arr[k];
        }

        public static void QuickSort3<T>(T[] arr)
        {
            Shuffle(arr);
            QuickSort3(arr, 0, arr.Length - 1);
        }
        public static void QuickSort3<T>(T[] arr, int lo, int hi)
        {
            if (hi <= lo) return;
            int left = lo, right = hi;
            T val = arr[lo];
            int i = lo;

            while (i <= right)
            {
                if      (CompareTo(arr[i], val) < 0) Swap(arr, left++, i++);
                else if (CompareTo(arr[i], val) > 0) Swap(arr, i, right--);
                else i++;
            }
            QuickSort3(arr, lo, left - 1);
            QuickSort3(arr, right + 1, hi);
        }


        public static void Menu()
        {
            Console.WriteLine("========== Test Sorting Algorithms ==========");
            Console.WriteLine(" S - Set array size;");
            Console.WriteLine(" F - Fill an array by random values;");
            Console.WriteLine(" H - Check an array: is already sorted?");
            Console.WriteLine(" U - Shuffle an array;");

            Console.WriteLine(" 1 - Test Selection sort;");
            Console.WriteLine(" 2 - Test Insertion sort;");
            Console.WriteLine(" 3 - Test Shell sort;");
            Console.WriteLine(" 4 - Test Merge sort");
            Console.WriteLine(" 5 - Test Merge sort(recursive)");
            Console.WriteLine(" 6 - Test Quick sort");
            Console.WriteLine(" 7 - Test Quick sort with 3-way.");
            Console.WriteLine(" 8 - Test Selection from array by index");

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
                    case "u":
                        Console.Write("Shuffle an array...");
                        Shuffle(test);
                        Console.WriteLine("Done!");
                        break;
                    case "h":
                        Console.WriteLine("Array is sorted: {0}", IsSorted(test));
                        break;
                    case "c":
                        Console.Clear();
                        count = 0; // set Count to default;
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
                    case "4":
                        test = MergeSort(test);
                        Console.WriteLine("Merge Sort: Count = {0}", count);
                        break;
                    case "5":
                        MergeSortRecursive(test);
                        Console.WriteLine("Merge Sort Recursive: Count = {0}", count);
                        break;
                    case "6":
                        QuickSort(test);
                        Console.WriteLine("QuickSort C: Count = {0}", count);
                        break;
                    case "7":
                        QuickSort3(test);
                        Console.WriteLine("QuickSort 3-way: Count = {0}", count);
                        break;
                    case "8":
                        Console.Write("Set index of element K: ");
                        string strIndexK = Console.ReadLine();
                        int indexK = 0; //default
                        if (int.TryParse(strIndexK, out indexK))
                            Console.WriteLine("Element K(n) = {0}",Select(test,indexK-1).ToString());
                        break;
                    default:
                        Console.WriteLine("Try Again.");
                        break;
                }                
            }
        }
    }
}
