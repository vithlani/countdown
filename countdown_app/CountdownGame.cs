using NetSpell.SpellChecker.Dictionary;
using NetSpell.SpellChecker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace countdown_app
{
    public class CountdownGame
    {
        private SimpleDictionary Dictionary;

        public CountdownGame()
        {
            string dictionaryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dic.txt");

            if (!File.Exists(dictionaryPath))
            {
                throw new FileNotFoundException($"Dictionary file not found at {dictionaryPath}");
            }

            try
            {
                Dictionary = new SimpleDictionary(dictionaryPath);
                Console.WriteLine("Dictionary loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dictionary: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        public string FindLongestWord(string letters)
        {
            var longestWord = string.Empty;
            var letterSet = new HashSet<char>(letters.ToUpper());

            // Generate all possible combinations of letters
            for (int len = letters.Length; len > 0; len--)
            {
                var combinations = GetUniqueCombinations(letters.ToUpper(), len);
                foreach (var combo in combinations)
                {
                    if (Dictionary.Contains(combo) && combo.Length > longestWord.Length)
                    {
                        longestWord = combo;
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(longestWord))
                    break;
            }

            return longestWord;
        }
        private IEnumerable<string> GetUniqueCombinations(string letters, int length)
        {
            if (letters.Length < length) return new string[0];
            if (length == 0) return new[] { string.Empty };
            if (length == 1) return letters.Select(c => c.ToString()).Distinct();

            var combinations = new HashSet<string>();
            for (int i = 0; i < letters.Length; i++)
            {
                char currentChar = letters[i];
                var remainingCombinations = GetUniqueCombinations(letters.Substring(0, i) + letters.Substring(i + 1), length - 1);
                foreach (var combination in remainingCombinations)
                {
                    combinations.Add(currentChar + combination);
                }
            }
            return combinations;
        }

        public int CalculateScore(string word) => word.Length;
    }
} 