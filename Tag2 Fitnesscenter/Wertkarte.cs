using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag2_Fitnesscenter
{
    internal class Wertkarte
    {
        private static int _wertkartenGesamtAusgegeben = 0;

        static int Mindestalter = 14;

        private int _wertkarteNummer;
        private string _vorname;
        private string _nachname;
        private decimal _guthaben;
		
		public int GetMindestalter()
		{
			return Mindestalter;
		}

        public int WertkarteNummer
		{
			get { return _wertkarteNummer; }
			private set { _wertkarteNummer = value;}
		}

		public static int WertkartenGesamtAusgegeben
		{
			get { return _wertkartenGesamtAusgegeben; }
			set { _wertkartenGesamtAusgegeben = value; }
		}


		public string Vorname
		{
			get { return _vorname; }
			set { _vorname = value; }
		}
		
		public string Nachname
		{
			get { return _nachname; }
			set { _nachname = value; }
		}

		// Geburtsjahr muss noch implementiert werden

		public decimal Guthaben
		{
			get { return _guthaben; }
			set { _guthaben = value; }
		}




		public Wertkarte(string vorname, string nachname, decimal guthaben, DateOnly geburtstag)
		{
			int alter = BerechneAlter(geburtstag);
			if(alter >= Mindestalter)
			{
                this._vorname = vorname;
                this._nachname = nachname;
                this._guthaben = guthaben;
                WertkarteNummer = ++WertkartenGesamtAusgegeben;
				Console.WriteLine("Neue Wertkarte wurde erfolgreich erstellt.");
				Console.WriteLine($"Deine Daten lauten:");
				Console.WriteLine($"Kartennummer: {WertkarteNummer:00000}");
				Console.WriteLine($"Name: {Vorname} {Nachname}");
				Console.WriteLine($"Startguthaben: {guthaben:000} €");
			}
			else
			{
				int jahreBisAltGenug = Mindestalter - alter;
                Console.WriteLine($"Du bist leider noch nicht alt genug.\n" +
					$"Mindestalter {Mindestalter} Jahre! Versuche es wieder in {jahreBisAltGenug} Jahren.");
            }
        }

		public int BerechneAlter(DateOnly geburtstag)
		{

			DateOnly heute = DateOnly.FromDateTime(DateTime.Now);

            // Berechnen der vollen Jahre
            int alter = heute.Year - geburtstag.Year;	

			// Überprüfen ob dieses Jahr schon der Geburtstag war. Wenn nicht wird diese Sleife ausgeführt
            if (heute.Month <= geburtstag.Month && heute.Day < geburtstag.Day)
            {
				alter--;
            }
            //Console.WriteLine($"alter: {alter}");
            return alter;
		}

	}
}
