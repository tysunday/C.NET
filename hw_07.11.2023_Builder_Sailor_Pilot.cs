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
        public abstract void Introduce();
    }

    public abstract class Employee : Human
    {
        decimal salary;

        public Employee(string firstName, string lastName) : base(firstName, lastName) { }
        public Employee(string firstName, string lastName, decimal salary) : base(firstName, lastName)
        {
            this.salary = salary;
        }
        public Employee(string firstName, string lastName, DateTime date, decimal salary) 
            : base(firstName, lastName, date)
        {
            this.salary = salary;
        }
        public override string ToString()
        {
            return base.ToString() + $"Salary: {salary} ruble\n";
        }
    }

    class Builder : Employee
    {
        int numberOfHousesBuilt;
        public Builder(string firstName, string lastName, DateTime date, decimal salary, int numberOfHousesBuilt)
            : base(firstName, lastName, date, salary)
        {
            this.numberOfHousesBuilt = numberOfHousesBuilt;
        }

        public override void Introduce()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString() + $"I built many buildings, there are {numberOfHousesBuilt} in total.\n";
        }
    }

    class Sailor : Employee
    {
        int numberOfMilesSailedBySailor;

        public Sailor(string firstName, string lastName, DateTime date, decimal salary, int numberOfMilesSailedBySailor) 
            : base(firstName, lastName, date, salary)
        {
            this.numberOfMilesSailedBySailor = numberOfMilesSailedBySailor;
        }

        public override void Introduce()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString() + $"Sailor. I have sailed many miles, about {numberOfMilesSailedBySailor}.\n";
        }
    }

    class Pilot : Employee
    {
        int targetsHit;
        public Pilot(string firstName, string lastName, DateTime date, decimal salary, int targetsHit)
           : base(firstName, lastName, date, salary)
        {
            this.targetsHit = targetsHit;
        }

        public override void Introduce()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return base.ToString() + $"Pilot. Destroyed {targetsHit} targets.\n";
        }
    }

    internal class Program
    {
        static void Main()
        {
            Employee[] employees =
            {
                new Builder("Oleg", "Olegov", new DateTime(1220, 05, 29), 5, 3),
                new Sailor("Morykov", "Moryak", new DateTime(0005, 09, 09), 0.4m, 38348),
                new Pilot("Vladislav", "Vladislavov", new DateTime(2001, 10, 18), 0.1m, 7)
            };

            foreach(Employee emp in employees)
            {
                Console.WriteLine(emp);
            }
        }
    }
}