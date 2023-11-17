using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace hw_15._11._2023_reading_list
{
    public class Book : ICloneable
    {
        public string author { get; set; }
        public string name { get; set; }
        public DateTime publicationDate { get; set; }
        public Book()
        {
            author = "default";
            name = "default";
            publicationDate = new DateTime(0001, 01, 01);
        }
        public Book(string author, string name)
        {
            this.author = author;
            this.name = name;
            publicationDate = new DateTime(0001, 01, 01);
        }
        public Book(string author, string name, DateTime publicationDate) : this(author, name)
        {
            this.publicationDate = publicationDate;
        }

        public static Book CreateBook() // Я не до конца понял "Почему данные книги запрашиваются в списке книг, а не в самой книге?"
        {                               // Скорее всего я что-то не так делаю, но я убрал метод AddBook в классе ReadList, где создавал данные для книги прямо в методе.
            Console.WriteLine("Input author, name and publication date (YYYY MM DD):");  // Теперь у меня есть метод CreateBook с помощью которого я создаю книгу.
            string author = Console.ReadLine();         // А AddBook в ReadList теперь не имеет перегрузки и принимает в себя 1 аргумент - книгу.
            string name = Console.ReadLine();
            string dateInput = Console.ReadLine();

            if (DateTime.TryParseExact(dateInput, "yyyy MM dd", null, System.Globalization.DateTimeStyles.None, out DateTime publicationDate))
            {
                return new Book(author, name, publicationDate);
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in the format 'YYYY MM DD'.");
                return null;
            }
        }

        public override string ToString()
        {
            return $"Author: {author}\nName: {name}\nPublication Date: {publicationDate.ToShortDateString()}\n";
        }

        public object Clone()
        {
            Book temp = MemberwiseClone() as Book;
            temp.author = author;
            temp.name = name;
            temp.publicationDate = publicationDate;
        
            return temp;
        }
    }
    public class ReadList : IEnumerable
    {

        public List<Book> books { get; private set; }
        public ReadList()
        {
            books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }
        public void DeleteBook(Book book) // Теперь в этом методе внутри не вводятся данные для метода Search, а сразу передаётся нужная книга.
        {
            if (books.Remove(book))
                Console.WriteLine($"Book {book.author} - {book.name} deleted.") ;
            else
                Console.WriteLine("Book not deleted.");
        }

        public Book Search(string author, string name) // Добавил дополнительное условие, чтобы проверялся не только автор но и название книги.
        {
            Console.WriteLine("Enter the author and name of the book you want to delete.");
            author = Console.ReadLine();
            name = Console.ReadLine();
            foreach (Book book in books)
            {
                if (book.author == author && book.name == name) 
                    return book;
            }
            Console.WriteLine("Book not identified.");
            return null;
        }

        public override string ToString()
        {
            string allCollections = "";
            foreach (Book book in books)
                allCollections += book.ToString();
            return allCollections;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return books.GetEnumerator();
        }

    }
    internal class Program
    {
        static void Main()
        {
            ReadList readList = new ReadList();
            Book book = new Book("Author", "Name", new DateTime(0001, 01, 01));
            Book book1 = new Book("VSEVOLOD", "IVANOV", new DateTime(1443, 09, 11));
            Book book2 = new Book("GEORGIY", "VASILEVICH", new DateTime(144, 10, 23));
            Book book3 = Book.CreateBook();

            readList.AddBook(book);
            readList.AddBook(book1);
            readList.AddBook(book2);
            readList.AddBook(book3);

            Console.WriteLine(readList);

            readList.DeleteBook(book1);

            Console.WriteLine(readList);

            Book book4 = book2.Clone() as Book;

            book4.author = "CLONE";
            book4.name = "CLONE";
            readList.AddBook(book4);

            Console.WriteLine(readList);


        }
    }
}
