using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    public class NameGenerator
    {
        private List<string> prefixes = new List<string>();
        private List<string> suffixes = new List<string>();
        private bool prefixMode = false;
        private bool suffixMode = false;
        private Random rnd = new Random();

        private const string CODE_INDICATOR = ":";

        public NameGenerator()
        {
            string line;

            // Read the file and display it line by line.  
            var path = @"c:\users\deltagare\documents\visual studio 2015\Projects\Calculator\ConsoleApplication1\names.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith(CODE_INDICATOR))
                {
                    SetNameMode(line);
                }
                else
                {
                    SaveToList(line);
                }
            }
            file.Close();
        }

        public string GetRandomName()
        {
            int prefixIndex = rnd.Next(0, prefixes.Count);
            int suffixIndex = rnd.Next(0, suffixes.Count);

            return prefixes[prefixIndex] + suffixes[suffixIndex];
        }

        private void SaveToList(string line)
        {
            if (prefixMode)
            {
                prefixes.Add(line);
            }
            else if (suffixMode)
            {
                suffixes.Add(line);
            }
        }
        private void SetNameMode(string line)
        {
            if (line.Equals(":pre"))
            {
                prefixMode = true;
                suffixMode = false;
            }
            else if (line.Equals(":suf"))
            {
                suffixMode = true;
                prefixMode = false;
            }
        }
    }
}
