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
            UInt64 result = 0;
                foreach (char c in base27getal)
                {
                    int digit = BASE27CIJFERS.IndexOf(c);
                if (digit < 0)
                    Console.WriteLine(" not found buddy");

                    if (result > (UInt64.MaxValue - (UInt64)digit) / 27UL)
                        return UInt64.MaxValue;

                    result = result * 27 + (UInt64)digit;
                }
                return result;
        }
        public static string Opg1bEncodeBase27(UInt64 base10getal)
        {
            String result="";

            if (base10getal == 0){
                return "-";
            }
            var chars = new List<char>();
            UInt64 n = base10getal;

            while (n > 0)
            {
                int digit = (int)(n % 27);
                chars.Add(BASE27CIJFERS[digit]);
                n /= 27;
                
            }
            chars.Reverse();
            result = new string(chars.ToArray());
            return result;

        }

        // ***************
        // * OPGAVE 2a/b *
        // ***************
        public static Stack<UInt64> Opdr2aWoordStack(List<string> woorden)
        {
            var stack = new Stack<UInt64>();

            foreach (var woord in woorden)
            {
                UInt64 number = Opg1aDecodeBase27(woord);
                stack.Push(number);
            }
            return stack;
        }
        public static Queue<string> Opdr2bKorteWoordenQueue(Stack<UInt64> woordstack)
        {
            
            var queue = new Queue<string>();
            ulong max = 27 * 27 * 27 - 1;

            while (woordstack.Count > 0)
            {
                ulong woord = woordstack.Pop();
                if (woord <= max)
                {
                    string decoded = Opg1bEncodeBase27(woord);
                    queue.Enqueue(decoded);
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
