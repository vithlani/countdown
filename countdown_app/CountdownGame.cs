using NetSpell.SpellChecker.Dictionary;
using NetSpell.SpellChecker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace countdown_app
{
    public class CountdownGame
    {
        private static readonly char[] Vowels = { 'A', 'E', 'I', 'O', 'U' };
        private static readonly char[] Consonants = "BCDFGHJKLMNPQRSTVWXYZ".ToCharArray();
        private static readonly Random Random = new Random();
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

        public string GenerateLetters(int vowelCount, int consonantCount)
        {
            var letters = new List<char>();
            for (int i = 0; i < vowelCount; i++)
                letters.Add(GetRandomVowel());
            for (int i = 0; i < consonantCount; i++)
                letters.Add(GetRandomConsonant());
            return new string(letters.OrderBy(_ => Random.Next()).ToArray());
        }

        public char GetRandomVowel() => Vowels[Random.Next(Vowels.Length)];
        public char GetRandomConsonant() => Consonants[Random.Next(Consonants.Length)];

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