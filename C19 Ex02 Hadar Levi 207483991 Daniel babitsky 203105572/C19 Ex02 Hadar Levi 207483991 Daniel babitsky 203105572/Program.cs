using System;

namespace C19_Ex02
{
    public class Program
    {
        private const string END_GAME = "Q";
        private const int NUMBER_OF_GUESSES_LOW = 4;
        private const int NUMBER_OF_GUESSES_HIGH = 10;

        private static BullseyeGame game = new BullseyeGame();
        private static ConsoleWriter writer = new ConsoleWriter();

        private static string readGuesses()
        {
            Console.WriteLine("Please type in your next guess (A-H) or 'Q' to quit:");
            var userInput = Console.ReadLine();
            bool isInputValid = false;

            if (userInput == END_GAME)
            {
                return userInput;
            }

            while (!isInputValid && userInput != END_GAME)
            {
                isInputValid = true;
                if (userInput.Length != 4)
                {
                    Console.WriteLine("The guess should contain 4 letters");
                    isInputValid = false;
                }
                else
                {
                    for (int i = 0; i < userInput.Length; i++)
                    {
                        bool characterInRange = userInput[i] >= 'A' && userInput[i] <= 'H';

                        if (characterInRange)
                        {
                            continue;
                        }

                        bool isUpperCaseLetter = userInput[i] >= 'A' && userInput[i] <= 'Z';
                        bool isLowerCaseLetter = userInput[i] >= 'a' && userInput[i] <= 'z';

                        if (isUpperCaseLetter)
                        {
                            Console.WriteLine("Character is not in range. should be between A-H!");
                            isInputValid = false;
                            break;
                        }
                        else if (isLowerCaseLetter)
                        {
                            Console.WriteLine("input must be a valid uppercase Characters. should be between A-H!");
                            isInputValid = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Input is not valid. should be between A-H!");
                            break;
                        }
                    }
                }

                if (!isInputValid)
                {
                    Console.WriteLine("Please type in your next guess (A-H) or 'Q' to quit:");
                    userInput = Console.ReadLine();
                }
            }

            return userInput;
        }

        private static int getNumberOfGuesses()
        {
            while (true)
            {
                Console.WriteLine("Please type the number of guesses (4-10):");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int numberOfGuesses))
                {
                    if (numberOfGuesses >= NUMBER_OF_GUESSES_LOW && numberOfGuesses <= NUMBER_OF_GUESSES_HIGH)
                    {
                        return numberOfGuesses;
                    }
                    else
                    {
                        Console.WriteLine("number is not in range. should be between 4-10!");
                    }
                }
                else
                {
                    Console.WriteLine("user input is not in number format!");
                }
            }
        }

        private static bool checkIfUserWantNewGame()
        {
            while (true)
            {
                Console.WriteLine("Would you like to start a new game (Y/N) or 'Q' to quit:");
                string userInput = Console.ReadLine();

                if (userInput == "Q" || userInput == "N")
                {
                    return false;
                }
                else if (userInput == "Y")
                {
                    clearScreen();
                    return true;
                }
                else
                {
                    Console.WriteLine("Input is not valid.");
                }
            }
        }

        private static void clearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public static void Main()
        {
            string userInput;
            bool playerInGame = true;

            while (playerInGame)
            {
                int numberOfGuesses = getNumberOfGuesses();

                game.Reset(numberOfGuesses);

                while (!game.PlayerHasWon && !game.GameOver)
                {
                    clearScreen();

                    writer.Write(game.NumberOfGuesses, game.Guesses);

                    userInput = readGuesses();

                    if (userInput == END_GAME)
                    {
                        playerInGame = false;
                        Console.WriteLine("Goodbye!");
                        Console.ReadKey();
                        return;
                    }

                    var result = game.GuessCharacters(userInput);
                }

                clearScreen();
                writer.Write(game.NumberOfGuesses, game.Guesses);

                if (game.PlayerHasWon)
                {
                    Console.WriteLine($"You gueesed after {game.Guesses.Count} steps!");
                }
                else if (game.GameOver)
                {
                    Console.WriteLine($"No more guesses allowed. You lost!\n Computer selection was: {string.Join("", game.SelectedCharacters)}");
                }

                if (!checkIfUserWantNewGame())
                {
                    break;
                }
            }

            Console.WriteLine("Goodbye!");
            Console.ReadKey();
        }
    }
}