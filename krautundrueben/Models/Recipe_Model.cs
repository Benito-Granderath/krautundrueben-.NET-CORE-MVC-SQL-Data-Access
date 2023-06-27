using System.ComponentModel.DataAnnotations;

namespace krautundrueben.Models
{
    public class Recipe_Model
    { 
        [Key] public int REZEPTID { get; set; }
        public string? REZEPTBEZEICHNUNG { get; set; }
        public string? ANLEITUNG { get; set; }
        public int KALORIEN {get; set; }
        public int PROTEIN {get; set; }
        public string? ALLERGIEN {get; set; }
        public decimal KOHLENHYDRATE { get; set; }
        public string? ZUTATEN { get; set; } 
        public bool ISVEGAN { get; set; }
        public string? SelectedIngredientIds { get; set; }
    }
}
