using System;

namespace hw_07._11._2023_Journal
{
    public class Journal
    {
        public Journal() { }
        public Journal(string title, DateTime year_of_foundation, string description, int phone_number, string e_mail)
        {
            this.title = title;
            this.year_of_foundation = year_of_foundation;
            this.description = description;
            this.phone_number = phone_number;
            this.e_mail = e_mail;
        }

        private string title { get; set; }
        private DateTime year_of_foundation { get; set; }
        private string description { get; set; }
        private int phone_number { get; set; }
        private string e_mail { get; set; }

        public override string ToString()
        {
            return $"Title: {title}\n" 
                + $"Year of foundation: {year_of_foundation.ToShortDateString()}\n"
                + $"Description: {description}\n" 
                + $"Phone number: {phone_number}\n"
                + $"Email: {e_mail}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime t = new DateTime(2001, 10, 18);
            Journal journal = new Journal("MONETA", t , "Лучше позвонить чем у кого-то занимать", 5553535, "Hello@email.ru" );
            Console.WriteLine(journal);
        }
    }
}
