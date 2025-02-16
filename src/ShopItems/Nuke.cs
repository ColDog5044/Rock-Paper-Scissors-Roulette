namespace ColDogStudios.RockPaperScissors.src.ShopItems
{
    class Nuke : ShopItem
    {
        public Nuke(int tier) : base("Nuke", GetPrice(tier), tier) { }

        private static int GetPrice(int tier)
        {
            return tier switch
            {
                1 => 5000,
                2 => 25000,
                3 => 150000,
                _ => 0
            };
        }
    }
}
