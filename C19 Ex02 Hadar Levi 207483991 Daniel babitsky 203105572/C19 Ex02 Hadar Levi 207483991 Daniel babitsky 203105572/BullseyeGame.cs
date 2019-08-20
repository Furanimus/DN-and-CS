using System;
using System.Collections.Generic;
using System.Linq;

namespace C19_Ex02
{
    public class BullseyeGame
    {
        private const string ALLOWED_CHARACTERS = "ABCDEFGH";
        private const int CHARACTERS_TO_SELECT = 4;

        private Random random = new Random();

        private int numberOfGuesses;

        public bool PlayerHasWon = false;

        public bool GameOver
        {
            get
            {
                return numberOfGuesses == guesses.Count;
            }
        }

        public List<GuessResult> Guesses 
        { 
            get 
            {
                return guesses;
            }
        }

        public int NumberOfGuesses
        {
            get
            {
                return numberOfGuesses;
            }
        }

        private List<GuessResult> guesses = new List<GuessResult>();
        public List<char> SelectedCharacters = new List<char>();

        public BullseyeGame()
        {
        }

        public BullseyeGame(int i_numberOfGuesses)
        {
            this.numberOfGuesses = i_numberOfGuesses;

            SelectNewCharacters();
        }

        public void Reset(int i_numberOfGuesses)
        {
            this.numberOfGuesses = i_numberOfGuesses;

            SelectedCharacters = new List<char>();
            guesses = new List<GuessResult>();
            PlayerHasWon = false;

            SelectNewCharacters();
        }

        private void SelectNewCharacters()
        {
            List<char> letters = new List<char>();
            letters.AddRange(ALLOWED_CHARACTERS);

            SelectedCharacters = new List<char>();

            for (int i = 0; i < CHARACTERS_TO_SELECT; i++)
            {
                int indexToAdd = random.Next(letters.Count);

                SelectedCharacters.Add(letters[indexToAdd]);
                letters.RemoveAt(indexToAdd);
            }
        }

        private int exactMatch(string i_letters)
        {
            int matchCount = 0;

            for (int i = 0; i < i_letters.Length; i++)
            {
                if (SelectedCharacters[i] == i_letters[i])
                {
                    matchCount++;
                }
            }

            return matchCount;
        }

        private int notExactMatch(string i_letters)
        {
            int matchCount = 0;

            for (int i = 0; i < i_letters.Length; i++)
            {
                char currentChar = i_letters[i];

                // Exact match we don't want to count it
                if (SelectedCharacters[i] == currentChar)
                {
                    continue;
                }

                if (SelectedCharacters.IndexOf(currentChar) > -1)
                {
                    matchCount++;
                }
            }

            return matchCount;
        }

        public GuessResult GuessCharacters(string i_guessCharacters)
        {
            var result = new GuessResult()
            {
                m_Guess = i_guessCharacters
            };           

            result.m_ExactMatch = exactMatch(result.m_Guess);
            result.m_NotExactMatch = notExactMatch(result.m_Guess);

            guesses.Add(result);

            if (result.m_ExactMatch == SelectedCharacters.Count)
            {
                PlayerHasWon = true;
            }

            return result;
        }
    }
}
