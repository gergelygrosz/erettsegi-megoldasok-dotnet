internal class Program
{
    const string PATH_TO_INPUT = @"C:\Prog\erettsegi-megoldasok-dotnet\emelt-2023-osz\rendel.txt";
    const string PATH_TO_OUTPUT = @"C:\Prog\erettsegi-megoldasok-dotnet\emelt-2023-osz\kampany.txt";

    static void Main(string[] args)
    {

        List<Order> data = [];

        Console.WriteLine("--- 2023. ŐSZ - DIGITÁLIS KULTÚRA EMELT - REKLÁM ---");

        Feladat1();
        Feladat2();
        Feladat3();
        Feladat4();
        Feladat5();
        Console.WriteLine("\n6. feladat");
        Console.WriteLine($"osszes(City.PL, 7) = {osszes(City.PL, 7)}");
        Feladat7();
        /*
        Feladat8();
        */

        /// Olvassa be és tárolja el a további feldolgozáshoz a <c>rendel.txt</c> állomány tartalmát!
        void Feladat1()
        {
            Console.WriteLine("\n1. feladat");

            string[] lines = File.ReadAllLines(PATH_TO_INPUT);
            foreach (var line in lines)
            {
                var lineSplit = line.Split(' ');
                data.Add(new Order(
                    int.Parse(lineSplit[0]),
                    Enum.Parse<City>(lineSplit[1]),
                    int.Parse(lineSplit[2])));
            }

            Console.WriteLine("Sikeresen beolvasva");
        }

        /// Állapítsa meg, hogy hány rendelés történt a teljes időszakban, és írja a képernyőre a rendelések számát!
        void Feladat2()
        {
            Console.WriteLine("\n2. feladat");
            Console.WriteLine($"Összesen {data.Count} rendelés történt.");
        }

        /// Kérje be a felhasználótól egy nap számát, és adja meg, hogy hány rendelés történt az adott napon!
        void Feladat3()
        {
            Console.WriteLine("\n3. feladat");

            Console.Write("Adja meg az egyik nap számát: ");
            var rawInput = Console.ReadLine();
            if (!int.TryParse(rawInput, out int inputDay) || inputDay < 0 || inputDay > 30) // ne engedjük meg, hogy nem létező napra kérdezzenek rá!
            {
                Console.WriteLine("Helytelen napot adott meg.");
                return;
            }

            var count = data.Count(x => x.Day == inputDay);
            Console.WriteLine($"A megadott napon {count} rendelés történt.");
        }

        /// Számolja meg, hogy hány nap nem volt rendelés a reklámban nem érintett városból, és írja ki a napok számát! Ha egy ilyen nap sem volt, akkor írja ki „Minden nap volt rendelés a reklámban nem érintett városból” szöveget!
        void Feladat4()
        {
            Console.WriteLine("\n4. feladat");

            // mert a bool alapértéke false
            bool[] wereThereOrders = new bool[30];
            foreach (var order in data)
            {
                if (order.City == City.NR)
                {
                    wereThereOrders[order.Day - 1] = true;
                }
            }

            Console.WriteLine($"{wereThereOrders.Count(orderOnDay => orderOnDay == false)} napon nem volt rendelés a reklámban nem érintett városból.");
        }

        /// Állapítsa meg, hogy mennyi volt az egy rendelésben szereplő legnagyobb darabszám, és melyik volt az a nap, amikor az első ilyen számú rendelést leadták! Az eredményt a lenti minta szerint írja ki!
        void Feladat5()
        {
            Console.WriteLine("\n5. feladat");

            var maxAmount = data.Max(order => order.Amount);
            var firstDay = data.First(order => order.Amount == maxAmount).Day;
            Console.WriteLine($"A legnagyobb rendelt mennyiség {maxAmount}, az első ilyen mértékű rendelés napja {firstDay}.");
        }

        /// Készítsen függvényt osszes néven, amely megadja, hogy mennyi volt egy adott városból egy adott napon a rendelt termékek száma! A függvény bemenete a három város egyikére utaló kétbetűs szöveg és a nap sorszáma legyen. Amennyiben szükséges, akkor további paramétert is felvehet a rendelések adatainak elérése érdekében. A függvény visszaadott értéke a rendelt darabszámok összege legyen! A függvényt például a következő módon lehessen meghívni: osszes("PL", 7). A függvényt a későbbiekben felhasználhatja a további feladatok megoldásakor.
        int osszes(City city, int day)
        {
            return data.Sum(order => (order.City == city && order.Day == day) ? order.Amount : 0);
        }

        /// Számítsa ki, hogy a kampány utáni első napon, azaz a 21-edik napon melyik városból mennyit rendeltek a termékből!Az eredményt a lenti mintának megfelelő formában írja ki!
        void Feladat7()
        {
            Console.WriteLine("\n7. feladat");

            int ordersFromTV = osszes(City.TV, 21);
            int ordersFromPL = osszes(City.PL, 21);
            int ordersFromNR = osszes(City.NR, 21);

            Console.WriteLine($"A rendelt termékek darabszáma a 21. napon: TV: {ordersFromTV}, PL: {ordersFromPL}, NR: {ordersFromNR}.");
        }
    }
}