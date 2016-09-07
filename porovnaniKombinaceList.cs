using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {


            Parser parser = new Parser();

            string keyKombinaction = "AA"; /* tady bude misto //string keyWord = "AA";//    //string keyWord = (var nejake jmeno)*/

            const string GameActionPreFlop = "Akce s kartamy";

            // Ctenni souboru line by line
            try
            {
                string[] ALLlines = System.IO.File.ReadAllLines(@"C:\shk.txt");
                foreach (string Handline in ALLlines)
                {
                    if (Handline.Contains(keyKombinaction)) // Jmeno stolu
                    {

                        var LineOfHAND = Parser.getNameOfHand(Handline, keyKombinaction);

                        Console.WriteLine(GameActionPreFlop + "-" + LineOfHAND);

                    }
                }
            }
            catch (Exception e)
            {
                // Co budes delat, kdyz ten soubor neexistuje?   
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Press ENTER for finish.");
            Console.ReadLine();
        }
    }


    class Parser
    {

        internal static string getNameOfHand(string Handline, string keyKombinaction)
        {
            return Handline.Substring(Handline.IndexOf(keyKombinaction));
        }

    }
}
