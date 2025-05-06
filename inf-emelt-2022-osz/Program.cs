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

    void Feladat2() { }
    void Eltelt() { }
    void Feladat4() { }
    void Feladat5() { }
    void Feladat6() { }
    void Feladat7() { }
}