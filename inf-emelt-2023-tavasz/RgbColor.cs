internal readonly struct RgbColor(byte Red, byte Green, byte Blue)
{
    public byte Red { get; } = Red;
    public byte Green { get; } = Green;
    public byte Blue { get; } = Blue;

    public int TotalValue => Red + Green + Blue;
    public bool Bright => TotalValue > 600;

    public override string ToString() => $"RgbColor {{ {Red}, {Green}, {Blue} }}";
    public string Display() => $"RGB({Red},{Green},{Blue})";
}