using System.Text;

internal class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("--- 2024. TAVASZ EMELT ÉRETTSÉGI - DIGITÁLIS KULTÚRA - BELÉPTETŐ RENDSZER ---");
        var program = new Program();
        program.Feladat1();
        program.Feladat2();
        program.Feladat3();
        program.Feladat4();
        program.Feladat5();
    }

    readonly List<Signal> data = [];
    int eatCounter = 0;

    const string PathToInput = "C:\\Prog\\erettsegi-megoldasok-dotnet\\emelt-2024-tavasz\\bedat.txt";
    const string PathToOutput = "C:\\Prog\\erettsegi-megoldasok-dotnet\\emelt-2024-tavasz\\kesok.txt";

    /// <summary>
    /// Olvassa be a <c>bedat.txt</c> állomány tartalmát, tárolja el az abban szereplő adatokat, és annak felhasználásával oldja meg a következő feladatokat! Feltételezheti, hogy az állomány legfeljebb 2000 adatsort tartalmaz.
    /// </summary>
    void Feladat1()
    {
        Console.WriteLine("\n1. feladat");

        var lines = File.ReadAllLines(PathToInput);
        foreach (var cLine in lines)
        {
            var cLineSplit = cLine.Split(' ');

            data.Add(new Signal(
                cLineSplit[0],
                TimeOnly.Parse(cLineSplit[1]),
                (SignalType)int.Parse(cLineSplit[2])
            ));
        }

        Console.WriteLine("Sikeresen beolvasva.");
    }

    /// <summary>
    /// Határozza meg, hogy mikor lépett be az épületbe az első tanuló, és mikor távozott az utolsó! Az időpontokat a mintához hasonlóan jelenítse meg a képernyőn!
    /// </summary>
    void Feladat2()
    {
        Console.WriteLine("\n2. feladat");

        TimeOnly? firstEntryTime = null;
        TimeOnly? lastExitTime = null;

        // Assuming data is ordered by time, ascending
        foreach (var cSignal in data)
        {
            if (cSignal.Action == SignalType.Enter)
            {
                firstEntryTime = cSignal.Time;
                break;
            }
        }

        data.Reverse();
        // Assuming now, data is ordered by time, descending
        foreach (var cSignal in data)
        {
            if (cSignal.Action == SignalType.Exit)
            {
                lastExitTime = cSignal.Time;
                break;
            }
        }
        data.Reverse(); // Two reverses to not disturb data

        Console.WriteLine($"Az első tanuló {firstEntryTime}-kor lépett be a főkapun.");
        Console.WriteLine($"Az utolsó tanuló {lastExitTime}-kor lépett ki a főkapun.");
    }

    /// <summary>
    /// Készítsen listát a <c>kesok.txt</c> nevű állományba, amely megadja, hogy mely tanulók léptek be a nagykapun 07:50 után, de legkésőbb 08:15-kor! A fájlban a belépések a mintának megfelelően külön sorban szerepeljenek, az időpontot egy szóköz válassza el a tanuló azonosítójától! Ha egy tanuló ezalatt többször is belépett, minden belépése jelenjen meg a fájlban!
    /// </summary>
    void Feladat3()
    {
        Console.WriteLine("\n3. feladat");

        var sb = new StringBuilder();
        foreach (var cSignal in data)
        {
            if (cSignal.Action == SignalType.Enter &&
                cSignal.Time.CompareTo(new TimeOnly(7, 50)) >= 0 &&
                cSignal.Time.CompareTo(new TimeOnly(8, 15)) <= 0)
            {
                sb.Append($"{cSignal.Time} {cSignal.StudentId}\n");
            }
        }

        File.WriteAllText(PathToOutput, sb.ToString());
        Console.WriteLine("Sikeresen kiírva.");
    }

    /// <summary>
    /// Határozza meg, hány tanuló ebédelt aznap a menzán! Írassa ki az eredményt a képernyőre a mintának megfelelően!
    /// </summary>
    void Feladat4()
    {
        Console.WriteLine("\n4. feladat");

        foreach (var cSignal in data)
        {
            if (cSignal.Action == SignalType.Eat)
            {
                eatCounter++;
            }
        }

        Console.WriteLine($"A menzán aznap {eatCounter} tanuló ebédelt.");
    }

    /// <summary>
    /// Szeretnénk tudni, hogy a könyvtári kölcsönzés vagy a menza a népszerűbb-e ezen a napon. <list type="number"> <item> Határozza meg, hány tanuló kölcsönzött aznap a könyvtárban! Ha egy tanuló többször is kölcsönzött, akkor azt csak egyszer vegye figyelembe! Írassa ki az eredményt a képernyőre a mintának megfelelően! </item> <item> A könyvtárosok szerint több tanuló kölcsönöz egy nap a könyvtárban, mint ahányan a menzán ebédelnek. Így volt-e ez ezen a napon is? A választ („Többen voltak, mint a menzán.” vagy „Nem voltak többen, mint a menzán.”) a mintának megfelelő formában írassa ki a képernyőre! </item> </list>
    /// </summary>
    void Feladat5()
    {
        Console.WriteLine("\n5. feladat");

        HashSet<string> readers = [];
        foreach (var cSignal in data)
        {
            if (cSignal.Action == SignalType.BorrowBook)
            {
                readers.Add(cSignal.StudentId);
            }
        }

        Console.WriteLine($"Aznap {readers.Count} tanuló kölcsönzött a könyvtárban.");
        if (readers.Count > eatCounter)
        {
            Console.WriteLine("Többen voltak, mint a menzán.");
        }
        else
        {
            Console.WriteLine("Nem voltak többen, mint a menzán.");
        }
    }
}