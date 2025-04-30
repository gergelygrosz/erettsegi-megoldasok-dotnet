class Signal
{
    public string StudentId { get; }
    public TimeOnly Time { get; }
    public SignalType Action { get; }

    public Signal(string studentId, TimeOnly time, SignalType action)
    {
        this.StudentId = studentId;
        this.Time = time;
        this.Action = action;
    }

    public override string ToString()
    {
        return $"Signal {{ {StudentId}, {Time}, {Action} }}";
    }

    public string Export()
    {
        return $"{StudentId}\t{Time}\t{Action}";
    }
}

enum SignalType
{
    Enter = 1,
    Exit = 2,
    Eat = 3,
    BorrowBook = 4
}