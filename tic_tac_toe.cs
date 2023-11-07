using System;
using static System.Console;
using Players;

namespace Players
{
    public class User
    {
        private const char symbol = 'X';
        public char Symbol { get { return symbol; } }

        public (int row, int column) MakeMove()
        {
            Console.Write("Input row:");
            int i = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input column:");
            int j = Convert.ToInt32(Console.ReadLine());
            i -= 1; j -= 1;
            return (i, j);
        }
    }
    public class Computer
    {
        private const char symbol = '0';
        public char Symbol { get { return symbol; } }
        public (int row, int column) MakeMove()
        {
            Random random = new Random();
            int i = random.Next(1, 4);
            int j = random.Next(1, 4);
            i -= 1; j -= 1;
            return (i, j);
        }
    }
}

namespace tic_tac_toe
{

    public class Field
    {
        private char[,] field = new char[3, 3];

        public char this[int row, int column]
        {
            get { return field[row, column]; }
            set { field[row, column] = value; }
        }

        public Field()
        {
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    field[i, j] = '*';
        }

        public void Print()
        {
            Console.Clear();
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Write(field[i, j]);
                }
                WriteLine();
            }
        }

    }

    public class Game
    {
        Field field = new Field();
        User user = new User();
        Computer computer = new Computer();
        private bool TestMove(int i, int j)
        {
            if ((i < 0 || i > 2) || (j < 0 || j > 2))
            {
                Console.WriteLine("Вы вышли за границы массива");
                return false;
            }
            if (!(field[i, j] != 'X' && field[i, j] != '0'))
            {
                Console.WriteLine("Ячейка уже занята");
                return false;
            }
            return true;
        }

        private void UserMove()
        {
            IsDraw();
            (int i, int j) move = user.MakeMove();
            if (TestMove(move.i, move.j))
                field[move.i, move.j] = user.Symbol;
            else
                UserMove();
            IsUserWin();
        }
        private void ComputerMove()
        {
            IsDraw();
            (int i, int j) move = computer.MakeMove();
            if (TestMove(move.i, move.j))
                field[move.i, move.j] = computer.Symbol;
            else
                ComputerMove();
            IsComputerWin();
        }

        private void IsUserWin(char symbol = 'X')
        {
            for (int i = 0; i < 3; i++)
            {
                if (field[i, 0] == symbol && field[i, 1] == symbol && field[i, 2] == symbol)
                {
                    field.Print();
                    Console.WriteLine("User is win");
                    Environment.Exit(0);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (field[0, i] == symbol && field[1, i] == symbol && field[2, i] == symbol)
                {
                    field.Print();
                    Console.WriteLine("User is win");
                    Environment.Exit(0);
                }
            }
            if ((field[0, 0] == symbol && field[1, 1] == symbol && field[2, 2] == symbol) ||
                (field[0, 2] == symbol && field[1, 1] == symbol && field[2, 0] == symbol))
            {
                field.Print();
                Console.WriteLine("User is win");
                Environment.Exit(0);
            }
        }
        private void IsComputerWin(char symbol = '0')
        {
            for (int i = 0; i < 3; i++)
            {
                if (field[i, 0] == symbol && field[i, 1] == symbol && field[i, 2] == symbol)
                {
                    field.Print();
                    Console.WriteLine("Computer is win");
                    Environment.Exit(0);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (field[0, i] == symbol && field[1, i] == symbol && field[2, i] == symbol)
                {
                    field.Print();
                    Console.WriteLine("Computer is win");
                    Environment.Exit(0);
                }
            }
            if ((field[0, 0] == symbol && field[1, 1] == symbol && field[2, 2] == symbol) ||
                (field[0, 2] == symbol && field[1, 1] == symbol && field[2, 0] == symbol))
            {
                field.Print();
                Console.WriteLine("Computer is win");
                Environment.Exit(0);
            }
        }

        private void IsDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[i, j] == '*')
                        return;

                }
            }
            Console.WriteLine("Draw");
            Environment.Exit(0);
        }

        public void StartGame()
        {
            Random rand = new Random();
            if (rand.Next(1,3) % 2 == 0)
            {
                while (true)
                {
                    field.Print();
                    ComputerMove();
                    field.Print();
                    UserMove();
                    
                }

            }
            else
            {
                while (true)
                {
                    field.Print();
                    UserMove();
                    field.Print();
                    ComputerMove();
                }
            }

        }
    }
    internal class Program
    {
        static void Main()
        {
            Game game = new Game();
            game.StartGame();
        }
    }

}
