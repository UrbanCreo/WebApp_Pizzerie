using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzerie.Models
{
    public class PizzaViewModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [NotMapped]
        public IFormFile? ImageFile { get; set; } // Atribut pro přijímání obrázku

        [Required]
        [DisplayName("Název")]
        public string Name { get; set; } = "";

        [Required]
        [DisplayName("Gramáž")]
        public string Weight { get; set; } = "";

        [Required]
        [DisplayName("Popis")]
        public string Descripton { get; set; } = "";

        [Required]
        [DisplayName("Cena")]
        public string Price { get; set; } = "";

        [Required]
        [DisplayName("Průměr")]
        public string Diameter { get; set; } = "";
    }
}