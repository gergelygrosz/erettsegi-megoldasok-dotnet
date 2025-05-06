internal class Program
{
    private static readonly string PathToInput = Environment.GetEnvironmentVariable("PROJECTS") + @"\dotnet\erettsegi-megoldasok-dotnet\inf-emelt-2022-osz\jel.txt";

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

    void Feladat1() { }
    void Feladat2() { }
    void Eltelt() { }
    void Feladat4() { }
    void Feladat5() { }
    void Feladat6() { }
    void Feladat7() { }
}