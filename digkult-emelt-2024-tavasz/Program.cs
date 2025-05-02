using System.Text;

internal class Program
{
    private const string PathToInput = @"C:\Prog\erettsegi-megoldasok-dotnet\digkult-emelt-2024-tavasz\bedat.txt";
    private const string PathToOutput = @"C:\Prog\erettsegi-megoldasok-dotnet\digkult-emelt-2024-tavasz\kesok.txt";

    private readonly List<Signal> data = [];
    private int eatCounter = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("--- 2024. TAVASZ EMELT ÉRETTSÉGI - DIGITÁLIS KULTÚRA - BELÉPTETŐ RENDSZER ---");

        Program program = new Program();
        program.Feladat1();
        program.Feladat2();
        program.Feladat3();
        program.Feladat4();
        program.Feladat5();
        program.Feladat6();
        program.Feladat7();
    }

    /// <summary>
    /// Olvassa be a <c>bedat.txt</c> állomány tartalmát, tárolja el az abban szereplő adatokat, és annak felhasználásával oldja meg a következő feladatokat! Feltételezheti, hogy az állomány legfeljebb 2000 adatsort tartalmaz.
    /// </summary>
    void Feladat1()
    {
        Console.WriteLine("\n1. feladat");

        string[] lines = File.ReadAllLines(PathToInput);
        foreach (string line in lines)
        {
            string[] lineSplit = line.Split(' ');

            data.Add(new Signal(
                lineSplit[0],
                TimeOnly.Parse(lineSplit[1]),
                (SignalType)int.Parse(lineSplit[2])
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

        TimeOnly firstEntryTime = TimeOnly.MaxValue;
        TimeOnly lastExitTime = TimeOnly.MinValue;
        foreach (Signal signal in data)
        {
            if (signal.SignalType == SignalType.Enter &&
                signal.Time < firstEntryTime)
            {
                firstEntryTime = signal.Time;
            }

            if (signal.SignalType == SignalType.Exit &&
                signal.Time > lastExitTime)
            {
                lastExitTime = signal.Time;
            }
        }

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
        foreach (var signal in data)
        {
            if (signal.SignalType == SignalType.Enter &&
                signal.Time > new TimeOnly(7, 50) &&
                signal.Time <= new TimeOnly(8, 15))
            {
                sb.Append($"{signal.Time} {signal.StudentId}\n");
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

        foreach (var signal in data)
        {
            if (signal.SignalType == SignalType.Eat)
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
        foreach (var signal in data)
        {
            if (signal.SignalType == SignalType.BorrowBook)
            {
                readers.Add(signal.StudentId);
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

    /// <summary>
    /// A portás reggel elfelejtette a hátsó kaput bezárni, ezért a 10:45-kor kezdődő szünetben néhány tanuló kiment a hátsó kijáraton át a szemközti pékségbe tízórait venni. A portás csak 10:50-kor zárta be a hátsó kaput, így 10:50 után a korábban a hátsó kapun át távozott tanulóknak a főbejáraton át kellett visszajönniük. Írassa ki a képernyőre egy-egy szóközzel elválasztva ezeknek a tanulóknak az azonosítóját! (A szünet 11:00-ig tartott, és feltételezheti, hogy azt megelőzően valamennyi érintett tanuló visszaért.) Vegye figyelembe, hogy a tanulók egy része aznap csak 11:00-ra jött iskolába, illetve szabályosan lépett ki!
    /// </summary>
    void Feladat6()
    {
        Console.WriteLine("\n6. feladat");
        // Azokat a tanulókat keressük, akik: 
        // 1. 10:50 és 11:00 között beléptek
        // 2. ÉS 10:45 előtt már egyszer beléptek (tehát nem 10:50 és 11:00 között van az első belépésük)
        // 3. DE NEM léptek ki HIVATALOSAN 10:45 és 11:00 között

        // a condition3-ba azokat rakjuk, akik hivatalosan kiléptek 10:45 és 11:00, majd kivonjuk a condition1 és condition2 metszetéből
        HashSet<string> condition1 = [];
        HashSet<string> condition2 = [];
        HashSet<string> condition3 = [];

        foreach (var signal in data)
        {
            // fill up condition1
            if (signal.SignalType == SignalType.Enter &&
                signal.Time >= new TimeOnly(10, 50) &&
                signal.Time < new TimeOnly(11, 0))
            {
                condition1.Add(signal.StudentId);
            }

            // fill up condition2
            if (signal.SignalType == SignalType.Enter &&
                signal.Time < new TimeOnly(10, 45))
            {
                condition2.Add(signal.StudentId);
            }

            // fill up condition3
            if (signal.SignalType == SignalType.Exit &&
                signal.Time >= new TimeOnly(10, 45) &&
                signal.Time < new TimeOnly(11, 0))
            {
                condition3.Add(signal.StudentId);
            }
        }

        List<string> result = [.. condition1.Intersect(condition2).Except(condition3)];
        result.Sort();

        Console.WriteLine($"Az érintett tanulók: {string.Join(' ', result)}");
    }

    /// <summary>
    /// Kérje be egy tanuló azonosítóját, és írassa ki a minta szerinti formátumban, hogy mennyi idő telt el az iskolába való első belépése és utolsó távozása között! Feltételezheti, hogy 19:00-ig minden tanuló elhagyta az iskolát. Ha aznap az adott azonosítójú tanuló nem járt az iskolában, akkor írassa ki az <c>Ilyen azonosítójú tanuló aznap nem volt az iskolában.</c> üzenetet!
    /// </summary>
    void Feladat7()
    {
        Console.WriteLine("\n7. feladat");

        Console.Write("Adja meg egy tanuló azonosítóját: ");
        var input = Console.ReadLine().Trim().ToUpper();

        // Reverse-order search
        TimeOnly firstEntryTime = TimeOnly.MaxValue;
        TimeOnly lastExitTime = TimeOnly.MinValue;
        foreach (var signal in data)
        {
            if (Equals(signal.StudentId, input) &&
                signal.SignalType == SignalType.Enter &&
                signal.Time < firstEntryTime)
            {
                firstEntryTime = signal.Time;
            }

            if (Equals(signal.StudentId, input) &&
                signal.SignalType == SignalType.Exit &&
                signal.Time > lastExitTime)
            {
                lastExitTime = signal.Time;
            }
        }

        if (Equals(firstEntryTime, TimeOnly.MaxValue) ||
            Equals(lastExitTime, TimeOnly.MinValue))
        {
            Console.WriteLine("Ilyen azonosítójú tanuló aznap nem volt az iskolában.");
            return;
        }

        var elapsedTime = lastExitTime - firstEntryTime;

        Console.WriteLine($"A tanuló érekezése és távozása között {elapsedTime.Hours} óra és {elapsedTime.Minutes} perc telt el.");
    }
}