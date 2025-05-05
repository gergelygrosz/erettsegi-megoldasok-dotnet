internal class Program
{
    private static readonly string PathToInput = Environment.GetEnvironmentVariable("PROJECTS") + @"\dotnet\erettsegi-megoldasok-dotnet\inf-emelt-2023-tavasz\kep.txt";

    const int NumberOfRows = 360;
    const int NumberOfColumns = 640;

    /// <summary>
    /// !TODO: shift by one for accurate access and representation of row and column numbers
    /// </summary>
    private readonly RgbColor[,] image = new RgbColor[NumberOfRows, NumberOfColumns];

    private static void Main(string[] args)
    {
        Console.WriteLine("--- 2023. TAVASZ - INFOMATIKA EMELT - RGB SZÍNEK ---");

        Program program = new();

        program.Feladat1();
        program.Feladat2();
        program.Feladat3();
        program.Feladat4();
        program.Feladat6();
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
                    y++;
                    Array.Clear(currentColor);
                }
            }

            x++;
        }

        Console.WriteLine("Sikeresen beolvasva.");
    }

    /// <summary>
    /// Kérje be a felhasználótól a kép egy pontjának sor- és oszlopszámát (a számozás mindkét esetben 1-től indul), és írja a képernyőre az adott képpont RGB színösszetevőit a minta szerint!
    /// </summary>
    void Feladat2()
    {
        Console.WriteLine("\n2. feladat");

        Console.WriteLine("Adja meg egy képpont koordinátáit!");

        Console.Write("Sor: ");
        if (!int.TryParse(Console.ReadLine(), out int x))
        {
            Console.WriteLine("Hibás számot adott meg!");
            return;
        }

        Console.Write("Oszlop: ");
        if (!int.TryParse(Console.ReadLine(), out int y))
        {
            Console.WriteLine("Hibás számot adott meg!");
            return;
        }

        Console.WriteLine($"A képpont színe: {image[x, y].Display()}");
    }

    /// <summary>
    /// Világosnak tekintjük az olyan képpontot, amely RGB-értékeinek összege 600-nál nagyobb. Számolja meg és írja ki, hogy a teljes képen hány világos képpont van!
    /// </summary>
    void Feladat3()
    {
        Console.WriteLine("\n3. feladat");

        int count = 0;
        foreach (RgbColor color in image)
        {
            if (color.Bright) { count++; }
        }

        Console.WriteLine($"A világos képpontok száma: {count}");
    }

    /// <summary>
    /// A kép legsötétebb pontjainak azokat a pontokat tekintjük, amelyek RGB-értékeinek összege a legkisebb. Adja meg, hogy mennyi a legkisebb összeg, illetve keresse meg az ilyen RGB összegű pixeleket, és írja ki mindegyik színét RGB(r,g,b) formában a mintának megfelelően!
    /// </summary>
    void Feladat4()
    {
        Console.WriteLine("\n4. feladat");

        int min = image[0, 0].TotalValue;
        foreach (RgbColor color in image)
        {
            if (color.TotalValue < min) { min = color.TotalValue; }
        }

        List<RgbColor> darkColors = [];
        foreach (RgbColor color in image)
        {
            if (color.TotalValue == min) { darkColors.Add(color); }
        }

        Console.WriteLine($"A legsötétebb képpont RGB-értékeinek összege: {min}");
        Console.WriteLine($"A legsötétebb képpontok színei: ");
        Console.WriteLine(string.Join('\n', darkColors.Select(c => c.Display())));
    }

    /// <summary>
    /// A képen a kék ég látható közepén egy felhővel. Az ég és a felhő színe között jelentős különbség van, így az ég-felhő határvonal programmal is felismerhető. Ennek megtalálásához készítsen függvényt <c>hatar</c> néven, amely megadja, hogy egy adott sorban van-e olyan hely a képen, ahol az egymás melletti képpontok kék színösszetevőinek eltérése meghalad egy adott értéket! A függvény kapja meg paraméterként a sor számát, illetve az eltérés értékét, melyek egészek! A függvény visszatérési értéke egy logikai érték legyen, amely megadja, hogy az adott sorban volt-e az eltérést meghaladó különbség az egymás melletti képpontok kék színében!
    /// </summary>
    bool Hatar(int row, int difference)
    {
        int lastCheckedBlueValue = image[row, 0].Blue;
        for (int column = 0; column < NumberOfColumns; column++)
        {
            if (int.Abs(image[row, column].Blue - lastCheckedBlueValue) > difference)
            {
                return true;
            }

            lastCheckedBlueValue = image[row, column].Blue;
        }

        return false;
    }

    /// <summary>
    /// Keresse meg a képen a felhő első és utolsó sorát az előzőleg elkészített függvény segítségével úgy, hogy eltérésként 10-et ad meg a függvénynek bemenetként! Adja meg az első és az utolsó olyan sor sorszámát, ahol az eltérés a soron belül valahol 10-nél nagyobb!
    /// </summary>
    void Feladat6()
    {
        Console.WriteLine("\n6. feladat");

        int firstRow = -1;
        for (int row = 0; row < NumberOfRows - 1; row++)
        {
            if (Hatar(row, 10))
            {
                firstRow = row;
                break;
            }
        }

        int lastRow = -1;
        for (int row = NumberOfRows - 1; row > 0; row--)
        {
            if (Hatar(row, 10))
            {
                lastRow = row;
                break;
            }
        }

        Console.WriteLine($"A felhő legfelső sora: {firstRow}");
        Console.WriteLine($"A felhő legalsó sora: {lastRow}");
    }
}