using System;
using System.ComponentModel;

namespace hw_08._11._2023_device
{
    abstract class Device
    {
        string name;
        int cost;
        protected Device() { }
        protected Device(string name, int cost)
        {
            this.name = name;
            this.cost = cost;
        }

        public override string ToString()
        {
            return $"Product Name: {name}.\nCost: {cost}.\n";
        }

        public abstract void Sound();
        public virtual string Desc()
        {
            return "I'm just a device in this world.\n";
        }
    }
    abstract class KitchenAppliances : Device
    {
        bool isElectrical;
        protected KitchenAppliances(string name, int cost)
            : base(name, cost) { }

        protected KitchenAppliances(string name, int cost, bool isElectrical)
            : base(name, cost)
        {
            this.isElectrical = isElectrical;
        }

        public override string ToString()
        {
            string sentence;
            if (isElectrical)
                sentence = "Powered by electricity.\n";
            else
                sentence = "does not work on electricity.\n";

            return base.ToString() + sentence;
        }

        public override string Desc()
        {
            return base.Desc() + "I'm in the kitchen myself.\n";
        }
    }

    abstract class Transport : Device
    {
        public enum TypeOfTransport
        {
            RIVER,
            LAND,
            UNDERGROUND,
            AIR
        }

        TypeOfTransport type;

        protected Transport(string name, int cost)
            : base(name, cost) { }

        protected Transport(string name, int cost, TypeOfTransport type)
            : base(name, cost)
        {
            this.type = type;
        }

        public override string Desc()
        {
            return base.Desc() + "I'am transport, i like to move, but I don’t like not to move.\n";
        }

        public override string ToString()
        {
            string sentence = "unknown";
            if (type == TypeOfTransport.RIVER)
                sentence = "I like swimming. I'am a river transport. Nice to meet u\n";
            if (type == TypeOfTransport.LAND)
                sentence = "I'am what u see everyday, i'am very useful. I'am land transport\n";
            if (type == TypeOfTransport.UNDERGROUND)
                sentence = "Hey, I'm under you. I'am underground transport\n";
            if (type == TypeOfTransport.AIR)
                sentence = "I'am just flying in the sky...\n";
            return base.ToString() + sentence;
        }
    }

    class Kettle : KitchenAppliances
    {
        string color;
        public Kettle(string name, int cost, bool isElectrical)
            : base(name, cost, isElectrical)
        { }
        public Kettle(string name, int cost, bool isElectrical, string color)
            : base(name, cost, isElectrical)
        {
            this.color = color;
        }

        public override string Desc()
        {
            return base.Desc() + "Here is my pen and here is my nose. Do you realize who I am?\n";
        }

        public override void Sound()
        {
            Console.WriteLine($"seething seething... boiling boiling...\n");
        }
        public override string ToString()
        {
            return base.ToString() + $"Color: {color}\n";
        }
    }
    class Microwave : KitchenAppliances
    {
        int capacity;
        public Microwave(string name, int cost, bool isElectrical)
            : base(name, cost, isElectrical) { }
        public Microwave(string name, int cost, bool isElectrical, int capacity)
            : base(name, cost, isElectrical)
        {
            this.capacity = capacity;
        }
        public override string Desc()
        {
            return base.Desc() + "I'm a microwave oven, I like to heat things up.\n";
        }
        public override void Sound()
        {
            Console.WriteLine("warming up warming up...\n");
        }
        public override string ToString()
        {
            return base.ToString() + $"Capacity: {capacity}\n";
        }
    }

    class Automobile : Transport
    {
        int enginePower;

        public Automobile(string name, int cost, TypeOfTransport type)
            : base(name, cost, type) { }

        public Automobile(string name, int cost, TypeOfTransport type, int enginePower)
            : base(name, cost, type)
        {
            this.enginePower = enginePower;
        }
        public override string Desc()
        {
            return base.Desc() + "I'm a machine and I love to drive.\n";
        }
        public override void Sound()
        {
            Console.WriteLine("KCHAU");
        }
        public override string ToString()
        {
            return base.ToString() + $"Engine power: {enginePower}.\n";
        }
    }
    class Steamship : Transport
    {
        int maxSpeed;

        public Steamship(string name, int cost, TypeOfTransport type)
            : base(name, cost, type) { }

        public Steamship(string name, int cost, TypeOfTransport type, int maxSpeed)
            : base(name, cost, type)
        {
            this.maxSpeed = maxSpeed;
        }
        public override string Desc()
        {
            return base.Desc() + "I'm a steamship and I love to sail.\n";
        }
        public override void Sound()
        {
            Console.WriteLine("THE LOUD SOUND OF A STEAMER");
        }
        public override string ToString()
        {
            return base.ToString() + $"Max Speed: {maxSpeed}.\n";
        }
    }
    internal class Program
    {
        static void Main()
        {
            Device[] devices =
            {
                new Kettle("Bosh", 500, false, "Yellow"),
                new Microwave("Yota", 350, true, 35),
                new Automobile("Nissan", 200, Transport.TypeOfTransport.LAND, 50000),
                new Steamship("Molodoy", 666999, Transport.TypeOfTransport.RIVER, 250)
            };
            foreach (Device device in devices)
            {
                Console.WriteLine(device);
                Console.WriteLine(device.Desc());
            }
        }
    }
}
