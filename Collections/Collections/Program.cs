using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Collections
{

    class Species
    {
        public IList<Pet> Pets { get; set; }
        public double Cost { get; set; }
        public string Name { get; set; }
    }

    class Pet : IEquatable<Pet>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        //defined by IEquatable interface
        public bool Equals(Pet other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Age == other.Age;
        }

        //should always be overriden when overriding public bool Equals(T other)
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Pet) obj);
        }

        //should always be overriden when overriding public bool Equals(T other)
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Age;
            }
        }

        //good to define when overriding Equals
        public static bool operator ==(Pet p1, Pet p2)
        {
            return p1.Equals(p2);
        }

        //operator '==' requires a matching operator '!=' to be also defined
        public static bool operator !=(Pet p1, Pet p2)
        {
            return !(p1 == p2);
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            var pet1 = new Pet()
            {
                Name = "Burek",
                Age = 2
            };

            var pet2 = new Pet()
            {
                Name = "Burek",
                Age = 2
            };

            //Co by się stało gdybyśmy nie nadpisali metody Equals?
            //Co jeśli nie zdefiniowalibyśmy własnego operatora '=='?
            //pet2 = pet1; - co jeśli bez nadpisywania Equals zdefiniowalibyśmy pet2 w ten sposób?
            Console.WriteLine(pet1.Equals(pet2));
            Console.WriteLine(pet1 == pet2);

            //overflow
            const int max = int.MaxValue;
            int one = 1;

            Console.WriteLine(max);
            Console.WriteLine(max + one);
            //checked
            //{
            //    Console.WriteLine(max + one); //Throws OverflowException
            //}

            //Console.WriteLine(max + 1); //Error 'The operation overflows at compile time in checked mode'
            unchecked
            {
                Console.WriteLine(max + 1);
            }


            //ZWIERZAKI
            var dogs = new List<Pet>()
            {
                new Pet() {Age = 2, Name = "Burek"},
                new Pet() {Age = 3, Name = "Azor"},
                new Pet() {Age = 12, Name = "Reksio"},
                new Pet() {Age = 1, Name = "Pimpek"}
            };

            var cats = new List<Pet>()
            {
                new Pet() {Age = 7, Name = "Behemot"},
                new Pet() {Age = 3, Name = "Tom"},
                new Pet() {Age = 17, Name = "Klakier"}
            };

            var mice = new List<Pet>()
            {
                new Pet() {Age = 1, Name = "Jerry"},
                new Pet() {Age = 2, Name = "Mickey"}
            };

            var ponies = new List<Pet>()
            {
                new Pet() {Age = 3, Name = "Bapplejack"},
                new Pet() {Age = 2, Name = "Bimkie Guy"},
                new Pet() {Age = 2, Name = "Flubbershy"},
                new Pet() {Age = 4, Name = "Bumfight Sparkle"},
                new Pet() {Age = 3, Name = "Dziunia"}
            };

            var species = new List<Species>()
            {
                new Species() {Cost = 213.20, Pets = dogs, Name = "Pieski"},
                new Species() {Cost = 123.40, Pets = cats, Name = "Kotki"},
                new Species() {Cost = 21.0, Pets = mice, Name = "Myszki"},
                new Species() {Cost = 999.99, Pets = ponies, Name = "Kuce"}
            };
            Console.Write("\n\n\n");

            //Dwa równoważne sposoby zapisu wyrażeń LINQ - my będziemy używać pierwszego
            //var oldDogs = dogs.Where(dog => dog.Age > 2);
            //var oldDogs = from dog in dogs where dog.Age > 2 select dog;

            //ZADANKA
            //1. Where
            //a) Wybierz wszystkie psy o parzystym wieku.
            var evenAgedDogs = dogs.Where(d => d.Age % 2 == 0);
            Console.WriteLine("Psy o parzystym wieku");
            foreach (var dog in evenAgedDogs)
            {
                Console.WriteLine($"{dog.Name} lat {dog.Age}");
            }
            Console.Write("\n\n");


            //b) Wybierz gatunki zwierząt o cenie wyższej niż 100.
            var priceySpecies = species.Where(s => s.Cost > 100.00);
            Console.WriteLine("Gatunki droższe niż 100/szt.:");
            foreach (var s in priceySpecies)
            {
                Console.WriteLine($"{s.Name}: {s.Cost}/szt.");
            }
            Console.Write("\n\n");

            //1. Select
            //a) Wybierz imiona myszek poprzedzone słowem "Myszka" ("Mickey" -> "Myszka Mickey")
            Console.WriteLine("Myszki:");
            foreach (var name in mice.Select(m => $"Myszka {m.Name}"))
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("\n\n");

            //b) Wybierz nazwy gatunków wraz z ceną zaokrągloną do liczby dziesiątek
            // (jako string "NAZWA - CENA" bądź tworząc Anonymous Type new{Name = ..., Price  = ...}
            var speciesWithPrices =
                species.Select(s => new {Name = s.Name, Price = (int)Math.Round(s.Cost / 10) * 10});
            Console.WriteLine("Gatunki");
            foreach (var s in speciesWithPrices)
            {
                Console.WriteLine($"{s.Name} - {s.Price}");
            }
            Console.WriteLine("\n\n");
            
            //Nie było na zajęciach - przykład użycia SelectMany() - w skrócie służy do "spłaszczania" kolekcji
            //Po więcej informacji odsyłam do Google, możliwe też, że powiemy coś jeszcze na następnych zajęciach
            var allPets = species.SelectMany(s => s.Pets);
            Console.WriteLine("Wszystkie zwierzaki");
            foreach (var pet in allPets)
            {
                Console.WriteLine($"{pet.Name} lat {pet.Age}");
            }

            Console.WriteLine("\n\n");

            //3. Kombinacje
            //a) Wybierz imiona wszystkich zwierząt młodszych niż 3 lata
            var youngPets = species.SelectMany(s => s.Pets).Where(p => p.Age < 3).Select(p => p.Name);
            Console.WriteLine("Młode zwierzaki:");
            foreach (var name in youngPets)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("\n\n");


            //b) Wybierz skrócone imiona kucy, których imię ma dwie części
            //("Bimkie Guy" -> "B. Guy", przydatne metody: String.Contains, String.Split)
            var shortenedPonyNames = ponies.Where(p => p.Name.Contains(' '))
                .Select(p => $"{p.Name.Split(' ')[0][0]}. {p.Name.Split(' ')[1]}");
            Console.WriteLine("Skrócone imiona");
            foreach (var name in shortenedPonyNames)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("\n\n");

            //c) Czy jest jakieś zwierze, którego wiek jest równy liczbie znaków imienia? (użyć Any(); "Behemot lat 7", "Tom lat 3")
            Console.WriteLine("Czy wiek któregoś ze zwierząt jest równy liczbie znaków jego imienia?");
            Console.WriteLine(species.SelectMany(s => s.Pets).Any(p => p.Name.Length == p.Age)
                ? "A i owszem\n\n"
                : "Nie bardzo\n\n");

            //d) Policz wartość wszystkich zwierząt w species (użyć Sum())
            var totalValue = species.Sum(s => s.Pets.Count * s.Cost);
            Console.WriteLine($"Wartość wszystkich zwierząt: {totalValue}");

            //e) Czy wszystkie zwierzęta starsze od 5 lat mają w imieniu literę 'o'? (Użyć All(); "Klakier lat 17")
            Console.WriteLine("Czy wszystkie zwierzęta starsze od 5 lat mają w imieniu literę 'o'?");
            Console.WriteLine(species.SelectMany(s => s.Pets).Where(p => p.Age > 5).All(p => p.Name.Contains('o'))
                ? "A i owszem\n\n"
                : "Nie bardzo\n\n");



            //Dla "zaawansowanych" - mam nadzieję, że pokażemy jeszcze na zajęciach,
            //ale jeśli ktoś wytrwał aż dotąd, to poniżej macie przykład użycia grupowania w LINQ :D

            var petsGroupedByAge = species.SelectMany(s => s.Pets).GroupBy(p => p.Age);

            foreach (var pets in petsGroupedByAge)
            {
                Console.WriteLine($"Wiek: {pets.Key}");
                foreach (var pet in pets)
                {
                    Console.WriteLine(pet.Name);
                }
                Console.WriteLine("---------------");
            }


            Console.ReadKey();
        }
    }
}
