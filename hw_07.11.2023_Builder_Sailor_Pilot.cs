//Задание 1. На основе классов Human и Employee, созданных в Академии, с
//помощью механизма наследования, реализуйте класс Builder (содержит
//информацию о строителе), класс Sailor(содержит информацию о моряке),
//класс Pilot(содержит информацию о летчике). Каждый из классов должен
//содержать необходимые для работы методы.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_07._11._2023_Builder_Sailor_Pilot
{
    public abstract class Human
    {
        string firstName;
        string lastName;
        DateTime birthDate;

        public Human() { }
        public Human(string firstName, string lastName)
        { 
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public Human(string firstName, string lastName, DateTime birthDate) : this(firstName, lastName)
        {
            this.birthDate = birthDate;
        }
        public override string ToString()
        {
            return $"Last name: {lastName}\n" +
                $"First name: {firstName}\n" +
                $"Date of birth: {birthDate.ToShortDateString()}\n";
        }
    }

    public abstract class Employee : Human
    {
        decimal salary;
        public Employee(string firstName, string lastName) : base(firstName, lastName) { }
        public Employee(string firstName, string lastName, decimal salary) : base(firstName, lastName)
        {
            this.salary = salary;
        }
        public Employee(string firstName, string lastName, DateTime date, decimal salary) :
            base(firstName, lastName, date)
        { 
            this.salary = salary;
        }
        public override string ToString()
        {
            return base.ToString() + $"Salary: {salary} ruble\n";
        }
    }
    internal class Program
    {
        static void Main()
        {

        }
    }
}
