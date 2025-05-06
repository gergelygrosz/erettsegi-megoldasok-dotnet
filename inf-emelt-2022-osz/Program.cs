using System.Drawing;

internal class Program
{
    private static readonly string PathToInput = Environment.GetEnvironmentVariable("PROJECTS") + @"\dotnet\erettsegi-megoldasok-dotnet\inf-emelt-2022-osz\jel.txt";

    private readonly List<Signal> signals = [];

    private static void Main(string[] args)
    {
        Console.WriteLine("--- 2022. ŐSZ - INFOMATIKA EMELT - ÁLLATOK MOZGÁSA ---");

        Program program = new();
        program.Feladat1();
        program.Feladat2();
        program.Feladat4();
        program.Feladat5();
        program.Feladat6();
        program.Feladat7();
    }

    /// <summary>
    /// Olvassa be a <c>jel.txt</c> állomány tartalmát, tárolja el a rögzített jelek adatait, és azok felhasználásával oldja meg a következő feladatokat!
    /// </summary>
    void Feladat1()
    {
        Console.WriteLine("\n1. feladat");
        string[] lines = File.ReadAllLines(PathToInput);
        foreach (string line in lines)
        {
            string[] lineSplit = line.Split(' ');

            signals.Add(new Signal(
                hour: int.Parse(lineSplit[0]),
                minute: int.Parse(lineSplit[1]),
                second: int.Parse(lineSplit[2]),
                x: int.Parse(lineSplit[3]),
                y: int.Parse(lineSplit[4])
            ));
        }

        Console.WriteLine("Sikeresen beolvasva.");
    }

    /// <summary>
    /// Kérje be a felhasználótól egy jel sorszámát (a sorszámozás 1-től indul), és írja a képernyőre az adott jeladáshoz tartozó <c>x</c> és <c>y</c> koordinátát!
    /// </summary>
    void Feladat2()
    {
        Console.WriteLine("\n2. feladat");

        Console.Write("Adja meg egy jel sorszámát: ");
        string rawInput = Console.ReadLine();
        bool result = int.TryParse(rawInput, out int row);
        if (!result || row <= 0)
        {
            Console.WriteLine("Hibás számot adott meg!");
            return;
        }

        Signal queriedSignal = signals[row - 1];
        Console.WriteLine($"x={queriedSignal.Position.X}, y={queriedSignal.Position.Y}");
    }

    /// <summary>
    /// Készítsen függvényt <c>eltelt</c> néven, amely megadja, hogy a paraméterként átadott két időpont között hány másodperc telik el! A két időpontot, mint paramétert tetszőleges módon átadhatja. Használhat három-három számértéket, két tömböt vagy listát, de más, a célnak megfelelő típusú változót is. Ezt a függvényt később használja fel legalább egy feladat megoldása során!
    /// </summary>
    int Eltelt(TimeOnly time1, TimeOnly time2)
    {
        return (time2 - time1).Seconds;
    }

    /// <summary>
    /// Adja meg, mennyi idő telt el az első és az utolsó észlelés között! Az időt <c>óra:perc:mperc</c> alakban írja a képernyőre!
    /// </summary>
    void Feladat4()
    {
        Console.WriteLine("\n4. feladat");

        Signal first = signals.First();
        Signal last = signals.Last();
        TimeSpan elapsedTime = last.Time - first.Time;

        Console.WriteLine($"Az első és utolsó jelzés között eltelt időtartam: {elapsedTime}");
    }

    /// <summary>
    /// Határozza meg azt a legkisebb, a koordináta-rendszer tengelyeivel párhuzamos oldalú téglalapot, amelyből nem lépett ki a jeladó! Adja meg a téglalap bal alsó és jobb felső sarkának koordinátáit!
    /// </summary>
    void Feladat5()
    {
        Console.WriteLine("\n5. feladat");

        // közelítő keresés
        Point min = new(10000, 10000); // bal alsó lesz
        Point max = new(-10000, -10000); // jobb felső lesz

        foreach (Signal signal in signals)
        {
            if (signal.Position.X < min.X) { min.X = signal.Position.X; }
            if (signal.Position.Y < min.Y) { min.Y = signal.Position.Y; }
            if (signal.Position.X > max.X) { max.X = signal.Position.X; }
            if (signal.Position.Y > max.Y) { max.Y = signal.Position.Y; }
        }

        Console.WriteLine($"Bal alsó: x={min.X}, y={min.Y}");
        Console.WriteLine($"Jobb felső: x={max.X}, y={max.Y}");
    }

    /// <summary>
    /// Írja a képernyőre, hogy mennyi volt a jeladó elmozdulásainak összege! Úgy tekintjük, hogy a jeladó két pozíciója közötti elmozdulása a pozíciókat összekötő egyenes mentén történt. Az összeget három tizedes pontossággal jelenítse meg! A kiírásnál a tizedesvessző és tizedespont kiírása is elfogadott. Az <c>i</c>-edik és az <c>i+1</c>-edik pontok távolságát a <c>sqrt((x_(i) - x_(i+1))^2 + (y_(i) - y_(i+1))^2)</c> képlet segítségével határozhatja meg.
    /// </summary>
    void Feladat6()
    {
        Console.WriteLine("\n6. feladat");

        double distance = 0d;

        for (int i = 0; i < signals.Count - 1; i++)
        {
            distance += Math.Sqrt(
                Math.Pow(signals[i].Position.X - signals[i + 1].Position.X, 2) +
                Math.Pow(signals[i].Position.Y - signals[i + 1].Position.Y, 2)
            );
        }

        Console.WriteLine($"Elmozdulások összege: {distance:0.000}");
    }

    void Feladat7() { }
}