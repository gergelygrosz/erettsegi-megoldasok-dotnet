public readonly record struct Signal(string Car, TimeOnly Time, int Speed)
{

    public override string ToString()
    {
        return $"Signal {{ {Car}, {Time}, {Speed} }}";
    }
}