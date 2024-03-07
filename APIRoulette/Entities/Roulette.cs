namespace APIRoulette.Entities
{
    public class Roulette
    {
        public int id { get; set; }
        public int numMin { get; set; }
        public int numMax { get; set; }

        public virtual ICollection<Bet> _Bet { get; set; }
    }
}
