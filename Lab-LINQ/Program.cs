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
        /// <summary>
        /// Main to connect JSON file and connect it to RootObject, then call our Query method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            try
            {
                String nyData = @"/Users/Sue Machtley/source/repos/Lab-LINQ/Lab-LINQ/data.json";

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

        /// <summary>
        /// Using streamreader to get JSON file of nyData into C# 
        /// </summary>
        /// <param name="nyData"></param>
        /// <returns>list of data from JSON file</returns>
        public static JObject CreateJObject(string nyData)
        {
            using (StreamReader sr = File.OpenText(nyData))
            {
                JObject jList = (JObject)JToken.ReadFrom(new JsonTextReader(sr));
                return jList;
            }                                                                        
        }
        
        /// <summary>
        /// Query method for all 5 queries
        /// </summary>
        /// <param name="rootObject">all neighbhorhoods object</param>
        static void Query(RootObject rootObject)
        {
            //Query 1: show all neighbhorhoods
            var allNeighborhoods = rootObject.features.Select(hood => hood.properties.neighborhood);
            Console.WriteLine("Show all neighborhoods ");

            int count = 0;
            foreach (string neighborhood in allNeighborhoods)
            {
                count++;
                Console.WriteLine($"{count}: { neighborhood}, \n");

            }   

            //Query 2: remove all blanks
            var removeBlankHoods = allNeighborhoods.Where(x => x != "");
            count = 0;
            foreach(string neighbhorhood in removeBlankHoods)
            {
                count++;
                Console.WriteLine($"{count}: { neighbhorhood}, \n");
            }

            //Query 3: filter out duplicates
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

            //Query 4: combine into one query
            var oneQuery = allNeighborhoods.Where(x => x != "").Distinct();
            count = 0;
            foreach(string neighborhood in oneQuery)
            {
                count++;
                Console.WriteLine($"{count}: { neighborhood}, \n");
            }


            //Query 5: use a different syntax for LINQ
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
