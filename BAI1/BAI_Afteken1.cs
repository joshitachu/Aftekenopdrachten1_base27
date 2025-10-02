using System;
using System.Collections.Generic;
using System.Globalization;


namespace BAI
{
    public class BAI_Afteken1
    {
        private const string BASE27CIJFERS = "-ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // ***************
        // * OPGAVE 1a/b *
        // ***************
        public static UInt64 Opg1aDecodeBase27(string base27getal)
        {
            /* Zuja: voeg nog wat nuttige commentaar toe, 
            bijv. variabele uitleg, wat de loop doet, etc. */            

            UInt64 result = 0;
            foreach (char c in base27getal) //loopt door elke karakter in de string
            {

                int digit = BASE27CIJFERS.IndexOf(c); //controleert of het karakter in de base27 cijfers zit
                if (digit < 0)// als het karakter er niet in zit returned het -1 , dit checken we hier
                    // Zuja: in dit soort methodes mag je geen Console.WriteLine doen.
                    // Je zou hier een exceptie moeten gooien.
                    throw new Exception("Ongeldig teken gevonden in base27-getal.");// exceptie gooien

                // Zuja: Deze Check for overflow is goed bedoeld, maar niet correct.
                // Zie ook jouw result aan het einde van deze method.
                if (result > (UInt64.MaxValue - (UInt64)digit) / 27UL) // Check voor overflow:
                    throw new Exception("base getal is te groot."); // exceptie gooien

                result = result * 27 + (UInt64)digit;// resultaat berekenen 
            }
            return result;
        }
        public static string Opg1bEncodeBase27(UInt64 base10getal)
        {

            /* Zuja: voeg nog wat nuttige commentaar toe, 
            bijv. variabele uitleg, wat de loop doet, etc. */            
            String result="";
            if (base10getal == 0){// speciaal geval als het getal 0 is return dan "-"
                return "-";
            }
            while (base10getal > 0)// zolang het getal groter is dan 0
            {
                int digit = (int)(base10getal % 27); // bepaal de rest bij deling door modulus 27
                result= BASE27CIJFERS[digit] + result;// voeg het karakter toe aan het result
                // Zuja: onderstaande regel is correct, maar je kunt ook anders oplossen, zonder List.
                base10getal /= 27;// deel n door 27
            }
            // Zuja: Twee overbodige regels.
            return result;

        }

        // ***************
        // * OPGAVE 2a/b *
        // ***************
        public static Stack<UInt64> Opdr2aWoordStack(List<string> woorden)
        {
            var stack = new Stack<UInt64>();

            foreach (var woord in woorden)// loop door elk woord in de lijst
            {
                UInt64 number = Opg1aDecodeBase27(woord);
                stack.Push(number);// zet het getal op de stack
            }
            return stack;
        }
        public static Queue<string> Opdr2bKorteWoordenQueue(Stack<UInt64> woordstack)
        {
            
            var queue = new Queue<string>();
            // Zuja: Waarom geen Const? En waarom -1?
            const ulong max = 27 * 27 * 27;// geeft het max waarde van een 3 letter woord in base27

            while (woordstack.Count > 0)// zolang er woorden in de stack zitten
            {
                ulong woord = woordstack.Pop();
                // Zuja: Verband met die -1?
                if (woord <= max)
                {
                    string decoded = Opg1bEncodeBase27(woord);
                    queue.Enqueue(decoded);// zet het woord in de queue
                }
            }

             return queue;
        }

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("=== Opdracht 1a : Decode base-27 ===");
            Console.WriteLine($"BAI    => {Opg1aDecodeBase27("BAI")}");         // 1494
            Console.WriteLine($"HBO-ICT => {Opg1aDecodeBase27("HBO-ICT")}");    // 3136040003
            Console.WriteLine($"BINGO  => {Opg1aDecodeBase27("BINGO")}");       // 1250439
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("=== Opdracht 1b : Encode base-27 ===");
            Console.WriteLine($"1494       => {Opg1bEncodeBase27(1494)}");          // "BAI"
            Console.WriteLine($"3136040003 => {Opg1bEncodeBase27(3136040003)}");    // "HBO-ICT"
            Console.WriteLine($"1250439   => {Opg1bEncodeBase27(1250439)}");        // BINGO
            Console.WriteLine();

            Console.WriteLine("=== Opdracht 2 : Stack / Queue - korte woorden ===");
            string[] woordarray = {"CONCEPT", "OK", "BLAUW", "TOEN", "IS",
                "HBOICT", "GEEL", "DIT", "GOED", "NIEUW"};
            List<string> woorden = new List<string>(woordarray);
            Stack<UInt64> stack = Opdr2aWoordStack(woorden);
            Queue<string> queue = Opdr2bKorteWoordenQueue(stack);

            foreach (string kortwoord in queue)
            {
                Console.Write(kortwoord + " ");                             // Zou "DIT IS OK" moeten opleveren
            }
            Console.WriteLine();
        }
    }
}
