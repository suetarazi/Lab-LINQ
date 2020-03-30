using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Lab_LINQ.Classes;
using System.Linq;
using System.Collections.Generic;

namespace Lab_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            try
            {
                String nyData = @"/Users/Sue Machtley/source/repos/Lab-LINQ/Lab-LINQ/data.json";

                //Console.WriteLine(nyData);

                JObject JsonObject = CreateJObject(nyData);
                RootObject rootObject = new RootObject();
                rootObject = JsonObject.ToObject<RootObject>();
                
                Query(rootObject);
                   
            }
        
            catch (Exception e)
            {                                                                                  
                Console.WriteLine("There was an error reading your file");
                Console.WriteLine(e.Message);
            }

            Console.Read();
        }

        public static JObject CreateJObject(string nyData)
        {
            using (StreamReader sr = File.OpenText(nyData))
            {
                JObject jList = (JObject)JToken.ReadFrom(new JsonTextReader(sr));
                return jList;
            }                                                                        
        }
        
        /// <summary>
        /// query 1
        /// </summary>
        /// <param name="rootObject"></param>
        static void Query(RootObject rootObject)
        {
            var allNeighborhoods = rootObject.features.Select(hood => hood.properties.neighborhood);
            Console.WriteLine("Show all neighborhoods ");

            int count = 0;
            foreach (string neighborhood in allNeighborhoods)
            {
                count++;
                Console.WriteLine($"{count}: { neighborhood}, \n");

            }   

            var removeBlankHoods = allNeighborhoods.Where(x => x != "");
            count = 0;
            foreach(string neighbhorhood in removeBlankHoods)
            {
                count++;
                Console.WriteLine($"{count}: { neighbhorhood}, \n");
            }


            var filteredHoods = removeBlankHoods.Distinct();
            count = 0;

            
            foreach (string neighborhood in filteredHoods)
            {
                if (filteredHoods.Equals(""))
                {
                    Console.WriteLine("no hood");

                }
                else
                {
                    count++;
                    Console.WriteLine($"{count}: { neighborhood}, \n");
                }
            }

            var oneQuery = allNeighborhoods.Where(x => x != "").Distinct();
            count = 0;
            foreach(string neighborhood in oneQuery)
            {
                count++;
                Console.WriteLine($"{count}: { neighborhood}, \n");
            }


            var otherMethod =
                (from item in rootObject.features 
                where item.properties.neighborhood != ("")
                select item.properties.neighborhood).Distinct();

            count = 0;
            foreach (string Neighborhood in otherMethod)
            {
                count++;
                Console.WriteLine($"{count}: { Neighborhood}");
            }

        }
    
    }
}
