using System;

namespace hw_13._12._2023_guess_number
{
    class Guess
    {
        int CountOfTries = 0;
        int TryToGuessTheNumber()
        {
            int number;
            bool isValidAnswer;
            do
            {
                Console.WriteLine("Enter number...");
                isValidAnswer = int.TryParse(Console.ReadLine(), out number);

                if (!isValidAnswer || number < 1 || number > 2000)
                {
                    Console.WriteLine("Invalid answer. Please choose a number between 1 and 2000.");
                    isValidAnswer = false;
                }

            } while (!isValidAnswer);
            return number;
        }
        int MakeNumber()
        {
            Random rand = new Random();
            return rand.Next(1, 2001);
        }
        public bool TRY()
        {
            int HiddenNumber = MakeNumber();
            while (HiddenNumber != TryToGuessTheNumber())
            {
                Console.WriteLine("No, this isn't right number.");
                CountOfTries++;
                Console.WriteLine($"Right answer is: {HiddenNumber}");
            }

            Console.Clear();
            Console.WriteLine($"Yes, it's right answer. Hidden number is: {HiddenNumber}");
            Console.WriteLine($"Count of try: {CountOfTries}");
            CountOfTries = 0;
            return true;

        }
    }
    class GuessComputer
    {
        int HiddenNumber;
        public void EnterHideNumber()
        {
            bool isValidAnswer;
            do
            {
                Console.WriteLine("Guess the number");
                isValidAnswer = int.TryParse(Console.ReadLine(), out HiddenNumber);

                if (!isValidAnswer || HiddenNumber < 1 || HiddenNumber > 2000)
                {
                    Console.WriteLine("Invalid answer. Please choose a number between 1 and 2000.");
                    isValidAnswer = false;
                }

            } while (!isValidAnswer);
        }
        public void TryToGuessTheNumber()
        {
            for(int i = 1; i < 2001; i++)
            {
                
                if(i == HiddenNumber)
                {
                    Console.WriteLine($"Yes, it's right answer. Hidden number is: {HiddenNumber}");
                    Console.WriteLine($"Count of try: {i--}");
                    break;
                }
                else
                {
                    Console.WriteLine("No, this isn't right number.");
                }
            }
        }
        public bool Answer()
        {
            bool isValidAnswer;
            int answer;
            Console.WriteLine("Do you want to play more? 1 - yes, 2 - no");
            do
            {
                isValidAnswer = int.TryParse(Console.ReadLine(), out answer);

                if (!isValidAnswer || (answer != 1 && answer != 2))
                {
                    Console.WriteLine("Invalid answer. Please choose a number 1 or 2.");
                    isValidAnswer = false;
                }
            } while (!isValidAnswer);
            if (answer == 1)
                return true;
            return false;
        }
    }
    internal class Program
    {
        static void Main()
        {
            //Guess guess = new Guess();
            //do
            //{
            //    guess.TRY() ;
            //    Console.WriteLine("Do you want to play again? (yes/no)");
            //} while (Console.ReadLine().Trim().ToLower() == "yes");

            GuessComputer guessComputer = new GuessComputer();
            do
            {
                guessComputer.EnterHideNumber();
                guessComputer.TryToGuessTheNumber();
            } while (guessComputer.Answer());
        }
    }
}
