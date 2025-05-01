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
        /*
        Feladat6();
        Feladat7();
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

            Console.WriteLine($"{wereThereOrders.Count(x => x == false)} napon nem volt rendelés a reklámban nem érintett városból.");
        }

        ///
        void Feladat5()
        {
            Console.WriteLine("\n5. feladat");
        }
    }
}