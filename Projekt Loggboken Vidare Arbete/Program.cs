using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projekt_Loggboken_Vidare_Arbete
{
    public class LogEntry //Klass med allt ett logginlägg ska innehålla
    {
        private long logNumber;
        private DateTime datum;
        private string rubrik;
        private string loggText;

        //Konstruktor för logginlägget. Vi kommer att skicka in logginformation i det här formatet
        public LogEntry (long logNumber, DateTime datum, string rubrik, string loggText) 
        {
            this.logNumber = logNumber;
            this.datum = datum;
            this.rubrik = rubrik;
            this.loggText = loggText;
        }

        //Engenskaper som är kopplade till logNumber, datum, rubrik och loggtext.
        //Ändrar värdet för våra objekt
        public long LogNumber
        {
            get
            {
                return logNumber;
            }
            set
            {
                logNumber = value;
            }
        }

        public DateTime Datum
        {
            get
            {
                return datum;
            }
            set
            {
                datum = value;
            }
        }

        public string Rubrik
        {
            get
            {
                return rubrik;
            }
            set
            {
                rubrik = value;
            }
        }

        public string LoggText
        {
            get
            {
                return loggText;
            }
            set
            {
                loggText = value;
            }
        }
    }

    class Program
    {

        //Skriver ut huvudmenyn:
        static void huvudMeny()
        {
            Console.WriteLine("\n\t[Välkommen till Loggboken!]");
            Console.WriteLine("\n\t[1]Skriv en ny logg");
            Console.WriteLine("\t[2]Visa alla loggar");
            Console.WriteLine("\t[3]Sök efter en logg");
            Console.WriteLine("\t[4]Radera alla loggar");
            Console.WriteLine("\n\t[5]Avsluta");
            Console.Write("\n\tVälj: ");
        }

        static void Main(string[] args)
        {
            List<LogEntry> loggBoken = new List<LogEntry>();

            string rubrik; //Platshållare för rubrik
            string loggText; //Platshållare för loggtexten
            
            //Programmets on/off-switch:
            bool isRunning = true;

            //START:
            while (isRunning)
            {
                huvudMeny(); //Anropar metoden huvudMeny och skriver ut menyn

                string menyVal = Console.ReadLine();//Användaren skriver in sitt menyval

                Console.Clear();

                switch (menyVal) //Huvudmeny
                {

                    //Skriv en ny logg
                    case "1":
                        Console.WriteLine("\n\t[Skriv en ny logg!]");

                        Console.WriteLine("\n\tSkriv en rubrik för din logg:\n\t");
                        rubrik = Console.ReadLine().ToUpper();

                        if (rubrik == "") //Autorubrik för tomma strängar
                        {
                            rubrik = "INGEN RUBRIK";
                        }

                        Console.WriteLine("\n\tSkriv in din logg:\n\t");
                        loggText = Console.ReadLine();

                        if (loggText == "") //Autotext för tomma strängar
                        {
                            loggText = "Det finns ingenting i den här loggen";
                        }

                        //Lägger till det vi har skrivit som en ny LogEntry i listan loggBoken
                        loggBoken.Add(new LogEntry((loggBoken.LongCount() + 1), DateTime.Now, rubrik, loggText));

                        Console.ReadKey();
                        break;

                    //Visa alla loggar
                    case "2":
                        Console.WriteLine("\n\t[Visa alla loggar!]");

                        if (loggBoken.LongCount() == 0) //Om loggBok är tom
                        {
                            Console.WriteLine("\n\tDet finns inga registrerade loggar...");
                        }
                        else if (loggBoken.LongCount() == 1) //Grammatikändring om det bara finns en logg
                        {
                            Console.WriteLine("\n\tDet finns " + loggBoken.LongCount() + " registrerad logg:\n");
                        }
                        else //Skriver ut antalet loggar
                        {
                            Console.WriteLine("\n\tDet finns " + loggBoken.LongCount() + " registrerade loggar:\n");
                        }

                        foreach (var logEntry in loggBoken) //Skriver eu varje logEntry i loggBoken med formatet nedan:
                        {
                            Console.WriteLine("\n\tNr. {0}\n\tDatum: {1}\n\tRubrik: {2}\n\t{3}", Convert.ToString(logEntry.LogNumber), logEntry.Datum, logEntry.Rubrik, logEntry.LoggText);
                        }

                        Console.ReadKey();
                        break;

                    //Sök efter en logg
                    case "3":
                        Console.WriteLine("\n\t[Sök efter en logg!]");

                        bool searchHit = false; //Meddelar om sökningen lyckades
                        string rubrikSearch; //Användarens sökord
                        string loggSearch; //Användarens sökord
                        long loggNrSearch; //Användarens sökord

                        Console.WriteLine("\n\t[1]Sök loggnummer");
                        Console.WriteLine("\t[2]Sök efter rubrik");
                        Console.WriteLine("\t[3]Sök efter loggtext");
                        Console.WriteLine("\n\t[4]Tillbaka till huvudmeny");

                        menyVal = Console.ReadLine();

                        switch (menyVal) //Sökmeny
                        {
                            case "1": //Sök efter loggnummer
                                Console.WriteLine("\n\t[Sök efter loggnummer!]");

                                Console.WriteLine("\n\tSkriv in loggnummret du letar efter:");

                                loggNrSearch = Convert.ToInt32(Console.ReadLine()); //Konverterar användarens sökord till int så att det kan matchas till loggnummer

                                Console.WriteLine("\n\tSökresultat:\n");

                                for (int i = 0; i < loggBoken.LongCount(); i++) //Går igenom varje element i loggBoken
                                {
                                    if (loggBoken[i].LogNumber == loggNrSearch) //Om en logEntry i loggboken har samma LogNumber som det man söker efter:
                                    {
                                        searchHit = true; //Sökning lyckades
                                        Console.WriteLine("\n\tNr. {0}\n\tDatum: {1}\n\tRubrik: {2}\n\t{3}", Convert.ToString(loggBoken[i].LogNumber), loggBoken[i].Datum, loggBoken[i].Rubrik, loggBoken[i].LoggText); //Skriver ut resultat
                                    }
                                }

                                if (searchHit == false) //Om sökning misslyckas
                                {
                                    Console.WriteLine("\tLoggnummer " + loggNrSearch + " hittades inte...");
                                }

                                Console.ReadKey();
                                break;

                            //Sök efter rubrik
                            case "2":
                                Console.WriteLine("\n\t[Sök efter rubrik!]");

                                Console.WriteLine("\n\tSkriv in rubriken du letar efter:");

                                rubrikSearch = Console.ReadLine().ToUpper(); //Konverterar användarens sökord till versaler så att det kan matchas till rubrik

                                Console.WriteLine("\n\tSökresultat:\n");

                                for (int i = 0; i < loggBoken.Count; i++) //Går igenom loggBoken
                                {
                                    if (loggBoken[i].Rubrik.Contains(rubrikSearch)) //Om en rubrik i loggboken innehåller sökningen
                                    {
                                        searchHit = true; //Sökning lyckades
                                        Console.WriteLine("\n\tNr. {0}\n\tDatum: {1}\n\tRubrik: {2}\n\t{3}", Convert.ToString(loggBoken[i].LogNumber), loggBoken[i].Datum, loggBoken[i].Rubrik, loggBoken[i].LoggText); //Skriver ut resultat
                                    }
                                }

                                if (searchHit == false) //Om sökning misslyckas
                                {
                                    Console.WriteLine("\tDet finns inga loggar med den rubriken...");
                                }

                                Console.ReadKey();
                                break;

                            //Sök efter loggtext
                            case "3":
                                Console.WriteLine("\n\t[Sök efter loggtext!]");
                                Console.WriteLine("\n\tSkriv in texten som loggen innehåller:");

                                loggSearch = Console.ReadLine(); //Sökord

                                Console.WriteLine("\n\tSökresultat:\n");

                                for (int i = 0; i < loggBoken.Count; i++) //Går igenom loggboken
                                {
                                    if (loggBoken[i].LoggText.Contains(loggSearch)) //Om en loggtext i loggboken innehåller loggSearch
                                    {
                                        searchHit = true; //Sökning lyckades
                                        Console.WriteLine("\n\tNr. {0}\n\tDatum: {1}\n\tRubrik: {2}\n\t{3}", Convert.ToString(loggBoken[i].LogNumber), loggBoken[i].Datum, loggBoken[i].Rubrik, loggBoken[i].LoggText); //Skriver ut resultat
                                    }
                                }

                                if (searchHit == false) //Om sökning misslyckas
                                {
                                    Console.WriteLine("\tDet finns inga loggar med det innehållet...");
                                }

                                Console.ReadKey();
                                //En nackdel med denna sökning är att den är case-sensitive. Eftersom rubriken är i versaler
                                //är det inget problem men med loggtexten måste man vara noga med stora och små bokstäver.
                                break;

                            case "4": //Gör ingenting så att användaren går tillbaka till menyn direkt
                                break;
                            default:
                                Console.WriteLine("\n\tFelaktig inmatning!");
                                Console.ReadKey();
                                break;
                        }

                        break;

                    //Radera alla loggar
                    case "4":
                        Console.WriteLine("\n\t[Radera alla loggar!]");

                        Console.WriteLine("\n\tÄr du säker på att du vill radera alla loggar? [j]a / [n]ej");

                        menyVal = Console.ReadLine().ToUpper();

                        switch (menyVal)
                        {
                            case "J":
                            case "JA":
                                loggBoken.Clear(); //Rensar allt i loggBok

                                Console.WriteLine("\n\tAlla loggar har blivit raderade! Tryck på valfri tangent för att gå tillbaka till menyn...");
                                break;

                            default:
                                Console.WriteLine("\n\tRadering avbruten. Tryck på valfri tangent för att gå tillbaka till menyn...");
                                break;
                        }

                        Console.ReadKey();
                        break;

                    //Avsluta
                    case "5":
                        isRunning = false;
                        break;

                    //Felmeddelande om man skriver in något annat än 1-5
                    default:
                        Console.WriteLine("\n\tFelaktig inmatning!");
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
            }
        }
    }
}