using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Loggboken_Vidare_Arbete
{
    public class LogEntryClass
    {
        private long logNumber;
        private DateTime datum;
        private string rubrik;
        private string loggText;

        public LogEntryClass (long logNumber, DateTime datum, string rubrik, string loggText)
        {
            this.logNumber = logNumber;
            this.datum = datum;
            this.rubrik = rubrik;
            this.loggText = loggText;
        }

        public long LogNumber
        {
            get { return logNumber; }
            set { logNumber = value; }
        }

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }
        public string Rubrik
        {
            get { return rubrik; }
            set { rubrik = value; }
        }
        public string LoggText
        {
            get { return loggText; }
            set { loggText = value; }
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
            List<LogEntryClass> loggBoken = new List<LogEntryClass>();

            //Skapar en lista med vektorer som ska hålla i våra logginlägg:
            List<string[]> loggBok = new List<string[]> { };
            string[] logg = new string[2]; //Platshållare med 2 index för rubrik och text som vi sedan ska lägga till i loggBoken

            string rubrik; //Platshållare för rubrik
            string loggText; //Platshållare för loggtexten

            //Programmets on/off-switch:
            bool isRunning = true;

            //START:
            while (isRunning)
            {
                huvudMeny(); //Anropar metoden huvudMeny och skriver ut menyn

                string menyValString = Console.ReadLine();//Användaren skriver in sitt menyval
                int menyVal; //Vi skapar int som vi ska konvertera menyvalet till

                //OM menyValString kan konverteras till int(menyVal) OCH menyVal = 1-5, gå vidare. Annars felmeddelande.
                if (int.TryParse(menyValString, out menyVal) && menyVal > 0 && menyVal < 6)
                {
                    //Konverterar menyvalet till ett heltal som sedan kan användas i switchen nedanför:
                    menyVal = Convert.ToInt32(menyValString);
                    Console.Clear();

                    switch (menyVal)
                    {
                        //Skriv en ny logg
                        case 1:
                            Console.WriteLine("\n\t[Skriv en ny logg!]");

                            Console.WriteLine("\n\tSkriv en rubrik för din logg:\n\t");

                            rubrik = Console.ReadLine();

                            if (rubrik == "") //Autorubrik för tomma strängar
                            {
                                rubrik = "INGEN RUBRIK";
                            }

                            logg[0] = rubrik.ToUpper(); //Lägger till rubriken i första indexet på logg

                            Console.WriteLine("\n\tSkriv in din logg:\n\t");

                            loggText = Console.ReadLine();

                            if (loggText == "") //Autotext för tomma strängar
                            {
                                loggText = "Det finns ingenting i den här loggen";
                            }

                            logg[1] = loggText; //Lägger till loggtexten i andra indexet på logg

                            loggBok.Add(logg.ToArray()); //Kopierar logg till loggBok

                            //=============================================================

                            loggBoken.Add(new LogEntryClass((loggBoken.LongCount() + 1), DateTime.Now, rubrik, loggText));


                            //=============================================================
                            Console.ReadKey();
                            break;

                        //Visa alla loggar
                        case 2:
                            Console.WriteLine("\n\t[Visa alla loggar!]");

                            if (loggBoken.LongCount() == 0) //Om loggBok är tom
                            {
                                Console.WriteLine("\n\tDet finns inga registrerade loggar...");
                            }
                            else if (loggBok.LongCount() == 1) //Grammatikändring om det bara finns en logg
                            {
                                Console.WriteLine("\n\tDet finns " + loggBoken.LongCount() + " registrerad logg:\n");
                            }
                            else
                            {
                                Console.WriteLine("\n\tDet finns " + loggBoken.LongCount() + " registrerade loggar:\n"); //Skriver ut antalet loggar
                            }
                            //=============================================================
                            foreach (var logEntry in loggBoken)
                            {
                                Console.WriteLine("\n\tNr. {0}\n\tDatum: {1}\n\tRubrik: {2}\n\t{3}", Convert.ToString(logEntry.LogNumber), logEntry.Datum, logEntry.Rubrik, logEntry.LoggText);
                            }
                            //=============================================================
                            Console.ReadKey();
                            break;

                        //Sök efter en logg
                        case 3:
                            Console.WriteLine("\n\t[Sök efter en logg!]");

                            bool searchHit = false; //Meddelar om sökningen lyckades
                            string rubrikSearch; //Användarens sökord
                            string loggSearch; //Användarens sökord

                            Console.WriteLine("\n\t[1]Sök efter rubrik");
                            Console.WriteLine("\t[2]Sök efter loggtext");
                            Console.WriteLine("\n\t[3]Tillbaka till huvudmeny");

                            menyValString = Console.ReadLine();

                            switch (menyValString)
                            {
                                //Sök efter rubrik
                                case "1":
                                    Console.WriteLine("\n\t[Sök efter rubrik!]");

                                    Console.WriteLine("\n\tSkriv in rubriken du letar efter:");

                                    rubrikSearch = Console.ReadLine();
                                    rubrikSearch = rubrikSearch.ToUpper(); //Konverterar användarens sökord till versaler så att det kan matchas till rubrik


                                    Console.WriteLine("\n\tSökresultat:\n");

                                    foreach (string[] element in loggBok) //För varje element i loggBok
                                    {
                                        if (element[0].Contains(rubrikSearch)) //Om första index i elementet innehåller sökordet
                                        {
                                            searchHit = true; //Sökning lyckades
                                            Console.WriteLine("\tRukrik: " + element[0] + "\n\tLogg: " + element[1] + "\n"); //Skriver ut resultat
                                        }
                                    }

                                    if (searchHit == false) //Om sökning misslyckas
                                    {
                                        Console.WriteLine("\tDet finns inga loggar med den rubriken...");
                                    }

                                    Console.ReadKey();
                                    break;

                                //Sök efter loggtext
                                case "2":
                                    Console.WriteLine("\n\t[Sök efter loggtext!]");
                                    Console.WriteLine("\n\tSkriv in texten som loggen innehåller:");

                                    loggSearch = Console.ReadLine(); //Sökord

                                    Console.WriteLine("\n\tSökresultat:\n");

                                    foreach (string[] element in loggBok) //För varje element i loggBok
                                    {
                                        if (element[1].Contains(loggSearch)) //Om andra index i elementet innehåller sökordet
                                        {
                                            searchHit = true; //Sökning lyckades
                                            Console.WriteLine("\tRukrik: " + element[0] + "\n\tLogg: " + element[1] + "\n"); //Skriver ut resultat
                                        }
                                    }

                                    if (searchHit == false) //Om sökning misslyckas
                                    {
                                        Console.WriteLine("\tDet finns inga loggar med det innehållet...");
                                    }

                                    Console.ReadKey();
                                    //En nackdel med denna sökning är att den är case-sensitive. Eftersom rubriken är i versaler
                                    //är det inget problem men med loggtexten måste man vara väldigt specifik. Skulle kunna lösa det med några
                                    //extra rader kod men jag sparar det till uppgiften för högre betyg om jag har tid.
                                    break;

                                case "3": //Gör ingenting så att användaren går tillbaka till menyn direkt
                                    break;
                                default:
                                    Console.WriteLine("\n\tFelaktig inmatning!");
                                    Console.ReadKey();
                                    break;
                            }

                            break;

                        //Radera alla loggar
                        case 4:
                            Console.WriteLine("\n\t[Radera alla loggar!]");

                            Console.WriteLine("\n\tÄr du säker på att du vill radera alla loggar? [j]a / [n]ej");

                            menyValString = Console.ReadLine();
                            menyValString = menyValString.ToUpper(); //Så att det passar vårat case

                            switch (menyValString)
                            {
                                case "J":
                                case "JA":
                                    loggBok.Clear(); //Rensar allt i loggBok

                                    Console.WriteLine("\n\tAlla loggar har blivit raderade! Tryck på valfri tangent för att gå tillbaka till menyn...");
                                    break;

                                default:
                                    Console.WriteLine("\n\tRadering avbruten. Tryck på valfri tangent för att gå tillbaka till menyn...");
                                    break;
                            }

                            Console.ReadKey();
                            break;

                        //Avsluta
                        case 5:
                            isRunning = false;
                            break;

                        //Felmeddelande om man skriver in något annat än 1-5
                        default:
                            Console.WriteLine("\n\tFelaktig inmatning!");
                            Console.ReadKey();
                            break;
                    }
                }
                //Vid felaktig inmatning:
                else
                {
                    Console.WriteLine("\n\tFelaktig inmatning!");
                    Console.ReadKey();
                }
                Console.Clear();
            }
        }
    }
}