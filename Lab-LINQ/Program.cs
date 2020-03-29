using System;
using System.IO;
using Newtonsoft;


namespace Lab_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            try
            {
                String nyData = File.ReadAllText("C:/Users/Sue Machtley/source/repos/Lab-LINQ/Lab-LINQ/data.json");
                Console.WriteLine(nyData);

            }
        
            catch (Exception e)
            {
                Console.WriteLine("There was an error reading your file");
                Console.WriteLine(e.Message);
            }

            Console.Read();
        }
    }
}
