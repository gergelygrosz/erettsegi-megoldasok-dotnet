using System.Text;

internal class Program
{
    private static readonly string PathToInput = Environment.GetEnvironmentVariable("PROJECTS") + @"\erettsegi-megoldasok-dotnet\digkult-emelt-2023-osz\rendel.txt";
    private static readonly string PathToOutput = Environment.GetEnvironmentVariable("PROJECTS") + @"\erettsegi-megoldasok-dotnet\digkult-emelt-2023-osz\kampany.txt";

    private readonly List<Order> data = [];

    private static void Main(string[] args)
    {

        Console.WriteLine("--- 2023. ŐSZ - DIGITÁLIS KULTÚRA EMELT - REKLÁM ---");

        Program program = new();
        program.Feladat1();
        program.Feladat2();
        program.Feladat3();
        program.Feladat4();
        program.Feladat5();
        Console.WriteLine("\n6. feladat");
        Console.WriteLine($"osszes(\"PL\", 7) = {program.Osszes("PL", 7)}");
        program.Feladat7();
        program.Feladat8();
    }

    /// <summary>
    /// Olvassa be és tárolja el a további feldolgozáshoz a <c>rendel.txt</c> állomány tartalmát!
    /// </summary>
    void Feladat1()
    {
        Console.WriteLine("\n1. feladat");

        string[] lines = File.ReadAllLines(PathToInput);
        foreach (string line in lines)
        {
            string[] lineSplit = line.Split(' ');
            data.Add(new Order(
                int.Parse(lineSplit[0]),
                Enum.Parse<City>(lineSplit[1]),
                int.Parse(lineSplit[2])));
        }

        Console.WriteLine("Sikeresen beolvasva");
    }


    /// <summary>
    /// Állapítsa meg, hogy hány rendelés történt a teljes időszakban, és írja a képernyőre a rendelések számát!
    /// </summary>
    void Feladat2()
    {
        Console.WriteLine("\n2. feladat");
        Console.WriteLine($"Összesen {data.Count} rendelés történt.");
    }

    /// <summary>
    /// Kérje be a felhasználótól egy nap számát, és adja meg, hogy hány rendelés történt az adott napon!
    /// </summary>
    void Feladat3()
    {
        Console.WriteLine("\n3. feladat");

        Console.Write("Adja meg az egyik nap számát: ");
        string? rawInput = Console.ReadLine();
        if (!int.TryParse(rawInput, out int inputDay) || inputDay < 0 || inputDay > 30) // ne engedjük meg, hogy nem létező napra kérdezzenek rá!
        {
            Console.WriteLine("Helytelen napot adott meg.");
            return;
        }

        int count = data.Count(x => x.Day == inputDay);
        Console.WriteLine($"A megadott napon {count} rendelés történt.");
    }

    /// <summary>
    /// Számolja meg, hogy hány nap nem volt rendelés a reklámban nem érintett városból, és írja ki a napok számát! Ha egy ilyen nap sem volt, akkor írja ki „Minden nap volt rendelés a reklámban nem érintett városból” szöveget!
    /// </summary>
    void Feladat4()
    {
        Console.WriteLine("\n4. feladat");

        // mert a bool alapértéke false
        bool[] wereThereOrders = new bool[30];
        foreach (Order order in data)
        {
            if (order.City == City.NR)
            {
                wereThereOrders[order.Day - 1] = true;
            }
        }

        Console.WriteLine($"{wereThereOrders.Count(orderOnDay => orderOnDay == false)} napon nem volt rendelés a reklámban nem érintett városból.");
    }

    /// <summary>
    /// Állapítsa meg, hogy mennyi volt az egy rendelésben szereplő legnagyobb darabszám, és melyik volt az a nap, amikor az első ilyen számú rendelést leadták! Az eredményt a lenti minta szerint írja ki!
    /// </summary>
    void Feladat5()
    {
        Console.WriteLine("\n5. feladat");

        int maxAmount = data.Max(order => order.Amount);
        int firstDay = data.First(order => order.Amount == maxAmount).Day;
        Console.WriteLine($"A legnagyobb rendelt mennyiség {maxAmount}, az első ilyen mértékű rendelés napja {firstDay}.");
    }

    /// <summary>
    /// Készítsen függvényt osszes néven, amely megadja, hogy mennyi volt egy adott városból egy adott napon a rendelt termékek száma! A függvény bemenete a három város egyikére utaló kétbetűs szöveg és a nap sorszáma legyen. Amennyiben szükséges, akkor további paramétert is felvehet a rendelések adatainak elérése érdekében. A függvény visszaadott értéke a rendelt darabszámok összege legyen! A függvényt például a következő módon lehessen meghívni: osszes("PL", 7). A függvényt a későbbiekben felhasználhatja a további feladatok megoldásakor.
    /// </summary>
    int Osszes(City city, int day)
    {
        return data.Sum(order => (order.City == city && order.Day == day) ? order.Amount : 0);
    }

    int Osszes(string city, int day)
    {
        return Osszes(Enum.Parse<City>(city), day);
    }

    /// <summary>
    /// Számítsa ki, hogy a kampány utáni első napon, azaz a 21-edik napon melyik városból mennyit rendeltek a termékből!Az eredményt a lenti mintának megfelelő formában írja ki!
    /// </summary>
    void Feladat7()
    {
        Console.WriteLine("\n7. feladat");

        int ordersFromTV = Osszes(City.TV, 21);
        int ordersFromPL = Osszes(City.PL, 21);
        int ordersFromNR = Osszes(City.NR, 21);

        Console.WriteLine($"A rendelt termékek darabszáma a 21. napon: TV: {ordersFromTV}, PL: {ordersFromPL}, NR: {ordersFromNR}.");
    }

    /// <summary>
    /// Összesítse városonként, hogy hány rendelés érkezett az első 10, a 11-20-adik valamint a záró 10 napon! Az eredményt (a fejlécet is beleértve) táblázatos formában, tabulátorokkal tagoltan jelenítse meg a képernyőn, illetve írja azonos formátumban a kampany.txt szöveges állományba!
    /// </summary>
    void Feladat8()
    {
        Console.WriteLine("\n8. feladat");

        int[,] table = new int[3, 3];
        foreach (Order order in data)
        {
            int insertPosition = 0;
            if (order.Day >= 1 && order.Day <= 10)
            {
                insertPosition = 0;
            }
            else if (order.Day >= 11 && order.Day <= 20)
            {
                insertPosition = 1;
            }
            else if (order.Day >= 21 && order.Day <= 30)
            {
                insertPosition = 2;
            }

            table[(int)order.City, insertPosition]++;
        }

        StringBuilder outputBuilder = new();
        outputBuilder.Append("Napok\t1..10\t11..20\t21..30\n");
        for (int row = 0; row < table.GetLength(0); row++)
        {
            outputBuilder.Append($"{(City)row}\t{table[row, 0]}\t{table[row, 1]}\t{table[row, 2]}\n");
        }

        Console.WriteLine(outputBuilder.ToString());
        File.WriteAllText(PathToOutput, outputBuilder.ToString());
    }
}
