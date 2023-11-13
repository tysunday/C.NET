using System;
using static System.Console;

namespace L18
{

    interface IA
    {
        void Show();
    }

    interface IB
    {
        void Show();
    }

    interface IC
    {
        void Show();
    }

    class ABC : IA, IB, IC
    {
        void IA.Show()
        {
            Console.WriteLine("Class <ABC>: Interface A");
        }

        void IB.Show()
        {
            Console.WriteLine("Class <ABC>: Interface B");
        }

        void IC.Show()
        {
            Console.WriteLine("Class <ABC>: Interface C");
        }

        public void Show()
        {
            Console.WriteLine("Class <ABC>");
        }
    }

    interface IPerson
    {
        string Name { get; set; }
        int Age { get; }
        int ID { set; }
    }

    interface IIndexer
    { 
        string this[int index]
        {
            get;
            set;
        }
        string this[string index]
        {
            get;
        }
    }

    class Indexer : IIndexer
    {
        string[] _names = new string[5];
        public string this[int index] {
            get { return _names[index]; }
            set { _names[index] = value; }
        }

        public string this[string index]
        {
            get
            {
                foreach (string n in _names)
                    if (n == index)
                        return n;
                return "NULL";
            }
        }
    }

    class Person : IPerson
    {
        public string Name { get; set; }

        public int Age { get; }

        public int ID { private get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Students.Group g = new Students.Group();

            WriteLine("\n\n==========Сортировка по фамилии==========");
            g.Sort();
            foreach (Students.Student s in g)
            {
                WriteLine(s);
            }

            WriteLine("\n\n==========Сортировка по дате рождения==========");
            g.Sort(new Students.DateComparer());
            foreach (Students.Student s in g)
            {
                WriteLine(s);
            }

            Students.Student s1 = new Students.Student
            {
                FirstName = "Alex",
                LastName = "Smith",
                BirthDate = new DateTime(1999, 3, 4),
                Card = new Students.StudentCard { 
                    Number = 897452, Series = "AC"
                }
            };

            WriteLine("\n\n==========Клонирование==========");
            Students.Student s2 = new Students.Student();
            s2 = s1.Clone() as Students.Student;
            s1.FirstName = "Jake";
            s2.Card.Number = 932612;
            s2.Card.Series = "AB";
            WriteLine(s1);
            WriteLine(s2);
        }
    }
}
