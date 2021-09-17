using System;
using System.IO;
using Newtonsoft.Json;

namespace JsonFlatten
{
    class Program
    {
        static void Main(string[] args)
        {
            string rawJson1 = @"C:\Users\ajh\Desktop\Task Json Converter\Raw1.json";
            string rawJson2 = @"C:\Users\ajh\Desktop\Task Json Converter\Raw2.json";

            string cJsonString = rawJson2;

            try
            {
                if (File.Exists(cJsonString))
                {
                    using var sr = new StreamReader(cJsonString);
                    var rawJsonString = sr.ReadToEnd();
                    var oDict = JsonHelper.DeserializeAndFlatten(rawJsonString);
                    foreach (var kvp in oDict)
                    {
                        Console.WriteLine(kvp.Key + ": " + kvp.Value);
                    }
                    var nDict = JsonHelper.TrimArrayValuesInDictionary(oDict);

                   
                    Console.WriteLine("..................................................................................................");
                   
                    var translatorJsonString = JsonConvert.SerializeObject(nDict);
                    //Console.WriteLine(translatorJsonString);
                    foreach (var kvp in nDict)
                    {
                        Console.WriteLine(kvp.Key + ": " + kvp.Value);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }
    }
}
