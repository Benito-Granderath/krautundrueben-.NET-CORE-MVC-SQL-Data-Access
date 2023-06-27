using System.ComponentModel.DataAnnotations;

namespace krautundrueben.Models
{
    public class DBData_Model
    {
        [Key] public int BESTELLNR { get; set; }
        [Key] public int KUNDENNR { get; set; }
        public DateTime BESTELLDATUM { get; set; }
        public double RECHNUNGSBETRAG { get; set; }
        [Key] public int ZUTATENNR { get; set; }
        public int MENGE { get; set; }
        public string? NACHNAME { get; set; }
        public string? VORNAME { get; set; }
        public DateTime GEBURTSDATUM { get; set; }
        public string? KUNDESTRASSE { get; set; }
        public string? KUNDEHAUSNR { get; set; }
        public string? KUNDEPLZ { get; set; }
        public string? KUNDEORT { get; set; }
        public string? KUNDETELEFON { get; set; }
        public string? KUNDEEMAIL { get; set; }
        [Key] public int LIEFERANTENNR { get; set; }
        public string? LIEFERANTENNAME { get; set; }
        public string? LIEFERANTSTRASSE { get; set; }
        public string? LIEFERANTHAUSNR { get; set; }
        public string? LIEFERANTPLZ { get; set; }
        public string? LIEFERANTORT { get; set; }
        public string? LIEFERANTTELEFON { get; set; }
        public string? LIEFERANTEMAIL { get; set; }
        public string? BEZEICHNUNG { get; set; }
        public string? EINHEIT { get; set; }
        public decimal NETTOPREIS { get; set; }
        public int BESTAND { get; set; }
        public int LIEFERANT { get; set; }
        public int KALORIEN { get; set; }
        public decimal KOHLENHYDRATE { get; set; }
        public decimal PROTEIN { get; set; }
        public decimal TOTAL { get; set; }
        public decimal TotalQuantitySold { get; set; }
        public int Ingredient_Count { get; set; }
        public string? SalesPerDateDate { get; set; }
        public decimal SalesPerDateSales { get; set; }
    }
}
