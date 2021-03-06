using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {


            Parser parser = new Parser();
            

            string keyWord = "Table";
            string keyWord1 = "Dealt";
            string keyWord2 = "PokerStars";
            string keyWord3 = "FLOP";
            string hranaKombinace ;
            string keyWordend = "SUMMARY";
            const string STR_NAME_OF_TABLE = "Jmeno stolu";
            const string TIME = "Cas";
            const string NUM_OF_HAND = "Cislo hry";
            const string FLOP = "Rozdano na flopu";

            // Ctenni souboru line by line
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"C:\Text.txt");
                string[] handline = System.IO.File.ReadAllLines(@"C:\Users\Viktor\Desktop\seznamdvojickaret.txt");
                foreach (string line in lines)
                {
                    if (line.Contains(keyWord)) // Jmeno stolu
                    {

                        var LineOfTable = Parser.getNameOfTable(line, keyWord);
                        int startIndex = LineOfTable.IndexOf(" '") + " '".Length;
                        int endIndex = LineOfTable.IndexOf("' ");
                        string jmenoStoluString = LineOfTable.Substring(startIndex, endIndex - startIndex);
                        Console.WriteLine(STR_NAME_OF_TABLE + ":" + jmenoStoluString);

                    }
                    if (line.Contains(keyWord1)) // Rozdane karty
                    {
                        var hand = Parser.getNameOfTable(line, keyWord1);
                        int startIndex = hand.IndexOf("[") + "[".Length;
                        int endIndex = hand.IndexOf("]");
                        /*musim prevest "hodnota + barva" a "hodnota + barva" na jednoduzsi format "hodnota, hodnota + barva shodna ano/ne"
                        melo by to vypadat treba   AKo = "Eso Kral offsuite"    QTs = "Dama Deset suite"       tim se snizi pocet kombynaci na 169 */
                        string dealtCardString = hand.Substring(startIndex, endIndex - startIndex);
                        //tady delim zapis na jednotlive karty    "prvniRozdana" a "druhaRozdana"
                        string prvnikarta = (".x" + dealtCardString + "x.");
                        int controlIndexA = prvnikarta.IndexOf(".x") + ".x".Length;
                        int controlIndexB = prvnikarta.IndexOf(" ");
                        string prvniRozdana = prvnikarta.Substring(controlIndexA, controlIndexB - controlIndexA);
                        int controlIndexA2 = prvnikarta.IndexOf(prvniRozdana) + prvniRozdana.Length;
                        int controlIndexB2 = prvnikarta.IndexOf("x.");
                        string druhaRozdana = prvnikarta.Substring(controlIndexA2, controlIndexB2 - controlIndexA2);

                        // tady zjistuju hodnotu a barvu prvni karty
                        string h1 = (prvniRozdana);
                        var hodnotaPrvniKarty = (h1[0]);
                        string b1 = (prvniRozdana);
                        var barvaPrvniKarty = (b1[1]);
                        //hodnota a barva druhe karty
                        string h2 = (druhaRozdana);
                        var hodnotaDruheKarty = (h2[1]);
                        string b2 = (druhaRozdana);
                        var barvaDruheKarty = (b2[2]);

                        // tady uz to kontroluje shodu barvy, bud je vysledek "s" nebo "o"
                        if (barvaPrvniKarty.Equals(barvaDruheKarty))
                        {
                            hranaKombinace = ("(" + hodnotaPrvniKarty + hodnotaDruheKarty + "s" + ")");
                        }

                        else
                        {
                            hranaKombinace = ("(" + hodnotaPrvniKarty + hodnotaDruheKarty + "o" + ")");
                        }

                        string input = hranaKombinace;
                        int start = input.IndexOf("(");
                        int stop = input.IndexOf(")");
                        string zkracenaKombinace = input.Substring(start + 1, stop - start - 1);
                        Console.WriteLine(zkracenaKombinace);    // vypsany finalni format format 


                        // string zkracenaKombinace je hotova kombinace, nezasahovat, nemazat, neupravovat  !!!!!
                        //...................................................................................................................
                        string listfold = File.ReadAllText(@"C:\Users\Viktor\Desktop\seznamdvojickaret.txt");

                        if (listfold.Contains(zkracenaKombinace))
                         {
                            Console.WriteLine(zkracenaKombinace + "fold");
                         }

                        else
                        {
                            Console.WriteLine(zkracenaKombinace +"bet");
                        }
                    }



// odsud dolu to taky facha tak jak ma, nemenit !!!!!
//.....................................................................................................................
                    if (line.Contains(keyWord2)) // Cas 
                    {
                        var time = Parser.getNameOfTable(line, keyWord2);
                        int startIndex = time.IndexOf("-") + "-".Length;
                        int endIndex = time.IndexOf("ET");
                        string timeString = time.Substring(startIndex, endIndex - startIndex);
                        Console.WriteLine(TIME + ":" + timeString);
                    }
                    if (line.Contains(keyWord2)) // Cislo hry
                    {
                        var num = Parser.getNameOfTable(line, keyWord2);
                        int startIndex = num.IndexOf("Hand #") + "Hand #".Length;
                        int endIndex = num.IndexOf(":");
                        string numString = num.Substring(startIndex, endIndex - startIndex);
                        Console.WriteLine(NUM_OF_HAND + ":" + numString);
                    }
                    if (line.Contains(keyWord3)) // Rozdano na flopu  >>> Mozna do budoucna pouziju na druhy krok ve hre (nejaka reakce na flop)                            
                    {
                        var flop = Parser.getNameOfTable(line, keyWord3);
                        int startIndex = flop.IndexOf("[") + "[".Length;
                        int endIndex = flop.IndexOf("]");
                        string flopString = flop.Substring(startIndex, endIndex - startIndex);
                        Console.WriteLine(FLOP + ":" + flopString);
                    }
                    if (line.Contains(keyWordend)) // Posledni radek jedne hry, tohle bude definovad bod zapisuj do array radku +1
                    {                              // To jeste musim pospekulovat jak to udelat
                        var end = Parser.getNameOfTable(line, keyWordend);
                        int startIndex = end.IndexOf("S") + "S".Length;
                        int endIndex = end.IndexOf("Y");
                        string endString = end.Substring(startIndex, endIndex - startIndex);
                        Console.WriteLine("........................................................................");
                    }
                    /*else
                    {
                        // Jestlize ho nenajde, tak 
                        Console.WriteLine(".");
                    }
                    */
                }
            }
            catch (Exception e)
            {
                // Co budes delat, kdyz ten soubor neexistuje?   
                // Tohle budu resit az ta aplikace bude v okne, myslim tem bude texbox kde se budou vypysovat chyby
                // Tech chyb muze byt kopa, takto tam necham vypisovat vsechno
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Press ENTER for finish.");
            Console.ReadLine();
        }
    }


    class Parser
    {
        internal static string getNameOfTable(string line, string keyWord)
        {
            return line.Substring(line.IndexOf(keyWord));
        }

    }
}
