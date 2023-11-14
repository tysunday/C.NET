using System;
using System.Collections.Generic;
using System.Linq;

namespace hw_13._11._2023_Interface_HomeConstruction
{
    public interface IPart
    {
        void Print();
    }
    public abstract class Part
    {
        public virtual void Print()
        {
            Console.WriteLine(this.GetType().Name + " is build");
        }
    }
    public class Basement : Part, IPart
    {
        public Basement() { Print(); }
    }
    public class Wall : Part, IPart
    {
        public Wall() { Print(); }
    }
    public class Door : Part, IPart
    {
        public Door() { Print(); }
    }
    public class Window : Part, IPart
    {
        public Window() { Print(); }
    }
    interface IWorker
    {
        bool CheckBasement(House house);
        bool CheckWall(House house);
        bool CheckDoor(House house);
        bool CheckWindow(House house);
    }
    public class Worker : IWorker
    {
        public bool CheckBasement(House house)
        {
            if (house.parts.OfType<Basement>().Any())
                return true;
            return false;
        }
        public bool CheckWall(House house)
        {

            if (house.parts.OfType<Wall>().Count() == 4)
                return true;
            return false;

        }
        public bool CheckWindow(House house)
        {
            if (house.parts.OfType<Window>().Count() == 4)
                return true;
            return false;
        }
        public bool CheckDoor(House house)
        {
            if (house.parts.OfType<Door>().Any())
                return true;
            return false;
        }
    }
    public class Team : Worker, IWorker
    {
        public void CreateHouse(House house)
        {
            if (!CheckBasement(house))
            {
                house.parts.Add(new Basement());
            }
            while (!CheckWall(house))
            {
                house.parts.Add(new Wall());
            }
            if (!CheckDoor(house))
            {
                house.parts.Add(new Door());
            }
            while (!CheckWindow(house))
            {
                house.parts.Add(new Window());
            }
        }
    }
    public class TeamLeader : Worker, IWorker
    {
        public void MakeReport(House house)
        {
            if (!CheckBasement(house))
                Console.WriteLine("U HAVENT BASEMENT");
            else if (!CheckWall(house))
                Console.WriteLine("U HAVENT WALL");
            else if (!CheckDoor(house))
                Console.WriteLine("U HAVENT WINDOW");
            else if (!CheckWindow(house))
                Console.WriteLine("U HAVENT DOOR");
            else
            {
                Console.WriteLine("all okay, house is completed.");
                Console.WriteLine("   _______");
                Console.WriteLine("  /       \\");
                Console.WriteLine(" /         \\");
                Console.WriteLine("/___________\\");
                Console.WriteLine("|    ___    |");
                Console.WriteLine("|   |   |   |");
                Console.WriteLine("|___|___|___|");
            }
        }
    }

    public class House
    {
        public List<IPart> parts = new List<IPart>();
        TeamLeader teamLeader = new TeamLeader();
        Team team = new Team();

        public House() { }

        public void StartCreateHouse()
        {
            team.CreateHouse(this);
            teamLeader.MakeReport(this);
        }
    }

    internal class Program
    {
        static void Main()
        {
            House house = new House();
            house.StartCreateHouse();
        }
    }
}
