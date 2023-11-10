using System;
using System.Xml.Linq;

namespace hw_31._10._2023
{
    class First
    {
        static bool Fizz(int number)
        {
            if (number % 3 == 0)
                return true;
            return false;
        }
        static bool Buzz(int number)
        {
            if (number % 5 == 0)
                return true;
            return false;
        }
        static bool RangeCheck(int number)
        {
            if (number > 100 || number < 1)
                return false;
            return true;

        }
        public static void StartProgram()
        {
            Console.WriteLine("Input number 1 - 100");
            int number = Convert.ToInt32(Console.ReadLine());
            Console.Write($"Number is: {number} \n");
            if (RangeCheck(number))
            {
                if (Fizz(number))
                    Console.WriteLine("FIZZ");
                if (Buzz(number))
                    Console.WriteLine("BUZZ");
            }
            else
            {
                Console.WriteLine("Number not in range...");
            }
        }
    }
    class Second
    {
        static int ChooseTypeTemperature()
        {
            Console.Write(@"
    Select which temperature system you want to convert the value to:
            1 - Celsius
            2 - Fahrenheit" + "\n"
) ;
            int choose = Convert.ToInt32(Console.ReadLine());
            return choose;
        }

        static decimal ConvertTemperature(decimal temperature)
        {
            int choose = ChooseTypeTemperature();
            decimal result = 0;
            switch (choose)
            {
                case 1:
                    result = (temperature - 32) / 1.8m;
                    break;
                case 2:
                    result = temperature * 1.8m + 32;
                    break;
                default:
                    Console.WriteLine("NOT CORRECTED VALUE FOR THIS SWITCH CASE");
                    break;
            }
            return result;
        }

        public static void StartProgram()
        {
            Console.Write("Input temperature:");
            decimal temperature = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(ConvertTemperature(temperature));
        }
    }
    class Program
    {
        static void Main(string[] args)
        
        {
            First.StartProgram();
            Second.StartProgram();
        }
    }
}
