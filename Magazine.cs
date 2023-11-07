using System;

namespace hw_07._11._2023_Journal
{
    public class Magazine
    {
        public Magazine() { }
        public Magazine(string title, DateTime year_of_foundation, string description, string phone_number, string e_mail)
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
        private string phone_number { get; set; }
        private string e_mail { get; set; }

        public override string ToString()
        {
            return $"Title: {title}\n"
                + $"Year of foundation: {year_of_foundation.ToShortDateString()}\n"
                + $"Description: {description}\n"
                + $"Phone number: {phone_number}\n"
                + $"Email: {e_mail}\n";
        }
        public static Magazine CreateMagazineFromConsoleInput()
        {
            string title;
            DateTime year_of_foundation;
            string description;
            string phone_number;
            string e_mail;

            Console.Write("Title: ");
            title = Console.ReadLine();

            Console.Write("Year of foundation: ");
            if (!DateTime.TryParse(Console.ReadLine(), out year_of_foundation))
            {
                Console.WriteLine("INVALID INPUT");
            }

            Console.WriteLine("Description: ");
            description = Console.ReadLine();

            Console.WriteLine("Phone number:");
            phone_number = Console.ReadLine();

            Console.WriteLine("E_mail");
            e_mail = Console.ReadLine();

            return new Magazine(title, year_of_foundation, description, phone_number, e_mail);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime Date = new DateTime(2001, 10, 18);
            Magazine magazine1 = new Magazine("HOMEMONEY", Date, "Лучше позвонить чем у кого-то занимать", "88005553535", "Hello@email.ru");
            Console.WriteLine(magazine1);

            Magazine magazine2 = Magazine.CreateMagazineFromConsoleInput();
            Console.WriteLine(magazine2);
        }
    }
}
