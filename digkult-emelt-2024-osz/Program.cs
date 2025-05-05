using System.Text;

internal class Program
{
    private static readonly string PathToInput = Environment.GetEnvironmentVariable("PROJECTS") + @"\dotnet\erettsegi-megoldasok-dotnet\digkult-emelt-2024-osz\jeladas.txt";
    private static readonly string PathToOutput = Environment.GetEnvironmentVariable("PROJECTS") + @"\dotnet\erettsegi-megoldasok-dotnet\digkult-emelt-2024-osz\ido.txt";

    private readonly List<Signal> data = [];

    private static void Main(string[] args)
    {

        Console.WriteLine("--- 2024. ŐSZ EMELT ÉRETTSÉGI - DIGITÁLIS KULTÚRA - AUTÓK MOZGÁSA ---");

        Program program = new();
        program.Feladat1();
        program.Feladat2();
        program.Feladat3();
        program.Feladat4();
        program.Feladat5();
        program.Feladat6();
        program.Feladat7();
    }

    /// <summary>
    /// Olvassa be és tárolja el a további feldolgozáshoz a <c>jeladas.txt</c> állomány tartalmát!
    /// </summary>
    void Feladat1()
    {
        Console.WriteLine("\n1. feladat");

        string[] lines = File.ReadAllLines(PathToInput);

        foreach (string currentLine in lines)
        {
            string[] lineSplit = currentLine.Split("\t");
            data.Add(new Signal(
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

        Signal lastSignal = data.Last();

        Console.WriteLine($"Az utolsó jeladás időpontja {lastSignal.Time}, a jármű rendszáma {lastSignal.Car}");
    }

    /// <summary>
    /// Írja ki a bemeneti állományban elsőként szereplő jármű rendszámát, valamint azt, hogy milyen időpontokban adott jelzést! Az időpontokat <c>óra:perc</c> formátumban, szóközzel elválasztva, egy sorban jelenítse meg!
    /// </summary>
    void Feladat3()
    {
        Console.WriteLine("\n3. feladat");

        string firstCar = data.First().Car;
        List<TimeOnly> signalTimes = [];
        foreach (Signal signal in data)
        {
            if (signal.Car == firstCar)
            {
                signalTimes.Add(signal.Time);
            }
        }

        Console.WriteLine($"Az első jármű: {firstCar}");
        Console.WriteLine($"Jeladásainak időpontjai: {string.Join(" ", signalTimes)}");
    }

    /// <summary>
    /// Kérje be a felhasználótól egy időpont óra és perc értékét, és adja meg, hogy hány jeladás történt az adott időpontban! Ha nem történt jeladás, akkor 0-t írjon ki!
    /// </summary>
    void Feladat4()
    {
        Console.WriteLine("\n4. feladat");

        Console.Write("Kérem, adjon meg egy időpontot: (óó:pp) ");
        string? input = Console.ReadLine();
        if (!TimeOnly.TryParse(input, out TimeOnly inputTime))
        {
            Console.WriteLine("Helytelen időpontot adott meg");
            return;
        }

        int counter = 0;
        foreach (Signal signal in data)
        {
            if (signal.Time.Equals(inputTime))
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

        int maxSpeed = data[0].Speed;
        foreach (Signal signal in data)
        {
            if (signal.Speed > maxSpeed)
            {
                maxSpeed = signal.Speed;
            }
        }

        HashSet<string> speedingCars = [];
        foreach (Signal signal in data)
        {
            if (signal.Speed == maxSpeed)
            {
                speedingCars.Add(signal.Car);
            }
        }

        Console.WriteLine($"A legnagyobb sebesség: {maxSpeed} km/h.");
        Console.WriteLine($"A járművek: {string.Join(", ", speedingCars)}.");
    }

    /// <summary>
    /// Kérje be a felhasználótól egy jármű rendszámát, és jelenítse meg a jármű jeladásainak időpontját és az adott rendszámú autó távolságát az útszakasz kezdetétől! A bevezető példában az első jármű esetén a 6:04-kor a jármű távolsága az útszakasz kezdetétől 0,0 km, míg 6:14-kor 15,8 km, mivel a jármű az eltelt 10 perc (10/60 óra) alatt 95 km/h-val haladt. A kimenetet a mintának megfelelőn alakítsa ki, a távolságot minden esetben egy tizedesjegyre kerekítve írja ki km mértékegységben! Ha nem szerepel a bekért rendszámmal jármű, akkor azt egy rövid mondatban jelezze a felhasználónak!
    /// </summary>
    void Feladat6()
    {
        Console.WriteLine("\n6. feladat");

        Console.Write("Adja meg egy jármű rendszámát: ");
        string rawInput = Console.ReadLine().Trim().ToUpper();
        try
        {
            string inputCar = rawInput.Trim().ToUpper();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.StackTrace);
        }

        double totalDistanceTravelled = 0;
        Signal? lastSignal = null;

        foreach (Signal signal in data)
        {
            if (signal.Car != rawInput)
            {
                continue;
            }

            // if lastSignal is null: lastSignal = signal
            lastSignal ??= signal;

            double elapsedTime = (signal.Time - lastSignal.Value.Time).TotalHours;
            double lastDistanceTravelled = elapsedTime * lastSignal.Value.Speed;
            totalDistanceTravelled += lastDistanceTravelled;

            Console.WriteLine($"{signal.Time} {totalDistanceTravelled:0.0} km");

            lastSignal = signal;
        }

        if (lastSignal is null)
        {
            Console.WriteLine("A megadott rendszámmal nem közlekedett a vizsgált napon jármű.");
        }
    }

    /// <summary>
    /// Készítsen egy <c>ido.txt</c> szöveges állományt, amelynek mindegyik sorában egy-egy jármű rendszáma, illetve első és utolsó jeladásának óra és perc értéke szerepeljen! Az állományban minden jármű pontosan egyszer forduljon elő tetszőleges sorrendben!
    /// </summary>
    void Feladat7()
    {
        Console.WriteLine("\n7. feladat");

        HashSet<string> cars = [];
        foreach (Signal signal in data)
        {
            cars.Add(signal.Car);
        }

        List<string> carsAsList = [.. cars];
        carsAsList.Sort();

        List<Tuple<string, TimeOnly, TimeOnly>> x = [];

        foreach (string car in carsAsList)
        {

            TimeOnly first = data[0].Time;
            TimeOnly last = data[0].Time;

            foreach (Signal signal in data)
            {
                if (!string.Equals(signal.Car, car))
                {
                    continue;
                }

                if (signal.Time < first)
                {
                    first = signal.Time;
                }

                if (signal.Time > last)
                {
                    last = signal.Time;
                }
            }

            x.Add(Tuple.Create(car, first, last));
        }

        StringBuilder sb = new StringBuilder();
        foreach (Tuple<string, TimeOnly, TimeOnly> tuple in x)
        {
            sb.Append(tuple.Item1);
            sb.Append(' ');
            sb.Append(tuple.Item2.Hour);
            sb.Append(' ');
            sb.Append(tuple.Item2.Minute);
            sb.Append(' ');
            sb.Append(tuple.Item3.Hour);
            sb.Append(' ');
            sb.Append(tuple.Item3.Minute);
            sb.Append('\n');
        }

        File.WriteAllText(PathToOutput, sb.ToString());
        Console.WriteLine("Sikeresen kiírva.");
    }
}
