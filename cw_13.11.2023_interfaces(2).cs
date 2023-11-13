using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Students
{
    class StudentCard
    {
        public int Number { get; set; }
        public string Series { get; set; }

        public override string ToString()
        {
            return $"Студ. билет: {Series} - {Number}.";
        }
    }

    class Student : IComparable, ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public StudentCard Card { get; set; }

        public object Clone()
        {
            Student temp = MemberwiseClone() as Student;
            temp.Card = new StudentCard
            {
                Number = this.Card.Number,
                Series = this.Card.Series
            };
            return temp;
        }

        /*
         * Принимает object (с которым мы сравниваем)
         * Возвращает целое число:
         * 0 - если объекты находятся в одной позиции
         * <0 - если текущий экземпляр класса меньше объекта по позиции
         * >0 - если текущий экземпляр класса больше объекта по позиции
         */
        public int CompareTo(object obj)
        {
            if (obj is Student)
            {
                return LastName.CompareTo((obj as Student).LastName);
                //return BirthDate.CompareTo((obj as Student).BirthDate);
            }
            throw new NotImplementedException();
        }        

        public override string ToString()
        {
            return $"\nСтудент:\n" +
                $"Имя:{FirstName} {LastName}\n" +
                $"Дата рождения: {BirthDate.ToShortDateString()}\n" +
                Card.ToString();
        }
    }

    class DateComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is Student && y is Student)
            {
                return DateTime.Compare((x as Student).BirthDate, (y as Student).BirthDate);
            }
            throw new NotImplementedException();
        }
    }

    class Group : IEnumerable
    {
        Student[] students = {
            new Student
            {
                FirstName = "John",
                LastName = "Miller",
                BirthDate = new DateTime(2000, 3, 14),
                Card = new StudentCard{Number = 185694, Series = "AB" }
            },
            new Student
            {
                FirstName = "Candice",
                LastName = "Leman",
                BirthDate = new DateTime(2001, 7, 22),
                Card = new StudentCard{Number = 345185, Series = "XA" }
            },
            new Student
            {
                FirstName = "Joey",
                LastName = "Filch",
                BirthDate = new DateTime(1998, 11, 30),
                Card = new StudentCard{Number = 258322, Series = "AA" }
            },
            new Student
            {
                FirstName = "Nicole",
                LastName = "Taylor",
                BirthDate = new DateTime(1998, 5, 10),
                Card = new StudentCard{Number = 513454, Series = "AA" }
            }
        };

        /*
         * У интерфейса IEnumarator есть три члена:
         * свойство Current - возвращает текущий объект коллекции,
         * метод MoveNext() - перемещает перечислитель по элементам коллекции,
         * возвращает true, если достигли конца коллекции - false
         * метод Reset() - возвращает перечислитель к началу коллекции
         */
        IEnumerator IEnumerable.GetEnumerator()
        {
            return students.GetEnumerator();
        }

        public void Sort()
        {
            Array.Sort(students);
        }

        public void Sort(IComparer comparer)
        {
            Array.Sort(students, comparer);
        }
    }
}
