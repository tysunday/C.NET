using System;
using System.Collections.Generic;
using ComputersDll;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace hw_12._12._2023_computers
{
    [Serializable]
    class Serializator
    {
        public List<Computer> computers = new List<Computer>
        {
            new Computer("max", 00001111, "maxim"),
            new Computer("min", 11110000, "minimum"),
            new Computer("beatiful", 22223333, "handsome")
        };


        public void Serialize(List<Computer> computers)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("ComputerList.dat", FileMode.Create))
            {
                binaryFormatter.Serialize(fs, computers);
                fs.Close();
                Console.WriteLine("The object is serialized");
            }
        }
        public List<Computer> Deserialize()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("ComputerList.dat", FileMode.Open))
            {
                List<Computer> computers = (List<Computer>)binaryFormatter.Deserialize(fs);
                Console.WriteLine("The object is deserialized");
                return computers;
            }
        }


    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Serializator serializator = new Serializator();
            serializator.Serialize(serializator.computers);
            List<Computer> computers;
            computers = serializator.Deserialize();
            foreach(Computer computer in computers)
            {
                Console.WriteLine(computer);
                computer.Power();
                computer.Restart();
            }
        }
    }
}
