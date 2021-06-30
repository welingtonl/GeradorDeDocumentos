using System;
using System.Text.RegularExpressions;

namespace GeraDocs
{
    class Program
    {
        static void Main(string[] args)
        {
            var pis = GeraPis.Pis();
            Console.WriteLine(pis);
            Console.WriteLine(ValidaPis.Validar(pis));
            var cns = GeraCns.Cns();
            Console.WriteLine(cns);
            Console.WriteLine(ValidaCns.Validar(cns));
        }

        public class GeraCns
        {
            private static Random rnd;

            public static string Cns()
            {
                
                int soma;
                rnd = new Random();
                var cns = rnd.Next(7000000, 9999999 ).ToString() + rnd.Next(9000000, 9999999).ToString();

                soma = 0;
                for (int i = 0; i < cns.Length; i++)
                {
                    soma += (int)char.GetNumericValue(cns[i]) * (15 - i);
                }
                soma = soma % 11;
                cns += soma;

                return cns;
            }

        }

        public class GeraPis
        {
            private static Random rnd;

            public static string Pis()
            {
                int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                rnd = new Random();
                var pis = rnd.Next(1000000000, 1999999999).ToString();

                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(pis[i].ToString()) * multiplicador[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                pis += resto;


                return pis;
            }

        }

        public class ValidaPis
        {
            public const int TamanhoPis = 11;

            public static bool Validar(string pis)
            {
                int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                if (pis.Trim().Length != TamanhoPis)
                    return false;

                pis = pis.Trim();
                pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(pis[i].ToString()) * multiplicador[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                return pis.EndsWith(resto.ToString());
            }
        }

        public class ValidaCns
        {
            public const int TamanhoCns = 15;

            public static bool Validar(string cns)
            {
                var regx1 = @"[1-2]\d{10}00[0-1]\d";
                var regx2 = @"[7-9]\d{14}";
                if (Regex.IsMatch(cns, regx1) || Regex.IsMatch(cns, regx2))
                {
                    return somaPonderada(cns) % 11 == 0;
                }
                return false;
            }

            private static int somaPonderada(string cns)
            {
                int soma = 0;
                for (int i = 0; i < cns.Length; i++)
                {
                    soma += (int)char.GetNumericValue(cns[i]) * (TamanhoCns - i);
                }
                return soma;
            }
        }
    }
}
