using System;

namespace RockPaperScissors
{
    class Game
    {
        static void Main( string[] args )
        {
            Console.WriteLine("Rock Paper Scissors Game\n");
            Console.WriteLine("Play best of 1, 3, 5, or 7 rounds?");
            int roundChoice = Convert.ToInt32(Console.ReadLine() ?? "");
            int roundsToWin;

            if (roundChoice == 1)
            {
                roundsToWin = 1;
            }
            else if (roundChoice == 3)
            {
                roundsToWin = 2;
            }
            else if (roundChoice == 5)
            {
                roundsToWin = 3;
            }
            else if (roundChoice == 7)
            {
                roundsToWin = 4;
            }
            else
            {
                Console.WriteLine("Invalid input.");
                return;
            }
            
            // Initialize Variables
            int playerScore = 0;
            int cpuScore = 0;
            string playerChoice;
            int cpuChoice;

            Console.Clear();

            while (playerScore != roundsToWin && cpuScore != roundsToWin)
            {
                Console.WriteLine("Rock Paper Scissors Game");
                Console.WriteLine("------------------------------");
                Console.WriteLine($"Player Score: {playerScore} | CPU Score: {cpuScore}\n");

                // Get Player Choice
                Console.WriteLine("Choose Rock (r), Paper (p), or Scissors (s): ");
                playerChoice = Console.ReadLine() ?? "";

                // Validate User Input
                if (!(playerChoice == "r" || playerChoice == "p" || playerChoice == "s"))
                {
                    Console.WriteLine("Invalid input.\n");
                    return;
                }

                // Generate CPU Choice
                Random randomSelection = new Random();
                cpuChoice = randomSelection.Next(0, 3);

                // Game Logic
                switch (cpuChoice)
                {
                    case 0:
                        Console.WriteLine("\nCPU chose Rock.");
                        if (playerChoice == "r")
                        {
                            Console.WriteLine("It's a tie!\n");
                        }
                        else if (playerChoice == "p")
                        {
                            Console.WriteLine("You win!\n");
                            playerScore++;
                        }
                        else if (playerChoice == "s")
                        {
                            Console.WriteLine("You lose!\n");
                            cpuScore++;
                        }
                        break;
                    case 1:
                        Console.WriteLine("\nCPU chose Paper.");
                        if (playerChoice == "r")
                        {
                            Console.WriteLine("You lose!\n");
                            cpuScore++;
                        }
                        else if (playerChoice == "p")
                        {
                            Console.WriteLine("It's a tie!\n");
                        }
                        else if (playerChoice == "s")
                        {
                            Console.WriteLine("You win!\n");
                            playerScore++;
                        }
                        break;
                    case 2:
                        Console.WriteLine("\nCPU chose Scissors.");
                        if (playerChoice == "r")
                        {
                            Console.WriteLine("You win!\n");
                            playerScore++;
                        }
                        else if (playerChoice == "p")
                        {
                            Console.WriteLine("You lose!\n");
                            cpuScore++;
                        }
                        else if (playerChoice == "s")
                        {
                            Console.WriteLine("It's a tie!\n");
                        }
                        break;
                }
            }
        }
    }
}