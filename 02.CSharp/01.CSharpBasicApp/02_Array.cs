using System;

namespace CSharpBasicApp
{
    internal class Array
    {
        static double CalculateAverage(int[] arr)
        {
            int sum = 0;
            foreach (int item in arr)
            {
                sum += item;
            }
            return 1.0 * sum / arr.Length;

        }
        static void Main(string[] args)
        {

            
            //one dimension array
            int[] arr = { 1, 2, 3 };
            for(int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
            Console.WriteLine(args[0]);
            // multi-dimension array (2D)
            //int[,] arr2D = { { 10, 20, 30 }, { 100, 200, 300} };
            //Console.WriteLine(arr2D);

            //int x = 10;
            //int y = 20;
            //unsafe
            //{
            //    int* ptr1 = &x;
            //    int* ptr2 = &y;
            //    Console.WriteLine((int)ptr1);
            //}

            // average of numbers provided on fly
            double average = CalculateAverage(new int[] { 10, 20, 35 });
            Console.WriteLine("Average is numbers provided on fly is: " + average + "\n");

            // average of numbers as input from command line args
            int size = args.Length;
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = int.Parse(args[i]);
            }
            double average2 = CalculateAverage(arr);
            Console.WriteLine("Average of numbers given in CLI is: " + average2 + "\n");

            // average of numbers provide using Console.ReadLine() function
            Console.Write("Enter the size of array: ");
            int size2 = int.Parse(Console.ReadLine());
            int[] arr2 = new int[size2];
            for (int i = 0; i < size2; i++)
            {
                Console.Write("Enter the " + (i+1) + "th number: ");
                arr2[i] = int.Parse(Console.ReadLine());
            }
            double average3 = CalculateAverage(arr2);
            Console.WriteLine("average of numbers given using Console.ReadLine() one by one is: " + average3 + "\n");

            // 2 dimension array
            int[,] numbers = { { 1, 4, 2 }, { 3, 6, 8 } };
            Console.WriteLine(numbers[0, 2]);
            numbers[0, 1] = 12;

            // iterating over 2d array using foreach
            foreach (int i in numbers)
            {
                Console.WriteLine(i);
            }

            // iterating over 2d using for loop
            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    Console.WriteLine(numbers[i, j]);
                }
            }
        }
    }
}