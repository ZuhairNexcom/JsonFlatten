﻿using System;
using System.IO;
using Newtonsoft.Json;

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
                    var rawJsonString = sr.ReadToEnd();
                    var oDict = JsonHelper.DeserializeAndFlatten(rawJsonString);
                    var nDict = JsonHelper.TrimArrayValuesInDictionary(oDict);
                    var translatorJsonString = JsonConvert.SerializeObject(nDict);
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
