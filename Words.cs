using System;
using System.IO;
using System.Collections.Generic;

namespace Scramble
{
    class Words
    {
        private string[] wordsArray;
        private int difficulty;
        private int tries;
        private string currentWord;
        private Random rand = new Random();

        public Words()
        {
            this.wordsArray = File.ReadAllLines("words.txt");
            this.difficulty = 3;
            this.tries = 5;
        }

        public Words(string sentence)
        {
            this.wordsArray = sentence.Split(' ');
            this.difficulty = 3;
            this.tries = 5;
        }

        public string GetWord()
        {
            return currentWord;
        }

        public string GetDifficulty()
        {
            if (difficulty == 2)
            {
                return "hard";
            }
            else if (difficulty == 1)
            {
                return "easy";
            }
            else
            {
                return "unknown";
            }
        }

        public void SetDifficulty(string newDiff)
        {
            if (newDiff.Equals("hard", StringComparison.OrdinalIgnoreCase))
            {
                this.difficulty = 2;
            }
            else if (newDiff.Equals("i dont care", StringComparison.OrdinalIgnoreCase))
            {
                this.difficulty = rand.Next(2) == 0 ? 1 : 2;
            }
            else if (newDiff.Equals("easy", StringComparison.OrdinalIgnoreCase))
            {
                this.difficulty = 1;
            }
            else
            {
                throw new Exception("Invalid difficulty, please enter 'easy', 'i dont care', or 'hard'");
            }
            SetTries();
        }

        public int GetTries()
        {
            return tries;
        }

        public void SetTries(int tries)
        {
            this.tries = tries;
        }

        private void SetTries()
        {
            if (difficulty == 1)
            {
                this.tries = 10;
            }
            else
            {
                this.tries = 7;
            }
        }

        public int GetWordLength()
        {
            return currentWord.Length;
        }

        public void ShuffleWord()
        {
            if (currentWord == null) return;

            char[] array = currentWord.ToCharArray();
            int n = array.Length;

            while (n > 1)
            {
                int k = rand.Next(n--);
                char temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }

            currentWord = new string(array);
        }
	}
}

