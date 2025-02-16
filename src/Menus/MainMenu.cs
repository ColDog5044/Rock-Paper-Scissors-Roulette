using ColDogStudios.RockPaperScissors.src.Logic;

namespace ColDogStudios.RockPaperScissors.src.Menus
{
    class MainMenu
    {
        public static void ShowMainMenu(Player player, bool developerMode)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ColDog's Rock Paper Scissors Game\n");
                Console.WriteLine("1. Play Game");
                Console.WriteLine("2. Enter the shop");
                Console.WriteLine("0. Exit");
                string? choice = Console.ReadLine();

                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                int cleansedChoice = Convert.ToInt32(choice);

                if (cleansedChoice == 0)
                {
                    break;
                }

                switch (cleansedChoice)
                {
                    case 1:
                        GameLogic.PlayGameMenu(player, developerMode);
                        break;
                    case 2:
                        ShopMenu.ShowShopMenu(player);
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        Console.ReadLine();
                        break;
                }

                player.SavePlayerStats();
            }
        }
    }
}
