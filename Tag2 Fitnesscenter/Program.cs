namespace Tag2_Fitnesscenter
{
    internal class Program
    {

        private static List<Wertkarte> wertkarten = new List<Wertkarte>();

        static void Main(string[] args)
        {

            Console.WriteLine("Hallo");
            Console.WriteLine("Um ein neues Konto anzulegen drücke N");
            Console.WriteLine("Für die Verwaltung eines bestehenden Kontos drücke V");
            EingabeverarbeitungKontoNeuOderVerwalten(Console.ReadLine());
            




            //DateOnly geburtstag = new DateOnly(2010, 04, 05);

            //Wertkarte w1 = new Wertkarte("Philipp", "Bauer", 300, geburtstag);
            //Wertkarte w2 = new Wertkarte("Renate", "Bauer", 200,geburtstag);
            //Wertkarte w3 = new Wertkarte("Nadine", "Bauer", 100, geburtstag);

            //Console.WriteLine($"w1 Vorname: {w1.Vorname} " +
            //    $"w2 Vorname: {w2.Vorname} " +
            //    $"w3 Vorname: {w3.Vorname}");
        }

        public static void EingabeverarbeitungKontoNeuOderVerwalten(string eingabeString)
        {
            switch (eingabeString.ToUpper())
            {
                case "N":
                    Console.WriteLine("Zur Einrichtung einer neuen Wertkarte benötigen wir vorab einige Angaben");
                    NeueWertkarteEinrichten();
                    break;
                case "V":
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe!");
                    Console.WriteLine("Gültige Eingaben sind V und N");
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
        }
        

    }
}
