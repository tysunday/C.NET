using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;

namespace hw_15._11._2023_reading_list
{

    public class BookException : Exception
    {
        public BookException(string message) : base(message) { }
    }
    public class Book : ICloneable
    {
        private string _author;
        private string _name;
        private DateTime _publicationDate;
        public string Author
        {
            get { return _author; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new BookException("Author cannot be null or empty");
                _author = value;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new BookException("Name cannot be null or empty");
                _name = value;
            }
        }

        public DateTime PublicationDate
        {
            get { return _publicationDate; }
            set
            {
                if (value < DateTime.MinValue || value > DateTime.MaxValue)
                    throw new BookException("Invalid publication date.");
                _publicationDate = value;
            }
        }
        public Book()
        {
            Author = "default";
            Name = "default";
            PublicationDate = new DateTime(0001, 01, 01);
        }
        public Book(string author, string name)
        {
            this.Author = author;
            this.Name = name;
            PublicationDate = new DateTime(0001, 01, 01);
        }
        public Book(string author, string name, DateTime publicationDate) : this(author, name)
        {
            this.PublicationDate = publicationDate;
        }

        public static Book CreateBook()
        {
            Console.WriteLine("Input author, name and publication date (YYYY MM DD):");
            string author = Console.ReadLine();
            string name = Console.ReadLine();
            string dateInput = Console.ReadLine();

            if (DateTime.TryParseExact(dateInput, "yyyy MM dd", null, System.Globalization.DateTimeStyles.None, out DateTime publicationDate))
            {
                return new Book(author, name, publicationDate);
            }
            else
            {
                //Console.WriteLine("Invalid date format. Please enter the date in the format 'YYYY MM DD'.");
                return null;
            }
        }

        public override string ToString()
        {
            return $"Author: {Author}\nName: {Name}\nPublication Date: {PublicationDate.ToShortDateString()}\n";
        }

        public object Clone()
        {
            Book temp = MemberwiseClone() as Book;
            temp.Author = Author;
            temp.Name = Name;
            temp.PublicationDate = PublicationDate;

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

        public Book this[int key]
        { 
            get
            {
                if(key >= 0 && key < books.Count)
                {
                    return books[key];
                }
                throw new BookException($"Out of range key in readlist (key == {key})");
            }
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }
        public void DeleteBook(Book book)
        {
            if (books.Remove(book))
                Console.WriteLine($"Book {book.Author} - {book.Name} deleted.");
            else
                Console.WriteLine("Book not deleted.");
        }

        public Book Search(string author, string name)
        {
            Console.WriteLine("Enter the author and name of the book you want to delete.");
            author = Console.ReadLine();
            name = Console.ReadLine();
            foreach (Book book in books)
            {
                if (book.Author == author && book.Name == name)
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
            try
            {
                //Book book = new Book(null, "SomeName", new DateTime(0001, 01, 01)); // проверка BookException.
                Book book1 = new Book("VSEVOLOD", "SERGEY", new DateTime(1443, 09, 11));
                Book book2 = new Book("GEORGIY", "VASILEVICH", new DateTime(144, 10, 23));
                Book book3 = Book.CreateBook();

                readList.AddBook(book1);
                readList.AddBook(book2);
                readList.AddBook(book3);

                //Console.WriteLine(readList[44]); // проверка BookException.

                Console.WriteLine(readList);

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();

                readList.DeleteBook(book1);
                
                Console.WriteLine(readList);
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();

                Book book4 = book2.Clone() as Book;

                book4.Author = "CLONE";
                book4.Name = "CLONE";
                readList.AddBook(book4);

                Console.WriteLine(readList);
            }
            catch (BookException ex) { 
                Console.WriteLine(ex.Message);
            }
        }
    }
}
