public readonly record struct Signal(string Plate, TimeOnly Time, int Speed)
{

    public override string ToString()
    {
        return $"Signal {{ {Plate}, {Time}, {Speed} }}";
    }
}