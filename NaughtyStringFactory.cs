using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace big_list_of_naughty_strings
{
    public class NaughtyStringFactory
    {
        public static IReadOnlyList<string> NaughtyStrings { get; }

        static NaughtyStringFactory()
        {
            var naughtyStrings = GetNaughtyStringsFromEmbeddedResource();
            NaughtyStrings = new ReadOnlyCollection<string>(naughtyStrings);
        }

        private static List<string> GetNaughtyStringsFromEmbeddedResource()
        {
            var stringsFromJson = GetNaughtyStringsFromJson();
            var stringsFromTxt = GetNaughtyStringsFromTxt();
            var allStrings = stringsFromJson
                .Concat(stringsFromTxt)
                .Distinct()
                .ToList();
            return allStrings;
        }

        private static List<string> GetNaughtyStringsFromJson()
        {
            var jsonString = GetJsonStringFromEmbeddedResource();
            var deserializedList = JsonConvert.DeserializeObject<List<string>>(jsonString);
            return deserializedList;
        }

        private static string GetJsonStringFromEmbeddedResource()
        {
            using (var resourceStream = GetJsonStreamFromEmbeddedResource())
            {
                using (var streamReader = new StreamReader(resourceStream))
                {
                    var fileString = streamReader.ReadToEnd();
                    return fileString;
                }
            }
        }

        private static Stream GetJsonStreamFromEmbeddedResource()
        {
            var assembly = typeof(NaughtyStringFactory).GetTypeInfo().Assembly;
            var resourceName = $"{nameof(big_list_of_naughty_strings)}.blns.json"
                .Replace("_", "-");
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            return resourceStream;
        }

        private static List<string> GetNaughtyStringsFromTxt()
        {
            var assembly = typeof(NaughtyStringFactory).GetTypeInfo().Assembly;
            var resourceName = $"{nameof(big_list_of_naughty_strings)}.blns.txt"
                .Replace("_", "-");
            var strings = new List<string>();
            using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var streamReader = new StreamReader(resourceStream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var currentString = streamReader.ReadLine();
                        strings.Add(currentString);
                    }

                }
            }
            return strings;
        }
    }
}
