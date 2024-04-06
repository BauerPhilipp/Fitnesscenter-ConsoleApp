namespace Tag2_Fitnesscenter
{
    internal class Program
    {

        private static List<Wertkarte> wertkartenListe = new List<Wertkarte>();

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
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Bitte Kartennummer eingeben:");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ungültige Eingabe!");
                    Console.WriteLine("Gültige Eingaben sind N und V");
                    EingabeverarbeitungKontoNeuOderVerwalten(Console.ReadLine());
                    break;
            }
        }

        public static void NeueWertkarteEinrichten()
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

            wertkartenListe.Add(new Wertkarte(vorname, nachname, startGuthaben, geburtsDatum));
        }   
    }
}
