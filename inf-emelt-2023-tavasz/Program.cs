internal class Program
{
    private static readonly string PathToInput = Environment.GetEnvironmentVariable("PROJECTS") + @"\dotnet\erettsegi-megoldasok-dotnet\inf-emelt-2023-tavasz\kep.txt";

    private readonly RgbColor[,] image = new RgbColor[360, 640];

    private static void Main(string[] args)
    {
        Console.WriteLine("--- 2023. TAVASZ - INFOMATIKA EMELT - RGB SZÍNEK ---");

        Program program = new();

        program.Feladat1();
        program.Feladat2();
    }

    /// <summary>
    /// Olvassa be a kep.txt állomány tartalmát, és tárolja el a 640×360 képpont színét!
    /// </summary>
    void Feladat1()
    {
        Console.WriteLine("\n1. feladat");

        string[] lines = File.ReadAllLines(PathToInput);

        byte[] currentColor = new byte[3];
        int x = 0;
        int y = 0;
        int colorIdx = 0;
        foreach (string line in lines)
        {
            y = 0;

            foreach (string value in line.Split(' '))
            {
                currentColor[colorIdx] = byte.Parse(value);

                colorIdx += 1;
                if (colorIdx == 3)
                {
                    image[x, y] = new RgbColor(Red: currentColor[0], Green: currentColor[1], Blue: currentColor[2]);

                    colorIdx = 0;
                    y += 1;
                    Array.Clear(currentColor);
                }
            }

            x += 1;
        }

        Console.WriteLine("Sikeresen beolvasva.");
    }

    /// <summary>
    /// Kérje be a felhasználótól a kép egy pontjának sor- és oszlopszámát (a számozás mindkét esetben 1-től indul), és írja a képernyőre az adott képpont RGB színösszetevőit a minta szerint!
    /// </summary>
    void Feladat2()
    {
        Console.WriteLine("\n2. feladat");

        Console.WriteLine("Adja meg egy pont koordinátáit!");

        Console.Write("Sor: ");
        string? rowString = Console.ReadLine();
        int x = int.Parse(rowString);

        Console.Write("Oszlop: ");
        string? columnString = Console.ReadLine();
        int y = int.Parse(columnString);

        Console.WriteLine($"A képpont színe: {image[x, y].Display()}");
    }
}