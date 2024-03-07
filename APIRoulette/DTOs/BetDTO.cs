namespace APIRoulette.DTOs
{
    public class BetDTO
    {
        public int roulette_Id { get; set; }
        public int betNumber { get; set; }
        public int numberRandon { get; set; }
        public bool? won { get; set; } = false;
        public int betValue { get; set; }

    }
}
