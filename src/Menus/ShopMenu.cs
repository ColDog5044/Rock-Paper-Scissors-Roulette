using ColDogStudios.RockPaperScissors.src.Logic;
using ColDogStudios.RockPaperScissors.src.ShopItems;

namespace ColDogStudios.RockPaperScissors.src.Menus
{
    class ShopMenu
    {
        private static readonly List<ShopItem> shopItems =
            [
                new Gun(1),
                new Gun(2),
                new Gun(3),
                new Nuke(1),
                new Nuke(2),
                new Nuke(3)
            ];

        public static void ShowShopMenu(Player player)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the shop!");

            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                Console.WriteLine($"{i + 1}. {item.Name} Tier {item.Tier} - {item.Price} points");
            }

            Console.WriteLine("0. Return to main menu");
            int choice = Convert.ToInt32(Console.ReadLine() ?? "");

            if (choice == 0)
            {
                return;
            }

            if (choice > 0 && choice <= shopItems.Count)
            {
                player.PurchaseItem(shopItems[choice - 1]);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }

            Console.WriteLine("Press Enter to return to the shop menu.");
            Console.ReadLine();
            ShowShopMenu(player);
        }
    }
}
