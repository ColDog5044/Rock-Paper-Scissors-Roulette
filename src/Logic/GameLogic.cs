namespace ColDogStudios.RockPaperScissors.src.Logic
{
    class GameLogic
    {
        public static void PlayGameMenu(Player player, bool developerMode)
        {
            Console.Clear();
            Console.WriteLine($"You have {player.Points} points.");
            Console.Write("Play best of 1, 3, 5, or 7 rounds?: ");
            string? roundChoice = Console.ReadLine();

            if (string.IsNullOrEmpty(roundChoice))
            {
                Console.WriteLine("Invalid input.");
                Console.ReadLine();
                return;
            }

            int cleansedRoundChoice = Convert.ToInt32(roundChoice);

            int roundsToWin;

            switch (cleansedRoundChoice)
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
                    return;
            }

            Console.WriteLine("Enter the amount of points to wager (0 is valid): ");
            int wager = Convert.ToInt32(Console.ReadLine() ?? "");

            if (wager > player.Points)
            {
                Console.WriteLine("You cannot wager more points than you have.");
                return;
            }

            player.Wager = wager;
            player.Points -= wager; // Deduct wagered points

            PlayGame(player, roundsToWin, cleansedRoundChoice, developerMode);
        }

        public static void PlayGame(Player player, int roundsToWin, int roundChoice, bool developerMode)
        {
            int playerScore = 0;
            int cpuScore = 0;
            string playerChoice;
            int cpuChoice;
            bool firstRound = true;

            Console.Clear();

            while (playerScore != roundsToWin && cpuScore != roundsToWin)
            {
                Console.WriteLine("ColDog's Rock Paper Scissors Game");
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"Player Score: {playerScore} | CPU Score: {cpuScore}");
                Console.WriteLine($"Player Points: {player.Points}");
                Console.WriteLine("----------------------------------\n");

                // Generate CPU Choice
                Random randomSelection = new();
                cpuChoice = randomSelection.Next(0, 3);

                if (developerMode)
                {
                    Console.WriteLine($"[Developer Mode] CPU Choice: {(cpuChoice == 0 ? "Rock" : cpuChoice == 1 ? "Paper" : "Scissors")}");
                }

                // Determine if the gun is available
                bool gunAvailable = false;
                if (player.PurchasedItems.TryGetValue("Gun", out int gunTier))
                {
                    int chance = gunTier switch
                    {
                        1 => 50,
                        2 => 25,
                        3 => 10,
                        _ => 0
                    };
                    gunAvailable = randomSelection.Next(0, chance) == 0;
                }

                // Determine if the nuke is available (only on the first round)
                bool nukeAvailable = false;
                if (firstRound && player.PurchasedItems.TryGetValue("Nuke", out int nukeTier))
                {
                    int chance = nukeTier switch
                    {
                        1 => 50,
                        2 => 25,
                        3 => 15,
                        _ => 0
                    };
                    nukeAvailable = randomSelection.Next(0, chance) == 0;
                }

                // Get Player Choice
                Console.WriteLine("Choose Rock (r), Paper (p), Scissors (s)" + (gunAvailable ? ", Gun (g)" : "") + (nukeAvailable ? ", Nuke (n)" : "") + ": ");
                playerChoice = Console.ReadLine() ?? "";

                // Validate User Input
                if (!(playerChoice == "r" || playerChoice == "p" || playerChoice == "s" || (gunAvailable && playerChoice == "g") || (nukeAvailable && playerChoice == "n")))
                {
                    Console.WriteLine("Invalid input.\n");
                    return;
                }

                // Game Logic
                if (playerChoice == "g")
                {
                    Console.WriteLine("You chose Gun. You win this round!\n");
                    playerScore++;
                }
                else if (playerChoice == "n")
                {
                    Console.WriteLine("You chose Nuke. You win the game!\n");
                    playerScore = roundsToWin;
                }
                else
                {
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

                firstRound = false;
            }
        }

        // TODO: Fix this method. Sweep bonus is not being applied at all.
        public static void CalculateWinnings(Player player, int roundsToWin, bool playerWon, int playerScore, int cpuScore)
        {
            if (playerWon)
            {
                player.Points += player.Wager * 2;
                if (!(roundsToWin == 1) && playerScore == roundsToWin && cpuScore == 0)
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
}
