internal class Program
{
    private static readonly string PathToInput = Environment.GetEnvironmentVariable("PROJECTS") + @"\dotnet\erettsegi-megoldasok-dotnet\inf-emelt-2023-tavasz\kep.txt";

    private readonly RgbColor[,] image = new RgbColor[360, 640];

    private static void Main(string[] args)
    {
        Console.WriteLine("--- 2023. TAVASZ - INFOMATIKA EMELT - RGB SZÍNEK ---");

        Program program = new();

        program.Feladat1();
    }

    /// Olvassa be a kep.txt állomány tartalmát, és tárolja el a 640×360 képpont színét!
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
}