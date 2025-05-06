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
        Console.WriteLine($"x={queriedSignal.X}, y={queriedSignal.Y}");
    }

    void Eltelt() { }
    void Feladat4() { }
    void Feladat5() { }
    void Feladat6() { }
    void Feladat7() { }
}