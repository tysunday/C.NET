using System;
using System.Collections.Generic;
using System.Threading;

namespace hw_24._11._2023_hw_race
{
    public abstract class Car
    {
        protected string Name { get; }
        protected Car(int maxSpeed, string name, char skin)
        {
            MaxSpeed = maxSpeed;
            Name = name;
            Skin = skin;
        }
        public int CurrentSpeed { get; private set; } = 0;
        public int DistanceTraveled { get; private set; } = 0;
        public char Skin { get; private set; }
        public int MaxSpeed { get; private set; }
        Random rand = new Random();
        virtual public int SpeedCalculation()
        {
            if (CurrentSpeed != MaxSpeed)
                CurrentSpeed += rand.Next(1, 4);
            else if (CurrentSpeed >= MaxSpeed)
                CurrentSpeed = rand.Next(1, 4);
            return CurrentSpeed;
        }

        virtual public void Go()
        {
            DistanceTraveled += SpeedCalculation();
        }
        public override string ToString()
        {
            return $"{Name} - {this.GetType().Name}";
        }
    }

    public class SportCar : Car
    {
        public SportCar(int maxSpeed, string name, char skin) : base(maxSpeed, name, skin) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class PassengerCar : Car
    {
        public PassengerCar(int maxSpeed, string name, char skin) : base(maxSpeed, name, skin) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    public class Truck : Car
    {
        public Truck(int maxSpeed, string name, char skin) : base(maxSpeed, name, skin) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    public class Bus : Car
    {
        public Bus(int maxSpeed, string name, char skin) : base(maxSpeed, name, skin) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }


    public class RaceWay
    {
        public int Distance { get; private set; } = 180;
        public RaceWay() { }
        public void ShowRace(List<Car> cars)
        {
            for (int currentCar = 0; currentCar < cars.Count; currentCar++)
            {
                for (int waypoint = 0; waypoint < Distance; waypoint++)
                {
                    if (cars[currentCar].DistanceTraveled < waypoint)
                        Console.Write(".");
                    if (cars[currentCar].DistanceTraveled == waypoint)
                        Console.Write(cars[currentCar].Skin);
                    if (cars[currentCar].DistanceTraveled > waypoint && cars[currentCar].DistanceTraveled < Distance)
                        Console.Write((char)95);
                }
                Console.WriteLine($"{cars[currentCar]}");

            }
            Thread.Sleep(500);
            Console.Clear();

        }
    }

    public delegate bool IsFinishDelegate(List<Car> cars, RaceWay raceway);
    public delegate void GoDelegate();

    class Game
    {
        public Game()
        {
            Console.WindowHeight = 30;
            Console.WindowWidth = 280;
        }
        public IsFinishDelegate IsFinishDelegateHandler;
        public GoDelegate GoDelegateHandler;
        public bool IsFinish(List<Car> cars, RaceWay raceWay)
        {
            foreach (Car car in cars)
                if (car.DistanceTraveled >= raceWay.Distance)
                {
                    Console.WriteLine($"{car} win race.");
                    return true;
                }
            return false;
        }
        public void Start()
        {
            SportCar sport = new SportCar(10, "King", (char)15);
            PassengerCar passenger_car = new PassengerCar(8, "Mark", (char)16);
            Truck truck = new Truck(7, "Queen", (char)17);
            Bus bus = new Bus(9, "Lord", (char)14);
            RaceWay raceWay = new RaceWay();
            List<Car> cars = new List<Car> { sport, passenger_car, truck, bus };

            IsFinishDelegateHandler = IsFinish;

            foreach (Car car in cars)
                GoDelegateHandler += car.Go;

            while (!IsFinishDelegateHandler(cars,raceWay))
            {
                raceWay.ShowRace(cars);
                GoDelegateHandler();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
