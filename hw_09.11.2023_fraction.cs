using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//public static <return type > operator <operation> (<parameters>)

namespace hw_09._11._2023_fraction
{

    class Fraction
    {
        public int numerator { get; private set; }
        public int denominator { get; private set; }
        public Fraction() { }
        public Fraction(int numerator)
        {
            this.numerator = numerator;
            this.denominator = 1;
        }
        public Fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public static Fraction operator ++(Fraction fraction)
        {
            ++fraction.numerator;
            return fraction; // не понимаю почему мы создавали новые объекты и возвращали их, можно ведь выполнять эти манипуляции с имеющимися объектами.
        }
        public static Fraction operator --(Fraction fraction)
        {
            --fraction.numerator;
            return fraction;
        }
        public static Fraction operator !(Fraction fraction)    // это оператор отрицания и сказано его перегрузить так чтобы он менял значение
        {                                                       // дроби на противоположное, но мне кажется это нарушает семантику, но всё таки
            fraction.numerator = -fraction.numerator;              
            return fraction; 
        }
        public static Fraction operator -(Fraction fraction) // заодно и его сделал
        {
            fraction.numerator = -fraction.numerator;
            return fraction;
        }
        public static bool operator true(Fraction fraction)
        {
            return fraction.numerator != 0 ? true : false;
        }
        public static bool operator false(Fraction fraction)
        {
            return fraction.numerator == 0 ? true : false;
        }
        public override string ToString()
        {
            return $"Fraction: {numerator}/{denominator}\n";
        }
    }
    internal class Program
    {
        static void Main()
        {
            Fraction f = new Fraction(2,11);
            Console.WriteLine(f);
            Console.WriteLine(f++);
            Console.WriteLine(f--);
            Console.WriteLine(!f);
            Console.WriteLine(-f);
            Console.WriteLine(f);
            if (f)
                Console.WriteLine("true");
        }
    }
}
