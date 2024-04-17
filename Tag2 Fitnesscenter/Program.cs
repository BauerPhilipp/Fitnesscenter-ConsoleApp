namespace Tag2_Fitnesscenter
{
    internal class Program
    {

        //private static List<Wertkarte> wertkartenListe = new List<Wertkarte>();
        private static Dictionary<int, Wertkarte> wertkartenDictionary = new Dictionary<int, Wertkarte>();

        static void Main(string[] args)
        {
            string weiter = "";
            do
            {
                Console.WriteLine("Hallo!");
                Console.WriteLine("Um ein neues Konto anzulegen drücke \"N\" (Mindestalter ist 14 Jahre)");
                Console.WriteLine("Für die Verwaltung eines bestehenden Kontos drücke V");
                EingabeverarbeitungKontoNeuOderVerwalten(Console.ReadLine());

                Console.WriteLine("Zum Beenden X eingeben.\nWeiter mit belibiter Taste.");  
                weiter = Console.ReadLine();
            }while (weiter != "x" && weiter != "X");

           
        }

        public static void EingabeverarbeitungKontoNeuOderVerwalten(string eingabeString)
        {
            switch (eingabeString.ToUpper())
            {
                case "N":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Zur Einrichtung einer neuen Wertkarte benötigen wir vorab einige Angaben");
                    NeueWertkarteEinrichten();
                    break;
                case "V":
                    if (Wertkarte.WertkartenGesamtAusgegeben == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Es wurden noch keine Wertkarten ausgegeben!");
                        Console.ResetColor();
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Magenta;                   
                    KontoVerwaltung();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ungültige Eingabe!");
                    Console.WriteLine("Gültige Eingaben sind N und V");
                    EingabeverarbeitungKontoNeuOderVerwalten(Console.ReadLine());
                    break;
            }
        }

        private static void NeueWertkarteEinrichten()
        {
            DateOnly geburtsDatum = new DateOnly();
            int geburtsJahr = 0;
            int geburtsMonat = 0;
            int geburtsTag = 0;

            decimal startGuthaben = 0m;

            string vorname = "";
            string nachname = "";

            // Geburtsjahr einlesen
            do
            {
                Console.WriteLine("Geburtsjahr z.B. 1984");
            } while (!(int.TryParse(Console.ReadLine(), out geburtsJahr) && geburtsJahr > 0 && geburtsJahr < 9999));

            // Geburtsmonat einlesen
            do
            {
                Console.WriteLine("Geburtsmonat z.B. 9");

            } while (!(int.TryParse(Console.ReadLine(), out geburtsMonat) && geburtsMonat > 0 && geburtsMonat < 13));

            // für die überprüfung der maximalen Tage in diesem monat
            geburtsDatum = new DateOnly(geburtsJahr, geburtsMonat, 1);

            // Geburtstag einlesen
            do
            {
                Console.WriteLine("Geburtstag z.B. 28");
            } while (!(int.TryParse(Console.ReadLine(), out geburtsTag) && 
            geburtsTag > 0 &&
            geburtsTag <= DateTime.DaysInMonth(geburtsDatum.Year, geburtsDatum.Month)));


            geburtsDatum = new DateOnly(geburtsJahr, geburtsMonat, geburtsTag);
            Console.WriteLine($"Du wurdest am {geburtsDatum} geboren");
            if (geburtsDatum == new DateOnly(geburtsJahr, DateTime.Now.Month, DateTime.Now.Day))
            {
                Console.WriteLine("   Du hast heute Geburtstag!");
                Console.WriteLine("      !!HAPPY BIRTHDAY!!");
            }

            Console.ResetColor();
            Console.WriteLine("Dein Vorname: ");
            vorname = Console.ReadLine();
            Console.WriteLine("Dein Nachname: ");
            nachname = Console.ReadLine();
            do
            {
                Console.WriteLine("Und zum Schluss noch das gewünsche Guthaben welches du aufladen möchtest (max 300€)");
                decimal.TryParse(Console.ReadLine(), out startGuthaben);
            } while (startGuthaben == 0 || startGuthaben > 300m);

            Wertkarte neueWertkarte = new Wertkarte(vorname, nachname, startGuthaben, geburtsDatum);
            //wertkartenListe.Add(new Wertkarte(vorname, nachname, startGuthaben, geburtsDatum));
            wertkartenDictionary.Add(neueWertkarte.WertkarteNummer, neueWertkarte);
        }   

        private static void KontoVerwaltung()
        {
            
            int kartenNummer = 0;
            while (kartenNummer == 0 || kartenNummer > Wertkarte.WertkartenGesamtAusgegeben)
            {
                Console.WriteLine("Bitte deine Kartennummer eingeben:");
                int.TryParse(Console.ReadLine(), out kartenNummer);
                if (kartenNummer == 0 || kartenNummer > Wertkarte.WertkartenGesamtAusgegeben)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Eine Karte mit der Nummer {kartenNummer:00} existiert nicht!");
                    Console.WriteLine("Versuche es noch einmal.");
                    Console.ResetColor();
                }
            }
            Wertkarte karte = wertkartenDictionary[kartenNummer];

            Console.WriteLine($"Hallo {karte.Vorname}! Dein aktuelles Guthaben beträgt {karte.Guthaben} euro.");
            Console.WriteLine("Wie kann ich die helfen?");
            Console.WriteLine("[G] = Gymnastikstunde (kostet 7 EUR)");    
            Console.WriteLine("[A] = Guthaben aufladen");
            Console.WriteLine("[S] = Gymnastikstunde Spezial (kostet 9 EUR)");
            Console.WriteLine("[F] = Fitnessraum 2h (kostet 14 EUR)");
            Console.WriteLine("[T] = Fitnessraum Tag (kostet 19 EUR)");
            Console.WriteLine("[W] = Wellnessbereich 3h (kostet 15 EUR)");

            bool eingabeKorrekt = false;
            string auswahl = "";
            do
            {
                string eingabe = Console.ReadLine();
                eingabeKorrekt = eingabeUeberpruefung(eingabe, out auswahl);
            } while (!eingabeKorrekt);
            karte.Guthaben += LeistungBuchen(auswahl, karte);
            Console.WriteLine($"Die Buchung wurde erfolgreich durchgeführt!");
            Console.WriteLine("Zusammenfassung:");
            Console.WriteLine($"Kartennummer: {karte.WertkarteNummer:00}");
            Console.WriteLine($"Name: {karte.Vorname} {karte.Nachname}");
            Console.WriteLine($"Neues Guthaben: {karte.Guthaben} euro");

        }

       private static bool eingabeUeberpruefung(string eingabe, out string auswahl)
        {
            switch (eingabe.ToUpper())
            {
                case "A":
                    auswahl = "A";
                    return true;
                case "G":
                    auswahl = "G";
                    return true;
                case "S":
                    auswahl = "S";
                    return true;
                case "F":
                    auswahl = "F";
                    return true;
                case "T":
                    auswahl = "T";
                    return true;
                case "W":
                    auswahl = "W";
                    return true;
                default:
                    auswahl = "";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ungültige Auswahl!");
                    Console.ResetColor();
                    return false;
            }
        }  

        private static decimal LeistungBuchen(string auswahl , Wertkarte karte)
        {
            decimal rabattMultiplikator = 1m;

            // Rabatt ermitteln
            if (karte.Alter <= 20)
            {
                Console.WriteLine("Du bist unter 21 und erhälltst somit 20% Rabatt!");
                rabattMultiplikator = 0.8m;
            }
            else if (karte.Alter >= 60)
            {
                Console.WriteLine("Du bist über 59 und erhälltst somit 30% Rabatt!");
                rabattMultiplikator = 0.7m;
            }

            switch (auswahl.ToUpper())
            {
                case "A":
                    if (karte.Guthaben == 300m)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Guthabenlimit erreicht!");
                        Console.ResetColor();
                        return 0m;
                    }
                    Console.WriteLine("Wieviel Guthaben möchten sie aufladen?");
                    Console.WriteLine("Maximales Guthaben 300 euro");
                    Console.WriteLine($"Aktuelles Guthaben {karte.Guthaben} euro");
                    decimal aufladeBetrag = 0m;
                    do
                    {                       
                        if((!decimal.TryParse(Console.ReadLine(), out aufladeBetrag)) ||
                           aufladeBetrag <= 0 ||
                           (aufladeBetrag + karte.Guthaben) > 300)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ungültige Auswahl!");
                            Console.ResetColor();
                        }
                    } while (aufladeBetrag <= 0 || (aufladeBetrag + karte.Guthaben) > 300);
                    return aufladeBetrag;
                case "G":
                    return -7m * rabattMultiplikator;
                case "S":
                    return -9m * rabattMultiplikator;
                case "F":
                    return -14 * rabattMultiplikator;
                case "T":
                    return -19 * rabattMultiplikator;
                case "W":
                    return -15 * rabattMultiplikator;
                default:
                    Console.WriteLine("Ein fehler ist aufgetreten");
                    Console.WriteLine("Es wurde keine Buchung vorgenommen!");
                    return 0;
            }
        }

    }
}
