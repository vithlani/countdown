using System;

namespace countdown_app
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Current Directory: {Environment.CurrentDirectory}");
                Console.WriteLine($"Base Directory: {AppDomain.CurrentDomain.BaseDirectory}");
                var game = new CountdownGame();
                var totalScore = 0;
                const int rounds = 4;

                for (int round = 1; round <= rounds; round++)
                {
                    Console.WriteLine($"Round {round}");
                    string stringInput = GetValidStringInput("Enter a string containing vowels and constants (1-9): ", 1, 9);

                    Console.WriteLine($"Given string: {stringInput}");

                    var longestWord = game.FindLongestWord(stringInput);
                    if (string.IsNullOrEmpty(longestWord))
                    {
                        Console.WriteLine("No valid word found in this round.");
                        continue;
                    }

                    var score = game.CalculateScore(longestWord);
                    totalScore += score;

                    Console.WriteLine($"Longest possible word: {longestWord}");
                    Console.WriteLine($"Score for this round: {score}");
                    Console.WriteLine($"Total score so far: {totalScore}");
                    Console.WriteLine();
                }

                Console.WriteLine($"Game over! Your total score is: {totalScore}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: Dictionary file not found. Make sure 'dic.txt' is in the output directory.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static string GetValidStringInput(string prompt, int min, int max)
        {
            string value;
            while (true)
            {
                Console.Write(prompt);
                value = Console.ReadLine();
                if (value.Length >= min && value.Length <= max)
                {
                    return value;
                }
                Console.WriteLine($"Invalid input. Please enter a string with {min} length and {max} length");
            }
        }
    }
}