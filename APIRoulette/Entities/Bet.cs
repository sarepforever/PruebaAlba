using System.ComponentModel.DataAnnotations.Schema;

namespace APIRoulette.Entities
{
    public class Bet
    {
        public int id { get; set; }

        public int roulette_Id { get; set; }
        public int betNumber { get; set; }
        public bool won { get; set; }
        public int betValue { get; set; }

        [ForeignKey("roulette_Id")]
        public virtual Roulette Roulette { get; set; }
       
    }
}
