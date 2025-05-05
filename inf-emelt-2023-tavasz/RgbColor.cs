internal readonly struct RgbColor(byte Red, byte Green, byte Blue)
{
    public byte Red { get; } = Red;
    public byte Green { get; } = Green;
    public byte Blue { get; } = Blue;

    public override string ToString() => $"RgbColor {{ {Red}, {Green}, {Blue} }}";

    public string Display() => $"RGB({Red},{Green},{Blue})";
    public bool Bright => (Red + Green + Blue) > 600;
}