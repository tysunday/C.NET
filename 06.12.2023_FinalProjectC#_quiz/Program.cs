using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace _06._12._2023_FinalProjectC__quiz
{
    class Quiz
    {
        int[] answers = new int[20];
        int[] AnswersBiology = { 1, 2, 1, 1, 1, 3, 2, 1, 1, 1, 2, 3, 2, 1, 1, 1, 2, 3, 2, 1 };
        int[] AnswersHistory = { 1, 4, 3, 1, 1, 3, 4, 3, 1, 4, 1, 1, 1, 2, 1, 4, 2, 4, 1, 3 };
        int[] AnswersMath = { 1, 2, 3, 4, 1, 3, 4, 4, 3, 3, 4, 3, 3, 2, 4, 1, 3, 4, 1, 2 };
        string[] Quizs = {
            "Biology",
            "History",
            "Math"
        };
        public Tuple<string, decimal> CheckingAnswers(string chapter_quiz, User user)
        {
            int[] CorrectAnswers = new int[20];
            decimal point = 0;
            switch (chapter_quiz)
            {
                case "Biology.txt":
                    CorrectAnswers = AnswersBiology;
                    break;
                case "History.txt":
                    CorrectAnswers = AnswersHistory;
                    break;
                case "Math.txt":
                    CorrectAnswers = AnswersMath;
                    break;
            }
            for (int i = 0; i < 20; i++)
                if (answers[i] == CorrectAnswers[i])
                    point++;

            Console.WriteLine($"Количество баллов: {point}");
            Console.WriteLine($"Оценка: {point / 20m * 5m}");
            return new Tuple<string, decimal>(chapter_quiz, point);

        }
        public string ChooseQuiz()
        {
            int i = 1;
            foreach (string quiz in Quizs)
            {
                Console.WriteLine($"{i} - {quiz}.");
                i++;
            }
            int answer = Convert.ToInt32(Console.ReadLine());
            switch (answer)
            {
                case 1:
                    return "Biology.txt";
                case 2:
                    return "History.txt";
                case 3:
                    return "Math.txt";
                default:
                    return null;
            }
        }

        public void ReadQuestion(int answer, string fileName)
        {
            answer *= 5;
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine($"{answer / 5 + 1}){lines[answer]}");
            for (int j = 1; j <= 4; j++)
                Console.WriteLine($"{j}. {lines[answer + j]}");
        }
        public void EnterAnswer(int responseAnswer)
        {
            int answer;
            bool isValidAnswer;
            do
            {
                isValidAnswer = int.TryParse(Console.ReadLine(), out answer);

                if (!isValidAnswer || answer < 1 || answer > 4)
                {
                    Console.WriteLine("Invalid answer. Please choose a number between 1 and 4.");
                    isValidAnswer = false;
                }

            } while (!isValidAnswer);
            answers[responseAnswer] = answer;
        }
    }
    public class User
    {
        string Name;
        DateTime Birthday;
        public User(string name)
        {
            Name = name;
        }
        public bool IsSignSystem { private get; set; } = false;
        List<Tuple<string, decimal>> CompletedQuizzes = new List<Tuple<string, decimal>>();
        static AuthenticationSystem AuthenticationSystem = new AuthenticationSystem();
        public void ShowPastQuizResults()
        {
            Console.WriteLine($"Name user: {Name}");
            Console.WriteLine($"Birthday user: {Birthday}");
            if (IsSignSystem)
            {
                Console.Clear();
                int i = 1;
                foreach (var quiz in CompletedQuizzes)
                {
                    Console.WriteLine($"{i} - In chapter {quiz.Item1} point: {quiz.Item2}");
                    i++;
                }
            }
            else
                Console.WriteLine("Pls sign in system...");
        }
        public string EnterData(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
        /// <summary>
        /// This method is intended for registration in the system.
        /// </summary>
        public void SignUp()
        {
            AuthenticationSystem.SignUpSystem(this);
        }
        /// <summary>
        /// This method is for user login. 
        /// And his change IsSignSystem = true.
        ///<returns>void</returns>
        /// </summary>
        public void SignIn()
        {
            AuthenticationSystem.SignInSystem(this);
        }
        public void StartQuiz()
        {
            if (IsSignSystem)
            {
                Quiz quiz = new Quiz();
                Console.WriteLine("Enter chapter: ");
                string chapter_quiz = quiz.ChooseQuiz();
                for (int responseAnswer = 0; responseAnswer < 20; responseAnswer++)
                {
                    quiz.ReadQuestion(responseAnswer, chapter_quiz);
                    quiz.EnterAnswer(responseAnswer);
                    Console.Clear();
                }
                Tuple<string, decimal> chapterQuiz_Point = quiz.CheckingAnswers(chapter_quiz, this);
                CompletedQuizzes.Add(chapterQuiz_Point);
            }
            else
                Console.WriteLine("Pls sign in system");
        }
    }

    public class AuthenticationSystem
    {
        bool IsLoginTaken(string login)
        {
            string textFromFile = File.ReadAllText("RegisteredUsers.txt");
            return textFromFile.Contains($"Login:{login}");
        }
        bool IsValidDateFormat(string date)
        {
            string dateFormatPattern = @"^\d{2}.\d{2}.\d{4}$";
            return Regex.IsMatch(date, dateFormatPattern);
        }
        public bool SignInSystem(User user)
        {
            Console.Clear();
            Console.WriteLine("***Sign in system***");
            string login = user.EnterData("Enter login");
            string password = user.EnterData("Enter password");
            string TextFromFile = File.ReadAllText("RegisteredUsers.txt");
            if (TextFromFile.Contains($"Login:{login}\r\nPassword:{password}"))
            {
                Console.WriteLine("You have successfully logged in");
                user.IsSignSystem = true;
                return true;
            }
            Console.WriteLine("Login unsuccessful");
            return false;
        }
        public void SignUpSystem(User user)
        {
            string login, password, birthday;
            Console.WriteLine("***Registration***");
            do
            {
                login = user.EnterData("Enter login");
            } while (IsLoginTaken(login));
            password = user.EnterData("Enter password");
            do
            {
                birthday = user.EnterData("Enter your birthday (DD.MM.YYYY");
            } while (!IsValidDateFormat(birthday));
            string NewUserInformation = $"\nLogin:{login}\r\nPassword:{password}\r\nBirthday:{birthday}";
            File.AppendAllText("RegisteredUsers.txt", NewUserInformation);
            Console.WriteLine("Registration is successfully");
        }
    }
    class Menu
    {
        User user = new User("Vladislav");

        public bool StartApp()
        {
            StartMenuInterface();
            bool flag = true;
            switch (Console.ReadLine())
            {
                case "1":
                    user.SignIn();
                    break;
                case "2":
                    user.SignUp();
                    break;
                case "3":
                    user.StartQuiz();
                    break;
                case "4":
                    user.ShowPastQuizResults();
                    break;
                default:
                    flag = false;
                    return flag;
            }
            user.EnterData("Click anything button to continue...");
            return flag;
        }
        void StartMenuInterface()
        {
            Console.Clear();
            Console.WriteLine("**********MENU**********\n" +
                "Press:\n" +
                "1 - Sign in.\n" +
                "2 - Sign up.\n" +
                "3 - Start quiz.\n" +
                "4 - Show past quiz results\n" +
                "  - Another button to exit.\n");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            while (menu.StartApp()) ;
        }
    }
}
