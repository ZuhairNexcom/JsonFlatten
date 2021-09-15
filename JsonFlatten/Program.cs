using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonFlatten
{
    class Program
    {
        static void Main(string[] args)
        {
            string rawJson1 = @"C:\Users\ajh\Desktop\Task Json Converter\Raw.json";

            try
            {

                if (File.Exists(rawJson1))
                {
                    using var sr = new StreamReader(rawJson1);
                    var jsonString = sr.ReadToEnd();
                    var oDict = JsonHelper.DeserializeAndFlatten(jsonString);
                    var nDict = JsonHelper.TrimArrayValuesInDictionary(oDict);
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
