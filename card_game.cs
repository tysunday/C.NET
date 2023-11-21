using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hw_20._11._2023_card_game
{
    class Game
    {
        public Game() { }

        public List<Card> CreateDeck()
        {
            List<Card> cards = new List<Card>();

            List<char> suit = new List<char>()
            { (char)3, (char)4, (char)5, (char)6 };

            List<string> type = new List<string>()
            {"6", "7", "8", "9", "10", "jack", "lady", "king", "ace"};

            for (int j = 0; j < type.Count; j++)
            {
                for (int i = 0; i < suit.Count; i++)
                {
                    Card newCard = new Card(suit[i], type[j]);
                    cards.Add(newCard);
                }
            }
            return cards;
        }

        public void Shuffle(List<Card> cards)
        {
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public void DealCards(Player player1, Player player2, List<Card> cards)
        {
            for (int i = 0; i < 6; i++)
            {
                player1.cardsInHand.Add(cards[0]);
                cards.Remove(cards[0]);
                player2.cardsInHand.Add(cards[0]);
                cards.Remove(cards[0]);
            }
        }

        void PrintCurrentField(List<Card> RoundCards) {
            Console.WriteLine("CURRENT FIELD: ");
            foreach (Card card in RoundCards)
                Console.WriteLine($"{card} ");
        }

        public bool StartGame(Player player, Player computer)
        {
            List<Card> RoundCards = new List<Card>();
            do
            {
                Console.Clear();

                Card ComputerCard = computer.RandomCard();
                Card PlayerCard = player.ChooseCard(); // здесь можно поменять на RandomCard и игра будет сама вычислять победителя.

                RoundCards.Add(PlayerCard);
                RoundCards.Add(ComputerCard);

                player.cardsInHand.Remove(PlayerCard);
                computer.cardsInHand.Remove(ComputerCard);

                PrintCurrentField(RoundCards);

                Console.WriteLine($"Computer choose: {ComputerCard}");
                Console.WriteLine($"Player choose: {PlayerCard}");

                if (ComputerCard < PlayerCard)
                {
                    player.cardsInHand.AddRange(RoundCards);
                    RoundCards.Clear();
                }
                else if (ComputerCard > PlayerCard)
                {
                    computer.cardsInHand.AddRange(RoundCards);
                    RoundCards.Clear();
                }
                else if (ComputerCard == PlayerCard)
                    RoundCards.Clear();
                
            } while (player.cardsInHand.Count != 0 && computer.cardsInHand.Count != 0);

            Console.Clear();
            if (player.cardsInHand.Count == 0)
                Console.WriteLine("PLAYER LOSE. COMPUTER WIN.");
            if (computer.cardsInHand.Count == 0)
                Console.WriteLine("COMPUTER LOSE. PLAYER WIN.");
            return false;

        }
    }
    class Card
    {
        public Card(char suit, string type)
        {
            this.suit = suit;
            this.type = type;
        }

        public override string ToString()
        {
            return $"{suit}{type}";
        }

        private int GetCardRank()
        {
            if (this.type == "6") return 1;
            else if (this.type == "7") return 2;
            else if (this.type == "8") return 3;
            else if (this.type == "9") return 4;
            else if (this.type == "10") return 5;
            else if (this.type == "jack") return 6;
            else if (this.type == "lady") return 7;
            else if (this.type == "king") return 8;
            else if (this.type == "ace") return 9;
            else
            {
                Console.Clear();
                Console.WriteLine("ERROR. UNKNOWN TYPE CARD.");
                Console.WriteLine(this);
                return 0;
            }
        }

        char suit;
        string type;

        public static bool operator <(Card card1, Card card2)
        {
            if (card1.GetCardRank() < card2.GetCardRank())
                return true;
            return false;
        }
        public static bool operator >(Card card1, Card card2)
        {
            if (card1.GetCardRank() > card2.GetCardRank())
                return true;
            return false;
        }
        public static bool operator <=(Card card1, Card card2)
        {
            if (card1.GetCardRank() <= card2.GetCardRank())
                return true;
            return false;
        }
        public static bool operator >=(Card card1, Card card2)
        {
            if (card1.GetCardRank() >= card2.GetCardRank())
                return true;
            return false;
        }
        public static bool operator ==(Card card1, Card card2)
        {
            if (card1.GetCardRank() == card2.GetCardRank())
                return true;
            return false;
        }
        public static bool operator !=(Card card1, Card card2)
        {
            if (card1.GetCardRank() != card2.GetCardRank())
                return true;
            return false;
        }
    }

    class Player
    {
        public List<Card> cardsInHand;
        public Player()
        {
            cardsInHand = new List<Card>();
        }

        public Card ChooseCard()
        {
            Console.WriteLine("Choose card:");
            Print();
            int choose;
            while (!int.TryParse(Console.ReadLine(), out choose) || choose < 0 || choose >= cardsInHand.Count)
                Console.WriteLine("Invalid input. Please enter a valid number within the range.");

            return this.cardsInHand[choose];
        }

        public Card RandomCard()
        {
            Console.WriteLine("Computer cards:");
            Print();
            Random random = new Random();
            int randomCard = 0;
                randomCard = random.Next(0, cardsInHand.Count);

            return this.cardsInHand[randomCard];

        }

        public void Print()
        {
            int i = 0;
            foreach (Card card in cardsInHand)
            {
                Console.WriteLine(i + " " + card);
                i++;
            }
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            List<Card> cards = game.CreateDeck();
            game.Shuffle(cards);

            Player player1 = new Player();
            Player player2 = new Player();

            game.DealCards(player1, player2, cards);
            game.StartGame(player1, player2);

        }
    }
}
