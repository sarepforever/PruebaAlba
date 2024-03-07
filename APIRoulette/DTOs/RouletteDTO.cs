using System.ComponentModel.DataAnnotations;

namespace APIRoulette.DTOs
{
    public class RouletteDTO
    {

        [Required(ErrorMessage = "Ingrese el valor minimo de la ruleta")]
        public int? numMin { get; set; } = 1;
        [Required(ErrorMessage = "Ingrese el valor maximo de la ruleta")]
        public int? numMax { get; set; } = 18;
    }
    public class RouletteGetDTO
    {
        public int id { get; set; }
        public int numMin { get; set; }
        public int numMax { get; set; }
        public int? minValue { get; set; }
        public int? maxValue { get; set; }

    }
}
