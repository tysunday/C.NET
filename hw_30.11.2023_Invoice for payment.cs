using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;

namespace hw_30._11._2023_Invoice_for_payment
{
    [Serializable]
    class InvoiceForPayment
    {
        public InvoiceForPayment(int countOfDays, int numberOfDaysOfPaymentDelay)
        {
            _countOfDays = countOfDays;
            _numberOfDaysOfPaymentDelay = numberOfDaysOfPaymentDelay;
        }

        const int _paymentPerDay = 5;
        public int PaymentPerDay
        {
            get { return _paymentPerDay; }
        }

        int _countOfDays;
        public int CountOfDays
        {
            get { return _countOfDays; }
            set { _countOfDays = value; }
        }

        const int _penaltyForOneDayOfLatePayment = 2;
        public int PenaltyForOneDayOfLatePayment
        {
            get { return _penaltyForOneDayOfLatePayment; }
        }

        int _numberOfDaysOfPaymentDelay;
        public int NumberOfPaymentDelay
        {
            get { return _numberOfDaysOfPaymentDelay; }
            set { _numberOfDaysOfPaymentDelay = value; }
        }

        [NonSerialized]
        public int _amountToBePaidWithoutPenalty;
        [NonSerialized]
        int _penalty;
        [NonSerialized]
        int _totalAmountDue;

        int AmountToBePaidWithoutPenalty()
        {
            _amountToBePaidWithoutPenalty = _paymentPerDay * _countOfDays;
            return _amountToBePaidWithoutPenalty;
        }

        int Penalty()
        {
            _penalty = _penaltyForOneDayOfLatePayment * _numberOfDaysOfPaymentDelay;
            return _penalty;
        }

        public int TotalAmountDue()
        {
            _totalAmountDue = AmountToBePaidWithoutPenalty() + Penalty();
            return _totalAmountDue;
        }
    }

    internal class Program
    {
        static void SerializeInvoice(InvoiceForPayment invoice)
        {
            try
            {
                using (FileStream fs = new FileStream("invoice.dat", FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, invoice);
                }
                Console.WriteLine("Invoice serialized seccessfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during serialization: {ex.Message}");
            }
        }
        static InvoiceForPayment DeserializeInvoice(string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (InvoiceForPayment)formatter.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deserialization: {ex.Message}");
                return null;
            }
        }
        static void DatFormat(InvoiceForPayment invoice)
        {
            SerializeInvoice(invoice);

            InvoiceForPayment deserializedInvoice = DeserializeInvoice("invoice.dat");
            if (deserializedInvoice != null)
            {
                Console.WriteLine($"Payment per day:  {deserializedInvoice.PaymentPerDay}$");
                Console.WriteLine($"Count of days: {deserializedInvoice.CountOfDays}");
                Console.WriteLine($"Penalty for one day of late payment: {deserializedInvoice.PenaltyForOneDayOfLatePayment}$");
                Console.WriteLine($"Number of days of payment delay: {deserializedInvoice.NumberOfPaymentDelay}");
                Console.WriteLine($"Deserialized Total amount due: {deserializedInvoice.TotalAmountDue()}$");
            }
        }
        static void CreateXml(InvoiceForPayment invoice)
        {
            XmlTextWriter writer = null;
            try
            {
                writer = new XmlTextWriter("InvoiceForPayment.xml", System.Text.Encoding.Unicode);
                writer.Indentation = 1;
                writer.IndentChar = '\t';
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("InvoicesForPayment");
                writer.WriteStartElement("InvoiceForPayment");
                writer.WriteElementString("PaymentPerDay", invoice.PaymentPerDay.ToString());
                writer.WriteElementString("CountOfDays", invoice.CountOfDays.ToString());
                writer.WriteElementString("PenaltyForOneDayOfLatePayment", invoice.PenaltyForOneDayOfLatePayment.ToString());
                writer.WriteElementString("NumberOfPaymentDelay", invoice.NumberOfPaymentDelay.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                Console.WriteLine("InvoiceForPayment.xml created");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        static void PrintXmlInfo(string fileName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);

                Console.WriteLine(fileName);

                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    OutputNode(node, 1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading XML file: {ex.Message}");
            }
        }
        static void OutputNode(XmlNode node, int indentationLevel)
        {
            string indentation = new string('\t', indentationLevel);

            Console.WriteLine($"{indentation}{node.Name}");

            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType == XmlNodeType.Element)
                {
                    Console.WriteLine($"{indentation}\t{childNode.Name} - {childNode.InnerText}");
                }
            }
        }
        
        static void Main(string[] args)
        {
            InvoiceForPayment invoice = new InvoiceForPayment(9, 7);
            Console.WriteLine("******XML******");
            CreateXml(invoice);
            PrintXmlInfo("InvoiceForPayment.xml");


            Console.WriteLine("******DAT******");
            DatFormat(invoice);
        }
    }
}


