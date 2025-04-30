using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("--- 2024. ŐSZ EMELT ÉRETTSÉGI - DIGITÁLIS KULTÚRA - AUTÓK MOZGÁSA ---");
        var programInstance = new Program();
        programInstance.Feladat1();
        programInstance.Feladat2();
        programInstance.Feladat3();
        programInstance.Feladat4();
        programInstance.Feladat5();
        programInstance.Feladat6();
        programInstance.Feladat7();
    }

    readonly List<Signal> fileData = [];

    const string PathToInput = "C:\\Prog\\erettsegi-megoldasok-dotnet\\emelt-2024-osz\\jeladas.txt";
    const string PathToOutput = "C:\\Prog\\erettsegi-megoldasok-dotnet\\emelt-2024-osz\\ido.txt";

    /// <summary>
    /// Olvassa be és tárolja el a további feldolgozáshoz a <c>jeladas.txt</c> állomány tartalmát!
    /// </summary>
    void Feladat1()
    {
        Console.WriteLine("\n1. feladat");

        var lines = File.ReadAllLines(PathToInput).ToList();

        foreach (var currentLine in lines)
        {
            var lineSplit = currentLine.Split("\t");
            fileData.Add(new Signal(
                lineSplit[0],
                new TimeOnly(int.Parse(lineSplit[1]), int.Parse(lineSplit[2])),
                int.Parse(lineSplit[3])
            ));
        }

        Console.WriteLine("Sikeresen beolvasva.");
    }

    /// <summary>
    /// Állapítsa meg, hogy milyen időpontban történt a legutolsó jeladás, és írja a képernyőre az időpontot, valamint az utoljára jelet adó autó rendszámát!
    /// </summary>
    void Feladat2()
    {
        Console.WriteLine("\n2. feladat");

        var lastSignal = fileData.Last();

        Console.WriteLine($"Az utolsó jeladás időpontja {lastSignal.Time}, a jármű rendszáma {lastSignal.Plate}");
    }

    /// <summary>
    /// Írja ki a bemeneti állományban elsőként szereplő jármű rendszámát, valamint azt, hogy milyen időpontokban adott jelzést! Az időpontokat <c>óra:perc</c> formátumban, szóközzel elválasztva, egy sorban jelenítse meg!
    /// </summary>
    void Feladat3()
    {
        Console.WriteLine("\n3. feladat");

        var firstPlate = fileData.First().Plate;
        List<TimeOnly> times = [];

        foreach (var currentSignal in fileData)
        {
            if (currentSignal.Plate == firstPlate)
            {
                times.Add(currentSignal.Time);
            }
        }

        Console.WriteLine($"Az első jármű: {firstPlate}");
        Console.WriteLine($"Jeladásainak időpontjai: {string.Join(" ", times)}");
    }

    /// <summary>
    /// Kérje be a felhasználótól egy időpont óra és perc értékét, és adja meg, hogy hány jeladás történt az adott időpontban! Ha nem történt jeladás, akkor 0-t írjon ki!
    /// </summary>
    void Feladat4()
    {
        Console.WriteLine("\n4. feladat");

        Console.Write("Kérem, adjon meg egy időpontot: (óó:pp) ");
        var input = Console.ReadLine();
        if (!TimeOnly.TryParse(input, out TimeOnly inputTime))
        {
            Console.WriteLine("Helytelen időpontot adott meg");
            return;
        }

        var counter = 0;
        foreach (var currentSignal in fileData)
        {
            if (currentSignal.Time.Equals(inputTime))
            {
                counter++;
            }
        }

        Console.WriteLine($"A megadott időpontban {counter} jeladás történt");
    }

    /// <summary>
    /// Állapítsa meg, hogy mennyi az adatok szerint a legnagyobb sebesség, amellyel egy jármű a jeladáskor haladt, illetve adja meg az összes autó rendszámát, ami haladt ilyen sebességgel! Amennyiben egy jármű többször is haladt a legnagyobb sebességgel, akkor a rendszámát többször is megjelenítheti. A rendszámokat egy sorban, szóközzel elválasztva jelenítse meg a minta szerint!
    /// </summary>
    void Feladat5()
    {
        Console.WriteLine("\n5. feladat");

        int maxSpeed = 0;
        foreach (var currentSignal in fileData)
        {
            if (currentSignal.Speed > maxSpeed)
            {
                maxSpeed = currentSignal.Speed;
            }
        }

        HashSet<string> fastPlates = [];
        foreach (var currentSignal in fileData)
        {
            if (currentSignal.Speed == maxSpeed)
            {
                fastPlates.Add(currentSignal.Plate);
            }
        }

        Console.WriteLine($"A legnagyobb sebesség: {maxSpeed} km/h");
        Console.WriteLine($"A járművek: {string.Join(' ', fastPlates)}");
    }

    /// <summary>
    /// Kérje be a felhasználótól egy jármű rendszámát, és jelenítse meg a jármű jeladásainak időpontját és az adott rendszámú autó távolságát az útszakasz kezdetétől! A bevezető példában az első jármű esetén a 6:04-kor a jármű távolsága az útszakasz kezdetétől 0,0 km, míg 6:14-kor 15,8 km, mivel a jármű az eltelt 10 perc (10/60 óra) alatt 95 km/h-val haladt. A kimenetet a mintának megfelelőn alakítsa ki, a távolságot minden esetben egy tizedesjegyre kerekítve írja ki km mértékegységben! Ha nem szerepel a bekért rendszámmal jármű, akkor azt egy rövid mondatban jelezze a felhasználónak!
    /// </summary>
    void Feladat6()
    {
        Console.WriteLine("\n6. feladat");

        Console.Write("Adja meg egy jármű rendszámát: ");
        var inputPlate = Console.ReadLine().Trim().ToUpper();

        double totalDistanceTravelled = 0;
        Signal? lastSignal = null;

        foreach (var currentSignal in fileData)
        {
            if (currentSignal.Plate != inputPlate)
            {
                continue;
            }

            if (lastSignal is null)
            {
                lastSignal = currentSignal;
            }

            var elapsedTime = (currentSignal.Time - lastSignal.Value.Time).TotalHours;
            var lastDistanceTravelled = elapsedTime * lastSignal.Value.Speed;
            totalDistanceTravelled += lastDistanceTravelled;

            Console.WriteLine($"{currentSignal.Time} {totalDistanceTravelled:0.0} km");

            lastSignal = currentSignal;
        }

        // Akkor lesz null még mindig a lastSignal, ha egyszer sem találtuk meg az autót, tehát nem közlekedett
        if (lastSignal is null)
        {
            Console.WriteLine("A megadott rendszámmal nem közlekedett a vizsgált napon jármű");
        }
    }

    /// <summary>
    /// Készítsen egy <c>ido.txt</c> szöveges állományt, amelynek mindegyik sorában egy-egy jármű rendszáma, illetve első és utolsó jeladásának óra és perc értéke szerepeljen! Az állományban minden jármű pontosan egyszer forduljon elő tetszőleges sorrendben!
    /// </summary>
    void Feladat7()
    {
        Console.WriteLine("\n7. feladat");

        HashSet<string> plates = [];
        foreach (var currentSignal in fileData)
        {
            plates.Add(currentSignal.Plate);
        }

        var platesList = plates.ToList();
        platesList.Sort();

        List<Tuple<string, TimeOnly, TimeOnly>> x = [];

        foreach (var currentPlate in platesList)
        {
            // The values are reversed because the search searches backwards
            TimeOnly min = TimeOnly.MaxValue;
            TimeOnly max = TimeOnly.MinValue;

            foreach (var currentSignal in fileData)
            {
                if (!string.Equals(currentSignal.Plate, currentPlate))
                {
                    continue;
                }

                // If current time is before current min
                if (currentSignal.Time.CompareTo(min) < 0)
                {
                    min = currentSignal.Time;
                }

                // If current time is after current max
                if (currentSignal.Time.CompareTo(max) > 0)
                {
                    max = currentSignal.Time;
                }
            }

            x.Add(Tuple.Create(currentPlate, min, max));
        }

        var sb = new StringBuilder();
        foreach (var currentTuple in x)
        {
            sb.Append(currentTuple.Item1);
            sb.Append(' ');
            sb.Append(currentTuple.Item2.Hour);
            sb.Append(' ');
            sb.Append(currentTuple.Item2.Minute);
            sb.Append(' ');
            sb.Append(currentTuple.Item3.Hour);
            sb.Append(' ');
            sb.Append(currentTuple.Item3.Minute);
            sb.Append('\n');
        }

        File.WriteAllText(PathToOutput, sb.ToString());

        Console.WriteLine("Sikeresen kiírva");
    }
}
