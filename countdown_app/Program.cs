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

                    int vowelCount = GetValidInput("Enter number of vowels (1-5): ", 1, 5);
                    int consonantCount = GetValidInput("Enter number of consonants (1-5): ", 1, 5);

                    var letters = game.GenerateLetters(vowelCount, consonantCount);
                    Console.WriteLine($"Generated letters: {letters}");

                    var longestWord = game.FindLongestWord(letters);
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

        static int GetValidInput(string prompt, int min, int max)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
            }
        }
    }
}