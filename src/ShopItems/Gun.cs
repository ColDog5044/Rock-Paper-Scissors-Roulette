namespace ColDogStudios.RockPaperScissors.src.ShopItems
{
    class Gun : ShopItem
    {
        public Gun(int tier) : base("Gun", GetPrice(tier), tier) { }

        private static int GetPrice(int tier)
        {
            return tier switch
            {
                1 => 2500,
                2 => 10000,
                3 => 100000,
                _ => 0
            };
        }
    }
}
