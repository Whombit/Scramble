using System;
using System.IO;
using System.Linq;

namespace Scramble
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 30);
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Pick a difficulty:" + "\n-Hard" + "\n-Easy" + "\n-I don't care");

            Words word = new Words();

            string[] wordsArray = File.ReadAllLines("words.txt");

            bool retry = true;

            do
            {
                try
                {
                    string newDiff = Console.ReadLine();
                    word.setDiff(newDiff);

                    Console.WriteLine("\nDifficulty chosen: " + word.getDiff());

                    Console.WriteLine("Tries left: " + word.getTries());

                    retry = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("That's not a difficulty. Please try again.");
                    retry = true;
                }
            } while (retry);

            if (word.getDiff().Equals("hard", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    HardWords hw = new HardWords(wordsArray);

                    hw.setWord();

                    Console.WriteLine("Chosen word length: " + hw.getWordLength());

                    word.setWord(hw.getWord());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reading file: " + e.Message);
                }
            }
            else
            {
                try
                {
                    EasyWords ew = new EasyWords(wordsArray);

                    ew.setWord();

                    Console.WriteLine("Chosen word length: " + ew.getWordLength());

                    word.setWord(ew.getWord());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reading file: " + e.Message);
                }
            }

            word.shuffleWord();

            Console.WriteLine("Shuffled word: " + word.getGeneratedString());

            bool correct = false;

            while (!correct && word.getTries() > 0)
            {
                Console.WriteLine("Guess the word: ");

                string guess = Console.ReadLine();

                if (guess.Equals(word.getWord(), StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Congrats! You guessed it right!");
                    correct = true;

                    Console.WriteLine("Try another? - Y/N");

                    if (Console.ReadLine().Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        retry = true;
                    }
                }
                else
                {
                    word.setTries(word.getTries() - 1);
                    Console.WriteLine("Incorrect. Tries left: " + word.getTries());
                }
            }

            if (!correct)
            {
                Console.WriteLine("Out of tries. Try again? - Y/N");

                if (Console.ReadLine().Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    retry = true;
                }
            }
        }
	}
}
