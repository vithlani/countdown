using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace countdown_app
{
    public class SimpleDictionary
    {
        private HashSet<string> words;

        public SimpleDictionary(string filePath)
        {
            words = new HashSet<string>(File.ReadAllLines(filePath), StringComparer.OrdinalIgnoreCase);
        }

        public bool Contains(string word)
        {
            return words.Contains(word);
        }
    }
}
