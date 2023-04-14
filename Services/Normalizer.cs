using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Services
{
    public static class Normalizer
    {
        public static string NormalizeArray(Dictionary<string, object> data)
        {
            var flattened = FlattenArray(data);

            var sorted = flattened.OrderBy(x => x.Key);

            return ArrayToValueString(sorted);
        }

        private static Dictionary<string, object> FlattenArray(Dictionary<string, object> array)
        {
            var result = new Dictionary<string, object>();

            foreach (var pair in array)
            {
                if (pair.Value is Dictionary<string, object> nestedDict)
                {
                    var flattened = FlattenArray(nestedDict);

                    var combined = flattened.ToDictionary(
                        x => $"{pair.Key}.{x.Key}",
                        x => x.Value
                    );

                    result = result.Concat(combined).ToDictionary(x => x.Key, x => x.Value);
                }
                else
                {
                    result[pair.Key] = pair.Value;
                }
            }

            return result;
        }

        private static string ArrayToValueString(IEnumerable<KeyValuePair<string, object>> data)
        {
            var textValues = new List<string>();

            foreach (var pair in data)
            {
                var value = pair.Value;

                string textValue;

                if (value is bool boolValue)
                {
                    textValue = boolValue ? "true" : "false";
                }
                else if (value is null || value.ToString() == "")
                {
                    textValue = "#";
                }
                else
                {
                    textValue = value.ToString().Replace("#", "##");
                }

                textValues.Add(textValue);
            }

            return string.Join("#", textValues);
        }
    }
}
