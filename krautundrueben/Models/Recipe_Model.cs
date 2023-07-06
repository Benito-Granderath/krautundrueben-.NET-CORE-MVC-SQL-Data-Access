using System.ComponentModel.DataAnnotations;

namespace krautundrueben.Models
{
    public class Recipe_Model
    {
        [Key] public int Rezeptnr { get; set; }
        public string? Rezeptname { get; set; }
        public string? Anleitung { get; set; }
        public bool Vegan { get; set; }
        public bool LowCarb {get;set; }
        public bool Vegetarisch { get; set; }
        public bool Frutarisch { get; set; }
        public bool HighProtein { get; set; }
        public List<int> SelectedIngredients { get; set; }
    }
}
