using System;
using System.Collections;
using System.Collections.Generic;


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

        public void AddBook()
        {
            Console.WriteLine("Input author, name and publication date (YYYY MM DD):");
            string author = Console.ReadLine();
            string name = Console.ReadLine();
            string dateInput = Console.ReadLine();

            if (DateTime.TryParseExact(dateInput, "yyyy MM dd", null, System.Globalization.DateTimeStyles.None, out DateTime publicationDate))
            {
                Book book = new Book(author, name, publicationDate);
                books.Add(book);
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in the format 'YYYY MM DD'.");
            }
        }
        public void AddBook(Book book)
        {
            books.Add(book);
        }
        public void DeleteBook()
        {
            Console.WriteLine("Enter the author of the book you want to delete.");
            string author = Console.ReadLine();
            if (books.Remove(Search(author)))
                Console.WriteLine("Book deleted...");
            else
                Console.WriteLine("Book not deleted...");
        }

        public Book Search(string author)
        {
            foreach (Book book in books)
            {
                if (book.author == author) return book;
            }
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

            readList.AddBook(book);
            readList.AddBook(book1);
            readList.AddBook(book2);

            Console.WriteLine(readList);

            readList.DeleteBook();

            Console.WriteLine(readList);

            Book book3 = book2.Clone() as Book;

            book3.author = "5";
            readList.AddBook(book3);

            Console.WriteLine(readList);


        }
    }
}
