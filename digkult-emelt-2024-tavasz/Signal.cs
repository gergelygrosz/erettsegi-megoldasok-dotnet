class Signal(string studentId, TimeOnly time, SignalType type)
{
    public string StudentId { get; } = studentId;
    public TimeOnly Time { get; } = time;
    public SignalType Type { get; } = type;

    public override string ToString()
    {
        return $"Signal {{ {StudentId}, {Time}, {Type} }}";
    }

    public string Export()
    {
        return $"{StudentId}\t{Time}\t{Type}";
    }
}

enum SignalType
{
    Enter = 1,
    Exit = 2,
    Eat = 3,
    BorrowBook = 4
}