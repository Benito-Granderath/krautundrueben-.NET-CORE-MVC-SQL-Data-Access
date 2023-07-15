using System.Text.RegularExpressions;

namespace krautundrueben.Models
{
    public class SQLQueries_Model
    {

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //       TABELLEN QUERIES
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public string DisplayAllQuery { get; } = @"
                SELECT b.*, bz.*, k.NACHNAME, k.VORNAME, k.GEBURTSDATUM, k.STRASSE AS KUNDESTRASSE, k.HAUSNR AS KUNDEHAUSNR, k.PLZ AS KUNDEPLZ, 
                k.ORT AS KUNDEORT, k.TELEFON AS KUNDETELEFON, k.EMAIL AS KUNDEEMAIL, l.LIEFERANTENNR,
                l.LIEFERANTENNAME, l.STRASSE AS LIEFERANTSTRASSE, l.HAUSNR AS LIEFERANTHAUSNR, l.PLZ AS 
                LIEFERANTPLZ, l.ORT AS LIEFERANTORT, l.TELEFON AS LIEFERANTTELEFON, l.EMAIL AS LIEFERANTEMAIL, z.*
                FROM BESTELLUNG b

                INNER JOIN BESTELLUNGZUTAT bz ON b.BESTELLNR = bz.BESTELLNR
                INNER JOIN ZUTAT z ON bz.ZUTATENNR = z.ZUTATENNR
                INNER JOIN KUNDE k ON b.KUNDENNR = k.KUNDENNR
                INNER JOIN LIEFERANT l ON z.LIEFERANT = l.LIEFERANTENNR

                ORDER BY b.BESTELLNR";

        //Inner join um ALLE Felder zu zeigen, die etwas miteinander gemeinsam haben.
        public string GroupByCustomerQuery { get; } = @"
                SELECT b.*, bz.*, k.NACHNAME, k.VORNAME, k.GEBURTSDATUM, k.STRASSE AS KUNDESTRASSE, k.HAUSNR AS KUNDEHAUSNR, k.PLZ AS KUNDEPLZ, 
                k.ORT AS KUNDEORT, k.TELEFON AS KUNDETELEFON, k.EMAIL AS KUNDEEMAIL, l.LIEFERANTENNR,
                l.LIEFERANTENNAME, l.STRASSE AS LIEFERANTSTRASSE, l.HAUSNR AS LIEFERANTHAUSNR, l.PLZ AS 
                LIEFERANTPLZ, l.ORT AS LIEFERANTORT, l.TELEFON AS LIEFERANTTELEFON, l.EMAIL AS LIEFERANTEMAIL, z.* FROM KUNDE k 

                LEFT JOIN BESTELLUNG b ON k.KUNDENNR = b.KUNDENNR
                LEFT JOIN BESTELLUNGZUTAT bz ON b.BESTELLNR = bz.BESTELLNR
                LEFT JOIN ZUTAT z ON bz.ZUTATENNR = z.ZUTATENNR
                LEFT JOIN LIEFERANT l ON z.LIEFERANT = l.LIEFERANTENNR

                ORDER BY b.BESTELLNR";
        //Left Join um ALLE Kunden zu zeigen. Auch die, die potentiell noch keine Bestellung aufgegeben haben.

        public string GroupByOrderQuery { get; } = @"SELECT b.*, bz.*, k.NACHNAME, k.VORNAME, k.GEBURTSDATUM, k.STRASSE AS KUNDESTRASSE, k.HAUSNR AS KUNDEHAUSNR, k.PlZ AS kUNDEPlZ, 
                k.ORT AS KUNDEORT, k.TELEFON AS KUNDETELEFON, k.EMAIL AS KUNDEEMAIL, l.LIEFERANTENNR,
                l.LIEFERANTENNAME, l.STRASSE AS lIEFERANTSTRASSE, l.HAUSNR AS lIEFERANTHAUSNR, l.PLZ AS 
                LIEFERANTPlZ, l.ORT AS LIEFERANTORT, l.TELEFON AS LIEFERANTTELEFON, l.EMAIL AS LIEFERANTEMAIL, z.* FROM BESTELLUNG b

                LEFT JOIN KUNDE k ON b.KUNDENNR = k.KUNDENNR
                LEFT JOIN BESTELLUNGZUTAT bz ON b.BESTELLNR = bz.BESTELLNR
                LEFT JOIN ZUTAT z ON bz.ZUTATENNR = z.ZUTATENNR
                LEFT JOIN LIEFERANT l ON z.LIEFERANT = l.LIEFERANTENNR

                ORDER BY b.BESTELLNR";

        //Left Join um ALLE Bestellungen zu zeigen. Auch die, die potentiell noch keinem Kunden zugewiesen sind.

        public string GroupByDeliveryQuery { get; } = @"SELECT b.*, bz.*, k.NACHNAME, k.VORNAME, k.GEBURTSDATUM, k.STRASSE AS KUNDESTRASSE, k.HAUSNR AS KUNDEHAUSNR, k.PLZ AS KUNDEPLZ, 
                k.ORT AS KUNDEORT, k.TELEFON AS KUNDETELEFON, k.EMAIL AS KUNDEEMAIL, l.LIEFERANTENNR,
                l.LIEFERANTENNAME, l.STRASSE AS LIEFERANTSTRASSE, l.HAUSNR AS LIEFERANTHAUSNR, l.PLZ AS 
                LIEFERANTPLZ, l.ORT AS LIEFERANTORT, l.TELEFON AS LIEFERANTTELEFON, l.EMAIL AS LIEFERANTEMAIL, z.* FROM LIEFERANT l

                LEFT JOIN ZUTAT z ON l.LIEFERANTENNR = z.LIEFERANT
                LEFT JOIN BESTELLUNGZUTAT bz ON z.ZUTATENNR = bz.ZUTATENNR
                LEFT JOIN BESTELLUNG b ON bz.BESTELLNR = b.BESTELLNR
                LEFT JOIN KUNDE k ON b.KUNDENNR = k.KUNDENNR

                ORDER BY b.BESTELLNR";

        //Left Join um ALLE Lieferanten zu zeigen. Auch die, denen potentiell kein Auftrag zugewiesen ist.

        public string GroupByIngredientQuery { get; } = @"SELECT b.*, bz.*, k.NACHNAME, k.VORNAME, k.GEBURTSDATUM, k.STRASSE AS KUNDESTRASSE, k.HAUSNR AS KUNDEHAUSNR, k.PLZ AS KUNDEPLZ, 
                k.ORT AS KUNDEORT, k.TELEFON AS KUNDETELEFON, k.EMAIL AS KUNDEEMAIL, l.LIEFERANTENNR,
                l.LIEFERANTENNAME, l.STRASSE AS LIEFERANTSTRASSE, l.HAUSNR AS LIEFERANTHAUSNR, l.PLZ AS 
                LIEFERANTPLZ, l.ORT AS LIEFERANTORT, l.TELEFON AS LIEFERANTTELEFON, l.EMAIL AS LIEFERANTEMAIL, z.* FROM ZUTAT z

                LEFT JOIN LIEFERANT l ON z.LIEFERANT = l.LIEFERANTENNR
                LEFT JOIN BESTELLUNGZUTAT bz ON z.ZUTATENNR = bz.ZUTATENNR
                LEFT JOIN BESTELLUNG b ON bz.BESTELLNR = b.BESTELLNR
                LEFT JOIN KUNDE k ON b.KUNDENNR = k.KUNDENNR

                ORDER BY b.BESTELLNR";

        //Left Join um ALLE Zutaten zu zeigen. Auch die, die in keinen Bestellungen benutzt werden.

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //       GRAPHEN QUERIES
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public string AnalyzeOrderTrend { get; } = @"SELECT K.KUNDENNR, K.NACHNAME, SUM(B.RECHNUNGSBETRAG) AS TOTAL
                FROM BESTELLUNG B
                INNER JOIN KUNDE K ON B.KUNDENNR = K.KUNDENNR
                GROUP BY K.KUNDENNR, K.NACHNAME
                ORDER BY TOTAL DESC";

        //Kundenausgaben Chart Query.
        public string IngredientAnalysis { get; } = @"SELECT Z.BEZEICHNUNG, SUM(BZ.MENGE) AS TotalQuantitySold
                FROM BESTELLUNGZUTAT BZ
                INNER JOIN ZUTAT Z ON BZ.ZUTATENNR = Z.ZUTATENNR
                GROUP BY Z.BEZEICHNUNG
                ORDER BY TotalQuantitySold DESC";

        //Beliebteste Zutaten Chart query.

        public string MostActiveSuppliers { get; } = @"SELECT L.LIEFERANTENNAME, COUNT(Z.ZUTATENNR) AS Ingredient_Count
                FROM LIEFERANT L
                JOIN ZUTAT Z ON L.LIEFERANTENNR = Z.LIEFERANT
                GROUP BY L.LIEFERANTENNR, L.LIEFERANTENNAME
                ORDER BY Ingredient_Count DESC";

        //Lieferanten nach Zutatenangebot sortiert.
        public string SalesPerDate { get; } = @"SELECT DATE_FORMAT(BESTELLDATUM, '%d.%m.%Y') AS SalesPerDateDate, SUM(RECHNUNGSBETRAG) AS SalesPerDateSales
                FROM BESTELLUNG
                GROUP BY DATE_FORMAT(BESTELLDATUM, '%d.%m.%Y')
                ORDER BY DATE_FORMAT(BESTELLDATUM, '%d.%m.%Y')";

        //Verkäufe über Zeit.

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //      ANDERE QUERIES
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public string InsertQuery { get; } = @"INSERT INTO REZEPT (Rezeptname, Anleitung, Vegan, `Low-Carb`, Vegetarisch, Frutarisch, `High-Protein`) VALUES (@Rezeptname, @Anleitung, @Vegan, @LowCarb, @Vegetarisch, @Frutarisch, @HighProtein); SELECT LAST_INSERT_ID()";
        //Insert Query zum Einfügen neuer Rezepte in die Datenbank.
        public string IngredientQuery { get; } = @"SELECT BEZEICHNUNG FROM ZUTAT WHERE ZUTATENNR IN @SelectedIngredientIds";

        //Query zum Auswählen meherer Zutaten 
        public string DeleteQuery { get; } = @"DELETE FROM REZEPT WHERE REZEPTNR = @RecipeId";
        public string DeleteQuery2 { get; } = @"DELETE FROM REZEPTZUTAT WHERE REZEPTNR = @RecipeId";


        //Delete Query zum Löschen von Rezepten in der Datenbank.
    }

}
