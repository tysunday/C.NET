using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hw_01._11._2023_array
{
    class Homework
    {
        static int[] CreateArray(int size)
        {
            int[] array = new int[size];
            return array;
        }
        static decimal[,] CreateArray(int rows, int columns)
        {
            decimal[,] array = new decimal[rows, columns];
            return array;
        }

        static void FillArray(ref int[] array)
        {
            Console.WriteLine(string.Format("Input {0:d} elements", array.Length));
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Convert.ToInt32(Console.ReadLine());
            }
        }
        static void FillArray(ref decimal[,] array)
        {
            Random rand = new Random();
            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int column = 0; column < array.GetLength(1); column++)
                {
                    array[row, column] = rand.Next(1, 9);
                }
            }
        }
        static void PrintArray<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();

        }
        static void PrintArray<T>(T[,] array)
        {
            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int column = 0; column < array.GetLength(1); column++)
                {
                    Console.Write(array[row, column] + " ");
                }
                Console.WriteLine();
            }
        }

        static void MaxElement(int[] array)
        {
            int max = array[0];
            foreach (int element in array)
            {
                if (element > max)
                {
                    max = element;
                }
            }
            Console.WriteLine("Max element is {0:d}:", max);
        }
        static void MaxElement(decimal[,] array)
        {
            decimal max = array[0, 0];
            foreach (int element in array)
            {
                if (element > max)
                {
                    max = element;
                }
            }
            Console.WriteLine(string.Format("Max element is:" + max));
        }

        static void MinElement(int[] array)
        {
            Console.WriteLine(string.Format("Min element in array is: {0:d}", array.Min()));
        }
        static void MinElement(decimal[,] array)
        {
            Console.WriteLine(string.Format("Min element in array is: " + array.Cast<decimal>().Min()));
        }

        static void SumElements(int[] array)
        {
            int result = 0;
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i];
            }
            Console.WriteLine("Sum all elements = " + result);
        }
        static void SumEvenElements(int[] array)
        {
            int result = 0;
            for (int i = 0; i < array.Length; i += 2)
            {
                result += array[i];
            }
            Console.WriteLine("Sum all elements = " + result);
        }
        static void SumOddElements(decimal[,] array)
        {
            decimal result = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 1; j < array.GetLength(1); j+= 2)
                {
                    result += array[i, j];
                }
            }
            Console.WriteLine("Sum odd elements = " + result);
        }
        static void SumElements(decimal[,] array)
        {
            decimal result = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result += array[i, j];
                }
            }
            Console.WriteLine("Sum all elements = " + result);
        }

        static void MultElements(int[] array)
        {
            int result = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                result *= array[i];
            }
            Console.WriteLine("Multiplication all elements = " + result);
        }

        static void MultElements(decimal[,] array)
        {
            decimal result = array[0, 1];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 1; j < array.GetLength(1); j++)
                {
                    result *= array[i, j];
                }
            }
            Console.WriteLine("Multiplication all elements = " + result);
        }



        public static void StartProgram()
        {
            int[] first = CreateArray(5);
            FillArray(ref first);
            PrintArray(first);
            MaxElement(first);
            MinElement(first);
            SumElements(first);
            SumEvenElements(first);
            MultElements(first);

            decimal[,] second = CreateArray(3, 4);
            FillArray(ref second);
            PrintArray(second);
            MaxElement(second);
            MinElement(second);
            SumElements(second);
            SumOddElements(second);
            MultElements(second);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Homework.StartProgram();
        }
    }
}