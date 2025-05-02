class Signal(string studentId, TimeOnly time, SignalType signalType)
{
    public string StudentId { get; } = studentId;
    public TimeOnly Time { get; } = time;
    public SignalType SignalType { get; } = signalType;

    public override string ToString()
    {
        return $"Signal {{ {StudentId}, {Time}, {SignalType} }}";
    }

    public string Export()
    {
        return $"{StudentId}\t{Time}\t{SignalType}";
    }

}

public enum SignalType
{
    Enter = 1,
    Exit = 2,
    Eat = 3,
    BorrowBook = 4
}