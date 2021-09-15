using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace JsonFlatten
{
    public class JsonHelper
    {
        public static Dictionary<string, object> DeserializeAndFlatten(string json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            JToken token = JToken.Parse(json);
            FillDictionaryFromJToken(dict, token, "");
            return dict;
        }

        private static void FillDictionaryFromJToken(Dictionary<string, object> dict, JToken token, string prefix)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (JProperty prop in token.Children<JProperty>())
                    {
                        FillDictionaryFromJToken(dict, prop.Value, Join(prefix, prop.Name));
                    }
                    break;

                case JTokenType.Array:
                    int index = 1;
                    foreach (JToken value in token.Children())
                    {
                        string nPrefix = Join(prefix, index.ToString());
                        FillDictionaryFromJToken(dict, value, nPrefix);
                        index++;
                    }
                    break;

                default:
                    dict.Add(prefix, ((JValue)token).Value);
                    break;
            }
        }

        private static string Join(string prefix, string name)
        {
            return (string.IsNullOrEmpty(prefix) ? name : prefix + "_" + name);
        }

        public static Dictionary<string, object> TrimArrayValuesInDictionary(Dictionary<string, object> oDictioanry)
        {
            StringBuilder sbValue = new StringBuilder();
            Dictionary<string, object> nDictionary = new Dictionary<string, object>();
            for (int i = 0; i < oDictioanry.Count - 1; i++)
            {
                var currentElement = oDictioanry.ElementAt(i);
                var nextElement = oDictioanry.ElementAt(i + 1);

                var cKey = currentElement.Key;
                var cValue = currentElement.Value;
                var nKey = nextElement.Key;
                var nValue = nextElement.Value;
                // check if key has a last character is Digit
                if (char.IsDigit(cKey[^1]))
                {
                    if (char.IsDigit(cKey[^1]) && char.IsDigit(nKey[^1]))
                    {
                        sbValue.Append(cValue + ",");
                        if (oDictioanry.ContainsKey(cKey))
                        {
                            oDictioanry.Remove(cKey);
                            i--;
                        }
                    }
                    else
                    {
                        sbValue.Append(cValue);
                        var mKey = cKey.Substring(0, cKey.Length - 2);
                        nDictionary.TryAdd(mKey, sbValue.ToString());
                        sbValue.Clear();
                    }
                }
                else
                {
                    nDictionary.TryAdd(cKey, cValue);
                }

            }
            return nDictionary;
        }
    }
}
