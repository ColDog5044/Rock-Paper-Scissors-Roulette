using System;
using System.IO;
using System.Text.Json;

namespace ColDogStudios.RockPaperScissors.src
{
    class Game
    {
        static void Main(string[] args)
        {
            bool developerMode = args.Length > 0 && args[0] == "--dev";
            Player player = Player.LoadPlayerStats();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("ColDog's Rock Paper Scissors Game\n");
                Console.WriteLine($"You have {player.Points} points.");
                Console.WriteLine("Play best of 1, 3, 5, or 7 rounds?");
                Console.WriteLine("Enter 0 to exit.");
                int roundChoice = Convert.ToInt32(Console.ReadLine() ?? "");

                if (roundChoice == 0)
                {
                    break;
                }

                int roundsToWin;

                switch (roundChoice)
                {
                    case 1:
                        roundsToWin = 1;
                        break;
                    case 3:
                        roundsToWin = 2;
                        break;
                    case 5:
                        roundsToWin = 3;
                        break;
                    case 7:
                        roundsToWin = 4;
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        continue;
                }

                Console.WriteLine("Enter the amount of points to wager (0 is valid): ");
                int wager = Convert.ToInt32(Console.ReadLine() ?? "");

                if (wager > player.Points)
                {
                    Console.WriteLine("You cannot wager more points than you have.");
                    continue;
                }

                player.Wager = wager;
                player.Points -= wager; // Deduct wagered points

                PlayGame(player, roundsToWin, roundChoice, developerMode);
                player.SavePlayerStats();
            }
        }

        static void PlayGame(Player player, int roundsToWin, int roundChoice, bool developerMode)
        {
            int playerScore = 0;
            int cpuScore = 0;
            string playerChoice;
            int cpuChoice;

            Console.Clear();

            while (playerScore != roundsToWin && cpuScore != roundsToWin)
            {
                Console.WriteLine("ColDog's Rock Paper Scissors Game");
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"| Player Score: {playerScore} | CPU Score: {cpuScore} |");
                Console.WriteLine($"| Player Points: {player.Points}           |");
                Console.WriteLine("----------------------------------\n");

                // Generate CPU Choice
                Random randomSelection = new();
                cpuChoice = randomSelection.Next(0, 3);

                if (developerMode)
                {
                    Console.WriteLine($"[Developer Mode] CPU Choice: {(cpuChoice == 0 ? "Rock" : cpuChoice == 1 ? "Paper" : "Scissors")}");
                }

                // Get Player Choice
                Console.WriteLine("Choose Rock (r), Paper (p), or Scissors (s): ");
                playerChoice = Console.ReadLine() ?? "";

                // Validate User Input
                if (!(playerChoice == "r" || playerChoice == "p" || playerChoice == "s"))
                {
                    Console.WriteLine("Invalid input.\n");
                    return;
                }

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

                if (playerScore == roundsToWin)
                {
                    Console.WriteLine("You win the game!");
                    CalculateWinnings(player, roundChoice, true, playerScore, cpuScore);
                    Console.ReadLine();
                }
                else if (cpuScore == roundsToWin)
                {
                    Console.WriteLine("CPU wins the game!");
                    CalculateWinnings(player, roundChoice, false, playerScore, cpuScore);
                    Console.ReadLine();
                }
            }
        }

        static void CalculateWinnings(Player player, int roundsToWin, bool playerWon, int playerScore, int cpuScore)
        {
            if (playerWon)
            {
                player.Points += player.Wager * 2;
                if (roundsToWin > 1 && playerScore == roundsToWin && cpuScore == 0)
                {
                    switch (roundsToWin)
                    {
                        case 2:
                            player.Points += player.Wager * 3;
                            break;
                        case 3:
                            player.Points += player.Wager * 5;
                            break;
                        case 4:
                            player.Points += player.Wager * 10;
                            break;
                    }
                }
            }

            if (player.Points <= 0)
            {
                player.Points = 0;
            }
        }
    }

    class Player
    {
        public int Points { get; set; } = 100; // Initial points
        public int Wager { get; set; }

        private static string GetFilePath()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folderPath = Path.Combine(localAppData, "ColDog Studios", "Rock Paper Scissors");
            Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, "playerStats.json");
        }

        public void SavePlayerStats()
        {
            string filePath = GetFilePath();
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(filePath, json);
        }

        public static Player LoadPlayerStats()
        {
            string filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Player>(json) ?? new Player();
            }
            return new Player();
        }
    }
}
