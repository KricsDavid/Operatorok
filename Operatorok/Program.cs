using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Operatorok
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> kifejezesek = File.ReadAllLines("kifejezesek.txt").ToList();

            // 2. feladat: Az állomány hány kifejezést tartalmaz
            Console.WriteLine($"2. feladat: Az állomány {kifejezesek.Count} kifejezést tartalmaz.");

            // 3. feladat: Maradékos osztást tartalmazó kifejezések száma
            int modKifejezesekSzama = kifejezesek.Count(k => k.Contains("mod"));
            Console.WriteLine($"3. feladat: Maradékos osztást tartalmazó kifejezések száma: {modKifejezesekSzama}");

            // 4. feladat: Van-e olyan kifejezés, amelyben mindkét operandus maradék nélkül osztható tízzel
            bool vanOszthatoTizzel = kifejezesek.Any(k => int.Parse(k.Split(' ')[0]) % 10 == 0 && int.Parse(k.Split(' ')[2]) % 10 == 0);
            Console.WriteLine($"4. feladat: {(vanOszthatoTizzel ? "Van" : "Nincs")} olyan kifejezés, amelyben mindkét operandus maradék nélkül osztható tízzel.");

            // 5. feladat: Statisztika az operátorok számáról
            var operatorStat = new Dictionary<string, int>
            {
                {"+", kifejezesek.Count(k => k.Contains("+"))},
                {"-", kifejezesek.Count(k => k.Contains("-"))},
                {"*", kifejezesek.Count(k => k.Contains("*"))},
                {"/", kifejezesek.Count(k => k.Contains("/"))},
                {"div", kifejezesek.Count(k => k.Contains("div"))},
                {"mod", kifejezesek.Count(k => k.Contains("mod"))}
            };

            Console.WriteLine("5. feladat: Statisztika az operátorok számáról:");
            foreach (var kvp in operatorStat)
            {
                Console.WriteLine($"{kvp.Key} operátor: {kvp.Value} darab");
            }

            // 6. feladat: Kifejezés értékének meghatározása
            int KifejezesErteke(string kifejezes)
            {
                int operand1 = int.Parse(kifejezes.Split(' ')[0]);
                string operador = kifejezes.Split(' ')[1];
                int operand2 = int.Parse(kifejezes.Split(' ')[2]);

                switch (operador)
                {
                    case "+":
                        return operand1 + operand2;
                    case "-":
                        return operand1 - operand2;
                    case "*":
                        return operand1 * operand2;
                    case "/":
                        if (operand2 != 0)
                            return operand1 / operand2;
                        else
                            return -1; // Egyéb hiba: Nullával való osztás
                    case "div":
                        if (operand2 != 0)
                            return operand1 / operand2;
                        else
                            return -1; // Egyéb hiba: Nullával való osztás
                    case "mod":
                        if (operand2 != 0)
                            return operand1 % operand2;
                        else
                            return -1; // Egyéb hiba: Nullával való osztás
                    default:
                        return -1; // Hibás operátor
                }
            }

            // 7. feladat: Kifejezés értékének meghatározása felhasználói input alapján
            bool vege = false;
            while (!vege)
            {
                Console.Write("7. feladat: Kérem adjon meg egy kifejezést (vége = kilépés): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "vége")
                {
                    vege = true;
                }
                else
                {
                    int ertek = KifejezesErteke(input);
                    if (ertek == -1)
                    {
                        Console.WriteLine("Hibás operátor vagy egyéb hiba történt!");
                    }
                    else
                    {
                        Console.WriteLine($"A kifejezés értéke: {ertek}");
                    }
                }
            }

            // 8. feladat: Eredmények mentése eredmenyek.txt fájlba
            using (StreamWriter sw = new StreamWriter("eredmenyek.txt"))
            {
                foreach (string kifejezes in kifejezesek)
                {
                    int ertek = KifejezesErteke(kifejezes);
                    sw.WriteLine($"{kifejezes} = {ertek}");
                }
            }

            Console.WriteLine("Az eredmények mentése megtörtént az eredmenyek.txt fájlba.");
        }
    }
}
