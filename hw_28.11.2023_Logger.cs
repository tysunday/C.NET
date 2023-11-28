using System;
using System.IO;
using System.Text.RegularExpressions;

namespace hw_27._11._2023_Logger
{
    class Logger
    {
        string _text;
        const string _nameFile = "journal.log";
        public Logger() { }

        public void CreateLog(string text)
        {
            string date = $"{DateTime.Now} ";
            using (FileStream stream = new FileStream(_nameFile, FileMode.Append))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(date + text + '\n');
                stream.Write(array, 0, array.Length);
            }
        }

        public void CreateLog(Exception message)
        {
            string date = $"{DateTime.Now} ";
            using (FileStream stream = new FileStream(_nameFile, FileMode.Append))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(date + message.ToString() + '\n');
                stream.Write(array, 0, array.Length);
            }
        }

        public void ReadLog()
        {
            using (FileStream stream = File.OpenRead(_nameFile))
            {
                byte[] array = new byte[stream.Length];
                stream.Read(array, 0, array.Length);

                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine(textFromFile);
            }
        }
    }
    class Note
    {
        private string _text;
        private DateTime _recordedDate;
        private Logger _logger = new Logger();
        const string _nameFile = "notes.txt";

        public Note() { }
        void PrintCurrentDate()
        {
            _recordedDate = DateTime.Now;
            Console.WriteLine($"Date: {_recordedDate}\n" +
                "Input note:");
            _text = $"Date: {_recordedDate}\n";
        }

        string WriteTextNote()
        {
            _text = _recordedDate.ToString() + '\n';

            string buffer;
            do
            {
                buffer = Console.ReadLine();
                if (!string.IsNullOrEmpty(buffer))
                    _text += buffer + '\n';

            } while (!string.IsNullOrEmpty(buffer));

            return _text;
        }

        bool IsDateTimeFormat(string text)
        {
            string dateFormatPattern = @"^\d{2}.\d{2}.\d{4} \d{2}:\d{2}:\d{2}$";
            return Regex.IsMatch(text, dateFormatPattern);
        }

        void CreateFileNote()
        {
            try
            {
                if (IsDateTimeFormat(_text))
                    throw new Exception("The text contains only the date, no note was created.");
                using (FileStream stream = new FileStream(_nameFile, FileMode.Append))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(_text);
                    stream.Write(array, 0, array.Length);
                }
                _logger.CreateLog(DateTime.Now.ToString() + " Note created");
            }
            catch (Exception ex)
            {
                _logger.CreateLog(ex);
            }
        }

        public void CreateNote()
        {
            PrintCurrentDate();
            WriteTextNote();
            CreateFileNote();
        }

        public void ReadNotes(string Note = "Notes.txt")
        {
            try
            {
                using (FileStream stream = File.OpenRead(Note))
                {
                    byte[] array = new byte[stream.Length];
                    stream.Read(array, 0, array.Length);

                    string textFromFile = System.Text.Encoding.Default.GetString(array);
                    Console.WriteLine(textFromFile);
                }
                _logger.CreateLog(DateTime.Now.ToString() + " Notes readed");
            }
            catch (Exception ex)
            {
                _logger.CreateLog(ex);
            }
        }

        public void ReadLog()
        {
            _logger.ReadLog();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Note note = new Note();
            note.CreateNote();
            note.ReadNotes();
            note.ReadLog();

            // Обработал общие исключения и создал одно собственное, где если пользователь не впишет никакой текст, а оставит только дату, то сработает исключение.
            // Оно соответственно запишется в лог. Если сделать это в первую же итерацию, до создания первой заметки, то запишется ещё одно системное исключение.
            // Оно скажет о том, что не удалось прочитать файл, потому что его не существует.
            // В нормальных историях использованиях пишет в лог запись новой заметки и чтение всех заметок.
        }
    }
}


